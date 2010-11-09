using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Models {
	public class Property {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public Type Type { get; set; }
		virtual public Project Project { get; set; }
	}
}
