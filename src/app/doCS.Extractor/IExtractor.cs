using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor {

	/// <summary>
	/// This is the main interface through which external projects should extract assembly/xml documentation
	/// </summary>
	public interface IExtractor {

		/// <summary>
		/// Extracts documentation and returns a ProjectData object.
		/// </summary>
		/// <param name="contextFunc">The ExtractorContext passed to this action contains setup information for project.
		/// All assemblies/xml files to be scanned should be added to this context</param>
		/// <returns>ProjectData object with all types and their documenation. Project name/version etc will not be filled in.</returns>
		ProjectData Extract(Action<IExtractorContext> contextFunc);
	}
}
