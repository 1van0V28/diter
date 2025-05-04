namespace diter.Shapes;

public class Trapezoid(Color color): Shape(color)
{
    public override void SetBordersLines(List<Point> editRectCornersList)
    {
        var originalVerticesList = new List<Point>
        {
            new (
                (int)(0.2f * editRectCornersList[1].X + 0.8f * editRectCornersList[0].X),
                editRectCornersList[0].Y
            ),
            new (
                (int)(0.8f * editRectCornersList[1].X + 0.2f * editRectCornersList[0].X),
                editRectCornersList[0].Y
            ),
            new (
                editRectCornersList[2].X,
                editRectCornersList[2].Y
            ),
            new (
                editRectCornersList[3].X,
                editRectCornersList[3].Y
            )
        };
        BordersLines = new BrokenLine(originalVerticesList, color, false, true);
    }
}