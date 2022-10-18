using System;
using System.Threading;
using System.Threading.Tasks;

namespace ReturningValueFromTask
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

            // Represents async code
            Run();

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static async void Run()
        {
            // Creating new task
            var t1 = Task<string>.Run(() =>
            {
                Thread.Sleep(2_000);
                return "t1 executed";
            });
            
            // Grabbing task result
            var taskResult = await t1;
            Console.WriteLine(taskResult);
        }
    }
}