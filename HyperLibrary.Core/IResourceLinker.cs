using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Web.Http;
using HyperLibrary.Core.Resources;

namespace HyperLibrary.Core
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
        /// <param name="httpMethod"></param>
        /// <returns>A link to the resource</returns>
        Link GetResourceLink<TApiController>(Expression<Action<TApiController>> apiMethod, string rel, string name, HttpMethod httpMethod) where TApiController : ApiController;
    }
}