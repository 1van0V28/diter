namespace diter.Shapes;

public class Rect(Color color): Shape(color)
{
    public override void SetBordersLines(List<Point> editRectCornersList)
    {
        BordersLines = new BrokenLine(editRectCornersList, color, false, true);
    }
}