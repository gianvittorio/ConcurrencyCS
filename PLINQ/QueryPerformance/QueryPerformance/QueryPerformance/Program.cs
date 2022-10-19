using System;
using System.Diagnostics;
using System.Linq;

namespace QueryPerformance
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("*** Start Main ***");

            // Creating data
            var source = Enumerable.Range(0, 3_000_000);

            // Creating a potential parallel query
            var queryToMeasure = source.AsParallel().Where(n => n % 3 == 0).Select(n => n);

            Console.WriteLine("Measuring...");

            var sw = Stopwatch.StartNew();

            // For pure query cost, enumerate and do nothing else
            foreach (var n in queryToMeasure)
            {
            }

            sw.Stop();
            Console.WriteLine($"Total query time: {sw.ElapsedMilliseconds}ms");

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }
    }
}