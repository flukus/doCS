using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor.Implementation {

	/// <summary>
	/// This class is responsible for coordinating the extraction process, managing the context etc.
	/// This is essentially a wrapper clas for in future when other extractors are added.
	/// </summary>
	public class Extractor : IExtractor {
		private readonly DllExtractor DllExtractor;
		private readonly XmlExtractor XmlExtractor;

		/// <summary>
		/// Only constructor. Will create a valid object on which Extract can be called.
		/// </summary>
		/// <param name="dllExtractor"></param>
		/// <param name="xmlExtractor"></param>
		public Extractor(DllExtractor dllExtractor, XmlExtractor xmlExtractor) {
			DllExtractor = dllExtractor;
			XmlExtractor = xmlExtractor;
		}

		/// <summary>
		/// Manages the lifecycle of the ExtractorContext and delegates to child extractors.
		/// </summary>
		/// <param name="contextFunc"></param>
		/// <returns>A ProjectData object with all types from the requested assemblies</returns>
		public ProjectData Extract(Action<IExtractorContext> contextFunc) {

			ProjectData projectData = new ProjectData();
			using (ExtractorContext context = new ExtractorContext(projectData)) {
				contextFunc(context);

				DllExtractor.Extract(context);
				XmlExtractor.Extract(context);
			}
			return projectData;
		}

	}
}
