using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using doCS.Models;

namespace doCS.Web.Helpers.Implimentation.Extractor {

	public class ProjectUpdaterProvider {

		private readonly ISession DbSession;

		public ProjectUpdaterProvider(ISession session) {
			DbSession = session;
		}

		public void UpdateProject(Project project, Action<ProjectUpdater> projectUpdaterAction) {

			//Create the ProjectUpdater
			var assemblies = DbSession.QueryOver<Assembly>().Where(x => x.Project == project).List<Assembly>();
			var namespaces = DbSession.QueryOver<Namespace>().Where(x => x.Project == project).List<Namespace>().Distinct();
			var types = DbSession.QueryOver<doCS.Models.Type>().JoinQueryOver<Assembly>(x => x.Assembly).Where(x => x.Project == project).List<doCS.Models.Type>();
			var properties = DbSession.QueryOver<Property>().JoinQueryOver(x => x.Type).JoinQueryOver(x => x.Assembly).Where(x => x.Project == project).List();
			ProjectUpdater projectUpdater = new ProjectUpdater(namespaces, assemblies, types, properties);

			projectUpdaterAction(projectUpdater);

			SaveUpdates(projectUpdater);

		}

		private void SaveUpdates(ProjectUpdater projectUpdater) {

			using (var transaction = DbSession.BeginTransaction()) {
				//delete everything in the correct order (in case of dependencies)
				foreach (var type in projectUpdater.GetRemovedTypes())
					DbSession.Delete(type);
				foreach (var ns in projectUpdater.GetRemovedNamespaces())
					DbSession.Delete(ns);
				foreach (var ass in projectUpdater.GetRemovedAssemblies())
					DbSession.Delete(ass);

				//save everything
				foreach (var ns in projectUpdater.CurrentNamespaces)
					DbSession.Persist(ns);
				foreach (var ass in projectUpdater.CurrentAssemblies)
					DbSession.Persist(ass);
				foreach (var type in projectUpdater.CurrentTypes)
					DbSession.Persist(type);

				transaction.Commit();
				DbSession.Flush();
			}

		}

	}
}