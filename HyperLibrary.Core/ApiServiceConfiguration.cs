using Autofac;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using HyperLibrary.Core.Commands;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Queries;


namespace HyperLibrary.Core
{
    public class ApiServiceConfiguration : IApiApplication
    {
        private readonly HttpConfiguration _configuration;

        public ApiServiceConfiguration(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure()
        {
            ConfigureRoutes();
            ConfigureDependencies();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterHttpRequestMessage(_configuration);
            builder.Register(c => _configuration.Routes).As<HttpRouteCollection>();
            builder.RegisterType<InMemoryBookRepository>().As<IInMemoryBookRepository>().InstancePerApiRequest();
            builder.RegisterType<InMemoryFineRepository>().As<IInMemoryFineRepository>().InstancePerApiRequest();
            builder.RegisterType<GetBookQueryHandler>();
            builder.RegisterType<GetFinesQueryHandler>();
            builder.RegisterType<PayFinesCommandHandler>();
            builder.RegisterType<AllBooksQueryHandler>();
            builder.RegisterType<AddBookCommandHandler>();
            builder.RegisterType<DeleteBookCommandHandler>();
            builder.RegisterType<AllCheckedInBooksQueryHandler>();
            builder.RegisterType<CheckInCommandHandler>();
            builder.RegisterType<AllCheckedOutBooksQueryHandler>();
            builder.RegisterType<CheckOutCommandHandler>();
            builder.RegisterType<BookResourceMapper>();
            builder.RegisterType<GetLibraryRootHandler>();
            builder.RegisterType<HttpUrlProvider>().As<IHttpUrlProvider>();
            builder.RegisterType<ResourceLinker>().As<IResourceLinker>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            _configuration.DependencyResolver = resolver;
        }

        private void ConfigureRoutes()
        {
            _configuration.Routes.MapHttpRoute(
                name: "RootApi",
                routeTemplate: "api",
                defaults: new {controller = "Root", id = RouteParameter.Optional}
                );

            _configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}