using System;

// I just love this so much. 
public static class Utils
{
    public static string Input(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}