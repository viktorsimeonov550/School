using System.Text;

namespace TextFilter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] banList = Console.ReadLine().Split(", ");
            StringBuilder text = new StringBuilder(Console.ReadLine());
          
                foreach (string ban in banList)
                {
                    while(text.ToString().Contains(ban))
                    {
                        text.Replace(ban, new string('*', ban.Length));
                    }
                }
            Console.WriteLine(text.ToString());
        }
    }
}
