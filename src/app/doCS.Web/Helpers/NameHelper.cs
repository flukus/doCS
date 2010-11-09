using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace doCS.Web.Helpers {
	public static class NameHelper {

		public static string GetNameFor(doCS.Models.Type type) {
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

	}
}