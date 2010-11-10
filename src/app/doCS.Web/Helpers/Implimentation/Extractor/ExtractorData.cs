using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Extractor;
using doCS.Models;

namespace doCS.Web.Helpers.Implimentation.Extractor {
	public class ExtractorData {
		public ProjectData ProjectData { get; private set; }
		public Project Project { get; private set; }

		public ExtractorData(ProjectData projectData, Project project) {
			ProjectData = projectData;
			Project = project;
		}

	}
}