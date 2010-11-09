using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace doCS.Web.Models {

	public class TypeViewBase {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid NamespaceId { get; set; }
		public string NamespaceName { get; set; }
		public List<TypeViewBase> Interfaces { get; set; }

		public TypeViewBase() {
			Interfaces = new List<TypeViewBase>();
		}

	}

	public class TypeView  : TypeViewBase {

		public Guid AssemblyId { get; set; }
		public string AssemblyName { get; set; }
		public string XmlDocumentation { get; set; }
		public List<TypeViewBase> BaseClasses { get; set; }
		public List<TypeViewMember> Members { get; set; }
		public List<TypeViewProperty> Properties { get; set; }
		public List<TypeViewMethod> Methods { get; set; }

		public TypeView() {
			BaseClasses = new List<TypeViewBase>();
			Members = new List<TypeViewMember>();
			Properties = new List<TypeViewProperty>();
			Methods = new List<TypeViewMethod>();
		}

	}

	public class TypeViewMember {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid TypeId { get; set; }
		public string TypeName { get; set; }
	}

	public class TypeViewProperty {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid TypeId { get; set; }
		public string TypeName { get; set; }
	}

	public class TypeViewMethod {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public Guid ReturnTypeId { get; set; }
		public string ReturnTypeName { get; set; }
	}

}