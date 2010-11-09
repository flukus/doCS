using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.SimpleClasses {

	/// <summary>
	/// This is intended to act as a base class 
	/// </summary>
	public class ParentClass {

		private int PrivateMember;
		public readonly int ReadOnlyMember;

		public string Name { get; set; }
		private Guid Id { get; set; }

		/// <summary>
		/// A protected function
		/// </summary>
		protected void DoStuff() {
		}

		/// <summary>
		/// This class is here for a derived class to override (with the new keyword)
		/// </summary>
		/// <returns></returns>
		public string MethodToBeHidden() {
			return "";
		}

	}
}
