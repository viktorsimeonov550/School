using System;
using System.Linq;

namespace FunctionalProgrammingExam
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            string[] names = Console.ReadLine().Split(' ');

            Func<string, bool> func = x => x.Select(c => (int)c).Sum() >= n;

            string result = names.FirstOrDefault(func);

            if (result != null)
            {
                Console.WriteLine(result);
            }
        }

    }
}
