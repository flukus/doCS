using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.Web;
using Autofac;

namespace doCS.Web.ViewHelpers {

	/// <summary>
	/// This class overrides ViewPage because it acts as a Service Locator.
	/// Since many dependencies may be used but probably not all on the same page this treats the IOC container as a ServiceLocator.
	/// Dependencies are prefered over Html Extension methods because extension methods cannot have their own dependencies.
	/// This will probably be needed for the XMLDocumentationHelper because it will need to lookup references etc.
	/// 
	/// All Properties are cached after first being resolved.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class ViewPage<T> : System.Web.Mvc.ViewPage<T> {

		private ILifetimeScope Container;

		protected void Page_PreInit(object sender, EventArgs e) {
			var cpa = (IContainerProviderAccessor)HttpContext.Current.ApplicationInstance;
			Container = cpa.ContainerProvider.RequestLifetime as Autofac.ILifetimeScope;
		}

		private XmlDocumentationHelper _XmlDocumentationHelper;
		public XmlDocumentationHelper XmlDocumentationHelper {
			get {
				if (_XmlDocumentationHelper == null) {
					_XmlDocumentationHelper = Container.Resolve<XmlDocumentationHelper>();
				}
				return _XmlDocumentationHelper;
			}
		}
	}
}