using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;

namespace HyperLibrary.WebHost.Library
{
    public interface IResourceLinker
    {
        /// <summary>
        /// Generates a resource link to a given API controller
        /// </summary>
        /// <typeparam name="TApiController">An API controller</typeparam>
        /// <param name="apiMethod">An expression showing which API action to generate a URI for.</param>
        /// <param name="rel"></param>
        /// <param name="name"></param>
        /// <param name="method"></param>
        /// <returns>A link to the resource</returns>
        Link GetResourceLink<TApiController>(Expression<Action<TApiController>> apiMethod, string rel, string name, HttpMethod method) where TApiController : ApiController;
    }
}