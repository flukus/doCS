using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.Interfaces {

	/// <summary>
	/// Child class of an interface implementation that implements it's own interface
	/// </summary>
	public class ChildOfSomeImplementation : ImplementationClass, ISomeOtherInterface {
		public string SomeChildProperty { get; set; }

		public void DoSomethingElse() {
		}
	}

}
