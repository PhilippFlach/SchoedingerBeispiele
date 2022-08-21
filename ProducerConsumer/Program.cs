using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    class Program
    {
        static void Main()
        {
            Person[] persons = WCSimulation.InitializePersons();
            Task producer = WCSimulation.InitializeProducer(persons);
            /* Für alle 4 WC müssen 4 Abarbeitungstasks erstellt werden
             * amountOfToiletts = 4
             */
            Task[] consumer = new Task[WCSimulation.amountOfToiletts];
            for (int i = 0; i < consumer.Length; i++)
            {
                /* Jeder Task bekommt die gleiche Aufgabe.
                 * Die BlockingCollection sperrt die einzelnen Tasks und
                 * synchronisiert diese entsprechend ganz ohne lock-Befehl
                 * Dieser Synchronisationsmechanismus ist auch der grosse
                 * Vorteil der BlockingCollection Klasse.
                 */
                consumer[i] = Task.Run(() => WCSimulation.Consume());
            }
            List<Task> allTasks = new List<Task>();
            allTasks.AddRange(consumer);
            allTasks.Add(producer);
            Task.WaitAll(allTasks.ToArray());
            Console.WriteLine("Alle WCs wieder frei!");
            Console.ReadLine();
        }
    }
}
