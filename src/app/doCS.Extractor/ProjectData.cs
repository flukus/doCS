using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor {

	/// <summary>
	/// A structure to hold general project information and a list of all types contained within
	/// </summary>
	public class ProjectData {

		/// <summary>
		/// The name of the project.
		/// </summary>
		public string ProjectName { get; set; }

		/// <summary>
		/// The major/minor/release version of the project.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// The revision number of the project. Usually something like an SVN revision number.
		/// </summary>
		public string Revision { get; set; }

		/// <summary>
		/// List of all types which are extracted from the provided assemblies.
		/// </summary>
		public Dictionary<string, TypeData> AllTypes { get; private set; }

		/// <summary>
		/// List of all warnings/errors that occured during the extraction proccess.
		/// </summary>
		public List<string> Warnings { get; private set; }

		public ProjectData() : this("", "", "") {
		}

		public ProjectData(string projectName, string version, string revision) {
			ProjectName = projectName;
			Version = version;
			Revision = revision;
			AllTypes = new Dictionary<string, TypeData>();
			Warnings = new List<string>();
		}

	}
}
