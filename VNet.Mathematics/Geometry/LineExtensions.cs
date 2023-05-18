namespace VNet.Mathematics.Geometry
{
    public static class LineExtensions
    {
        public static bool Intersects(this Line lineA, Line lineB, int precision = 5)
        {
            return LineIntersection.Find(lineA, lineB, precision) != null;
        }

        public static Point Intersection(this Line lineA, Line lineB, int precision = 5)
        {
            return LineIntersection.Find(lineA, lineB, precision);
        }

        internal static bool Intersects(this Line lineA, Line lineB, double tolerance)
        {
            return LineIntersection.FindIntersection(lineA, lineB, tolerance) != null;
        }

        internal static Point Intersection(this Line lineA, Line lineB, double tolerance)
        {
            return LineIntersection.FindIntersection(lineA, lineB, tolerance);
        }
    }
}