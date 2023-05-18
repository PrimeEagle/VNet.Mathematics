namespace VNet.Mathematics.Geometry
{
    public class MbRectangle : Rectangle
    {
        public MbRectangle(Point leftTopCorner, Point rightBottomCorner)
        {
            LeftTop = leftTopCorner;
            RightBottom = rightBottomCorner;
        }

        public MbRectangle(Rectangle rectangle)
        {
            LeftTop = new Point(rectangle.LeftTop.X, rectangle.LeftTop.Y);
            RightBottom = new Point(rectangle.RightBottom.X, rectangle.RightBottom.Y);
        }

        public Polygon Polygon { get; set; }

        public double GetEnlargementArea(MbRectangle rectangleToFit)
        {
            return Math.Abs(GetMergedRectangle(rectangleToFit).Area() - Area());
        }

        public void Merge(MbRectangle rectangleToMerge)
        {
            var merged = GetMergedRectangle(rectangleToMerge);

            LeftTop = merged.LeftTop;
            RightBottom = merged.RightBottom;
        }

        private Rectangle GetMergedRectangle(MbRectangle rectangleToMerge)
        {
            var leftTopCorner = new Point(LeftTop.X > rectangleToMerge.LeftTop.X ? rectangleToMerge.LeftTop.X : LeftTop.X,
                LeftTop.Y < rectangleToMerge.LeftTop.Y ? rectangleToMerge.LeftTop.Y : LeftTop.Y);

            var rightBottomCorner = new Point(
                RightBottom.X < rectangleToMerge.RightBottom.X ? rectangleToMerge.RightBottom.X : RightBottom.X,
                RightBottom.Y > rectangleToMerge.RightBottom.Y ? rectangleToMerge.RightBottom.Y : RightBottom.Y);

            return new MbRectangle(leftTopCorner, rightBottomCorner);
        }

        public double Area()
        {
            return base.Area();
        }
    }
}