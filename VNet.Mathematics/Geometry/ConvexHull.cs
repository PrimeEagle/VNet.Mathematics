namespace VNet.Mathematics.Geometry
{
    public class ConvexHull
    {
        public static List<int[]> Find(List<int[]> points)
        {
            var currentPointIndex = FindLeftMostPoint(points);
            var startingPointIndex = currentPointIndex;

            var result = new List<int[]>();

            do
            {
                result.Add(points[currentPointIndex]);

                var nextPointIndex = (currentPointIndex + 1) % points.Count;

                for (var i = 0; i < points.Count; i++)
                {
                    if (i == nextPointIndex) continue;

                    var orientation = GetOrientation(points[currentPointIndex],
                        points[i], points[nextPointIndex]);

                    if (orientation == Orientation.Clockwise) nextPointIndex = i;
                }

                currentPointIndex = nextPointIndex;
            } while (currentPointIndex != startingPointIndex);

            return result;
        }

        private static Orientation GetOrientation(int[] p, int[] q, int[] r)
        {
            int x1 = p[0], y1 = p[1];
            int x2 = q[0], y2 = q[1];
            int x3 = r[0], y3 = r[1];

            var result = (y2 - y1) * (x3 - x2) - (y3 - y2) * (x2 - x1);

            if (result < 0) return Orientation.Clockwise;

            return result > 0 ? Orientation.CounterclockWise : Orientation.Colinear;
        }


        private static int FindLeftMostPoint(List<int[]> points)
        {
            var left = 0;

            for (var i = 1; i < points.Count; i++)
                if (points[i][0] < points[left][0])
                    left = i;

            return left;
        }

        private enum Orientation
        {
            Clockwise = 0,
            CounterclockWise = 1,
            Colinear = 2
        }
    }
}