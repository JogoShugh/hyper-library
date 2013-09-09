using System;

namespace HyperLibrary.Core
{
    public interface IHttpUrlProvider
    {
        Uri GetBaseUrl();
        string EncodeUrl(string urlParameter);
    }
}