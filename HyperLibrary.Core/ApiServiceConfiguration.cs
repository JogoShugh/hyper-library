using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using Autofac;
using System.Reflection;
using System.Web.Http;
using Autofac.Integration.WebApi;
using HyperLibrary.Core.Commands;
using HyperLibrary.Core.LibraryModel;
using HyperLibrary.Core.Queries;
using HyperLibrary.Core.Resources;
using Newtonsoft.Json;


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
          //  _configuration.Formatters.Add(new JsonHalMediaTypeFormatter());
            ConfigureRoutes();
            ConfigureDependencies();
        }

        private void ConfigureDependencies()
        {
            var builder = new ContainerBuilder();
            builder.RegisterHttpRequestMessage(_configuration);
            builder.Register(c => _configuration.Routes).As<HttpRouteCollection>();
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

    //public class JsonHalMediaTypeFormatter : JsonMediaTypeFormatter
    //{
    //   readonly ResourceListConverter resourceListConverter = new ResourceListConverter();
    //   readonly ResourceConverter resourceConverter = new ResourceConverter();
    //    readonly LinksConverter linksConverter = new LinksConverter();

    //    public JsonHalMediaTypeFormatter()
    //    {
    //        SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/hal+json"));
    //        SerializerSettings.Converters.Add(linksConverter);
    //        SerializerSettings.Converters.Add(resourceListConverter);
    //        SerializerSettings.Converters.Add(resourceConverter);
    //    }

    //    public override bool CanReadType(Type type)
    //    {
    //        return typeof(LinkableResource).IsAssignableFrom(type);
    //    }

    //    public override bool CanWriteType(Type type)
    //    {
    //        return typeof(LinkableResource).IsAssignableFrom(type);
    //    }
    //}

    //public class ResourceConverter : JsonConverter
    //{
    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {

    //        serializer.Converters.Remove(this);
    //        serializer.Serialize(writer, value);
    //        serializer.Converters.Add(this);
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
    //                                    JsonSerializer serializer)
    //    {
    //        return reader.Value;
    //    }

    //    public override bool CanConvert(Type objectType)
    //    {
    //        return IsResource(objectType) && !IsResourceList(objectType);
    //    }

    //    static bool IsResourceList(Type objectType)
    //    {
    //        return typeof(IRepresentationList).IsAssignableFrom(objectType);
    //    }

    //    static bool IsResource(Type objectType)
    //    {
    //        return typeof(LinkableResource).IsAssignableFrom(objectType);
    //    }
    //}

    //public class ResourceListConverter : JsonConverter
    //{
    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var list = (IRepresentationList)value;
    //        foreach (LinkableResource halResource in list)
    //        {
    //            serializer.Serialize(writer, halResource);
    //        }

    //        writer.WriteEndArray();
    //        writer.WriteEndObject();

    //        var listType = list.GetType();
    //        var propertyInfos = typeof(RepresentationList<>).GetProperties().Select(p => p.Name);
    //        foreach (var property in listType.GetProperties().Where(p => !propertyInfos.Contains(p.Name)))
    //        {
    //            writer.WritePropertyName(property.Name.ToLower());
    //            serializer.Serialize(writer, property.GetValue(value, null));
    //        }

    //        writer.WriteEndObject();
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        return reader.Value;
    //    }

    //    public override bool CanConvert(Type objectType)
    //    {
    //        return IsResource(objectType) && IsResourceList(objectType);
    //    }

    //    static bool IsResourceList(Type objectType)
    //    {
    //        return typeof(IRepresentationList).IsAssignableFrom(objectType);
    //    }

    //    static bool IsResource(Type objectType)
    //    {
    //        return typeof(Representation).IsAssignableFrom(objectType);
    //    }
    //}

    //public class LinksConverter : JsonConverter
    //{
    //    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    //    {
    //        var links = (IList<Link>)value;
    //        writer.WriteStartObject();

    //        foreach (var link in links)
    //        {
    //            writer.WritePropertyName(link.Rel);
    //            writer.WriteStartObject();
    //            writer.WritePropertyName("href");
    //            writer.WriteValue(link.Href);

    //            writer.WriteEndObject();
    //        }
    //        writer.WriteEndObject();
    //    }

    //    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    //    {
    //        return reader.Value;
    //    }

    //    public override bool CanConvert(Type objectType)
    //    {
    //        return typeof(IList<Link>).IsAssignableFrom(objectType);
    //    }
    //}

}