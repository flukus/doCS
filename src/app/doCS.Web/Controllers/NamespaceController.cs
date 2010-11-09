using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using System.Web.Mvc;
using doCS.Web.Helpers;
using doCS.Web.Models;
using doCS.Models;
using NHibernate.Criterion;

namespace doCS.Web.Controllers {
	public class NamespaceController : Controller {

		private readonly ISession DbSession;
		private readonly IProjectContext ProjectContext;

		public NamespaceController(ISession dbSession, IProjectContext projectContext) {
			DbSession = dbSession;
			ProjectContext = projectContext;
		}

		public ActionResult List(NamespaceSearch search) {
			var namespaces = DbSession.QueryOver<Namespace>()
				.Where(x => x.Project == ProjectContext.CurrentProject)
				.OrderBy(x=>x.Name).Asc
				.List<Namespace>();
			//var namespaces = DbSession.CreateCriteria<Namespace>().List<Namespace>();
			var viewModel = namespaces.Select(x => new NamespaceListItem() { Id = x.Id, Name = x.Name }).ToList();
			return View(viewModel);
		}

		public ActionResult View(Guid id) {
			var ns = DbSession.Get<Namespace>(id);
			var viewModel = new NamespaceView(ns.Id, ns.Name);
			foreach (var type in ns.Types) {
				viewModel.Types.Add(new NamespaceViewType { Id = type.Id, Name = NameHelper.GetNameFor(type) });
			}
			return View(viewModel);
		}

	}
}