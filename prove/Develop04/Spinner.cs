using System;

public class Spinner()
{
    private List<string> _spinners = new List<string> { "⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏" };

    public void PrintSpinners()
    {
        for (int i = 0; i < _spinners.Count * 2; i++)
        {
            Console.Write(_spinners[i % _spinners.Count]);
            Thread.Sleep(100);
            Console.Write("\b \b");
        }
    }
}