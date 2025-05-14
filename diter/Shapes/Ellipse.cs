namespace diter.Shapes;

public class Ellipse(Color color): Shape(color)
{
    public override void SetBordersLines(List<Point> editRectCornersList)
    {
        var midPointsList = new List<Point>();
        for (var i = 0; i < editRectCornersList.Count; i++)
        {
            double deltaX = editRectCornersList[(i + 1) % editRectCornersList.Count].X - editRectCornersList[i].X;
            double deltaY = editRectCornersList[(i + 1) % editRectCornersList.Count].Y - editRectCornersList[i].Y;
            var midPointX = editRectCornersList[i].X + deltaX / 2.0;
            var midPointY = editRectCornersList[i].Y + deltaY / 2.0;
            midPointsList.Add(new Point((int)Math.Round(midPointX), (int)Math.Round(midPointY)));
        }

        // Корректировка вершин для управляющих точек
        const double kappa = 0.5;
        var adjustedVertices = new List<Point>();
        for (var i = 0; i < editRectCornersList.Count; i++)
        {
            double mx = midPointsList[i].X;
            double my = midPointsList[i].Y;
            double vx = editRectCornersList[(i + 1) % editRectCornersList.Count].X;
            double vy = editRectCornersList[(i + 1) % editRectCornersList.Count].Y;
            var ax = vx + kappa * (mx - vx);
            var ay = vy + kappa * (my - vy);
            adjustedVertices.Add(new Point((int)Math.Round(ax), (int)Math.Round(ay)));
        }

        // Создание секторов
        var originalVerticesList = new List<Point>();
        for (var i = 0; i < editRectCornersList.Count; i++)
        {
            var sectorCurvePointsList = CurveLine.GetPixelsList([
                midPointsList[i],
                adjustedVertices[i],
                new Point(
                    2 * midPointsList[(i + 1) % editRectCornersList.Count].X - adjustedVertices[(i + 1) % editRectCornersList.Count].X,
                    2 * midPointsList[(i + 1) % editRectCornersList.Count].Y - adjustedVertices[(i + 1) % editRectCornersList.Count].Y
                    ),
                midPointsList[(i + 1) % editRectCornersList.Count]
            ]);
            originalVerticesList.AddRange(sectorCurvePointsList);
        }

        // Создание BrokenLine
        BordersLines = new BrokenLine(originalVerticesList, color, false, true);
    }
}