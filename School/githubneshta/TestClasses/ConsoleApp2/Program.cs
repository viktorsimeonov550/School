using System;
using System.Collections.Generic;
using System.Linq;

internal class Program
{
    static void Main(string[] args)
    {
        List<Employee> employees = new List<Employee>();
        int n = int.Parse(Console.ReadLine());

        for (int i = 0; i < n; i++)
        {
            string[] info = Console.ReadLine().Split(" ");
            string name = info[0];
            double salary = double.Parse(info[1]);
            string department = info[2];
            employees.Add(new Employee { Name = name, Salary = salary, Department = department });
        }

        var departments = employees.Select(employee => employee.Department).Distinct();

        double highestAverage = 0;
        string highAverageDepartment = "";

        foreach (var dep in departments)
        {
            var departmentEmployees = employees.Where(employee => employee.Department == dep);
            double avgSalary = departmentEmployees.Average(employee => employee.Salary);

            if (avgSalary > highestAverage)
            {
                highestAverage = avgSalary;
                highAverageDepartment = dep;
            }
        }

        Console.WriteLine($"Highest Average Salary: {highAverageDepartment}");

        var sorted = employees.Where(employee => employee.Department == highAverageDepartment)
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
