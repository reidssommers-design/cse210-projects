using System;
// Full disclosure: AI gave me the idea to write this snippet 
// and provided the below code when i asked why i couldn't do:
// job1._jobTitle = Console.ReadLine(Console.WriteLine("Title? "));
// or a little simpler:
// var = Console.ReadLine(Console.Write("Query: "));
public static class Utils
{
    public static string Input(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}
