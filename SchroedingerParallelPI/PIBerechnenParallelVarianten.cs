using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchroedingerParallelPI
{
    public static class PIBerechnenParallelVarianten
    {
        const int NumSteps = 100000000;
        public static double SerialPi()
        {
            double sum = 0.0;
            double step = 1.0 / (double)NumSteps;
            for (int i = 0; i < NumSteps; i++)
            {
                double x = (i + 0.5) * step;
                sum += 4.0 / (1.0 + x * x);

            }
            return step * sum;
        }

        /// <summary>
        /// Diese Variante ist sogar noch schlechter als SerialPi
        /// </summary>
        /// <returns></returns>
        public static double MyParallelPiEinfach()
        {
            double sum = 0.0;
            double step = 1.0 / (double)NumSteps;
            object monitor = new object();
            Parallel.For(0, NumSteps, (i) =>
            {
                double x = (i + 0.5) * step;
                double local = 4.0 / (1.0 + x * x);
                lock (monitor)
                    sum += local;
            });
            return step * sum;
        }

        /// <summary>
        /// Zweitbeste Variante
        /// </summary>
        /// <returns></returns>
        public static double ParallelPi()
        {
            double sum = 0.0;
            double step = 1.0 / (double)NumSteps;
            object monitor = new object();
            // local ist am Anfang 0
            Parallel.For(0, NumSteps, InitializeLocals(), (i, state, local) =>
            {
                // Eigentlicher Schleifeninhalt
                double x = (i + 0.5) * step;
                // Hier steht eigentlich local := local + 4.0 / (1.0 + x * x)
                return local + 4.0 / (1.0 + x * x);
            },
            // das localFinally kann auch weggelassen werden
            localFinally: local => { lock (monitor) { sum += local; } });
            return step * sum;

        }

        /// <summary>
        /// Dient dazu, die Werte für local zu initialisieren
        /// Dazu wird jeweils diese Funktion aufgerufen, die eine andere Funktion zurückgibt
        /// </summary>
        /// <returns></returns>
        private static Func<double> InitializeLocals()
        {
            return () => 0.0;
        }

        /// <summary>
        /// Dies ist die schnellste Variante
        /// </summary>
        /// <returns></returns>
        public static double ParallelPartitionerPi()
        {
            double sum = 0.0;
            double step = 1.0 / (double)NumSteps;
            object locker = new object();
            /* Durch die Partitioner-Klasse werden die Threads
             * einmalig vom Threadpool geladen und dann die grosse Aufgabe 
             * in gleich grosse Teile für jeden Thread geteilt
             * Die Partitionierungsklasse bekommt die zwei Schwellenwerte übergeben und
             * gibt die entsprechenden Tuple-Informationen zurück.
            */
            Parallel.ForEach(Partitioner.Create(0, NumSteps), InitializeLocals(),
                /* Es wird hier nicht ein Laufindex übergeben, 
                 * sondern die vom Partitioner erzeugten Tupel-Informationen.
                 * Darin sind die Indizes enthalten, die der entsprechende Thread
                 * verwenden soll.
                 */
                (range, state, local) =>
                {
                    /* Die Schleife läuft nun vom Startindex des Tuples bis 
                     * zum End-Index des Tuples.                     
                     */
                    for (int i = range.Item1; i < range.Item2; i++)
                    {
                        double x = (i + 0.5) * step;
                        local += 4.0 / (1.0 + x * x);
                    }
                    return local;
                }, local => { lock (locker) sum += local; });
            return step * sum;
        }
    }
}
