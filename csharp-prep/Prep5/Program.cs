using System;
using System.Globalization;

class Program
{
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    static string PromptUserName()
    {
        Console.WriteLine("What is your username? ");
        return Console.ReadLine();
    }

    static int PromptUserNumber()
    {
        Console.WriteLine("What is your favorite number? ");
        return int.Parse(Console.ReadLine());
    }

    static void PromptUserBirthYear(out int birthYear)
    {
        Console.Write("What year were you born? ");
        birthYear = int.Parse(Console.ReadLine());
    }

    static int SquareNumber(int squared)
    {
        squared = squared * squared;
        return squared;
    } 

    static void DisplayResult(string name, int favnum, int square, int year)
    {
        Console.WriteLine(name);
        Console.WriteLine(favnum);
        Console.WriteLine(square);
        Console.WriteLine(year);
        // Console.WriteLine("");

        int currentYear = DateTime.Now.Year; 
        Console.WriteLine($"You will turn {currentYear - year} years old this year.");

    }
    static void Main(string[] args)
    {
        DisplayWelcome();
        string name = PromptUserName();
        int number = PromptUserNumber();
        int square = SquareNumber(number);
        PromptUserBirthYear(out int birthYear);

        DisplayResult(name, number, square, birthYear);
        // Console.WriteLine($"You were born in {birthYear}.");

    }
}