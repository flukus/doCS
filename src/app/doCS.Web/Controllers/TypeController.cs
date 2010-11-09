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
			//add the basic types
			var viewModel = new TypeView() {
				Id = type.Id,
				Name = NameHelper.GetNameFor(type),
				NamespaceId = type.Namespace.Id,
				NamespaceName = type.Namespace.Name,
				XmlDocumentation = (type.XmlDocumentation != null && type.XmlDocumentation.XmlComments != null) ? type.XmlDocumentation.XmlComments : "",
				AssemblyId = type.Assembly.Id,
				AssemblyName = type.Assembly.Name
			};
			//add the interfaces
			foreach (var interfac in type.Interfaces) {
				viewModel.Interfaces.Add(new TypeViewBase {
					Id = interfac.Id,
					Name = interfac.Name
				});
			}
			//add the inheritence heirarchy
			var baseType = type.BaseType;
			while(baseType != null) {
				TypeViewBase typeViewBase = new TypeViewBase() {
					Id = baseType.Id,
					Name = baseType.Name,
					NamespaceId = baseType.Namespace.Id,
					NamespaceName = baseType.Namespace.Name
				};
				//add base type interfaces
				foreach (var interfac in baseType.Interfaces) {
					typeViewBase.Interfaces.Add(new TypeViewBase {
						Id = interfac.Id,
						Name = interfac.Name
					});
				}
				viewModel.BaseClasses.Add(typeViewBase);
				baseType = baseType.BaseType;
			}
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
