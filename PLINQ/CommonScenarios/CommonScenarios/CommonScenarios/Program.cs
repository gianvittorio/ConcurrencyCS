using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CommonScenarios
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

            // // Creating enumerable range
            // var source = Enumerable.Range(1, 1_000_000);
            // var stopwatch = new Stopwatch();
            // stopwatch.Start();
            //
            // // Calculating even numbers
            // var evenNumbers = source
            //     .AsParallel()
            //     .WithDegreeOfParallelism(2)
            //     .Where(num => num % 2 == 0)
            //     .Select(num => num);
            // stopwatch.Stop();
            //
            // Console.WriteLine(
            //     $"{evenNumbers.Count()}, even numbers out of {source.Count()}, in: {stopwatch.ElapsedMilliseconds}ms");

            // var source = new List<City>
            // {
            //     new City() {Name = "Amsterdam", Population = 1236},
            //     new City() {Name = "Berlin", Population = 65236},
            //     new City() {Name = "Boston", Population = 68921},
            //     new City() {Name = "Dublin", Population = 736},
            //     new City() {Name = "Lisbon", Population = 1436},
            //     new City() {Name = "London", Population = 41236},
            // };
            //
            // var cityNames = source
            //     .AsParallel()
            //     .AsOrdered()
            //     .Where(city => city.Population > 10_000)
            //     .Select(city => city.Name);
            //
            // foreach (var city in cityNames)
            // {
            //     Console.WriteLine(city);
            // }

            var nums = Enumerable.Range(10, 10_000);
            var query = nums.AsParallel().Where(n => n % 10 == 0).Select(n => n);

            // Process the results and add them to a Concurrent Bag, which safely accepts concurrent
            var cb = new ConcurrentBag<int>();
            query.ForAll(e => cb.Add(e));

            // Output all data from Concurrent Bag to console
            Console.WriteLine(String.Join(", ", cb));

            Console.WriteLine("*** End Main ***");

            Console.ReadKey();
        }
    }

    internal class City
    {
        public City() : this(String.Empty, 0)
        {
        }

        public City(string name, int population)
        {
            Name = name;
            Population = population;
        }

        public string Name { get; set; }
        public int Population { get; set; }
    }
}