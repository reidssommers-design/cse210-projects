using System;

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();
        string choice = "";

        while (choice != "5")
        {
            Console.WriteLine();
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");

            choice = Utils.Input("What would you like to do? ");

            if (choice == "1")
            {
                journal.AddEntry();
            }
            else if (choice == "2")
            {
                journal.Display();
            }
            else if (choice == "3")
            {
                string filename = Utils.Input("Filename? ");
                journal.SaveToFile(filename);
            }
            else if (choice == "4")
            {
                string filename = Utils.Input("Filename? ");
                journal.LoadFromFile(filename);
            }
        }
    }
}