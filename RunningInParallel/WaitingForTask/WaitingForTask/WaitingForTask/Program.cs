using System;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTask
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

            // Creates 3 different tasks
            Task[] tasks = new Task[]
            {
                Task.Factory.StartNew(() => Method1()),
                Task.Factory.StartNew(() => Method2()),
                Task.Factory.StartNew(() => Method3())
            };

            // Blocks main thread
            Task.WaitAll(tasks);
            
            // Continue on this thread...
            
            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static void Method1()
        {
            Thread.Sleep(2_000);
            Console.WriteLine("Method 1");
        }

        private static void Method2()
        {
            Console.WriteLine("Method 2");
        }

        private static void Method3()
        {
            Console.WriteLine("Method 3");
        }
    }
}