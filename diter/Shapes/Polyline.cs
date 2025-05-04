namespace diter.Shapes;

public class Polyline(List<Point> originalVerticesList, Color color): Shape(color)
{
    public override void SetBordersLines(Point mousePos)
    {
        originalVerticesList[^1] = mousePos;
        BordersLines = new BrokenLine(originalVerticesList, color);
    }

    public override void AddNewCorner(Point mousePos)
    {
        originalVerticesList[^1] = mousePos; 
        originalVerticesList.Add(mousePos);
    }
}