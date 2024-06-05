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
            string firstName = info[0];
            string lastName = info[1];
            double salary = double.Parse(info[2]);
            employees.Add(new Employee(firstName, lastName, salary));
        }

        employees = employees.OrderByDescending(e => e.Salary).ToList();

        foreach (var employee in employees)
        {
            Console.WriteLine(employee);
        }
    }
}
public class Employee
{
    public string firstname;
    public string lastname;
    public double salary;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }

    public Employee(string firstName, string lastName, double salary)
    {
        FirstName = firstName;
        LastName = lastName;
        Salary = salary;
    }

    public override string ToString()
    {
        return $"{FirstName} {LastName}: {Salary:F2}";
    }
}
