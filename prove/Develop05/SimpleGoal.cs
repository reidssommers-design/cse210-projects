using System;

/// <summary>One-and-done goal. Complete once, points once.</summary>
public class SimpleGoal : Goal
{
    private bool _isComplete;

    // isComplete defaults to false for new goals; loading from file passes the saved value.
    public SimpleGoal(string name, string description, int points, bool isComplete = false)
        : base(name, description, points)
    {
        _isComplete = isComplete;
    }

    public override int RecordEvent()
    {
        // TODO: if already complete -> return 0 (don't double-award).
        //       else mark complete and return GetPoints().
        throw new NotImplementedException();
    }

    public override bool IsComplete() => _isComplete;

    public override string GetStringRepresentation()
    {
        // TODO: "SimpleGoal:{name},{description},{points},{_isComplete}"
        throw new NotImplementedException();
    }
}
