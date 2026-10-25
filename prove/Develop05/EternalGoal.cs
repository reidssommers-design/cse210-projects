using System;

/// <summary>Never completes. Every recording earns the same points.</summary>
public class EternalGoal : Goal
{
    public EternalGoal(string name, string description, int points)
        : base(name, description, points)
    {
    }

    public override int RecordEvent()
    {
        // TODO: return GetPoints(). (No state to change.)
        throw new NotImplementedException();
    }

    public override bool IsComplete() => false;

    public override string GetStringRepresentation()
    {
        // TODO: "EternalGoal:{name},{description},{points}"
        throw new NotImplementedException();
    }
}
