# Eternal Quest - structure

## Files

| File | Job |
|---|---|
| `Program.cs` | Entry point. Creates a `GoalManager`, calls `Start()`. Holds the "exceeding requirements" comment. |
| `GoalManager.cs` | Menu, score, goal list, save/load. The only class that talks to the user. |
| `Goal.cs` | Abstract base: name, description, points + the methods every goal must have. |
| `SimpleGoal.cs` | Complete once. |
| `EternalGoal.cs` | Never completes. |
| `ChecklistGoal.cs` | Complete N times, bonus on the last one. |

## How they connect

```
Program.Main
   |  new / Start()
   v
GoalManager  --- owns --->  List<Goal> _goals     int _score
   |
   |  calls only Goal's methods (never checks the subclass):
   |     RecordEvent()   -> returns points earned
   |     GetDetailsString()
   |     GetStringRepresentation()   (save)
   |  CreateGoalFromString(line)     (load; the one place that picks a subclass)
   v
Goal (abstract)
   ^            ^              ^
SimpleGoal  EternalGoal  ChecklistGoal
```

## Flow of one "record event"

1. User picks option 5 in `GoalManager.Start()`
2. `RecordEvent()` lists names, user picks one
3. `_goals[i].RecordEvent()` runs the subclass's version, returns points
4. `GoalManager` adds the points to `_score`

## Save file format

```
1250                                         <- line 1: score
SimpleGoal:Run a marathon,Finish it,1000,False
EternalGoal:Read scriptures,Daily,100
ChecklistGoal:Temple,Go 10 times,50,500,10,2   <- points,bonus,target,amountCompleted
```

## Requirement -> where it lives

- Inheritance: `SimpleGoal`/`EternalGoal`/`ChecklistGoal` derive from `Goal`
- Polymorphism: `RecordEvent`, `IsComplete`, `GetStringRepresentation` (and `GetDetailsString` in checklist) overridden
- Encapsulation: all fields private; subclasses use protected getters
- Persistence: `SaveGoals` / `LoadGoals` / `CreateGoalFromString`
