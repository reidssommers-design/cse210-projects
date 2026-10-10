using System;
using System.Threading;

// Base class for Breathing, Reflection, and Listing.
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;
 
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

   public void ShowSpinner(int seconds = 4)
    {
        List<string> spinners = new List<string> {"⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏"};
        DateTime endTime = DateTime.Now.AddSeconds(seconds);
        int i = 0;

        while (DateTime.Now < endTime)
        {
            Console.Write(spinners[i % spinners.Count] + "   ");
            Thread.Sleep(50);
            Console.Write("\b\b\b\b");
            i++;
        }
    }
    
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine(_name + "\n" + _description);
        int seconds;
        Console.Write("How long would you like your session to last? ");

        while (!int.TryParse(Console.ReadLine(), out seconds) || seconds <= 0)
        {
            Console.Write("Please enter a whole number greater than 0: ");
        }
        _duration = seconds;

        Console.WriteLine("Get ready! ");
        ShowSpinner(2);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("Congratulations!");
        ShowSpinner(2);
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
        ShowSpinner(6);
    }

    public void ShowCountDown(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            Console.Write(i);
            Thread.Sleep(1000);
            Console.Write("\b \b");
        }
    }

    public int GetDuration()
    {
        return _duration;
    }
    protected void SetDuration(int seconds)
    {
        _duration = seconds;
    }

    protected string PickNoRepeat(List<string> source, List<string> remaining)
{
    if (remaining.Count == 0)
    {
        remaining.AddRange(source);
    }

    int index = Random.Shared.Next(remaining.Count);
    string item = remaining[index];
    remaining.RemoveAt(index);
    return item;
}

    public abstract void Run();
}