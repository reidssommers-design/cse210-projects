using System;

public class Fraction
{
    private int _noom;
    private int _denm;

    public Fraction()
    {
        _noom = 1;
        _denm = 1;
    }
    public Fraction(int noom)
    {
        _noom = noom;
        _denm = 1;
    }
    public Fraction(int noom, int denm)
    {
        _noom = noom;
        _denm = denm;
    }

    public void ReturnFract()
    {
        Console.WriteLine($"{_noom}/{_denm}");
        return;
    }
    
    public void SetVar()
    {
        int noom = int.Parse(Utils.Input("Set X "));
        int denm = int.Parse(Utils.Input("Set y: "));

        _noom = noom;
        _denm = denm;
        return;
    }
}