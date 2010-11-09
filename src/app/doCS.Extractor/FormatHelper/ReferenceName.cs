using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor.FormatHelper {

	/// <summary>
	/// A helper class to extract specific information from reference names which are output by the C# xml documentation compiler.
	/// </summary>
	public static class ReferenceName {

		/// <summary>
		/// Returns true if the referenceName is a type reference
		/// </summary>
		/// <param name="referenceName"></param>
		/// <returns></returns>
		public static bool IsType(string referenceName) {
			return referenceName.StartsWith("T:");
		}

		public static string GetFullName(string referenceName) {
			if (!IsType(referenceName))
				throw new FormatException("The provided referenceName is not a valid type reference name");
			return referenceName.Substring(2, referenceName.Length - 2);
		}

		public static bool IsProperty(string referenceName) {
			return referenceName.StartsWith("P:");
		}

		public static string GetPropertyName(string referenceName) {
			string[] parts = referenceName.Split('.');
			return parts.Last();
		}

		public static string GetFullNameFromPropertyReference(string referenceName) {
			if (!IsProperty(referenceName))
				throw new FormatException("The provided referenceName is not a valid property reference name");
			referenceName = referenceName.Substring(2, referenceName.Length - 2);
			int lastIndex = referenceName.LastIndexOf('.');
			return referenceName.Substring(0, lastIndex);
		}

	}
}
