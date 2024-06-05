using System;

namespace NamingSir
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] text = Console.ReadLine().Split(' ');

            Action<string[]> action = (array) =>
            {
                foreach (var name in array)
                {
                    Console.WriteLine("Sir " + name);
                }
            };

            action(text);
        }
    }
}
