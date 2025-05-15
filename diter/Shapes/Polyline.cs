namespace diter.Shapes;

public class Polyline(List<Point> originalVerticesList, Color color): Shape(color)
{
    public override void SetBordersLines(Point mousePos)
    {
        originalVerticesList[^1] = mousePos;
        BordersLines = new BrokenLine(originalVerticesList, color);
    }

    public override void AddNewCorner(MouseEventArgs e)
    {
        if (GetIsStartPoint(e.Location) && e.Button == MouseButtons.Right)
        {
            originalVerticesList.RemoveAt(originalVerticesList.Count - 1);
            BordersLines = new BrokenLine(originalVerticesList, color, false, true);
            BordersLines.UpdateLines();
            return;
        }
        originalVerticesList[^1] = e.Location; 
        originalVerticesList.Add(e.Location);
    }

    private bool GetIsStartPoint(Point mousePos)
    {
        var startPoint = originalVerticesList[0];
        var isStartPointX = mousePos.X >= startPoint.X - 10 && mousePos.X <= startPoint.X + 10;
        var isStartPointY = mousePos.Y >= startPoint.Y - 10 && mousePos.Y <= startPoint.Y + 10;
        return isStartPointX && isStartPointY;
    }
}