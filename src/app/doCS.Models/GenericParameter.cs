using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Models {
	public class GenericArgument {
		public virtual Guid Id { get; set; }
		public virtual string Name { get; set; }
		public virtual Int16 ArgumentOrder { get; set; }
		public virtual Type Type { get; set; }

		public GenericArgument() {
		}
	}
}
