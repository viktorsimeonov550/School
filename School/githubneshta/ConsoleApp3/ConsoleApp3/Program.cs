using System;
using System.Linq;

namespace FunctionalProgramingTestSecond
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split(' ');

            Func<string, bool> func = x => (names.Length <= n);
            foreach (var name in names)
            {
                Console.WriteLine(func(name));
            }

        }
    }
}

