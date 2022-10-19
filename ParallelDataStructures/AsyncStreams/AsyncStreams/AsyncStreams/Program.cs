using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncStreams
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Press any key when ready");
            Console.ReadLine();
            foreach (var item in GetData())
            {
                Console.WriteLine(item);
            }
        }
        
        static IEnumerable<string> GetData()
        {
            yield return "data A";
            yield return "data B";
        }
    }
}