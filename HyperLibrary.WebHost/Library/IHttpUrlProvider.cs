using System;

namespace HyperLibrary.WebHost.Library
{
    public interface IHttpUrlProvider
    {
        Uri GetBaseUrl();
        string EncodeUrl(string urlParameter);
    }
}