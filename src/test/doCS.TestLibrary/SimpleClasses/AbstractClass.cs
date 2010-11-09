using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.SimpleClasses {

	/// <summary>
	/// An abstract class
	/// </summary>
	public abstract class AbstractClass {

		public int PublicProperty { get; set; }

		public abstract string AbstractProperty { get; set; }

		/// <summary>
		/// Abstract Method
		/// </summary>
		public abstract void DoSomething();

	}

}
