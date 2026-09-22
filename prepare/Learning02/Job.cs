using System;

public class Job
{
    public string _company = "";
    public string _jobTitle = "";
    public int _startYear = 0;
    public int _endYear = 0;

    public Job()
    {
    }

    public void Display()
    {
        Console.WriteLine($"{_company}");
    }
}
class Program
{
    static void Main(string[] args)
    {
        Job job1 = new Job();
        job1._company = "Microsoft";

        Job job2 = new Job();
        job2._company = "Apple";

        job1.Display();
    }
}