using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using doCS.Models;
using doCS.Extractor;

namespace doCS.Web.Helpers.Implimentation.Extractor {
	public class ExtractorHelper : IExtractorHelper {
		private readonly ISession DbSession;
		private readonly IExtractor Extractor;
		private readonly ProjectUpdater EntityCache;

		public ExtractorHelper(ISession session, IExtractor extractor) {
			DbSession = session;
			Extractor = extractor;
			EntityCache = new ProjectUpdater();
		}

		public void Extract(ProjectSettings projectSettings) {

			FillEntityCache(projectSettings.Project);
			ProjectData projectData = Extractor.Extract((IExtractorContext context) => {

				string[] assemblyFileNames = projectSettings.IncludedAssemblies.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
				string[] xmlFileNames = projectSettings.IncludedXmlFiles.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string assemblyFileName in assemblyFileNames)
					context.AddAssembly(assemblyFileName);
				foreach (string xmlFileName in xmlFileNames)
					context.AddXmlFile(xmlFileName);

			});
			projectData.ProjectName = projectSettings.Project.Name;

			ExtractorData extractorData = new ExtractorData(projectData, projectSettings.Project);
			foreach (var typeData in projectData.AllTypes.Values) {
				doCS.Models.Type type = GetOrCreateType(typeData, extractorData);
			}


			using (var transaction = DbSession.BeginTransaction()) {
				foreach (var type in EntityCache.GetRemovedTypes())
					DbSession.Delete(type);
				foreach (var ns in EntityCache.GetRemovedNamespaces())
					DbSession.Delete(ns);
				foreach (var ass in EntityCache.GetRemovedAssemblies())
					DbSession.Delete(ass);
				DbSession.Persist(projectSettings.Project);
				foreach (var ns in EntityCache.CurrentNamespaces)
					DbSession.Persist(ns);
				foreach (var ass in EntityCache.CurrentAssemblies)
					DbSession.Persist(ass);
				foreach (var type in EntityCache.CurrentTypes)
					DbSession.Persist(type);
				transaction.Commit();
				DbSession.Flush();
			}

		}

		private Namespace GetOrCreateNamespace(string namespaceName, Project project) {
			Namespace ns = null;
			ns = EntityCache.FindNamespaceByName(namespaceName);
			if (ns == null) {
				ns = new Namespace() {
					Name = namespaceName,
					Project = project
				};
				EntityCache.AddNamespace(ns);
			}
			return ns;
		}

		private Assembly GetOrCreateAssembly(string assemblyName, Project project) {
			Assembly assembly = EntityCache.FindAssemblyByName(assemblyName);
			if (assembly == null) {
				assembly = new Assembly() {
					Name = assemblyName,
					Project = project
				};
				EntityCache.AddAssembly(assembly);
			}
			return assembly;
		}

		private doCS.Models.Type GetOrCreateType(TypeData typeData, ExtractorData extractorData) {
			Namespace ns = GetOrCreateNamespace(AssemblyQualifiedName.GetNamespaceName(typeData.Name), extractorData.Project);
			Assembly assembly = GetOrCreateAssembly(AssemblyQualifiedName.GetAssemblyName(typeData.Name), extractorData.Project);
			string typeName = AssemblyQualifiedName.GetTypeName(typeData.Name);
			doCS.Models.Type foundType = EntityCache.FindOrCreateType(typeName, assembly.Name, ns.Name, (doCS.Models.Type type) => {
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

		private void FillEntityCache(Project project) {
			var assemblies = DbSession.QueryOver<Assembly>().Where(x => x.Project == project).List<Assembly>();
			var namespaces = DbSession.QueryOver<Namespace>().Where(x => x.Project == project).List<Namespace>().Distinct();
			var types = DbSession.QueryOver<doCS.Models.Type>().JoinQueryOver<Assembly>(x => x.Assembly).Where(x => x.Project == project).List<doCS.Models.Type>();
			EntityCache.Initialize(namespaces, assemblies, types);
		}

	}
}