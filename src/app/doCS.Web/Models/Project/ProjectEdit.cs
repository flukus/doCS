using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace doCS.Web.Models {
	public class ProjectEdit {
		public Guid? Id { get; set; }
		[Required(ErrorMessage = "Name is required")]
		public string Name { get; set; }
		public string IncludedAssemblies { get; set; }
		public string IncludedXmlFiles { get; set; }

		public ProjectEdit() {
			Name = "";
			IncludedAssemblies = "";
			IncludedXmlFiles = "";
		}

	}
}