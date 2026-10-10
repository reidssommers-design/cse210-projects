using System;

// Goal: have the user list as many good things as they can in one area until time runs out.
public class ListingActivity : Activity
{
    private List<string> _prompts;
    private int _count;

    // Goal: set the name/description and fill _prompts.
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

    // Goal: return one random prompt from _prompts.
    private string GetRandomPrompt()
    {
        int randomIndex = Random.Shared.Next(_prompts.Count);
        return _prompts[randomIndex];
    }

    // Goal: show the prompt, then count down 5 seconds so the user can think before listing.
    private void DisplayPrompt()
    {
        Console.WriteLine(GetRandomPrompt());
        Console.WriteLine("You will have 5 seconds to think before you have to type :)");
        ShowCountDown(5);
    }

    // Goal: until the duration is up, read what the user types and add 1 to _count for each non-empty entry.
    private void GetListFromUser()
    {
        DateTime endTime = DateTime.Now.AddSeconds(GetDuration());

        while (DateTime.Now < endTime)
        {
            if (Console.ReadLine() != "")
            {
                _count++;
            }
        }
    }

    // Goal: start message, prompt, collect the list, show how many items were entered, end message.
    public override void Run()
    {
    DisplayStartingMessage();
    DisplayPrompt();
    GetListFromUser();
    Console.WriteLine($"You found {_count} things to be greatful for in {GetDuration()} seconds!");
    DisplayEndingMessage();
    }
}