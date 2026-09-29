using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction fract1 = new Fraction();
        Console.WriteLine(fract1.GetFractionString());
        Console.WriteLine(fract1.GetDecimalValue());

        Fraction fract2 = new Fraction(5);
        Console.WriteLine(fract2.GetFractionString());
        Console.WriteLine(fract2.GetDecimalValue());

        Fraction fract3 = new Fraction(3, 4);
        Console.WriteLine(fract3.GetFractionString());
        Console.WriteLine(fract3.GetDecimalValue());

        Fraction fract4 = new Fraction(1, 3);
        Console.WriteLine(fract4.GetFractionString());
        Console.WriteLine(fract4.GetDecimalValue());

        Console.WriteLine();

        Random random = new Random();
        Fraction fractR = new Fraction();

        for (int x = 0; x < 20; x ++)
        {
            int top = random.Next(1, 11);
            int bottom = random.Next(0, 11);

            fractR.SetTop(top);
            fractR.SetBottom(bottom);

            Console.WriteLine($"Fraction {x + 1:00}: string {fractR.GetFractionString(),-5} Number: {fractR.GetDecimalValue():0.000}");
        }

    }
}