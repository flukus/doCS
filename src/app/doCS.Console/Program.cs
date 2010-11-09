using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using doCS.Extractor;
using doCS.Extractor.Implementation;

namespace doCS.Console {
	class Program {
		static void Main(string[] args) {

			IExtractor extractor = new doCS.Extractor.Implementation.Extractor(new DllExtractor(), new XmlExtractor());
			ProjectData project = extractor.Extract((IExtractorContext context) => {
				context.AddAssembly(@"D:\projects\nhibernate\src\NHibernate\bin\Debug-2.0\NHibernate.dll");
				context.AddDependantAssembly(@"D:\projects\nhibernate\src\NHibernate\bin\Debug-2.0\Iesi.Collections.dll");
				context.AddDependantAssembly(@"D:\projects\nhibernate\src\NHibernate\bin\Debug-2.0\Antlr3.Runtime.dll");
				context.AddXmlFile(@"D:\projects\nhibernate\src\NHibernate\bin\Debug-2.0\nhibernate.xml");
			});

		}
	}
}
