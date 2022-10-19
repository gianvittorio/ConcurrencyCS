using System;
using System.Threading.Tasks;

namespace RaceConditions
{
    internal class Program
    {
        private static int counter;

        public static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentException(nameof(args));
            }

            Console.WriteLine("*** Start Main ***");

            var t1 = Task.Factory.StartNew(PrintStar);
            var t2 = t1.ContinueWith(t => PrintPlus());

            Task.WaitAll(new Task[] {t1, t2});

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static void PrintStar()
        {
            for (counter = 0; counter < 5; counter++)
            {
                Console.WriteLine(" * ");
            }
        }

        private static void PrintPlus()
        {
            for (counter = 0; counter < 5; counter++)
            {
                Console.WriteLine(" + ");
            }
        }
    }
}