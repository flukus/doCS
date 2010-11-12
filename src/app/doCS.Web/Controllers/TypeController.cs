using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using doCS.Web.Helpers;
using doCS.Web.Models;
using doCS.Models;

namespace doCS.Web.Controllers {
	public class TypeController : Controller {


		private readonly ISession DbSession;
		private readonly IProjectContext ProjectContext;

		public TypeController(ISession dbSession, IProjectContext projectContext) {
			DbSession = dbSession;
			ProjectContext = projectContext;
		}

		public ActionResult View(Guid id) {
			//TODO: This method is too long. Refactor!
			var type = DbSession.Get<doCS.Models.Type>(id);
			var viewModel = AutoMapper.Mapper.Map<doCS.Models.Type, TypeView>(type);
			return View(viewModel);
		}

		public ActionResult List() {
			var query = DbSession.QueryOver<doCS.Models.Type>()
				.JoinQueryOver<doCS.Models.Namespace>(x=>x.Namespace).Where(x=>x.Project == ProjectContext.CurrentProject)
				.List<doCS.Models.Type>();
			query.OrderBy(x => x.Name);

			var viewResult = new List<TypeListItem>();
			foreach (var type in query.ToList<doCS.Models.Type>()) {
				viewResult.Add(new TypeListItem() {
					Id = type.Id,
					Name = type.Name,
					NamespaceId = type.Namespace.Id,
					NamespaceName = type.Namespace.Name
				});
			}

			return View(viewResult);
		}

	}
}
