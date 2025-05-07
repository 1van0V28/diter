namespace diter.Shapes;

public class BezierCurve(List<Point> originalVerticesList, Color borderColor): Polyline(originalVerticesList, borderColor)
{
    public override void SetBordersLines(Point mousePos)
    {
        originalVerticesList[^1] = mousePos;
        BordersLines = new BrokenLine(GetPixelsList(), borderColor);
    }
    
    private Point Lerp(Point a, Point b, double t)
    {
        return new Point(
            (int)((1 - t) * a.X + t * b.X),
            (int)((1 - t) * a.Y + t * b.Y)
        );
    }

    private Point GetPoint(double t) // алгоритм де Кастельжо
    {
        Point[] verticesList = [..originalVerticesList];
        var n = originalVerticesList.Count - 1;
        
        for (var r = 1; r <= n; r++)
        {
            for (var i = 0; i < n - r + 1; i++)
            {
                verticesList[i] = Lerp(verticesList[i], verticesList[i + 1], t);
            }
        }

        return verticesList[0]; // Финальная точка — B(t)
    }

    private List<Point> GetPixelsList()
    {
        const int steps = 10000;
        var pixelsList = new List<Point>();
        for (var i = 0; i <= steps; i++)
        {
            var t = i / (double)steps;
            pixelsList.Add(GetPoint(t));
        }
        return pixelsList;
    }
}