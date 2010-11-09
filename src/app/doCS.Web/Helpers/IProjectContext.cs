using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Models;

namespace doCS.Web.Helpers {
	public interface IProjectContext {
		Project CurrentProject { get; }
	}
}