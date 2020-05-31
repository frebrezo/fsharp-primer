using System;

namespace CSharpTypeExamples
{
    public interface IPerimeter
    {
        double GetPerimeter();
    }

    public class SquarePerimeter : IPerimeter
    {
        public double S { get; }

        public SquarePerimeter(double s)
        {
            S = s;
        }

        public double GetPerimeter() =>
            2.0 * S;
    }

    public class RectanglePerimeter : IPerimeter
    {
        public double L { get; }
        public double W { get; }

        public RectanglePerimeter(double l, double w)
        {
            L = l;
            W = w;
        }

        public double GetPerimeter() =>
            2.0 * L + 2.0 * W;
    }

    public class TrianglePerimeter : IPerimeter
    {
        public double A { get; }
        public double B { get; }
        public double C { get; }

        public TrianglePerimeter(double a, double b, double c)
        {
            A = a;
            B = b;
            C = c;
        }

        public double GetPerimeter() =>
            A + B + C;
    }

    public class RightTrianglePerimeter : IPerimeter
    {
        public double A { get; }
        public double B { get; }

        public RightTrianglePerimeter(double a, double b)
        {
            A = a;
            B = b;
        }

        public double GetPerimeter() =>
            A + B + Math.Sqrt(Math.Pow(A, 2.0) + Math.Pow(B, 2.0));
    }

    public class CirclePerimeter : IPerimeter
    {
        public double R { get; }

        public CirclePerimeter(double r)
        {
            R = r;
        }

        public double GetPerimeter() =>
            2.0 * Math.PI * R;
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IPerimeter perimeter = null;
            double result = 0.0;

            perimeter = new SquarePerimeter(5.0);
            result = perimeter.GetPerimeter();
            Console.WriteLine(result);

            perimeter = new RectanglePerimeter(5.0, 2.0);
            result = perimeter.GetPerimeter();
            Console.WriteLine(result);

            perimeter = new TrianglePerimeter(5.0, 2.0, 10.0);
            result = perimeter.GetPerimeter();
            Console.WriteLine(result);

            perimeter = new RightTrianglePerimeter(5.0, 2.0);
            result = perimeter.GetPerimeter();
            Console.WriteLine(result);

            perimeter = new CirclePerimeter(5.0);
            result = perimeter.GetPerimeter();
            Console.WriteLine(result);
        }
    }
}
