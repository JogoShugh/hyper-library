using Autofac;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using HyperLibrary.Core.Commands;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Queries;

namespace HyperLibrary.Core
{
    public class ApiServiceConfiguration
    {
        private readonly HttpConfiguration _configuration;

        public ApiServiceConfiguration(HttpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure()
        {
            _configuration.MessageHandlers.Add(new CurrentRequestResolver());
            ConfigureRoutes();
            ConfigureDependencies();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.Register(c => _configuration).As<HttpConfiguration>();
            builder.Register(c => _configuration.Routes).As<HttpRouteCollection>();
            builder.Register(c => new CurrentRequestUri()).InstancePerApiRequest();
            builder.RegisterType<InMemoryBookRepository>().As<IInMemoryBookRepository>().InstancePerApiRequest();
            builder.RegisterType<GetBookQueryHandler>();
            builder.RegisterType<AllBooksQueryHandler>();
            builder.RegisterType<AddBookCommandHandler>();
            builder.RegisterType<DeleteBookCommandHandler>();
            builder.RegisterType<AllCheckedInBooksQueryHandler>();
            builder.RegisterType<CheckInCommandHandler>();
            builder.RegisterType<AllCheckedOutBooksQueryHandler>();
            builder.RegisterType<CheckOutCommandHandler>();
            builder.RegisterType<BookResourceMapper>();
            builder.RegisterType<HttpUrlProvider>().As<IHttpUrlProvider>().InstancePerApiRequest();
            builder.RegisterType<ResourceLinker>().As<IResourceLinker>();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            _configuration.DependencyResolver = resolver;
        }

        private void ConfigureRoutes()
        {
            _configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {controller = "Books", id = RouteParameter.Optional}
                );
        }
    }
}