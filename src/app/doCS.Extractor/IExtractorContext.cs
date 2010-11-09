using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace doCS.Extractor {

	/// <summary>
	/// This is an external inteface intended for consumers of the Extractor project to add configuration information
	/// </summary>
	public interface IExtractorContext {

		/// <summary>
		/// Add a .Net Assembly to extract documentation from
		/// </summary>
		/// <remarks>The order in which these are added will be preserved</remarks>
		void AddAssembly(string assemblyFileName);

		/// <summary>
		/// A a .Net Assembly no documenation will be extracted but is a required dependency of another Assembly
		/// </summary>
		/// <remarks>The order in which these are added will be preserved</remarks>
		void AddDependantAssembly(string assemblyFileName);

		/// <summary>
		/// Add an XML file that was generated with the /doc option of the c# compiler
		/// </summary>
		void AddXmlFile(string xmlFileName);

	}
}
