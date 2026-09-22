using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Resume resume1 = new Resume();
        resume1._name = Utils.Input("Name? ");

        Job job1 = new Job();
        Job job2 = new Job();
        // Job job3 = new Job();

        job1.MakeJob();
        job2.MakeJob();
        // job3.MakeJob();

        resume1._jobs.Add(job1);
        resume1._jobs.Add(job2);
        // resume1._jobs.Add(job3);
        
        resume1.Display();

        Console.WriteLine(resume1._jobs[0]._jobTitle); //Chained command
    }
}