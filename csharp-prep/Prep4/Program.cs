using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");
        List<string> numbers = new List<string>();

        string number;

        do
        {
            Console.WriteLine("Enter a number");
            number = Console.ReadLine();

            if (number != "0")
            {
                numbers.Add(number);
            }
        } while (number != "0");

        int total = numbers
            .Select(int.Parse)
            .Sum();

        Console.WriteLine($"Total: {total}");

        double average;
        average = (double)total / numbers.Count;
        Console.WriteLine($"Average: {average}");

        int max = numbers
            .Select(int.Parse)
            .Max();

        Console.WriteLine($"Max: {max}");

    }
}