using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;
using doCS.Extractor.FormatHelper;

namespace doCS.Extractor.Implementation {
	public class XmlExtractor {

		public void Extract(ExtractorContext context) {
			foreach (FileInfo fi in context.XmlDocumentation) {
				//var xmlReader = new XmlTextReader(fi.FullName);
				//xmlReader.Close();

				FileStream stream = new FileStream(fi.FullName, FileMode.Open);
				XDocument doc = XDocument.Load(stream);
				//XmlReader reader = XmlReader.Create(stream);
				ReadXDocument(context, doc);
				stream.Close();
			}
		}

		private void ReadXDocument(ExtractorContext context, XDocument document) {
			var members = document.Document.Root.Element("members").Elements("member");
			foreach (XElement member in members){
				string referenceName = member.Attributes("name").FirstOrDefault().Value;
				if (ReferenceName.IsProperty(referenceName))
					ReadProperty(context, referenceName, member);
				else if (ReferenceName.IsType(referenceName))
					ReadType(context, referenceName, member);
				
			}
		}

		private void ReadType(ExtractorContext context, string referenceName, XElement element) {
			XElement summary = element.Element("summary");
				if (summary != null) {
					string summaryText = summary.Value.Trim();
					context.AddDocumentation(referenceName, element.ToString());
				}
		}

		private void ReadProperty(ExtractorContext context, string referenceName, XElement element) {
			XElement summary = element.Element("summary");
				if (summary != null) {
					string summaryText = summary.Value.Trim();
					context.AddPropertySummary(referenceName, summaryText);
				}
		}

	}
}
