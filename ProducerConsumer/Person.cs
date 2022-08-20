using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public class Person
    {
        /// <summary>
        /// Person ist ein Produzent von Urin
        /// Wenn die Person auf dem WC sitzt, braucht sie Zeit 
        /// und dann ist sie der Konsument
        /// </summary>
        /// <param name="timeOnToilette">Gibt die Zeit an, die die Person auf der Toilette verbringen wird</param>
        /// <param name="waitUntilToilette">Gibt die maximale Zeit an, die die Person aushält, bis sie auf die Toilette muss</param>
        public Person(int timeOnToilette, int waitUntilToilette)
        {
            TimeOnToilette = timeOnToilette;
            WaitUntilToilette = waitUntilToilette;
        }

        public int TimeOnToilette { get; }
        public int WaitUntilToilette { get; }
    }
}
