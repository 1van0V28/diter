namespace diter.Shapes;

public class Triangle(Color color) : Shape(color)
{
    public override void SetBordersLines(List<Point> editRectCornersList)
    {
        var originalVerticesList = new List<Point>([
            new Point(
                Math.Abs(editRectCornersList[0].X + (editRectCornersList[1].X - editRectCornersList[0].X) / 2),
                Math.Abs(editRectCornersList[0].Y + (editRectCornersList[1].Y - editRectCornersList[0].Y) / 2)
            ),
            editRectCornersList[2],
            editRectCornersList[3]]
            );
        BordersLines = new BrokenLine(originalVerticesList, color, false, true);
    }
}