using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskVsFacade
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
            var stopWatch = Stopwatch.StartNew();
            
            // Creating new Task Completion Source
            var tcs = new TaskCompletionSource<bool>();
            Console.WriteLine($"Start: {stopWatch.ElapsedMilliseconds}ms");
            
            // Wrapping Task Completion Source
            var fnf = Task.Delay(2000)
                .ContinueWith(task => tcs.SetResult(true));
            Console.WriteLine($"Waiting: {stopWatch.ElapsedMilliseconds}ms");
            
            // Awaiting Task Completion Source
            await tcs.Task;
            Console.WriteLine($"Done: {stopWatch.ElapsedMilliseconds}ms");
            stopWatch.Stop();
        }
    }
}