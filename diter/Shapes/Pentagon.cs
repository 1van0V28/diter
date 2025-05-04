namespace diter.Shapes;

public class Pentagon(Color color): Shape(color)
{
    public override void SetBordersLines(List<Point> editRectCornersList)
    {
        var originalVerticesList = new List<Point>()
        {
            new(
                (int)Math.Round(editRectCornersList[0].X + (editRectCornersList[1].X - editRectCornersList[0].X) / 2f),
                editRectCornersList[0].Y
            ),
            new(
                editRectCornersList[0].X, 
                (int)Math.Round((1 - 0.4f) * editRectCornersList[0].Y + 0.4f * editRectCornersList[3].Y)
            ),
            new(
                (int)Math.Round((1 - 0.25f) * editRectCornersList[3].X + 0.25f * editRectCornersList[2].X),
                editRectCornersList[3].Y
            ),
            new(
                (int)Math.Round((1 - 0.75f) * editRectCornersList[3].X + 0.75f * editRectCornersList[2].X),
                editRectCornersList[3].Y
            ),
            new(
                editRectCornersList[1].X,
                (int)Math.Round((1 - 0.4f) * editRectCornersList[1].Y + 0.4f * editRectCornersList[2].Y)
            )
        };
        BordersLines = new BrokenLine(originalVerticesList, color, false, true);
    }
}