using System;

namespace ManOfWar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> ship1 = Console.ReadLine()
                .Split(">")
                .Select(int.Parse)
                .ToList();
            List<int> ship2 = Console.ReadLine()
                .Split(">")
                .Select(int.Parse)
                .ToList();

            int maxHealth = int.Parse(Console.ReadLine());
            string comm = Console.ReadLine();

            while (comm != "Retire")
            {
                string[] command = comm.Split().ToArray();
                switch (command[0])
                {
                    case "Fire":
                        int fire = int.Parse(command[1]);
                        int damage = int.Parse(command[2]);

                        if (fire >= 0 && fire < ship2.Count)
                        {
                            ship2[fire] -= damage;

                            if (ship2[fire] <= 0)
                            {
                                Console.WriteLine("You won! The enemy ship has sunken.");
                                return;
                            }
                        }
                        break;

                    case "Defend":
                        int defendS = int.Parse(command[1]);
                        int defendE = int.Parse(command[2]);

                        damage = int.Parse(command[3]);

                        if (defendS >= 0 && defendS < ship1.Count &&
                            defendE >= 0 && defendE < ship1.Count &&
                            defendS <= defendE)
                        {
                            for (int i = defendS; i <= defendE; i++)
                            {
                                ship1[i] -= damage;

                                if (ship1[i] <= 0)
                                {
                                    Console.WriteLine("You lost! The pirate ship has sunken.");
                                    return;
                                }
                            }
                        }
                        break;

                    case "Repair":
                        int repair = int.Parse(command[1]);
                        int health = int.Parse(command[2]);

                        if (repair >= 0 && repair < ship1.Count)
                        {
                            ship1[repair] = Math.Min(ship1[repair] + health, maxHealth);
                        }
                        break;

                    case "Status":
                        int count = ship1.Count(s => s < maxHealth * 0.2);
                        Console.WriteLine($"{count} sections need repair.");
                        break;

                }
                comm = Console.ReadLine();
            }
            Console.WriteLine($"Pirate ship status: {ship1.Sum()}");
            Console.WriteLine($"Warship status: {ship2.Sum()}");
        }
    }
}