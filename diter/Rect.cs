using System.Drawing.Drawing2D;

namespace diter;

public class Rect(Color color): Shape(color)
{
    public override void SetCornersPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        PointsList.Add(topLeft);
        PointsList.Add(topRight);
        PointsList.Add(bottomLeft);
        PointsList.Add(bottomRight);
    }
}