using System;
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
    }
}
