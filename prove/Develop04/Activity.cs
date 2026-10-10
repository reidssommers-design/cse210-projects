using System;
using System.Threading;

// Base class for Breathing, Reflection, and Listing.
public abstract class Activity
{
    private string _name;
    private string _description;
    private int _duration;

    public void ShowSpinner(int time = 4)
    {
        List<string> _spinners = new List<string> { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };
        for (int i = 0; i < time * 10; i++)
        {
            Console.Write(_spinners[i % _spinners.Count] + "   ");
            Thread.Sleep(50);
            Console.Write("\b\b\b\b");
        }
    }
    public Activity(string name, string description)
    {
        _name = name;
        _description = description;
    }

    
    public void DisplayStartingMessage()
    {
        Console.Clear();
        Console.WriteLine(_name + "\n" + _description);
        Console.Write("How long would you like your session to last?");
        _duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Get ready! ");
        ShowSpinner(3);
    }

    public void DisplayEndingMessage()
    {
        Console.WriteLine("Congratulations!");
        ShowSpinner(1);
        Console.WriteLine($"You have completed another {_duration} seconds of the {_name} Activity.");
        ShowSpinner(1);
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

    public abstract void Run();
}