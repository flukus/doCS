using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doCS.Web.Models {
	public class NamespaceView {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public List<NamespaceViewType> Types { get; private set; }

		public NamespaceView(Guid id, string name) {
			Id = id;
			Name = name;
			Types = new List<NamespaceViewType>();
		}

	}

	public class NamespaceViewType {
		public Guid Id { get; set; }
		public string Name { get; set; }
	}

}