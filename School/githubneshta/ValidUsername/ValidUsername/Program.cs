using System.Text;

namespace ValidUsername
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string usernames = Console.ReadLine().Split(', ');
            StringBuilder result = new StringBuilder();
            foreach(string username in usernames)
            {
                if(username.Length >= 3 && username.Length <= 16 && !username.Contains('-') && !username.Contains('_') && CheckForNumsLetter(username))
                {
                    result.Append(username);
                }
            }

        }
    }
    string bool CheckForNumsLetter(string str)
    {

    }
}
