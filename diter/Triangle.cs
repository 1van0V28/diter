namespace diter;

public class Triangle(Color color): Shape(color)
{
    public override void SetCornersPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        PointsList.Clear();
        PointsList.Add(new Point(topLeft.X + (topRight.X - topLeft.X) / 2, topLeft.Y));
        PointsList.Add(bottomLeft);
        PointsList.Add(bottomRight);
    }
}