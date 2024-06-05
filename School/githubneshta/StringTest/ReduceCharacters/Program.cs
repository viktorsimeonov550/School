using System;

namespace ReduceCharacters
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string text = Console.ReadLine();
            string output = "";
            string vowels = "aeiouAEIOU";

            foreach (char character in text)
            {
                if (vowels.IndexOf(character) == -1)
                {
                    output += character;
                }
            }
            Console.WriteLine(output);
        }
    }
}