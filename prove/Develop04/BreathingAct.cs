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
        // each breath cycle is 5 seconds
        // modulus division
        // repeat and there MUST be one out for each in XD

        //say duration is 20
        // duration / 5 = 4 sessions
        // print(breathin)
        //countdown(littleDur)
        // repeat

        int sessions = totalDuration / 5; //floor function
        if (sessions % 2 == 1)
        {
            // the duration / 5 is an odd amount of in / out
            // set sessions to sessions - 1
            sessions = sessions - 1;
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