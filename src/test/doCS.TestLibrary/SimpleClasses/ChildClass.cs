using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.SimpleClasses {

	/// <summary>
	/// This has ParentClass as a base class
	/// </summary>
	public class ChildClass : ParentClass {

		/// <summary>
		/// Counts Something
		/// </summary>
		public int CountOfSomething { get; set; }

		/// <summary>
		/// A readonly property with a private setter
		/// </summary>
		public int ReadOnlyProperty { get; private set; }

		private int PrivateProperty { get; set; }

		protected int ProtectedProperty { get; set; }

		public int GetOnlyProperty { get; set; }

		public int SetOnlyProperty { set { } }

		public ConcreteClass ReferenceToOtherClass { get; set; }

		public static int StaticProperty { get; set; }

		public virtual int VirtualProperty { get; set; }

	}
}
