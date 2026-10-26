using System;

public class ProgressGoal : Goal
{
    private int _currentAmount;
    private int _target;
    private int _bonus;

    // Base class _points means "points per unit of progress" for this goal type.
    public ProgressGoal(string name, string description, int pointsPerUnit, int target, int bonus, int currentAmount = 0)
        : base(name, description, pointsPerUnit)
    {
        _target = target;
        _bonus = bonus;
        _currentAmount = currentAmount;
    }

    public override int RecordEvent()
    {
        if (IsComplete())
        {
            return 0;
        }

        int remaining = _target - _currentAmount;
        int amount = AskForAmount(remaining);

        _currentAmount += amount;
        int earned = amount * GetPoints();

        if (IsComplete())
        {
            earned += _bonus;
        }

        return earned;
    }

    public override bool IsComplete()
    {
        return _currentAmount >= _target;
    }

    public override string GetDetailsString()
    {
        return $"{base.GetDetailsString()} -- Progress: {_currentAmount}/{_target}";
    }

    public override string GetStringRepresentation()
    {
        return $"ProgressGoal:{GetShortName()},{GetDescription()},{GetPoints()},{_bonus},{_target},{_currentAmount}";
    }


    private int AskForAmount(int remaining)
    {
        while (true)
        {
            Console.Write($"How much progress did you make? ({remaining} left) ");
            if (int.TryParse(Console.ReadLine(), out int amount) && amount > 0)
            {
                return Math.Min(amount, remaining);
            }
            Console.WriteLine("Please enter a whole number greater than 0.");
        }
    }
}