using System;

public class Fraction
{
    private int _num;   // numerator. 
    private int _den;   // denominator. 

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
        _den = 1;
        SetBottom(bottom);
    }

    public int GetTop() {return _num;}
    public int GetBottom() {return _den;}
    public void SetTop(int num) 
    {  
        _num = num;
    }
    public void SetBottom(int den) 
    {   
        if (den == 0)
        {
            Console.WriteLine("ERROR: Denominator cannot be 0");
            return;
        }
        else {
            _den = den;
        }
    }

    public string GetFractionString()
    {
        return $"{_num}/{_den}";
    }
    public double GetDecimalValue()
    {
        return (double)_num / _den;
    }
}

