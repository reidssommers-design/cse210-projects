# Scripture Memorizer: Design Document

## Classes

REFERENCE
+ Private fields:
- `_book`, `_chapter`, `_startVerse`, `_endVerse`
+ Constructors:
 `Reference(string book, int chapter, int verse)` for a single verse like "John 3:16". It sets `_endVerse` equal to `_startVerse`.
- `Reference(string book, int chapter, int startVerse, int endVerse)` for a range like "Proverbs 3:5-6".

- `GetDisplayText()` returns "John 3:16" or "Proverbs 3:5-6", depending on whether the verses match.

WORD
+ Private fields: 
- `_text`, `_isHidden`

- `Hide()` and `Show()` change the hidden state.
- `IsHidden()` reports whether the word is hidden.
- `GetDisplayText()` returns the word, or underscores of the same length if it is hidden.

SCRIPTURE
+ Private fields: 
- `_reference` and `_words` (a `List<Word>`).

- `HideRandomWords(int count)` picks only from words that are not already hidden (the stretch goal). If fewer than `count` remain, it hides whatever is left.
- `GetDisplayText()` returns the reference followed by all words, hidden or visible.
- `IsCompletelyHidden()` returns true when every word is hidden.

PROGRAM
- Holds the main loop and all console input and output. It is the only class that talks to the console.

## Relationships
- `Program` uses `Scripture`.
- `Scripture` has one `Reference`.
- `Scripture` has many `Word`s.
- All fields are private. Outside code interacts only through public methods.

## Program Flow
1. Build a `Reference` and a `Scripture`.
2. Loop until `IsCompletelyHidden()` is true:
   - Clear the console and print `GetDisplayText()`.
   - Prompt: "Press Enter to continue or type 'quit' to exit."
   - If the user typed `quit` (case-insensitive), exit the loop.
   - Otherwise call `HideRandomWords(3)`.
3. After the loop, clear the console and show the final scripture once more.

## Pseudo-code
```csharp
Reference reference = new Reference("Proverbs", 3, 5, 6);
Scripture scripture = new Scripture(reference, "Trust in the LORD with all thine heart...");

bool running = true;
while (running && !scripture.IsCompletelyHidden())
{
    Console.Clear();
    Console.WriteLine(scripture.GetDisplayText());
    Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");

    string input = Console.ReadLine() ?? "";

    if (input.Trim().ToLower() == "quit")
        running = false;
    else
        scripture.HideRandomWords(3);
}

Console.Clear();
Console.WriteLine(scripture.GetDisplayText());
```

## Creativity / Exceeding Requirements
- **Scripture library:** keep a list of scriptures and pick one at random each run.
- **Load from file:** read scriptures from a text file instead of hardcoding them.