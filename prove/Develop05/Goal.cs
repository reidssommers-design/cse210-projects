using System;

/// <summary>
/// Base class for every goal type. Holds what ALL goals share and declares
/// what each type must do differently (abstract = must override).
/// Member variables are private; subclasses get at them through protected getters.
/// </summary>
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

    // Protected getters: subclasses can read, nobody outside can touch.
    protected string GetShortName() => _shortName;
    protected string GetDescription() => _description;
    protected int GetPoints() => _points;

    /// <summary>
    /// Called when the user records an event for this goal.
    /// Updates the goal's own state and RETURNS the points earned
    /// (GoalManager adds that to the score).
    /// </summary>
    public abstract int RecordEvent();

    /// <summary>True if the goal is finished (drives the [X] vs [ ] display).</summary>
    public abstract bool IsComplete();

    /// <summary>
    /// One line for saving to file, e.g. "SimpleGoal:name,desc,points,isComplete".
    /// The text before the first ':' is the class name - GoalManager uses it on load.
    /// </summary>
    public abstract string GetStringRepresentation();

    /// <summary>
    /// "[ ] Name (Description)" or "[X] Name (Description)".
    /// Virtual so ChecklistGoal can extend it with "-- Currently completed: 2/5".
    /// </summary>
    public virtual string GetDetailsString()
    {
        // TODO: build "[X]"/"[ ]" from IsComplete(), then name and description.
        throw new NotImplementedException();
    }

    /// <summary>Just the name, for the numbered "which goal did you accomplish?" list.</summary>
    public string GetName() => _shortName;
}
