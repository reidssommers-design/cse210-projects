using System;
using System.Collections.Generic;
using System.IO;


public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void Start()
    {
        int choice = 0;

        while (choice != 6)
        {
            Console.WriteLine();
            DisplayPlayerInfo();
            Console.WriteLine();
            Console.WriteLine("Menu Options:");
            Console.WriteLine("  1. Create New Goal");
            Console.WriteLine("  2. List Goals");
            Console.WriteLine("  3. Save Goals");
            Console.WriteLine("  4. Load Goals");
            Console.WriteLine("  5. Record Event");
            Console.WriteLine("  6. Quit");
            choice = ReadInt("Select a choice from the menu: ");

            switch (choice)
            {
                case 1: CreateGoal(); break;
                case 2: ListGoalDetails(); break;
                case 3: SaveGoals(); break;
                case 4: LoadGoals(); break;
                case 5: RecordEvent(); break;
                case 6: Console.WriteLine("Goodbye!"); break;
                default: Console.WriteLine("Please pick a number from 1 to 6."); break;
            }
        }
    }

    public void DisplayPlayerInfo()
    {
        Console.WriteLine($"You have {_score} points.");
    }

    public void ListGoalNames()
    {
        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetName()}");
        }
    }

    public void ListGoalDetails()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals yet. Create one first!");
            return;
        }

        Console.WriteLine("The goals are:");
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetailsString()}");
        }
    }

    public void CreateGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("  1. Simple Goal");
        Console.WriteLine("  2. Eternal Goal");
        Console.WriteLine("  3. Checklist Goal");
        int type = ReadInt("Which type of goal would you like to create? ");

        if (type < 1 || type > 3)
        {
            Console.WriteLine("That's not a goal type. Back to the menu.");
            return;
        }

        string name = ReadText("What is the name of your goal? ");
        string description = ReadText("What is a short description of it? ");
        int points = ReadInt("What is the amount of points associated with this goal? ");

        if (type == 1)
        {
            _goals.Add(new SimpleGoal(name, description, points));
        }
        else if (type == 2)
        {
            _goals.Add(new EternalGoal(name, description, points));
        }
        else
        {
            int target = ReadInt("How many times does this goal need to be accomplished for a bonus? ");
            int bonus = ReadInt("What is the bonus for accomplishing it that many times? ");
            _goals.Add(new ChecklistGoal(name, description, points, target, bonus));
        }

        Console.WriteLine("Goal created!");
    }

    public void RecordEvent()
    {
        if (_goals.Count == 0)
        {
            Console.WriteLine("No goals to record yet. Create one first!");
            return;
        }

        ListGoalNames();
        int choice = ReadInt("Which goal did you accomplish? ");

        if (choice < 1 || choice > _goals.Count)
        {
            Console.WriteLine("That's not a goal on the list.");
            return;
        }

        int earned = _goals[choice - 1].RecordEvent();
        _score += earned;

        if (earned > 0)
        {
            Console.WriteLine($"Congratulations! You have earned {earned} points!");
        }
        else
        {
            Console.WriteLine("That goal is already complete - no points this time.");
        }
        Console.WriteLine($"You now have {_score} points.");
    }

    public void SaveGoals()
    {
        string filename = ReadText("What is the filename for the goal file? ");

        using (StreamWriter writer = new StreamWriter(filename))
        {
            writer.WriteLine(_score);
            foreach (Goal goal in _goals)
            {
                writer.WriteLine(goal.GetStringRepresentation());
            }
        }

        Console.WriteLine($"Saved {_goals.Count} goals to {filename}.");
    }

    public void LoadGoals()
    {
        string filename = ReadText("What is the filename for the goal file? ");

        if (!File.Exists(filename))
        {
            Console.WriteLine($"Couldn't find {filename}.");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(filename);

            int loadedScore = int.Parse(lines[0]);
            List<Goal> loadedGoals = new List<Goal>();

            for (int i = 1; i < lines.Length; i++)
            {
                if (lines[i].Trim() == "")
                {
                    continue;
                }
                loadedGoals.Add(CreateGoalFromString(lines[i]));
            }

            _score = loadedScore;
            _goals = loadedGoals;
            Console.WriteLine($"Loaded {_goals.Count} goals.");
        }
        catch (Exception e) when (e is FormatException || e is IndexOutOfRangeException)
        {
            Console.WriteLine("That file isn't in the expected format, so nothing was loaded.");
        }
    }

    private Goal CreateGoalFromString(string line)
    {
        string[] halves = line.Split(':', 2);
        string type = halves[0];
        string[] parts = halves[1].Split(',');

        switch (type)
        {
            case "SimpleGoal":
                // name, description, points, isComplete
                return new SimpleGoal(parts[0], parts[1], int.Parse(parts[2]), bool.Parse(parts[3]));

            case "EternalGoal":
                // name, description, points
                return new EternalGoal(parts[0], parts[1], int.Parse(parts[2]));

            case "ChecklistGoal":
                // name, description, points, bonus, target, amountCompleted
                return new ChecklistGoal(parts[0], parts[1], int.Parse(parts[2]),
                    int.Parse(parts[4]), int.Parse(parts[3]), int.Parse(parts[5]));

            default:
                throw new FormatException($"Unknown goal type: {type}");
        }
    }

    private int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                return value;
            }
            Console.WriteLine("Please enter a whole number.");
        }
    }

    private string ReadText(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string text = (Console.ReadLine() ?? "").Trim();

            if (text == "")
            {
                Console.WriteLine("This can't be empty.");
            }
            else if (text.Contains(','))
            {
                Console.WriteLine("Please avoid commas - they break the save file.");
            }
            else
            {
                return text;
            }
        }
    }
}
