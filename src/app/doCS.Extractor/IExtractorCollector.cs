using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace doCS.Extractor {
	public interface IExtractorCollector {



		/// <summary>
		/// A list of assemblies to extract documentation from
		/// </summary>
		List<FileInfo> Assemblies { get; }

		/// <summary>
		/// A list of assemblies for which no documenation will be generated but are required dependencies of Assemblies
		/// </summary>
		List<FileInfo> DependantAssemblies { get; }

		/// <summary>
		/// List of files generated with the /doc option of the c# compiler
		/// </summary>
		List<FileInfo> XmlDocumentation { get; }

		void AddType(TypeData typeInfo);

		void AddDocumentation(string referenceName, string xmlFragment);

		void AddProperty(TypeData typeInfo, PropertyData propertyData);

		void AddPropertySummary(string referenceName, string summaryText);

		void AddAssembly(string assemblyFileName);

		void AddDependantAssembly(string assemblyFileName);

		void AddXmlFile(string xmlFileName);
	}
}
