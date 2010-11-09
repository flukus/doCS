using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace doCS.Extractor {

	public class TypeData {

		public string Name { get; set; }
		public string BaseTypeName { get; set; }
		public string DeclaringTypeName { get; set; }
		public string XmlDocumentation { get; set; }
		public bool IsInternal { get; set; }
		public List<PropertyData> Properties { get; private set; }
		public List<GenericArgumentData> GenericArguments { get; private set; }
		public List<string> Interfaces { get; private set; }

		public TypeData(string name) {
			this.Name = name;
			Properties = new List<PropertyData>();
			GenericArguments = new List<GenericArgumentData>();
			Interfaces = new List<string>();
		}


	}
}
