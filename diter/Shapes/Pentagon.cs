namespace diter.Shapes;

public class Pentagon(Color color): Shape(color)
{
    public override void SetCornersPoints(Point[] editRectCornersList) // стоит рассмотреть вариант реализации фигур через ломаные
    {
        PointsList.Clear();
        PointsList.Add(new Point(
            (int)Math.Round(editRectCornersList[0].X + (editRectCornersList[1].X - editRectCornersList[0].X) / 2f),
            editRectCornersList[0].Y
            ));
        PointsList.Add(new Point(
            editRectCornersList[0].X, 
            (int)Math.Round((1 - 0.4f) * editRectCornersList[0].Y + 0.4f * editRectCornersList[3].Y)
            ));
        PointsList.Add(new Point(
            (int)Math.Round((1 - 0.25f) * editRectCornersList[3].X + 0.25f * editRectCornersList[2].X),
            editRectCornersList[3].Y
            ));
        PointsList.Add(new Point(
            (int)Math.Round((1 - 0.75f) * editRectCornersList[3].X + 0.75f * editRectCornersList[2].X),
            editRectCornersList[3].Y
            ));
        PointsList.Add(new Point(
            editRectCornersList[1].X,
            (int)Math.Round((1 - 0.4f) * editRectCornersList[1].Y + 0.4f * editRectCornersList[2].Y)
            ));
    }
}