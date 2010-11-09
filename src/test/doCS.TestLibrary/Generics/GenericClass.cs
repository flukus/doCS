using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.TestLibrary.Generics {

	/// <summary>
	/// A class with 2 Generic parameters
	/// </summary>
	public class GenericClass<TKey, TValue> where TValue : class {

		/// <summary>
		/// A generic property
		/// </summary>
		public TValue Value { get; set; }

		/// <summary>
		/// A function that returns a generic parameter
		/// </summary>
		/// <returns></returns>
		public TValue GetFirstValue() {
			return null;
		}

		/// <summary>
		/// A function that take a generic argument
		/// </summary>
		/// <param name="value"></param>
		public void AddValue(TValue value) {
		}
		
	}

}
