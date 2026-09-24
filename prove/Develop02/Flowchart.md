# CSE 210 – Journal Program Design
// Again, this was regenerated with AI. I had it ingest my own flowchart and create 
// this file as a better formatted representation of my plans. The AI did NOT change
// my original intent or the plans that I made with my group

## Class Diagrams

### Program
```
Program
-----------------------------------
- _journal: Journal
- _promptGenerator: PromptGenerator
-----------------------------------
+ Main(): void
+ DisplayMenu(): void
+ WriteEntry(): void
+ Display(): void
+ Save(): void
+ Load(): void
```
**Responsibility:** owns the interaction loop — shows the menu, reads user input, calls into `Journal`/`PromptGenerator`, and prints results. Does not manage entry data or file I/O directly.

### PromptGenerator
```
PromptGenerator
-----------------------------------
- _prompts: List[str]
-----------------------------------
+ GetRandPrompt(): str
```
**Responsibility:** owns the fixed list of prompts and returns one at random. Prompts are not consumed/removed — the same prompt may come up more than once.

### Journal
```
Journal
-----------------------------------
- _entries: List[Entry]
-----------------------------------
+ AddEntry(date: str, prompt: str, response: str): void
+ Display(): void
+ SaveToFile(filename: str): void
+ LoadFromFile(filename: str): void
```
**Responsibility:** owns the entry data. `AddEntry` builds an `Entry` from the pieces `Program` collected and appends it to `_entries`. `Display` loops over `_entries` and prints each one (renamed from `DisplayEntry` since it displays *all* entries, not one). `SaveToFile`/`LoadFromFile` handle the actual file reading/writing.

### Entry
```
Entry
-----------------------------------
- _date: str
- _prompt: str
- _response: str
-----------------------------------
+ Display(): str
```
**Responsibility:** holds one entry's data and knows how to format itself as a string (date, prompt, response). Does not print — just returns the formatted text so `Journal.Display()` can print it.

---

## Program Flow

```
Main()
  loop:
    DisplayMenu()
    read user choice
    ├─ "Write" ──────► WriteEntry()
    │                     ├─ prompt = _promptGenerator.GetRandPrompt()
    │                     ├─ print prompt, read response from user
    │                     ├─ date = today's date
    │                     └─ _journal.AddEntry(date, prompt, response)
    │
    ├─ "Display" ────► Display()
    │                     └─ _journal.Display()
    │                           └─ for each entry in _entries: print(entry.Display())
    │
    ├─ "Save" ───────► Save()
    │                     └─ _journal.SaveToFile(filename)
    │
    ├─ "Load" ───────► Load()
    │                     └─ _journal.LoadFromFile(filename)
    │
    └─ "Quit" ───────► exit loop
```

**Key boundary:** `Program` handles all console I/O (menus, prompts, reading input, printing). `Journal`, `Entry`, and `PromptGenerator` never call `print()` or `input()` directly — they only return data/strings to `Program` (or to each other). This keeps the data classes testable independent of the console.