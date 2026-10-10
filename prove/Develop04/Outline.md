# Mindfulness Program: Design Document

| File | Class | Purpose |
|---|---|---|
| `Activity.cs` | `Activity` (base) | Shared name, description, duration, start/end messages, spinner, countdown |
| `BreathingActivity.cs` | `BreathingActivity : Activity` | Breathe in / breathe out loop |
| `ReflectionActivity.cs` | `ReflectionActivity : Activity` | Random prompt, then random reflection questions |
| `ListingActivity.cs` | `ListingActivity : Activity` | Random prompt, countdown, user types items |
| `Outline.md` | none | This file |

`Program.cs` is edited (menu only), not added.

## Classes

ACTIVITY (base class)
+ Private fields:
- `_name`, `_description`, `_duration`

+ Constructor:
- `Activity(string name, string description)`

+ Public methods:
- `DisplayStartingMessage()` shows the name and description, asks "How long, in seconds, would you like for your session?", stores the answer in `_duration`, then prints "Get ready..." and calls `ShowSpinner(3)`.
- `DisplayEndingMessage()` prints "Well done!", calls `ShowSpinner(3)`, then prints "You have completed another {_duration} seconds of the {_name} Activity." and calls `ShowSpinner(3)`.
- `ShowSpinner(int seconds)` cycles through `|`, `/`, `-`, `\` using `Thread.Sleep` and `\b` to overwrite the previous character.
- `ShowCountDown(int seconds)` prints `5 4 3 2 1`, one number per second, erasing each with `\b`.
- `GetDuration()` returns `_duration`. This lets subclasses read it without making the field non-private.
- `Run()` is the one method each activity must supply (see below).

BREATHINGACTIVITY : Activity
+ No extra fields.
- Constructor passes the name "Breathing" and the description to `base(...)`.
- `Run()`:
  1. `DisplayStartingMessage()`
  2. Loop until the elapsed time reaches `GetDuration()`: print "Breathe in...", `ShowCountDown(4)`, print "Now breathe out...", `ShowCountDown(6)`.
  3. `DisplayEndingMessage()`

REFLECTIONACTIVITY : Activity
+ Private fields:
- `_prompts` (`List<string>`, the 4 prompts from the assignment)
- `_questions` (`List<string>`, the 9 questions from the assignment)

- Constructor passes the name "Reflection" and the description to `base(...)`.
- `GetRandomPrompt()` and `GetRandomQuestion()` each return one random item from their list.
- `DisplayPrompt()` prints the prompt, waits for Enter, so the user can think.
- `DisplayQuestions()` loops until the duration is up: print a random question, `ShowSpinner(10)`.
- `Run()`: `DisplayStartingMessage()`, `DisplayPrompt()`, `DisplayQuestions()`, `DisplayEndingMessage()`.

LISTINGACTIVITY : Activity
+ Private fields:
- `_prompts` (`List<string>`, the 5 prompts from the assignment)
- `_count` (`int`, number of items entered)

- Constructor passes the name "Listing" and the description to `base(...)`.
- `GetRandomPrompt()` returns one random prompt.
- `GetListFromUser()` loops until the duration is up. It reads a line with `Console.ReadLine()` and adds 1 to `_count` for each non-empty entry.
- `Run()`: `DisplayStartingMessage()`, show the prompt, "You may begin in: " then `ShowCountDown(5)`, `GetListFromUser()`, print "You listed {_count} items!", `DisplayEndingMessage()`.

PROGRAM
- Holds the menu loop. It creates the activity the user picks and calls `Run()`. It does not know how any activity works internally.

## Relationships
- `BreathingActivity`, `ReflectionActivity`, and `ListingActivity` each inherit from `Activity`.
- `Program` uses all three, but only through the `Activity` type.
- All fields are private. Subclasses reach shared data only through public methods.
- Shared behavior (messages, spinner, countdown, duration) lives only in `Activity`, never copied into a subclass.

## Program Flow
1. Show the menu:
   1. Start breathing activity
   2. Start reflection activity
   3. Start listing activity
   4. Quit
2. Read the choice. Build the matching activity and call `Run()`.
3. When `Run()` returns, go back to the menu. Exit on 4.

## Pseudo-code
```csharp
// Program.cs
bool running = true;
while (running)
{
    Console.Clear();
    Console.WriteLine("Menu Options:");
    Console.WriteLine("  1. Start breathing activity");
    Console.WriteLine("  2. Start reflection activity");
    Console.WriteLine("  3. Start listing activity");
    Console.WriteLine("  4. Quit");
    Console.Write("Select a choice from the menu: ");

    string choice = Console.ReadLine() ?? "";
    Activity activity = null;

    if (choice == "1") activity = new BreathingActivity();
    else if (choice == "2") activity = new ReflectionActivity();
    else if (choice == "3") activity = new ListingActivity();
    else if (choice == "4") running = false;

    if (activity != null)
        activity.Run();
}
```

```csharp
// Timing loop pattern shared by all three Run() methods
DateTime endTime = DateTime.Now.AddSeconds(GetDuration());
while (DateTime.Now < endTime)
{
    // do one round of the activity
}
```

## Things to Watch For
- **Duration input:** use `int.TryParse` so a typo does not crash the program.
- **Clearing the console:** call `Console.Clear()` at the start of each activity so the output stays clean.
- **Listing timing:** `Console.ReadLine()` blocks, so the time check only happens after the user presses Enter. That is fine for this assignment.
- **Backspace trick:** `\b` only works if you print `"\b \b"` to erase a character. Test the spinner and countdown early.
- **Abstract base:** you can mark `Activity` as `abstract` and `Run()` as `public abstract void Run()` so the compiler forces each subclass to provide it.

## Creativity / Exceeding Requirements
Document whichever of these you build in a comment at the top of `Program.cs`.
- **No repeats:** keep a shuffled copy of the prompts and questions so every one is used before any repeats.
- **Activity log:** add an `ActivityLog` class that counts how many times each activity ran and saves it to a file, loading it on startup.
- **Better breathing animation:** make the "Breathe in..." text grow one character at a time, then shrink on the way out.
- **Fourth activity:** for example a Gratitude or Scripture Pondering activity that inherits from `Activity` the same way.