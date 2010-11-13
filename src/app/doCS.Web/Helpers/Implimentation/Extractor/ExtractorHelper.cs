using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using doCS.Models;
using doCS.Extractor;

namespace doCS.Web.Helpers.Implimentation.Extractor {
	public class ExtractorHelper : IExtractorHelper {
		private readonly IExtractor Extractor;
		private readonly ProjectUpdaterProvider ProjectUpdaterProvider;

		public ExtractorHelper(ProjectUpdaterProvider projectUpdaterProvider, IExtractor extractor) {
			ProjectUpdaterProvider = projectUpdaterProvider;
			Extractor = extractor;
		}

		public void Extract(ProjectSettings projectSettings) {

			ProjectData projectData = Extractor.Extract((IExtractorContext context) => {

				string[] assemblyFileNames = projectSettings.IncludedAssemblies.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				string[] xmlFileNames = projectSettings.IncludedXmlFiles.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string assemblyFileName in assemblyFileNames)
					context.AddAssembly(assemblyFileName);
				foreach (string xmlFileName in xmlFileNames)
					context.AddXmlFile(xmlFileName);

			});
			projectData.ProjectName = projectSettings.Project.Name;

			ProjectUpdaterProvider.UpdateProject(projectSettings.Project, (updater) => {
				ExtractorData extractorData = new ExtractorData(projectData, projectSettings.Project, updater);
				foreach (var typeData in projectData.AllTypes.Values) {
					doCS.Models.Type type = GetOrCreateType(typeData, extractorData);
				}
			});


		}

		private Namespace GetOrCreateNamespace(string namespaceName, ExtractorData extractorData) {
			var result = extractorData.ProjectUpdater.FindOrCreateNamespace(namespaceName, (Namespace ns) => {
					ns.Name = namespaceName;
					ns.Project = extractorData.Project;
			});
			return result;
		}

		private Assembly GetOrCreateAssembly(string assemblyName, ExtractorData extractorData) {
			var result = extractorData.ProjectUpdater.FindOrCreateAssembly(assemblyName, (Assembly assembly) => {
					assembly.Name = assemblyName;
					assembly.Project = extractorData.Project;
			});
			return result;
		}

		private doCS.Models.Type GetOrCreateType(TypeData typeData, ExtractorData extractorData) {
			Namespace ns = GetOrCreateNamespace(AssemblyQualifiedName.GetNamespaceName(typeData.Name), extractorData);
			Assembly assembly = GetOrCreateAssembly(AssemblyQualifiedName.GetAssemblyName(typeData.Name), extractorData);
			string typeName = AssemblyQualifiedName.GetTypeName(typeData.Name);
			doCS.Models.Type foundType = extractorData.ProjectUpdater.FindOrCreateType(typeName, assembly.Name, ns.Name, (doCS.Models.Type type) => {
				type.Name = typeName;
				type.Namespace = ns;
				type.Assembly = assembly;
				type.BaseType = GetBaseType(typeData, extractorData);
				var interfaces = GetInterfaces(typeData, extractorData);
				UpdateInterfaces(type, interfaces);
				UpdateGenericArguments(type, typeData);
				if (type.XmlDocumentation == null)
					type.XmlDocumentation = new XmlDocumentation();
				type.XmlDocumentation.XmlComments = (string.IsNullOrEmpty(typeData.XmlDocumentation)) ? "" : typeData.XmlDocumentation;
				UpdateProperties(typeData, type, extractorData);
			});
			return foundType;
		}

		private doCS.Models.Type GetBaseType(TypeData typeData, ExtractorData extractorData) {
			doCS.Models.Type baseType = null;
			if (!string.IsNullOrEmpty(typeData.BaseTypeName)) {
				var baseTypeData = extractorData.ProjectData.AllTypes[typeData.BaseTypeName];
				baseType = GetOrCreateType(baseTypeData, extractorData);
			}
			return baseType;
		}

		private List<doCS.Models.Type> GetInterfaces(TypeData typeData, ExtractorData extractorData) {
			List<doCS.Models.Type> interfaces = new List<doCS.Models.Type>();
			foreach (string interfaceName in typeData.Interfaces) {
				var interfaceTypeData = extractorData.ProjectData.AllTypes[interfaceName];
				var interfaceType = GetOrCreateType(interfaceTypeData, extractorData);
				interfaces.Add(interfaceType);
			}
			return interfaces;
		}

		private void UpdateInterfaces(doCS.Models.Type type, List<doCS.Models.Type> newInterfaces) {
			//if any type have been added then add the relation
			foreach (var interfaceType in newInterfaces) {
				if (!type.Interfaces.Contains(interfaceType))
					type.Interfaces.Add(interfaceType);
			}
			//if any types have been removed then remove the relation
			List<doCS.Models.Type> removedInterfaces = new List<doCS.Models.Type>();
			foreach (var interfaceType in type.Interfaces) {
				if (!newInterfaces.Contains(interfaceType))
					removedInterfaces.Add(interfaceType);
			}
			type.Interfaces.RemoveAll(removedInterfaces);
		}

		private void UpdateGenericArguments(doCS.Models.Type type, TypeData typeData) {
			if (type.GenericArguments.Count == 0 && typeData.GenericArguments.Count == 0)
				return;
			if (type.GenericArguments.Count > 0 && typeData.GenericArguments.Count == 0) {
				type.GenericArguments.Clear();
				return;
			}
			//make sure the list is the correct length
			for (int excess = typeData.GenericArguments.Count; excess < type.GenericArguments.Count; excess++)
				type.GenericArguments.Remove(type.GenericArguments.ElementAt(excess));
			for (int incess = type.GenericArguments.Count; incess < typeData.GenericArguments.Count; incess++)
				type.GenericArguments.Add(new GenericArgument());
			int i = 0;
			for (i = 0; i < typeData.GenericArguments.Count && i < type.GenericArguments.Count; i++) {
				var genericParamterData = typeData.GenericArguments[i];
				var genericParamter = type.GenericArguments.ElementAt(i);
				genericParamter.Type = type;
				genericParamter.Name = genericParamterData.Name;
				genericParamter.ArgumentOrder = (short)i;
			}

		}

		private void UpdateProperties(TypeData typeData, doCS.Models.Type type, ExtractorData extractorData) {
			foreach (PropertyData propertyData in typeData.Properties) {
				//TODO: Skipping generic properties for now (properties with no name)
				if (!string.IsNullOrEmpty(propertyData.TypeName)) {
					var result = extractorData.ProjectUpdater.FindOrCreateProperty(propertyData.Name, type, (Property property) => {
						property.Name = propertyData.Name;
						property.DeclaringType = type;
						property.Type = GetOrCreateType(propertyData.TypeName, extractorData);
						property.GetAccessType = ConvertAccessType(propertyData.GetAccessType);
						property.SetAccessType = ConvertAccessType(propertyData.SetAccessType);
						property.IsAbstract = propertyData.IsAbstract;
						property.IsStatic = propertyData.IsStatic;
						property.IsVirtual = propertyData.IsVirtual;
						if (property.XmlDocumentation == null)
							property.XmlDocumentation = new XmlDocumentation();
						property.XmlDocumentation.XmlComments = propertyData.XmlComments;
						//TODO: update overrides and shadows here
					});
				}
			}
		}

		private doCS.Models.Type GetOrCreateType(string assemblyQualifiedName, ExtractorData extractorData) {
			if (extractorData.ProjectData.AllTypes.ContainsKey(assemblyQualifiedName)) {
				TypeData typeData = extractorData.ProjectData.AllTypes[assemblyQualifiedName];
				return GetOrCreateType(typeData, extractorData);
			} else {
				//this should resolve it from other projects but for now just create the assembly/namespace and type
				string assemblyName = AssemblyQualifiedName.GetAssemblyName(assemblyQualifiedName);
				string namespaceName = AssemblyQualifiedName.GetAssemblyName(assemblyQualifiedName);
				var typeName = AssemblyQualifiedName.GetTypeName(assemblyQualifiedName);
				var assembly = GetOrCreateAssembly(assemblyName, extractorData);
				var ns = GetOrCreateNamespace(namespaceName, extractorData);
				doCS.Models.Type foundType = extractorData.ProjectUpdater.FindOrCreateType(typeName, assembly.Name, ns.Name, (doCS.Models.Type type) => {
					type.Name = typeName;
					type.Namespace = ns;
					type.Assembly = assembly;
				});
				return foundType;
			}
		}

		private doCS.Models.AccessType ConvertAccessType(doCS.Extractor.AccessType accessType) {
			switch (accessType) {
				case doCS.Extractor.AccessType.Public:
					return doCS.Models.AccessType.Public;
				case doCS.Extractor.AccessType.Protected:
					return doCS.Models.AccessType.Protected;
				case doCS.Extractor.AccessType.Private:
					return doCS.Models.AccessType.Private;
				default:
					return doCS.Models.AccessType.Unknown;
			}
		}

	}
}