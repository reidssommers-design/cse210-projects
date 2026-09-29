using System;
using System.Runtime.Serialization;

class Program
{
    static void Main(string[] args)
    {
        Fraction firstFract = new Fraction();
        firstFract.SetTop(5);
        firstFract.SetBottom(4);

        int top = firstFract.GetTop();
        Console.WriteLine(top);
        Console.WriteLine(firstFract.GetBottom());

        Console.WriteLine(firstFract.GetFractionString());
        Console.WriteLine(firstFract.GetDecimalValue());

        Fraction second = new Fraction(6, 7);
        Console.WriteLine(second.GetFractionString());

        second.SetBottom(0);
        Console.WriteLine(second.GetBottom());

    }
}