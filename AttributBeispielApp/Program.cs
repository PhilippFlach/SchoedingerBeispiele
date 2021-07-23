using System;
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
            //GetAuthorInfo(typeof(Person));
        }
    }
}
