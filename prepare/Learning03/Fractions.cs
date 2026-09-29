using System;

public class Fraction
{
    private int _num;   // numerator. I'm not going to call it 'top'
    private int _den;   // denominator. Again, won't call it 'bottom'

    public Fraction() 
    {
        _num = 1; 
        _den = 1;
    }
    public Fraction(int top)
    {
        _num = top; 
        _den = 1;
    }
    public Fraction(int top, int bottom) 
    {
        _num = top; 
        SetBottom(bottom);
    }

    public int GetTop() {return _num;}
    public int GetBottom() {return _den;}
    public void SetTop(int num) 
    {  
        if (num.ToString() == "infinity")
        {
            Console.WriteLine("Cheater Cheater. You can't even do that.");
        }
        else
        {
            _num = num;
        }
    }
    public void SetBottom(int den) 
    {   
        if (den == 0)
        {
            Console.WriteLine("Error, Denominator cannot be 0");
            _den = 1;
            return;
        }
        else {
            _den = den;
        }
    }

    public string GetFractionString()
    {
        return($"{_num}/{_den}");
    }
    public double GetDecimalValue()
    {
        return (double)_num/_den;
    }

    
}