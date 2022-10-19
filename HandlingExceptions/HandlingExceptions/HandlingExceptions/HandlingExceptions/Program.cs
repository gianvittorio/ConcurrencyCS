using System;
using System.Threading.Tasks;

namespace HandlingExceptions
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

            // Creating a task with inner attached child tasks
            var task1 = Task.Factory.StartNew(() =>
            {
                var child1 = Task.Factory.StartNew(() =>
                {
                    var child2 = Task.Factory.StartNew(() =>
                    {
                        // This exception is nested inside 3 AggregateExceptions
                        throw new CustomException("Attached child2 faulted");
                    }, TaskCreationOptions.AttachedToParent);

                    // This exception is nested inside 2 AggregateExceptions
                    throw new CustomException("Attached child1 faulted");
                }, TaskCreationOptions.AttachedToParent);
            });

            try
            {
                task1.Wait();
            }
            catch (AggregateException ae)
            {
                foreach (var e in ae.Flatten().InnerExceptions)
                {
                    if (e is CustomException)
                    {
                        Console.WriteLine(e.Message);
                    }
                    else
                    {
                        throw e;
                    }
                }
            }

            Console.WriteLine("*** End main***");

            Console.ReadKey();
        }
    }

    public class CustomException : Exception
    {
        public CustomException() : base()
        {
        }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}