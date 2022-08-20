using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public static class Configuration
    {
        // ein paar Variablen und Objekte um das Programm steuern zu können
        static int amountOfToiletts = 4;
        static int amountOfPerson = 200;
        /* 
         * BlockingCollection sperrt den Zugriff von einzelnen Theads.
         * Es ist für unseren Anwendungsfall perfekt, da es den Produzenten blockiert,
         * wenn alle Toiletten besetzt sind, aber den oder die 
         * Konsumenten, wenn keiner die Toiletten benutzt.
         * Die Anzahl der verfügbaren Toiletten ergibt auch die Grösse 
         * der BlockingCollection
         */
        
        static BlockingCollection<Person> toiletts = new BlockingCollection<Person>(amountOfToiletts);
        /* Für die Simulation
         */
        static Random rnd = new Random(DateTime.Now.Millisecond);
        private static Person[] InitializePersons()
        {
            Person[] persons = new Person[amountOfPerson];
            for (int i = 0; i < amountOfPerson; i++)
            {
                // Die Personen werden mit zufälligen unterschiedlichen Toilettenzeiten belegt
                persons[i] = new Person(rnd.Next(2, 30), rnd.Next(80, 250));
            }
            return persons;
        }

    }
}
