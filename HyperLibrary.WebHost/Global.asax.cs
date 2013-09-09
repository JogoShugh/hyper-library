using System;
using System.Collections.Generic;
using Autofac;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac.Integration.WebApi;
using HyperLibrary.WebHost.Library;

namespace HyperLibrary.WebHost
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => GlobalConfiguration.Configuration.Routes);
            builder.RegisterType<InMemoryBookRepository>().As<IInMemoryBookRepository>().InstancePerApiRequest();
            builder.RegisterType<GetBookQueryHandler>().InstancePerApiRequest();
            builder.RegisterType<AllBooksQueryHandler>().InstancePerApiRequest();
            builder.RegisterType<AddBookCommandHandler>().InstancePerApiRequest();
            builder.RegisterType<DeleteBookCommandHandler>().InstancePerApiRequest();
            builder.RegisterType<BookResourceMapper>().InstancePerApiRequest();
            builder.RegisterType<HttpUrlProvider>().As<IHttpUrlProvider>().InstancePerApiRequest();
            builder.RegisterType<ResourceLinker>().As<IResourceLinker>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}