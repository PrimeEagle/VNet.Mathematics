using VNet.System.Extensions;

namespace VNet.Mathematics.Geometry
{
    public class LineIntersection
    {
        public static Point Find(Line lineA, Line lineB, int precision = 5)
        {
            var tolerance = Math.Round(Math.Pow(0.1, precision), precision);
            return FindIntersection(lineA, lineB, tolerance);
        }

        internal static Point FindIntersection(Line lineA, Line lineB, double tolerance)
        {
            if (lineA == lineB) throw new ArgumentException("Both lines are the same.");

            if (lineA.Left.X.CompareTo(lineB.Left.X) > 0)
            {
                (lineA, lineB) = (lineB, lineA);
            }
            else if (lineA.Left.X.CompareTo(lineB.Left.X) == 0)
            {
                if (lineA.Left.Y.CompareTo(lineB.Left.Y) > 0)
                {
                    (lineA, lineB) = (lineB, lineA);
                }
            }

            double x1 = lineA.Left.X, y1 = lineA.Left.Y;
            double x2 = lineA.Right.X, y2 = lineA.Right.Y;

            double x3 = lineB.Left.X, y3 = lineB.Left.Y;
            double x4 = lineB.Right.X, y4 = lineB.Right.Y;

            if (x1 == x2 && x3 == x4 && x1 == x3)
            {
                var firstIntersection = new Point(x3, y3);

                if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                    IsInsideLine(lineB, firstIntersection, tolerance))
                    return new Point(x3, y3);
            }

            if (y1 == y2 && y3 == y4 && y1 == y3)
            {
                var firstIntersection = new Point(x3, y3);

                if (IsInsideLine(lineA, firstIntersection, tolerance) &&
                    IsInsideLine(lineB, firstIntersection, tolerance))
                    return new Point(x3, y3);
            }

            if (x1 == x2 && x3 == x4) return null;

            if (y1 == y2 && y3 == y4) return null;

            double x, y;

            if (Math.Abs(x1 - x2) < tolerance)
            {
                var m2 = (y4 - y3) / (x4 - x3);
                var c2 = -m2 * x3 + y3;

                x = x1;
                y = c2 + m2 * x1;
            }
            else if (Math.Abs(x3 - x4) < tolerance)
            {
                var m1 = (y2 - y1) / (x2 - x1);
                var c1 = -m1 * x1 + y1;

                x = x3;
                y = c1 + m1 * x3;
            }
            else
            {
                var m1 = (y2 - y1) / (x2 - x1);
                var c1 = -m1 * x1 + y1;

                var m2 = (y4 - y3) / (x4 - x3);
                var c2 = -m2 * x3 + y3;

                x = (c1 - c2) / (m2 - m1);
                y = c2 + m2 * x;

                if (!(Math.Abs(-m1 * x + y - c1) < tolerance
                      && Math.Abs(-m2 * x + y - c2) < tolerance))
                    return null;
            }

            var result = new Point(x, y);

            if (IsInsideLine(lineA, result, tolerance) &&
                IsInsideLine(lineB, result, tolerance))
                return result;

            return null;
        }

        private static bool IsInsideLine(Line line, Point p, double tolerance)
        {
            double x = p.X, y = p.Y;

            var leftX = line.Left.X;
            var leftY = line.Left.Y;

            var rightX = line.Right.X;
            var rightY = line.Right.Y;

            return (x.IsGreaterThanOrEqual(leftX, tolerance) && x.IsLessThanOrEqual(rightX, tolerance)
                    || x.IsGreaterThanOrEqual(rightX, tolerance) && x.IsLessThanOrEqual(leftX, tolerance))
                   && (y.IsGreaterThanOrEqual(leftY, tolerance) && y.IsLessThanOrEqual(rightY, tolerance)
                       || y.IsGreaterThanOrEqual(rightY, tolerance) && y.IsLessThanOrEqual(leftY, tolerance));
        }
    }
}