using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doCS.Web.Models {
	public class TypeListItem {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid NamespaceId { get; set; }
		public string NamespaceName { get; set; }
	}
}