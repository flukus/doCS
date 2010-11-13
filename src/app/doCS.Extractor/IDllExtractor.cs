using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace doCS.Extractor {
	public interface IDllExtractor {
		void Extract(IExtractorCollector context);
	}
}
