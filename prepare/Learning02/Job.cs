using System;
using System.Collections.Generic;

public class Job
{
    public string _company = "";
    public string _jobTitle = "";
    public string _startYear = "";
    public string _endYear = "";

    public Job()
    {
    }

    public void Display()
    {
        Console.WriteLine($"{_jobTitle} ({_company}) {_startYear}-{_endYear}");
    }

    public void MakeJob()
    {
        _company = Utils.Input("Company? ");
        _jobTitle = Utils.Input("Title? ");
        _startYear = Utils.Input("Start? ");
        _endYear = Utils.Input("End? ");
    }
}