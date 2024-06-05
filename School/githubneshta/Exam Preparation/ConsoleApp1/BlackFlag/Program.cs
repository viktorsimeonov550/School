using System;

class Program
{
    static void Main()
    {
        int days = int.Parse(Console.ReadLine());
        int daily = int.Parse(Console.ReadLine());
        double expected = double.Parse(Console.ReadLine());

        double total = 0;

        for (int day = 1; day <= days; day++)
        {
            total += daily;

            if (day % 3 == 0)
            {
                total += 0.5 * daily;
            }

            if (day % 5 == 0)
            {
                total -= 0.3 * total;
            }
        }

        if (total >= expected)
        {
            Console.WriteLine($"Ahoy! {total:f2} plunder gained.");
        }
        else
        {
            double percentageCollected = (total / expected) * 100;
            Console.WriteLine($"Collected only {percentageCollected:f2}% of the plunder.");
        }
    }
}