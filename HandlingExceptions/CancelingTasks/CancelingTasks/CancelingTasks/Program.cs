using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace CancelingTasks
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("*** Start main***");

            // Creating a cancellation token source
            var tokenSource = new CancellationTokenSource();
            var token = tokenSource.Token;

            // Store references to the tasks so that we can ait on them and observe their status after cancellation
            Task t;
            var tasks = new ConcurrentBag<Task>();

            Console.WriteLine("Press any key to begin...");
            Console.ReadKey(true);
            Console.WriteLine("To terminate the example, press 'c' to cancel and exit");
            Console.WriteLine();

            // Request cancellation of a single task when the token source is cancelled
            t = Task.Factory.StartNew(() => LongRunningOperation(1, token), token);
            Console.WriteLine($"Task {t.Id} executing");
            tasks.Add(t);

            // Request cancellation
            char ch = Console.ReadKey().KeyChar;
            if (ch == 'c')
            {
                tokenSource.Cancel();
                Console.WriteLine("Task cancellation requested");
            }

            try
            {
                Task.WaitAll(tasks.ToArray());
            }
            catch (AggregateException e)
            {
                Console.WriteLine("Aggregate Exception thrown");
                // display information for each inner exception
                foreach (var v in e.InnerExceptions)
                {
                    if (v is TaskCanceledException)
                    {
                        Console.WriteLine("TaskCancelledException thrown");
                    }
                    else
                    {
                        Console.WriteLine($"Exception {v.GetType().Name} thrown");
                    }

                    Console.WriteLine();
                }
            }
            finally
            {
                tokenSource.Dispose();
            }

            // Display status of all tasks
            foreach (var task in tasks)
            {
                Console.WriteLine($"Task {task.Id} status is now {task.Status}");
            }

            Console.WriteLine("*** End main***");

            Console.ReadKey();
        }

        private static void LongRunningOperation(int taskNum, CancellationToken token)
        {
            // Was cancellation already requested
            if (token.IsCancellationRequested)
            {
                Console.WriteLine($"Task {taskNum} was cancelled before it got started");
                token.ThrowIfCancellationRequested();
            }

            int maxIterations = 100;
            for (int i = 0; i <= maxIterations; i++)
            {
                // Do some work here
                Thread.Sleep(100);

                if (token.IsCancellationRequested)
                {
                    Console.WriteLine($"Task {taskNum} was cancelled");
                    token.ThrowIfCancellationRequested();
                }
            }
        }
    }
}