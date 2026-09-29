using System;
using System.ComponentModel.DataAnnotations;

public class Fraction
{
    private int _num;
    private int _den;

    public Fraction() {_num = 1; _den = 1;}
    public Fraction(int noom){_num = noom; _den = 1;}
    public Fraction(int noom, int denm) {_num = noom; _den = denm;}

    public int GetNum() {return _num;}
    public void SetNum(int num) {_num = num;}
    public int GetDen() {return _den;}
    public void SetDen(int den) {_den = den;}

    public string GetFractionString()
    {
        return($"{_num}/{_den}");
    }
    public double GetDecimalVal()
    {
        return (double)_num/_den;
    }

    
    public void SetVar()
    {
        int noom = int.Parse(Utils.Input("Set X "));
        int denm = int.Parse(Utils.Input("Set y: "));

        _num = noom;
        _den = denm;
        return;
    }
}