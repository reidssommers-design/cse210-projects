using System;

public class ListingActivity : Activity
{
    private List<string> _prompts;
    private int _count;
    private List<string> _unusedPrompts = new List<string>();

    public ListingActivity() : base("Listing Activity", "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.")
    {
        _prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt the Holy Ghost this month?",
            "Who are some of your personal heroes?"
        };
    }

    private string GetRandomPrompt()
    {
        return PickNoRepeat(_prompts, _unusedPrompts);
    }

    private void DisplayPrompt()
    {
        Console.WriteLine(GetRandomPrompt());
        Console.WriteLine("You will have 5 seconds to think before you have to type :)");
        ShowCountDown(5);
    }

    private void GetListFromUser()  
    //DISCLAIMER! I had claude review my original function for this part because it would wait on Console.ReadLine() even if the timer was out.
    //I understand how to write this program and can provide the origanal code:
    //     private void GetListFromUser()
    // {
    //     DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
    //
    //     while (DateTime.Now < endTime)
    //     {
    //         if (Console.ReadLine() != "")
    //         {
    //             _count++;
    //         }
    //     }
    // }

    {
        // throw away anything typed during the 5-second countdown
        while (Console.KeyAvailable)
        {
            Console.ReadKey(true);
        }

        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
        string current = "";

        while (DateTime.Now < endTime)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);   // true = don't echo; we echo it ourselves

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    if (current.Trim() != "")
                    {
                        _count++;
                    }
                    current = "";
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (current.Length > 0)
                    {
                        current = current.Substring(0, current.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(key.KeyChar))
                {
                    current += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
            }
            else
            {
                Thread.Sleep(20);   // short nap so the loop doesn't max out the CPU
            }
        }

        Console.WriteLine();
    }

    public override void Run()
    {
    DisplayStartingMessage();
    DisplayPrompt();
    GetListFromUser();
    Console.WriteLine($"You found {_count} things to be grateful for in {GetDuration()} seconds!");
    DisplayEndingMessage();
    }
}