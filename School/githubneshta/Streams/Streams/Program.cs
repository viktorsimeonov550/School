namespace Streams
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = @"..\..\..\Files\input.txt";
            string outputFilePath = @"..\..\..\Files\output.txt";

            RewriteFileWithLineNumbers(inputFilePath, outputFilePath);

           
        }
        static void RewriteFileWithLineNumbers(string inputFilePath, string outputFilePath)
        {
            using (var reader = new StreamReader(inputFilePath))
            {
                string line = reader.ReadLine();
                int counter = 1;
                using (var writer = new StreamWriter(outputFilePath))

                    while (line != null)

                    {

                        writer.WriteLine($"{counter}. {line}");
                        line = reader.ReadLine();
                        counter++;

                    }
            }
        }
    }
}
