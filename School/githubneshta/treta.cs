using System;

namespace CalculationsWithCharacters
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string[] text = Console.ReadLine().Split(' ');
            string firstIndex = text[0];
            string secondIndex = text[1];
            int output = 0;

            int minimumValue = Math.Min(firstIndex.Length, secondIndex.Length);

            for (int i = 0; i < minimumValue; i++)
            {
                if (firstIndex.Length == secondIndex.Length)
                {
                    output += firstIndex[i] * secondIndex[i];
                }
                else
                {
                    output += firstIndex[i] + secondIndex[i];
                }                   
            }

            for (int j = minimumValue; j < firstIndex.Length; j++)
            {
                output += firstIndex[j];
            }

            for (int k = minimumValue; k < secondIndex.Length; k++)
            {
                output += secondIndex[k];
            }

            Console.WriteLine(output);
        }
    }
}