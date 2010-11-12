using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using doCS.Models;

namespace doCS.Web.Helpers {
	public static class NameHelper {

		public static string GetNameFor(doCS.Models.Type type) {
			if (type.Name == "Int32")
				return "int";
			if (type.GenericArguments.Count == 0)
				return type.Name;
			StringBuilder stringBuilder = new StringBuilder(type.Name);
			stringBuilder.Append('<');
			foreach (var genericParam in type.GenericArguments) {
				stringBuilder.Append(genericParam.Name);
				if (genericParam != type.GenericArguments.Last())
					stringBuilder.Append(", ");
			}
			stringBuilder.Append('>');
			return stringBuilder.ToString();
		}

		public static string GetNameFor(AccessType accessType) {
			string value = string.Empty;
			switch (accessType) {
				case AccessType.Private:
					value = "private";
					break;
				case AccessType.Protected: 
					value = "protected";
					break;
				case AccessType.Public :
					value = "public";
					break;
				default:
					value = "unknown";
					break;
			}
			return value;
		}

	}
}