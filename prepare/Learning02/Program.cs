using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._company = "Microsoft";
        job1._jobTitle = Input("Title? ");
        job1._startYear = Input("Start year? ");
        job1._endYear = Input("End year? ");

        Job job2 = new Job();
        job2._company = "Apple";

        job1.Display();
    }

    static string Input(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}