using System;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What is your grade score?");
        string scoreStr = Console.ReadLine();

        int score = int.Parse(scoreStr);
        string letter = null;

        if (score >= 90)
        {
            // Console.WriteLine("A");
            letter = "A";
        }
        else if (score >= 80)
        {
            // Console.WriteLine("B");
            letter = "B";
        }
        else if (score >= 70)
        {
            // Console.WriteLine("C");
            letter = "C";
        }
        else if (score >= 60)
        {
            // Console.WriteLine("D");
            letter = "D";
        }
        else
        {
            // Console.WriteLine("F");
            letter = "F";
        }
        Console.WriteLine($"Your letter grade is {letter}");

        if (!(score >= 70))
        {
            Console.WriteLine("You Failed! Try Again Next Time");
        }
        else
        {
            Console.WriteLine("You Passed!");
        }
    }
}