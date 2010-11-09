using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using doCS.Models;
using doCS.Web.Models;
using doCS.Web.Helpers;
using doCS.Extractor;

namespace doCS.Web.Controllers {
	public class ProjectController : Controller {

		private readonly ISession DbSession;
		private readonly IProjectContext ProjectContext;
		private readonly IExtractorHelper ExtractorHelper;

		public ProjectController(ISession session, IProjectContext projectContext, IExtractorHelper extractor) {
			DbSession = session;
			ProjectContext = projectContext;
			ExtractorHelper = extractor;
		}

		/// <summary>
		/// Provides a project overview. No id is needed because a project route is assumed.
		/// </summary>
		/// <returns></returns>
		public new ActionResult View() {
			Project project = ProjectContext.CurrentProject;
			return View(project);
		}

		public ActionResult List() {
			IList<Project> projects = DbSession.CreateCriteria<Project>().List<Project>();
			List<ProjectListItem> viewModel = new List<ProjectListItem>();
			foreach (var project in projects) {
				viewModel.Add(new ProjectListItem {
					ProjectId = project.Id,
					ProjectName = project.Name
				});
			}
			return View(viewModel);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit(Guid? id) {
			var viewModel = new ProjectEdit();
			if (id.HasValue) {
				var cs = DbSession.Get<ProjectSettings>(id.Value);
				viewModel.Id = cs.Id;
				viewModel.IncludedAssemblies = cs.IncludedAssemblies;
				viewModel.IncludedXmlFiles = cs.IncludedXmlFiles;
				viewModel.Name = cs.Project.Name;
			}

			return View(viewModel);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(ProjectEdit projectEdit) {

			if (ModelState.IsValid) {
				using (var transaction = DbSession.BeginTransaction()) {
					ProjectSettings projectSettings = null;
					if (projectEdit.Id.HasValue)
						projectSettings = DbSession.Get<ProjectSettings>(projectEdit.Id.Value);
					else
						projectSettings = new ProjectSettings() { Project = new Project() };

					projectSettings.IncludedAssemblies = projectEdit.IncludedAssemblies ?? "";
					projectSettings.IncludedXmlFiles = projectEdit.IncludedXmlFiles ?? "";
					projectSettings.Project.Name = projectEdit.Name ?? "";
					projectSettings.Project.Version = "";

					DbSession.Persist(projectSettings);
					transaction.Commit();
					return RedirectToAction("List");
				}
			}

			return View(projectEdit);
		}

		public ActionResult Extract(Guid id) {

			ProjectSettings projectSettings = DbSession.Get<ProjectSettings>(id);

			ExtractorHelper.Extract(projectSettings);
			
			return RedirectToAction("List", "Project");
		}

	}
}
