public class Shape
{
    private string _color;

    public Shape(string color)
    {
        _color = color;
    }

    public string GetColor()
    {
        return _color;
    }

    public void SetColor(string color)
    {
        _color = color;
    }

    // Derived classes override this. The area is never stored; it is computed on demand.
    public virtual double GetArea()
    {
        // A generic shape has no dimensions, so there is no meaningful area.
        // Every derived class overrides this with its own formula.
        return 0;
    }
}