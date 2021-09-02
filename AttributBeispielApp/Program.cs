using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AttributBeispielApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Uebung 1
            Person schroedi = new Person()
            {
                Firstname = "Meister",
                Lastname = "Schrödinger",
                Email = "schroedinger@bluewin.ch"
            };


            Type personType = typeof(Person);
            Console.WriteLine("Type: " + personType.Name);
            PropertyInfo[] properties = personType.GetProperties();

            foreach (PropertyInfo prop in properties)
            {
                Console.WriteLine($"Eigenschaft: {prop.Name} - Wert: {prop.GetValue(schroedi)} ");
            }

            MethodInfo fullNameMethod = personType.GetMethod("GetFullName");

            Console.WriteLine($"Methode {fullNameMethod?.Name ?? "-"} aufgerufen ergibt: " +
                $"{fullNameMethod?.Invoke(schroedi, null) ?? "-"}");

            // Uebung 2
            GetAuthorInfo(typeof(Person));
        }
        static void GetAuthorInfo(Type t)
        {
            IEnumerable<AuthorAttribute> attr = t.GetCustomAttributes<AuthorAttribute>();
            if (attr != null && attr.Any())
            {
                Console.WriteLine("Klassenautoren:");
                foreach (var a in attr)
                {
                    Console.WriteLine($"Klassenautor: {a.Name}; Version {a.Version}");
                }
            }
            Console.WriteLine("Methodenautoren");
            foreach (var method in t.GetMethods())
            {                
                IEnumerable<AuthorAttribute> attributes =
                    method.GetCustomAttributes<AuthorAttribute>();
                if (attributes != null && attributes.Any())
                {
                    Console.WriteLine($"Autoren der Methode {method.Name}");
                    foreach (var a in attributes)
                    {
                        Console.WriteLine($"Methodenautor: {a.Name}; Version {a.Version}");
                    }
                }
            }
        }
    }
}
