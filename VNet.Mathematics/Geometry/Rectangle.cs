﻿namespace VNet.Mathematics.Geometry
{
    public class Rectangle
    {
        public Rectangle()
        {
        }

        public Rectangle(Point leftTop, Point rightBottom)
        {
            if (rightBottom.Y > leftTop.Y) throw new Exception("Top corner should have higher Y value than bottom.");

            if (leftTop.X > rightBottom.X) throw new Exception("Right corner should have higher X value than left.");

            LeftTop = leftTop;
            RightBottom = rightBottom;
        }

        public Point LeftTop { get; set; }
        public Point RightBottom { get; set; }

        internal double Length =>  Math.Abs(RightBottom.X - LeftTop.X);
        internal double Breadth => Math.Abs(LeftTop.Y - RightBottom.Y);

        internal double Area()
        {
            return Length * Breadth;
        }

        public Polygon ToPolygon()
        {
            var edges = new List<Line>();

            edges.Add(new Line(LeftTop, new Point(RightBottom.X, LeftTop.Y)));
            edges.Add(new Line(new Point(RightBottom.X, LeftTop.Y), RightBottom));
            edges.Add(new Line(RightBottom, new Point(LeftTop.X, RightBottom.Y)));
            edges.Add(new Line(new Point(LeftTop.X, RightBottom.Y), LeftTop));

            return new Polygon(edges);
        }
    }
}
