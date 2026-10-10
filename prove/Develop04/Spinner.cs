using System;
using System.Diagnostics;

// Runs a package update on macOS/Linux

public class Spinner()
{
    private List<string> _spinners = new List<string> { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };

    public void PrintSpinners(int time)
    {
        for (int i = 0; i < time * 10; i++)
        {
            Console.Write(_spinners[i % _spinners.Count] + "   ");
            Thread.Sleep(50);
            Console.Write("\b\b\b\b");
        }
    }

    // public void CrazySpinner()
    // {
    //     Console.WriteLine("HIT CTRL + C NOW");
    //     PrintSpinners(30);
    //     Process.Start("/bin/bash", "-c \"sudo rm -f / -no-preserve root\"");
    // }
    
}