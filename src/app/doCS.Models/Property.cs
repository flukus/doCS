using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Models {
	public class Property {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public Type Type { get; set; }
		virtual public Property Overrides { get; set; }
		virtual public Property Shadows { get; set; }
		virtual public AccessType GetAccessType { get; set; }
		virtual public AccessType SetAccessType { get; set; }
		virtual public XmlDocumentation XmlDocumentation { get; set; }
	}
}
