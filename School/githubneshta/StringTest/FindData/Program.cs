using System;

namespace FindData
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int n = int.Parse(Console.ReadLine());
            string text = Console.ReadLine();

            while (true)
            {

                int firstIndex = text.IndexOf("@") + 1;
                int lastIndex = text.IndexOf("|", firstIndex);
                string name = text.Substring(firstIndex, lastIndex - firstIndex);

                int firstNum = text.IndexOf("#") + 1;
                int lastNum = text.IndexOf("*", firstNum);
                string age = text.Substring(firstNum, lastNum - firstNum);

                Console.WriteLine($"{name} is {age} years old.");

            }
        }
    }
}
