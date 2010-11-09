using System;
using System.Linq;
using System.Text;

using Iesi.Collections.Generic;

namespace doCS.Models {
	public class Type {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public Namespace Namespace { get; set; }
		virtual public Assembly Assembly { get; set; }
		virtual public Type BaseType { get; set; }
		virtual public XmlDocumentation XmlDocumentation { get; set; }
		virtual public ISet<GenericArgument> GenericArguments { get; set; }
		virtual public ISet<Type> Interfaces { get; set; }

		public Type() {
			GenericArguments = new HashedSet<GenericArgument>();
			Interfaces = new HashedSet<Type>();
		}
	}
}
