namespace VNet.Mathematics.Geometry
{
    public class PointInsidePolygon
    {
        public static bool IsInside(Polygon polygon, Point point)
        {
            var rayLine = new Line(point, new Point(double.MaxValue, point.Y));

            var intersectionCount = 0;
            for (var i = 0; i < polygon.Edges.Count - 1; i++)
            {
                var edgeLine = polygon.Edges[i];

                if (LineIntersection.Find(rayLine, edgeLine) != null) intersectionCount++;
            }

            return intersectionCount % 2 != 0;
        }
    }
}
