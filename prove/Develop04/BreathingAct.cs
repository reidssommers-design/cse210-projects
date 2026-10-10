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

        int sessions = totalDuration / 5; //floor function from desmos
        if (sessions % 2 == 1)
        {
            sessions--;
        }
        while (sessions > 0)
        {
            sessions--;
            Console.WriteLine("Breath in...");
            ShowCountDown(5);
            sessions--;
            Console.WriteLine("Breath out...");
            ShowCountDown(5);
        }

        DisplayEndingMessage();
    }
}