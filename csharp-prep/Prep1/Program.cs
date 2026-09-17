using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("WHat is your first name? ");
        string fn = Console.ReadLine();
        
        Console.WriteLine("What is your last name? ");
        string ln = Console.ReadLine();

        Console.WriteLine($"Your name is {ln}, {fn} {ln}!");
    }
}