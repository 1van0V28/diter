namespace diter.Shapes;

public class BezierCurve(List<Point> originalVerticesList, Color borderColor): Polyline(originalVerticesList, borderColor)
// план-капкан
// переносим вычисления точек кривой в отдельный класс
// используем его, чтобы вычислять точки для ломаной в BezierCurve и для ломаной в Elipse (объединение нескольких секторов)
{
    public override void SetBordersLines(Point mousePos)
    {
        if (originalVerticesList.Count <= 2)
        {
            originalVerticesList[^1] = mousePos;
        }
        else
        {
            originalVerticesList[^2] = mousePos;
        }

        var bezierCurvePoints = CurveLine.GetPixelsList(originalVerticesList);
        BordersLines = new BrokenLine(bezierCurvePoints, borderColor);
        // сделать отдельную кривую линию, которую можно использовать для расчётов точек
    }

    public override void AddNewCorner(Point mousePos)
    {
        if (originalVerticesList.Count <= 2)
        {
            base.AddNewCorner(mousePos);
        }
        else
        {
            originalVerticesList.Insert(originalVerticesList.Count - 1, mousePos);
        }
    }
}