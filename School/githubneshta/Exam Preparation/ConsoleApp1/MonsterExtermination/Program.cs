using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<int> armorValues = Console.ReadLine().Split(',')
            .Select(int.Parse)
            .ToList();
        List<int> strikingImpactValues = Console.ReadLine()
            .Split(',')
            .Select(int.Parse)
            .ToList();

        int killedMonsters = 0;

        while (armorValues.Any() && strikingImpactValues.Any())
        {
            int currentMonsterArmor = armorValues.First();
            int currentStrike = strikingImpactValues.Last();

            if (currentStrike >= currentMonsterArmor)
            {
                armorValues.RemoveAt(0);
                strikingImpactValues.RemoveAt(strikingImpactValues.Count - 1);

                int remainingImpact = currentStrike - currentMonsterArmor;

                if (strikingImpactValues.Any())
                {
                    strikingImpactValues[strikingImpactValues.Count - 1] += remainingImpact;
                }
            }
            else
            {
                armorValues[0] -= currentStrike;
                strikingImpactValues.RemoveAt(strikingImpactValues.Count - 1);
                armorValues.Add(armorValues[0]);
                armorValues.RemoveAt(0);
            }

            killedMonsters++;
        }

        if (armorValues.Any())
        {
            Console.WriteLine("The soldier has been defeated.");
            Console.WriteLine($"Total monsters killed: {killedMonsters - 1}");
        }
        else
        {
            Console.WriteLine("All monsters have been killed!");
            Console.WriteLine($"Total monsters killed: {killedMonsters}");
        }
    }
}