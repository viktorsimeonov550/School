using System;
using System.Collections.Generic;
using System.Linq;


internal class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>();
        Dictionary<string, List<double>> deps = new Dictionary<string, List<double>>();
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] info = Console.ReadLine().Split(" ");
            string name = info[0];
            double salary = double.Parse(info[1]);
            string department = info[2];
            employees.Add(new Employee { Name = name, Salary = salary, Department = department });

            if (!deps.ContainsKey(department))
            {
                deps[department] = new List<double>();
            }
            deps[department].Add(salary);
        }

        string highAvgDep = deps.OrderByDescending(e => e.Value.Average()).First().Key;

        Console.WriteLine($"Highest Average Salary: {highAvgDep}");

        var sorted = employees.Where(employee => employee.Department == highAvgDep)
                                       .OrderByDescending(employee => employee.Salary);

        foreach (var employee in sorted)
        {
            Console.WriteLine($"{employee.Name} {employee.Salary:f2}");
        }
    }
    class Employee
    {
        public string name;
        public double salary;
        public string department;

        public string Name { get; set; }
        public double Salary { get; set; }
        public string Department { get; set; }
    }
}