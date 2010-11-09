using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using doCS.Models;
using System.Web.Routing;
using NHibernate;
using NHibernate.Criterion;

namespace doCS.Web.Helpers.Implimentation {
	public class ProjectContext : IProjectContext {

		private readonly ISession DbSession;

		public ProjectContext(ISession session) {
			DbSession = session;
		}

		public Project CurrentProject {
			get {
				var routeData = RouteTable.Routes.GetRouteData(new HttpContextWrapper(HttpContext.Current));
				string projectName = routeData.Values["project"] as string;
				if (string.IsNullOrEmpty(projectName))
					throw new Exception("No route value is set for 'project'");
				Project project = DbSession.CreateCriteria<Project>().Add(Restrictions.Eq("Name", projectName)).UniqueResult<Project>();
				if (project == null)
					throw new Exception(string.Format("Unknown project '{0}'", projectName));
				return project;
			}
		}
	}
}