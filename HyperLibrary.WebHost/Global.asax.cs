using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using HyperLibrary.Core;
using HyperLibrary.Core.LibraryModel;

namespace HyperLibrary.WebHost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        private static readonly ApiServiceConfiguration WebApiConfig = new ApiServiceConfiguration(GlobalConfiguration.Configuration);

        protected void Application_Start()
        {
            WebApiConfig.Configure();
            var builder = new ContainerBuilder();
            builder.RegisterType<InMemoryBookRepository>().As<IInMemoryBookRepository>().InstancePerApiRequest();
            builder.RegisterControllers(typeof(WebApiApplication).Assembly);
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}