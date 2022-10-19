using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace ConcurrentQueueStack
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

            // create concurrent queue
            var queue = new ConcurrentQueue<int>();
            queue.Enqueue(10);
            queue.Enqueue(20);

            // display queue contents
            Console.WriteLine($"Current queue: {string.Join(", ", queue.ToArray())}");

            // get first element
            if (queue.TryPeek(out int resultPeek))
            {
                Console.WriteLine($"TryPeek result: {resultPeek}");
            }

            // get and remove first element
            if (queue.TryDequeue(out int resultDequeue))
            {
                Console.WriteLine($"TryPeek result: {resultDequeue}");
            }

            // display queue again
            Console.WriteLine($"Final queue: {string.Join(", ", queue.ToArray())}");


            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }
    }
}