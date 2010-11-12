using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor {
	public class PropertyData {
		public string Name { get; set; }
		public string TypeName { get; set; }
		public AccessType GetAccessType { get; set; }
		public AccessType SetAccessType { get; set; }
		public bool IsStatic { get; set; }
		public bool IsVirtual { get; set; }
		public bool IsAbstract { get; set; }
		public string XmlComments { get; set; }
	}
}
