using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Models;

namespace doCS.Web.Helpers {
	public interface IExtractorHelper {
		void Extract(ProjectSettings projectSettings);
	}
}