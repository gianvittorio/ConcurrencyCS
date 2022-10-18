using System;
using System.Threading;
using System.Threading.Tasks;

namespace ComposingTasks
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

            // Invoking continue with
            ContinueWithOperation();

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }

        private static void ContinueWithOperation()
        {
            // Creating a task
            var t = Task<string>.Run(
                () => LongRunningOperation("ContinueWith", 500)
            );
            
            // Adding continuation
            t.ContinueWith((task1) =>
            {
                Console.WriteLine(task1.Result);
            });
        }

        private static string LongRunningOperation(string s, int sec)
        {
            Thread.Sleep(sec);
            return $"{s} completed";
        }
    }
}