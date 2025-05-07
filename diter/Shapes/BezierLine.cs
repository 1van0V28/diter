namespace diter.Shapes;

public class BezierLine(List<Point> originalVerticesList, Color borderColor) : BrokenLine(originalVerticesList, borderColor)
{
    protected override void UpdateLines()
    {
        LinesList.Clear();
        var startLinePoint = VerticesList[0];
        var endLinePoint = VerticesList[1];
        var newLine = new CurveLine(startLinePoint, endLinePoint, borderColor, VerticesList);
        LinesList.Add(newLine);
    }
}