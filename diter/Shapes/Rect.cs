namespace diter.Shapes;

public class Rect(Color color): Shape(color)
{
    public override void SetCornersPoints(Point[] editRectCornersList)
    {
        PointsList.Clear();
        PointsList.Add(editRectCornersList[0]);
        PointsList.Add(editRectCornersList[1]);
        PointsList.Add(editRectCornersList[2]);
        PointsList.Add(editRectCornersList[3]);
    }
}