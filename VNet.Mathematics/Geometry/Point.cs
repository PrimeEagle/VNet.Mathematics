﻿namespace VNet.Mathematics.Geometry
{
    public class Point
    {
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; }
        public double Y { get; }

        public override string ToString()
        {
            return X.ToString("F") + " " + Y.ToString("F");
        }

        public Point Clone()
        {
            return new Point(X, Y);
        }
    }
}
