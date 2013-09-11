using System;
using System.Collections.Generic;

namespace HyperLibrary.LinkClient
{
    public class Library
    {
        public ResourceLink Self { get; set; }
        public IList<ResourceLink> Links { get; set; } 
    }

    public enum LibaryOptions
    {
        ExploreLibrary = 1,
        ViewCheckedOutBooks = 2,
        ReturnBooks = 3,
        LeaveLibary = 4,
    }

    public class ResourceLink
    {
        public Uri Uri { get; set; }
        public string Rel { get; set; }
        public string Name { get; set; }
        public string Method { get; set; }

        public Type GetTypeForRepresentation()
        {
            if (Rel == "Books")
            {
                return typeof (Books);
            }
            return typeof (object);
        }
    }


}
