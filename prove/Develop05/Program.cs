using System;

//   - ProgressGoal: a fourth goal type for working toward one big target
//     (e.g. 500 pages). Each recording asks how much progress was made and
//     earns points per unit, with a bonus when the target is reached.
//     Progress is capped at the target, and it saves/loads like the others.


class Program
{
    static void Main(string[] args)
    {
        GoalManager manager = new GoalManager();
        manager.Start();
    }
}