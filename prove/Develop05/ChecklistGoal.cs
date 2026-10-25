using System;

/// <summary>
/// Must be done _target times. Each recording earns points;
/// the recording that hits the target also earns the bonus.
/// </summary>
public class ChecklistGoal : Goal
{
    private int _amountCompleted;
    private int _target;
    private int _bonus;

    // amountCompleted defaults to 0 for new goals; loading passes the saved value.
    public ChecklistGoal(string name, string description, int points, int target, int bonus, int amountCompleted = 0)
        : base(name, description, points)
    {
        _target = target;
        _bonus = bonus;
        _amountCompleted = amountCompleted;
    }

    public override int RecordEvent()
    {
        // TODO: if already complete -> return 0.
        //       else _amountCompleted++; points = GetPoints();
        //       if now complete, points += _bonus. Return points.
        throw new NotImplementedException();
    }

    public override bool IsComplete()
    {
        // TODO: _amountCompleted >= _target
        throw new NotImplementedException();
    }

    public override string GetDetailsString()
    {
        // TODO: base.GetDetailsString() + " -- Currently completed: {_amountCompleted}/{_target}"
        throw new NotImplementedException();
    }

    public override string GetStringRepresentation()
    {
        // TODO: "ChecklistGoal:{name},{description},{points},{_bonus},{_target},{_amountCompleted}"
        throw new NotImplementedException();
    }
}
