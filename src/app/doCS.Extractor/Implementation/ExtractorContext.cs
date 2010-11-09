using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using doCS.Extractor.FormatHelper;

namespace doCS.Extractor.Implementation
{
	/// <summary>
	/// This is a context object to be used with a single extraction instance. IDisposable is implemented because it creates an AppDomain that requires disposing.
	/// </summary>
	public class ExtractorContext : IExtractorContext, IDisposable
	{

		/// <summary>
		/// A list of assemblies to extract documentation from
		/// </summary>
		public List<FileInfo> Assemblies {get;private set;}

		/// <summary>
		/// A list of assemblies for which no documenation will be generated but are required dependencies of Assemblies
		/// </summary>
		public List<FileInfo> DependantAssemblies { get; private set; }

		/// <summary>
		/// List of files generated with the /doc option of the c# compiler
		/// </summary>
		public List<FileInfo> XmlDocumentation { get; private set; }


		/// <summary>
		/// An isolated AppDomain to load assemblies into
		/// </summary>
		public AppDomain AppDomain { get; private set; }

		/// <summary>
		/// A ProjectData object that contains all the type information and documentation.
		/// </summary>
		public ProjectData ProjectData { get; private set; }

		private readonly SortedList<string, TypeData> fullNameIndex;

		public ExtractorContext(ProjectData project) {
			fullNameIndex = new SortedList<string, TypeData>();
			Assemblies = new List<FileInfo>();
			DependantAssemblies = new List<FileInfo>();
			XmlDocumentation = new List<FileInfo>();
			AppDomain = AppDomain.CreateDomain(Guid.NewGuid().ToString());
			ProjectData = project;
		}

		public void AddType(TypeData typeInfo) {
			string fullName = AssemblyQualifiedName.GetFullName(typeInfo.Name);
			ProjectData.AllTypes.Add(typeInfo.Name, typeInfo);
			fullNameIndex.Add(fullName, typeInfo);
		}

		public void AddDocumentation(string referenceName, string xmlFragment) {
			string fullName = ReferenceName.GetFullName(referenceName);
			if (fullNameIndex.ContainsKey(fullName))
				fullNameIndex[fullName].XmlDocumentation = xmlFragment;
		}

		public void AddProperty(TypeData typeInfo, PropertyData propertyData) {
			typeInfo.Properties.Add(propertyData);
		}

		public void AddPropertySummary(string referenceName, string summaryText) {
			string fullName = ReferenceName.GetFullNameFromPropertyReference(referenceName);
			string propertyName = ReferenceName.GetPropertyName(referenceName);
			if (fullNameIndex.ContainsKey(fullName)) {
				PropertyData pd = fullNameIndex[fullName].Properties.FirstOrDefault(x => x.Name == propertyName);
				if (pd != null)
					pd.Summary = summaryText;
				else {
				//TODO: log message here
				}
			} else {
				//TODO: log message here
			}
		}

		public void AddAssembly(string assemblyFileName) {
			FileInfo fi = new FileInfo(assemblyFileName);
			if (!fi.Exists) {
				//TODO: Log error here
				throw new FileNotFoundException(string.Format("Could not find assembly '{0}'. The full filename searched for will appear in the log", assemblyFileName));
			}
			//TODO: Maybe test if this is a valid file here
			Assemblies.Add(fi);
		}

		public void AddDependantAssembly(string assemblyFileName) {
			FileInfo fi = new FileInfo(assemblyFileName);
			if (!fi.Exists) {
				//TODO: Log error here
				throw new FileNotFoundException(string.Format("Could not find assembly '{0}'. The full filename searched for will appear in the log", assemblyFileName));
			}
			//TODO: Maybe test if this is a valid file here
			DependantAssemblies.Add(fi);
		}

		public void AddXmlFile(string xmlFileName) {
			FileInfo fi = new FileInfo(xmlFileName);
			if (!fi.Exists) {
				//TODO: Log error here
				throw new FileNotFoundException(string.Format("Could not find file '{0}'. The full filename searched for will appear in the log", xmlFileName));
			}
			//TODO: Maybe test if this is a valid file here
			XmlDocumentation.Add(fi);
		}

		public void Dispose() {
			AppDomain.Unload(AppDomain);
		}

	}
}
