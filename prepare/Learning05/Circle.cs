public class Circle : Shape
{
    private double _radius;
 
    public Circle(string color, double radius) : base(color)
    {
        _radius = radius;
    }
 
    public override double GetArea()
    {
        // TODO: return the area of a circle (hint: Math.PI)
        return 0;
    }
}