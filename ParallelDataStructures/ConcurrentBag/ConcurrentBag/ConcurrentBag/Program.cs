using System;
using System.Collections.Concurrent;

namespace ConcurrentBag
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

            // creating a concurrent bag and adding values to it
            var bag = new ConcurrentBag<int>();
            bag.Add(1);
            bag.Add(2);
            bag.Add(3);

            // getting last added element
            int result;
            if (bag.TryPeek(out result))
            {
                Console.WriteLine($"TryPeek result: {result}");
            }

            // getting and removing last added element
            if (bag.TryTake(out result))
            {
                Console.WriteLine($"TryTake result: {result}");
            }

            Console.WriteLine($"Final bag content: {string.Join(", ", bag.ToArray())}");

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }
    }
}