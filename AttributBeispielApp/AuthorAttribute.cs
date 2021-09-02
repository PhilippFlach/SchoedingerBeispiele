using System;

namespace AttributBeispielApp
{
    [AttributeUsage(AttributeTargets.Class
        | AttributeTargets.Interface
        | AttributeTargets.Method, AllowMultiple = true)]
    public class AuthorAttribute : Attribute 
    {
        public string Name { get; set; }
        public double Version { get; set; }

        public AuthorAttribute(string name, double version = 1.0)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Version = version;
        }
    }
}