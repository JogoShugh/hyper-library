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
        /// <typeparam name="TController">An API controller</typeparam>
        /// <param name="apiMethod">An expression showing which API action to generate a URI for.</param>
        /// <param name="rel">The rel of the link</param>
        /// <param name="name">The name or title of the link</param>
        /// <param name="httpMethod">What Http verb to use</param>
        /// <returns>A link to the resource</returns>
        Link GetResourceLink<TController>(Expression<Action<TController>> apiMethod, string rel, string name, HttpMethod httpMethod) where TController : ApiController;
    }
}