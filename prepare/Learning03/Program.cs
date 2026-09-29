using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();

        for (int x = 0; x < 20; x ++)
        {
            int top = random.Next(1, 11);
            int bottom = random.Next(1, 11);

            Fraction fract = new Fraction();
            fract.SetTop(top);
            fract.SetBottom(bottom);

            Console.WriteLine($"Fraction {x + 1:00}: string {fract.GetFractionString(),-5} Number: {fract.GetDecimalValue():0.000}");
        }

    }
}