using System;


public abstract class Goal
{
    private string _shortName;
    private string _description;
    private int _points;

    public Goal(string name, string description, int points)
    {
        _shortName = name;
        _description = description;
        _points = points;
    }

    protected string GetShortName() => _shortName;
    protected string GetDescription() => _description;
    protected int GetPoints() => _points;

    public abstract int RecordEvent();

    public abstract bool IsComplete();


    public abstract string GetStringRepresentation();


    public virtual string GetDetailsString()
    {
        string box = IsComplete() ? "[X]" : "[ ]";
        return $"{box} {_shortName} ({_description})";
    }

    public string GetName() => _shortName;
}
