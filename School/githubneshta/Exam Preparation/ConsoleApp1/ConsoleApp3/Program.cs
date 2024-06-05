namespace OffRoadChallenge
{

    class Program
    {
        static void Main()
        {
            List<int> fuel = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            List<int> index = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            List<int> neededFuel = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            List<int> reachedAltitudes = new List<int>();

            while (fuel.Any() && index.Any() && neededFuel.Any())
            {
                int currentFuel = fuel.Last();
                int currentConsumptionIndex = index.First();
                int currentAltitudeFuel = neededFuel.First();

                int result = currentFuel - currentConsumptionIndex;

                if (result >= currentAltitudeFuel)
                {
                    reachedAltitudes.Add(currentAltitudeFuel);
                    fuel.RemoveAt(fuel.Count - 1);
                    index.RemoveAt(0);
                    neededFuel.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }

            if (reachedAltitudes.Any())
            {
                Console.WriteLine(string.Join(Environment.NewLine, reachedAltitudes.Select(a => $"John has reached: Altitude {a}")));

                if (neededFuel.Any())
                {
                    Console.WriteLine("John failed to reach the top.");
                    Console.WriteLine($"Reached altitudes: {string.Join(", ", reachedAltitudes)}");
                }
                else
                {
                    Console.WriteLine("John has reached all the altitudes and managed to reach the top!");
                }
            }
            else
            {
                Console.WriteLine("John failed to reach the top.");
                Console.WriteLine("John didn't reach any altitude.");
            }
        }
    }
}