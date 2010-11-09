using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Iesi.Collections.Generic;

namespace doCS.Models {
	public class Project {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public string Version { get; set; }
		//virtual public IList<Namespace> Namespaces { get; protected set; }
		virtual public Iesi.Collections.Generic.ISet<Namespace> Namespaces { get; protected set; }
		virtual public Iesi.Collections.Generic.ISet<Assembly> Assemblies { get; protected set; }

		public Project() {
			//Namespaces = new List<Namespace>();
			Namespaces = new HashedSet<Namespace>();
			Assemblies = new Iesi.Collections.Generic.HashedSet<Assembly>();
		}

	}
}
