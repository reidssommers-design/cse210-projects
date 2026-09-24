using System;

public class Entry
{
    public string _date = "";
    public string _prompt = "";
    public string _response = "";

    public Entry()
    {
    }

    public void Display()
    {
        Console.WriteLine(_date);
        Console.WriteLine($"Prompt:   {_prompt}");
        Console.WriteLine($"Response: {_response}\n");
    }

    // Commas become &comma to preserve csv integrity. 
    // Idk how else to do this other than like character by character parsing?
    // Even then i would have to add some special marker and that would show up in the raw .csv file :(

    public string ToCsvLine()
    {
        string safePrompt = _prompt.Replace(",", "&comma;");
        string safeResponse = _response.Replace(",", "&comma;");
        return $"{_date},{safePrompt},{safeResponse}";
    }

    // Rebuilds an Entry from one saved line, swapping "&comma;" back into a real comma.
    public static Entry FromCsvLine(string line)
    {
        string[] parts = line.Split(',');

        Entry entry = new Entry();
        entry._date = parts[0];
        entry._prompt = parts[1].Replace("&comma;", ",");      // good thing the amount of commas isn't confusing there
        entry._response = parts[2].Replace("&comma;", ",");

        return entry;
    }
}