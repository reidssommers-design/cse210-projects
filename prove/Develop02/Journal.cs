using System;
using System.Collections.Generic;
using System.IO;

public class Journal
{
    public List<Entry> _entries = new List<Entry>();

    private PromptGenerator _promptGenerator = new PromptGenerator();

    public void AddEntry()
    {
        Entry entry = new Entry();
        entry._date = DateTime.Now.ToString("yyyy-MM-dd");  //Yea we fancy with it

        entry._prompt = _promptGenerator.GetRandomPrompt();
        Console.WriteLine(entry._prompt);
        
        entry._response = Utils.Input("> ");

        _entries.Add(entry);
    }

    public void Display()
    {
        foreach (Entry entry in _entries)
        {
            entry.Display();
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (Entry entry in _entries)
            {
                writer.WriteLine(entry.ToCsvLine());
            }
        }

        Console.WriteLine($"Journal saved to {filename}.");
    }

    public void LoadFromFile(string filename)
    {
        _entries = new List<Entry>();

        string[] lines = File.ReadAllLines(filename);

        foreach (string line in lines)
        {
            _entries.Add(Entry.FromCsvLine(line));
        }

        Console.WriteLine($"Journal loaded from {filename}.");
    }
}