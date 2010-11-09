using System;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;

namespace doCS.Models {
	public class Namespace {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public Project Project { get; set; }
		virtual public ISet<Type> Types { get; protected set; }

		public Namespace() {
			Types = new Iesi.Collections.Generic.HashedSet<Type>();
		}

	}

	public class NamespaceComparer : System.Collections.Generic.IComparer<Namespace> {
		public int Compare(Namespace x, Namespace y) {
			return string.Compare(x.Name, y.Name);
		}
	}

}
