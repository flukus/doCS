using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Models {
	public class ProjectSettings {
		virtual public Guid Id { get; set; }
		virtual public Project Project { get; set; }
		virtual public string IncludedAssemblies { get; set; }
		virtual public string IncludedXmlFiles { get; set; }
	}
}
