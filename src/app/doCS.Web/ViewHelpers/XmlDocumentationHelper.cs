using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.IO;

namespace doCS.Web.ViewHelpers {

	/// <summary>
	/// This class is responsible for turning c# xml documentation into html.
	/// </summary>
	public class XmlDocumentationHelper {

		public bool HasSummary(string xmlDocumentation) {
			return !string.IsNullOrEmpty(xmlDocumentation);
		}

		/// <summary>
		/// Currently a naive implementation. Treats all fields under summary as text.
		/// </summary>
		/// <param name="xmlDocumentation"></param>
		/// <returns></returns>
		public string GetSummary(string xmlDocumentation) {
			var frag = new System.Xml.XmlTextReader(xmlDocumentation, System.Xml.XmlNodeType.Element, null);
			string result = "";
			while (frag.Read()) {
				result += frag.Value;
			}
			frag.Close();
			return result;
		}

	}
}