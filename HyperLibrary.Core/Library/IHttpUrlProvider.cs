using System;

namespace HyperLibrary.Core.Library
{
    public interface IHttpUrlProvider
    {
        Uri GetBaseUrl();
        string EncodeUrl(string urlParameter);
    }
}