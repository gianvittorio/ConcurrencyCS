using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelSearch
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentException(nameof(args));
            }

            Console.WriteLine("*** Start Main ***");

            //IOOperation();

            // Parallel.Invoke(
            //     () => Operation1(),
            //     () => Operation2()
            // );

            var task = Task.Run(() => Operation1());
            Console.WriteLine("This is executed first");
            task.Wait();
            
            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static async void IOOperation()
        {
            Console.WriteLine("*** Start Operation ***");

            var task1 = Delay(5000);
            var task2 = Delay(2500);

            int[] result = await Task.WhenAll(task1, task2);

            Console.WriteLine($"total time: {result.Sum()}");
            Console.WriteLine("*** End Operation ***");
        }

        private static async Task<int> Delay(int ms)
        {
            Console.WriteLine($"Start delay for {ms}");

            await Task.Delay(ms);

            Console.WriteLine($"End delay for {ms}");

            return ms;
        }

        private static void Operation1()
        {
            Console.WriteLine("Operation 1 executed");
        }

        private static void Operation2()
        {
            Console.WriteLine("Operation 2 executed");
        }
    }
}