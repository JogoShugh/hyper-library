using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Routing;

namespace HyperLibrary.Core.Library
{
    public class ResourceLinker : IResourceLinker
    {
        private readonly IHttpUrlProvider _httpUrlProvider;
        private readonly HttpRouteCollection _apiRouteTable;

        public ResourceLinker(IHttpUrlProvider httpUrlProvider, HttpRouteCollection apiRouteTable)
        {
            _httpUrlProvider = httpUrlProvider;
            _apiRouteTable = apiRouteTable;
        }

        /// <summary>
        /// Resource linker only supports generating Uris which have a route template which matches the parameters you pass in to
        /// the action.
        /// For example if your action expression is
        /// for type of JobController (controller => controller.Get(jobId))
        /// then the matching route would need to define in it template {jobId} 'api/jobs/{jobId}
        /// Query string generation is not supported.
        /// </summary>
        /// <typeparam name="T">The type of the Api controller you are calling</typeparam>
        /// <param name="method">An expression to generate a url for</param>
        /// <param name="rel">The description of this links relation to current document</param>
        /// <param name="name">The name of the link relation</param>
        /// <param name="meth"> </param>
        /// <returns>A resource link relation</returns>
        public Link GetResourceLink<T>(Expression<Action<T>> method, string rel, string name, HttpMethod meth) where T : ApiController
        {
            string routeNameForAction = ReflectRouteNameForApiAction(method);
            IHttpRoute route = GetRouteForAction(routeNameForAction);
            if (route == null)
            {
                throw new RouteNotFoundException(string.Format("Route for action '{0}' was not found", routeNameForAction));
            }
            var uri = BindTemplate(method, route);
            return new Link{Name = name,Rel = rel, Uri = uri};
        }

        private Uri BindTemplate<T>(Expression<Action<T>> method, IHttpRoute route) where T : ApiController
        {
            IDictionary<string, string> defaults = new Dictionary<string, string> {{"id", null}};
            var uriTemplate = new UriTemplate(route.RouteTemplate, true, defaults);
            var baseUrl = _httpUrlProvider.GetBaseUrl();

            var paramaters = BuildUriTemplateValuesFromExpression(method);
            var uri = uriTemplate.BindByName(baseUrl, paramaters);
            return uri;
        }

        private IHttpRoute GetRouteForAction(string routeNameForAction)
        {
            IHttpRoute httpRoute;
            _apiRouteTable.TryGetValue(routeNameForAction, out httpRoute);
            return httpRoute;
        }

        private string ReflectRouteNameForApiAction<T>(Expression<Action<T>> actionMethod)
        {
            MethodCallExpression methodCallExpression = (MethodCallExpression)actionMethod.Body;
            var methodInfo = methodCallExpression.Method;
            RouteNameAttribute routNameAttribute = methodInfo.GetCustomAttributes(typeof(RouteNameAttribute), true).Cast<RouteNameAttribute>().FirstOrDefault();
            return routNameAttribute != null ? routNameAttribute.RouteName : "DefaultApi";
        }

        private IDictionary<string, string> BuildUriTemplateValuesFromExpression<T>(Expression<Action<T>> actionMethod)
        {
            IDictionary<string, string> result = new Dictionary<string, string>();
            MethodCallExpression callExpression = (MethodCallExpression)actionMethod.Body;

            ParameterInfo[] parameters = callExpression.Method.GetParameters();
            var controllerName = (typeof (T)).Name.Replace("Controller",string.Empty);
            result.Add("controller", controllerName);
            if (parameters.Any())
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    var expression = callExpression.Arguments[i];
                    ConstantExpression constantExpression = expression as ConstantExpression;
                    object value = constantExpression != null ? GetValueFromConstantExpression(constantExpression) : GetCurrentValueFromExpression(expression);
                    value = _httpUrlProvider.EncodeUrl(value.ToString());            
                    result.Add(parameters[i].Name, value.ToString());
                }
            }
            return result;
        }

        private object GetCurrentValueFromExpression(Expression argumentExpression)
        {
            LambdaExpression lambda = Expression.Lambda(argumentExpression);
            var compiledExpression = lambda.Compile();
            return compiledExpression.DynamicInvoke();
        }

        private object GetValueFromConstantExpression(ConstantExpression expression)
        {
            Expression conversion = Expression.Convert(expression, typeof(object));
            var getterLambda = Expression.Lambda<Func<object>>(conversion);
            var getter = getterLambda.Compile();
            return getter();
        }
    }
    public class RouteNameAttribute : Attribute
    {
        public string RouteName { get; private set; }

        public RouteNameAttribute(string routeName)
        {
            RouteName = routeName;
        }
    }
}