# Eternal Quest - TODO

Work top to bottom. Each step ends with something you can run and see.

## Step 0 - Project exists
- [ ] Put these files in `Prove/Develop05/` of your course repo
- [ ] `dotnet run` compiles (it will crash with NotImplementedException - that's fine)

## Step 1 - Goal base + SimpleGoal (smallest working slice)
- [ ] `Goal.GetDetailsString()`
- [ ] `SimpleGoal.RecordEvent()`, `GetStringRepresentation()`
- [ ] Temporarily in `Main`: make one SimpleGoal, print details, record, print again -> `[ ]` becomes `[X]`

## Step 2 - Other two goal types
- [ ] `EternalGoal` (2 methods)
- [ ] `ChecklistGoal` (bonus on final completion, no points after complete)
- [ ] Same temp test: checklist target 3 -> confirm bonus lands on the 3rd only

## Step 3 - GoalManager display + create + record
- [ ] `DisplayPlayerInfo`, `ListGoalNames`, `ListGoalDetails`
- [ ] `CreateGoal`
- [ ] `RecordEvent` (adds to `_score`)
- [ ] `Start()` menu loop; remove temp test from `Main`
- [ ] Core requirements 1-7 now done

## Step 4 - Persistence
- [ ] `SaveGoals`
- [ ] `CreateGoalFromString` + `LoadGoals`
- [ ] Round trip: create all 3 types, record some, save, quit, relaunch, load -> identical list and score

## Step 5 - Exceed requirements (needed for 100%)
- [ ] Pick 1-2: NegativeGoal (subtracts points), levels/titles by score, progress-toward-big-goal type
- [ ] Describe them in the comment at the top of `Program.cs`

## Step 6 - Submit
- [ ] Commit + push, verify files show on GitHub
- [ ] I-Learn: repo link + comment describing what you added beyond the core

## Edge cases to check before submitting
- [ ] Record event with zero goals
- [ ] Non-numeric menu input
- [ ] Recording an already-complete Simple/Checklist goal (should earn 0)
- [ ] Commas in a goal name/description break the save format (decide: forbid, or pick a different delimiter like `|`)
