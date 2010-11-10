using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NHibernate;
using NHibernate.Context;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using Autofac;

namespace doCS.Web {

	public class MvcApplication : System.Web.HttpApplication, IContainerProviderAccessor {

		private static ISessionFactory SessionFactory { get; set; }

		// Provider that holds the application container.
		static IContainerProvider _containerProvider;

		// Instance property that will be used by Autofac HttpModules
		// to resolve and inject dependencies.
		public IContainerProvider ContainerProvider {
			get { return _containerProvider; }
		}

		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default",
				"{project}/{controller}/{action}",
				new { controller = "Project", action = "List", id = UrlParameter.Optional } // Parameter defaults
			);
			routes.MapRoute(
				"ProjectWithIdentifier",
				"{project}/{controller}/{action}/{id}",
				new { controller = "Project", action = "List", id = UrlParameter.Optional, project = UrlParameter.Optional } // Parameter defaults
			);

		}

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);

			var configuration = new NHibernate.Cfg.Configuration();
			configuration.Configure();
			configuration.AddAssembly(typeof(doCS.Models.Project).Assembly);
			SessionFactory = configuration.BuildSessionFactory();

			// Build up your application container and register your dependencies.
			var builder = new ContainerBuilder();
			builder.RegisterControllers(System.Reflection.Assembly.GetExecutingAssembly());
			builder.Register(x => SessionFactory.OpenSession()).As<ISession>().HttpRequestScoped();
			builder.RegisterType<doCS.Web.Helpers.Implimentation.ProjectContext>().As<doCS.Web.Helpers.IProjectContext>().HttpRequestScoped();

			//register extractor helpers
			builder.RegisterType<doCS.Web.Helpers.Implimentation.Extractor.ExtractorHelper>().As<doCS.Web.Helpers.IExtractorHelper>().HttpRequestScoped();
			builder.RegisterType<doCS.Web.Helpers.Implimentation.Extractor.ProjectUpdaterProvider>().InstancePerDependency();

			//register extractor dependencies
			builder.RegisterType<doCS.Extractor.Implementation.Extractor>().As<doCS.Extractor.IExtractor>().HttpRequestScoped();
			builder.RegisterType<doCS.Extractor.Implementation.DllExtractor>();
			builder.RegisterType<doCS.Extractor.Implementation.XmlExtractor>();

			//register view helpers
			builder.RegisterType<doCS.Web.ViewHelpers.XmlDocumentationHelper>();

			_containerProvider = new ContainerProvider(builder.Build());
			ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(ContainerProvider));

		}

		protected void Application_BeginRequest() {
			//start new NHibernate session on each web request
			//var session = SessionFactory.OpenSession();
			//bind session to the thread so all the code can access it using SessionFactory.GetCurrentSession()
			//this relies on "current_session_context_class" property set to "web" in NHibernate.config
			//CurrentSessionContext.Bind(session);
		}

		protected void Application_EndRequest() {
			//unbind from the thread
			//no need to close the session as it is already automatically closed at this point (not sure why)
			//CurrentSessionContext.Unbind(SessionFactory);
		}

	}
}