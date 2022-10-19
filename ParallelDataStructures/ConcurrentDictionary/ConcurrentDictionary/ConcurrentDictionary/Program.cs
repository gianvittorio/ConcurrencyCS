using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace ConcurrentDictionary
{
    internal class Program
    {
        private static ConcurrentDictionary<string, int> _concurrent = new ConcurrentDictionary<string, int>();

        public static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("*** Start Main ***");

            // creating tasks
            var task1 = Task.Run(() => WriteToDictionary());
            var task2 = Task.Run(() => WriteToDictionary());

            // awaiting for tasks to execute
            Task.WaitAll(task1, task2);

            // looking at average number of values
            Console.WriteLine($"Average: {_concurrent.Values.Average()}");

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static void WriteToDictionary()
        {
            for (int num = 0; num < 999; num++)
            {
                _concurrent.TryAdd(num.ToString(), num);
            }
        }
    }
}