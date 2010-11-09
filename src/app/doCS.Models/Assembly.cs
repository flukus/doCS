using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Models {
	public class Assembly {
		virtual public Guid Id { get; set; }
		virtual public string Name { get; set; }
		virtual public Project Project { get; set; }
	}
}
