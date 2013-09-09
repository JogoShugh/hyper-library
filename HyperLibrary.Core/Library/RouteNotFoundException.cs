using System;
using System.Runtime.Serialization;

namespace HyperLibrary.Core.Library
{
    [Serializable]
    public class RouteNotFoundException : Exception
    {
        public RouteNotFoundException(string message) : base(message) { }
        protected RouteNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}