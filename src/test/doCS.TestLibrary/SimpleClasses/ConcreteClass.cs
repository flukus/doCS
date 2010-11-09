using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.SimpleClasses {

	/// <summary>
	/// Concrete child of an abstract class
	/// </summary>
	public class ConcreteClass : AbstractClass {

		public override string AbstractProperty { get; set; }

		public override void DoSomething() {
		}

	}

}
