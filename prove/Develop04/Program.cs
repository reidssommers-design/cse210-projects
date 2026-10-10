using System;
using System.ComponentModel.Design;

class Program
{

    public static void PrintMenu()
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("Menu Options");
            Console.WriteLine("  1. Start Breathing Activity");
            Console.WriteLine("  2. Start Reflection Activity");
            Console.WriteLine("  3. Start Listening Activity");
            Console.WriteLine("  4. Quit");
            Console.WriteLine("Select a choice to continue: ");
            int opt = int.Parse(Console.ReadLine());

            switch (opt)
            {
                case 1:
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    running = false;
                    break;
                default:
                    break;
            }
        }
    }
    static void Main(string[] args)
    {
        Spinner spinny = new Spinner();

        PrintMenu();
        spinny.PrintSpinners(10);

    }
}