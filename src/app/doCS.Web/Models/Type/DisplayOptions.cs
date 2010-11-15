using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace doCS.Web.Models.Type {
	public class DisplayOptions {
		[DisplayName("Public")]
		public bool ShowPublic { get; set; }
		[DisplayName("Private")]
		public bool ShowPrivate { get; set; }
		[DisplayName("Protected")]
		public bool ShowProtected { get; set; }
		[DisplayName("Inherited")]
		public bool ShowInherited { get; set; }

	}
}