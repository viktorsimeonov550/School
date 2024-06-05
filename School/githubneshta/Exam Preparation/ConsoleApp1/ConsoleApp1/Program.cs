namespace MealPlan
{
    class Program
    {
        static void Main()
        {
            List<string> meals = Console.ReadLine()
                .Split()
                .ToList();
            List<int> cals = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            int count = 0;

            while (meals.Count > 0 && cals.Count > 0)
            {
                int MealCals = GetCals(meals[0]);
                int DailyCals = cals[0];

                if (MealCals <= DailyCals)
                {
                    meals.RemoveAt(0);
                    cals[0] -= MealCals;

                    if (cals[0] == 0)
                    {
                        cals.RemoveAt(0);
                    }
                }
                else
                {
                    meals[0] = SubCals(meals[0], DailyCals);
                    cals.RemoveAt(0);
                }

                count++;
            }

            if (meals.Count == 0)
            {
                Console.WriteLine($"John had {count} meals.");
                Console.WriteLine($"For the next few days, he can eat {string.Join(", ", cals)} calories.");
            }
            else
            {
                Console.WriteLine($"John ate enough, he had {count} meals.");
                Console.WriteLine($"Meals left: {string.Join(" ", meals)}.");
            }
        }

        static int GetCals(string meal)
        {
            switch (meal)
            {
                case "salad": return 350;
                case "soup": return 490;
                case "pasta": return 680;
                case "steak": return 790;
                default: return 0;
            }
        }

        static string SubCals(string meal, int calories)
        {
            int mealCalories = GetCals(meal);
            int remainingCalories = mealCalories - calories;

            if (remainingCalories <= 0)
            {
                return "none";
            }

            return $"{meal.Substring(0, meal.Length - 1)} {remainingCalories}";
        }
    }
}