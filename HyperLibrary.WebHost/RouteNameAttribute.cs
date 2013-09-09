using System;

namespace HyperLibrary.WebHost
{
    public class RouteNameAttribute : Attribute
    {
        public string RouteName { get; private set; }

        public RouteNameAttribute(string routeName)
        {
            RouteName = routeName;
        }
    }
}