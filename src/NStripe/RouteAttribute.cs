﻿using System;

namespace NStripe
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    internal class RouteAttribute : Attribute
    {
        public RouteAttribute(string path)
            : this(path, null)
        {
        }

        public RouteAttribute(string path, string verbs)
        {
            this.Path = path;
            this.Verbs = verbs;
        }

        public string Path { get; set; }
        public string Verbs { get; set; }
    }
}
