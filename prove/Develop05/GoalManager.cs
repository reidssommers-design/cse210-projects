using System;
using System.Collections.Generic;

/// <summary>
/// Owns the goal list and the score. Program.cs only calls Start();
/// everything the user sees (menus, prompts) lives here.
/// It talks to goals ONLY through the Goal base class - it never needs
/// to know which subclass it's holding (that's the polymorphism).
/// </summary>
public class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    /// <summary>Main menu loop. Runs until the user picks Quit.</summary>
    public void Start()
    {
        // TODO: loop { DisplayPlayerInfo(); show menu; read choice; switch:
        //   1 CreateGoal   2 ListGoalDetails   3 SaveGoals
        //   4 LoadGoals    5 RecordEvent       6 Quit }
        throw new NotImplementedException();
    }

    public void DisplayPlayerInfo()
    {
        // TODO: "You have {_score} points."
        throw new NotImplementedException();
    }

    /// <summary>Numbered list of names only (used by RecordEvent to pick a goal).</summary>
    public void ListGoalNames()
    {
        // TODO: "1. Name" per goal using goal.GetName()
        throw new NotImplementedException();
    }

    /// <summary>Numbered list with [ ]/[X] and checklist progress.</summary>
    public void ListGoalDetails()
    {
        // TODO: "1. " + goal.GetDetailsString() per goal
        throw new NotImplementedException();
    }

    public void CreateGoal()
    {
        // TODO: ask type (Simple/Eternal/Checklist), then name, description, points,
        //       and for Checklist also target + bonus. Build the right subclass,
        //       add it to _goals.
        throw new NotImplementedException();
    }

    public void RecordEvent()
    {
        // TODO: ListGoalNames(), ask which one, then
        //       int earned = _goals[index].RecordEvent();
        //       _score += earned; print "Congratulations! You have earned {earned} points!"
        //       Guard against bad input / empty list.
        throw new NotImplementedException();
    }

    public void SaveGoals()
    {
        // TODO: ask filename. Line 1 = _score. Then one line per goal:
        //       goal.GetStringRepresentation()
        throw new NotImplementedException();
    }

    public void LoadGoals()
    {
        // TODO: ask filename. First line -> _score. Clear _goals, then for each
        //       remaining line call CreateGoalFromString(line) and add it.
        throw new NotImplementedException();
    }

    /// <summary>
    /// Reverse of GetStringRepresentation(): split on the first ':' to get the
    /// class name, split the rest on ',', build the matching subclass.
    /// </summary>
    private Goal CreateGoalFromString(string line)
    {
        // TODO: switch on type name -> new SimpleGoal(...) / EternalGoal(...) / ChecklistGoal(...)
        //       Remember int.Parse / bool.Parse on the string pieces.
        throw new NotImplementedException();
    }
}
