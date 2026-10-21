using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Step 1: test each class on its own first (GetColor / GetArea)
        Square s1 = new Square("Red", 3);
        Console.WriteLine(s1.GetColor());
        Console.WriteLine(s1.GetArea());

        // Step 2: one list holds every kind of shape
        List<Shape> shapes = new List<Shape>();

        shapes.Add(new Square("Red", 3));
        shapes.Add(new Rectangle("Blue", 4, 5));
        shapes.Add(new Circle("Green", 2));

        // Step 3: polymorphism
        foreach (Shape shape in shapes)
        {
            string color = shape.GetColor();
            double area = shape.GetArea();

            Console.WriteLine($"The {color} shape has an area of {area}.");
        }
    }
}