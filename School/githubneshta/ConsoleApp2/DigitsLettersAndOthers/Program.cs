namespace DigitsLettersAndOthers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input  = Console.ReadLine();
            string[] result = new string[3];
            foreach(char x in input)
            {
                if (char.IsDigit(x))
                {
                    result[0] += x;
                }
                else if (char.IsLetter(x))
                {
                    result[1] += x;
                }
                else
                {
                    result[2] += x;
                }
            }
            foreach (string x in result)
            {
                Console.WriteLine(x);
            }
        }
    }
}
