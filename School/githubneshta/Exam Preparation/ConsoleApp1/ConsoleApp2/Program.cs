using System;

namespace WormsAndHoles
{
    class Program
    {
        static void Main()
        {
            List<int> worms = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();
            List<int> holes = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int count = 0;

            for (int i = worms.Count - 1; i >= 0; i--)
            {
                if (holes.Count == 0)
                {
                    break;
                }

                int currentWorm = worms[i];
                int currentHole = holes[0];

                if (currentWorm == currentHole)
                {
                    count++;
                    worms.RemoveAt(i);
                    holes.RemoveAt(0);
                }
                else
                {
                    holes.RemoveAt(0);
                    currentWorm -= 3;

                    if (currentWorm > 0)
                    {
                        worms[i] = currentWorm;
                    }
                    else
                    {
                        worms.RemoveAt(i);
                    }
                }
            }
            Console.WriteLine($"Matches: {count}");

            if (worms.Count == 0)
            {
                Console.WriteLine("Every worm found a suitable hole!");
            }
            else
            {
                Console.WriteLine($"Worms left: {string.Join(", ", worms)}");
            }
            if (holes.Count == 0)
            {
                Console.WriteLine("Holes left: none");
            }
            else
            {
                Console.WriteLine($"Holes left: {string.Join(", ", holes)}");
            }
        }
    }
}