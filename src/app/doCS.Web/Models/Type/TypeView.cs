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

	public class TypeView : TypeViewBase {

		public Guid AssemblyId { get; set; }
		public string AssemblyName { get; set; }
		public string XmlDocumentation { get; set; }
		public List<TypeViewBase> BaseClasses { get; set; }
		public List<TypeView.Member> Members { get; set; }
		public List<TypeView.Property> Properties { get; set; }
		public List<TypeView.Method> Methods { get; set; }

		public TypeView() {
			BaseClasses = new List<TypeViewBase>();
			Members = new List<TypeView.Member>();
			Properties = new List<TypeView.Property>();
			Methods = new List<TypeView.Method>();
		}


		public class Member {
			public Guid Id { get; set; }
			public string Name { get; set; }
			public Guid TypeId { get; set; }
			public string TypeName { get; set; }
		}


		public class Property {
			public Guid Id { get; set; }
			public string Name { get; set; }
			public Guid TypeId { get; set; }
			public string TypeName { get; set; }
			public string GetAccessor { get; set; }
			public string SetAccessor { get; set; }
			public Guid DeclaringTypeId { get; set; }
			public string DeclaringTypeName { get; set; }
			public string XmlDocumentation { get; set; }
		}


		public class Method {
			public Guid Id { get; set; }
			public string Name { get; set; }
			public Guid ReturnTypeId { get; set; }
			public string ReturnTypeName { get; set; }
		}

	}

}