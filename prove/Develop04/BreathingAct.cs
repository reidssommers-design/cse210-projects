using System;

public class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing Activity", "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.")
    {
        
    }

    public override void Run()
    {
        DisplayStartingMessage();
        int totalDuration = GetDuration();

        int rounded = Math.Max(10, ((totalDuration + 9) / 10) * 10);
        if (rounded != totalDuration)
        {
            Console.WriteLine($"Rounding up to {rounded} seconds so you get every full breath.");
            SetDuration(rounded);
            totalDuration = rounded;
            ShowSpinner(2);
        }

        int sessions = totalDuration / 5;
        while (sessions > 0)
        {
            sessions--;
            Console.WriteLine("Breathe in...");
            ShowCountDown(5);
            sessions--;
            Console.WriteLine("Breathe out...");
            ShowCountDown(5);
        }

        DisplayEndingMessage();
    }
}