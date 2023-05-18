namespace VNet.Mathematics.Geometry
{
    public static class Util
    {
        public static MbRectangle GetContainingRectangle(this Polygon polygon)
        {
            var x = polygon.Edges.SelectMany(z => new[] { z.Left.X, z.Right.X })
                .Aggregate(new
                {
                    Max = double.MinValue,
                    Min = double.MaxValue
                }, (accumulator, o) => new
                {
                    Max = Math.Max(o, accumulator.Max),
                    Min = Math.Min(o, accumulator.Min)
                });


            var y = polygon.Edges.SelectMany(z => new[] { z.Left.Y, z.Right.Y })
                .Aggregate(new
                {
                    Max = double.MinValue,
                    Min = double.MaxValue
                }, (accumulator, o) => new
                {
                    Max = Math.Max(o, accumulator.Max),
                    Min = Math.Min(o, accumulator.Min)
                });

            return new MbRectangle(new Point(x.Min, y.Max), new Point(x.Max, y.Min))
            {
                Polygon = polygon
            };
        }
    }
}