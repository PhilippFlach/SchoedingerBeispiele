using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public static class WCSimulation
    {
        // ein paar Variablen und Objekte um das Programm steuern zu können
        internal static int amountOfToiletts = 4;
        internal static int amountOfPerson = 200;
        /* 
         * BlockingCollection sperrt den Zugriff von einzelnen Theads.
         * Es ist für unseren Anwendungsfall perfekt, da es den Produzenten blockiert,
         * wenn alle Toiletten besetzt sind, aber den oder die 
         * Konsumenten, wenn keiner die Toiletten benutzt.
         * Die Anzahl der verfügbaren Toiletten ergibt auch die Grösse 
         * der BlockingCollection
         */
        
        static readonly BlockingCollection<Person> toiletts = new BlockingCollection<Person>(amountOfToiletts);

        /* Initialisierung für die Simulation der Toilettenzeiten  */
        static readonly Random rnd = new Random(DateTime.Now.Millisecond);
        internal static Person[] InitializePersons()
        {
            Person[] persons = new Person[amountOfPerson];
            for (int i = 0; i < amountOfPerson; i++)
            {
                // Die Personen werden mit zufälligen unterschiedlichen Toilettenzeiten belegt
                persons[i] = new Person(rnd.Next(2, 30), rnd.Next(80, 250))
                {
                    Number = i
                };
            }
            return persons;
        }
        internal static Task InitializeProducer(Person[] persons)
        {
            Task producer = Task.Run(() =>
            {
                /* Wenn die Toiletten alle besetzt sind, so wird der Befüllungs-Thread blockiert
                 * Daher ist es wichtig, dass die Befüllung von einem eigenen Task durchgeführt wird
                 */
                DateTime startTime = DateTime.Now;
                DateTime endTime;
                double elapsedSecondsSinceStartTime;
                for (int i = 0; i < persons.Length; i++)
                {
                    endTime = DateTime.Now;
                    elapsedSecondsSinceStartTime = endTime.Subtract(startTime).TotalSeconds;
                    if (persons[i].WaitUntilToilette < elapsedSecondsSinceStartTime)
                    {
                        // Zu spät. Benutzer muss sich nicht mehr anstellen
                        Console.WriteLine($"Person {persons[i]} musste zu lange warten und hat WC verpasst.");
                    }
                    else
                    {
                        /* Sobald alle Toiletten besetzt sind (die Kapaztität wurde 
                         * für die BlockingCollection im Konstruktor angegeben
                         */
                        toiletts.Add(persons[i]);
                        Console.WriteLine(
                            $"Gegenwärtig {toiletts.Count} Personen auf Toilette.");
                        Console.WriteLine(
                            $"Person {persons[i].Number} ist für {persons[i].TimeOnToilette} s auf dem WC.");
                    }

                }
            });
            return producer;
        }
        internal static void Consume()
        {            
            /* Der Consumer ruft nun die Take Methode auf
             * Dadurch nimmt der Consumer ein Element aus der BlockingCollection
             * um es weiter zu bearbeiten. 
             */
            Person person = toiletts.Take();
            
            while (person != null)
            {
                // Simuliere die Verrichtung der Notdurft
                System.Threading.Thread.Sleep(person.TimeOnToilette * 1000);
                Console.WriteLine($@"Person {person.Number} ist fertig. Thread: {
                    System.Threading.Thread.CurrentThread.ManagedThreadId}");
                /* ManagedThreadId zeigt, dass immer wieder die gleichen Threads verwendet
                 * werden, um dies abzuarbeiten
                 */
                person = toiletts.Take();
             /* Ist die BlockingCollection leer, wird der Thread hier
             *  wieder blockiert, bis wieder mindestens ein Element vorhanden ist
             */
            }
            Console.WriteLine("Niemand ist auf der Toilette");
        }

    }
}
