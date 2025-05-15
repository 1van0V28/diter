namespace diter.Shapes;

public class BezierCurve(List<Point> originalVerticesList, Color borderColor): Polyline(originalVerticesList, borderColor)
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