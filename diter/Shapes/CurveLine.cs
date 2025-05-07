namespace diter.Shapes;

public class CurveLine(Point start, Point end, Color color, List<Point> originalVerticesList): Line(start, end, color)
{
    private List<Line> _linesList = [];
    public override void Draw(Graphics g)
    {
        foreach (var line in _linesList)
        {
            line.Draw(g);
        }
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

    protected override List<Point> GetPixelsList()
    {
        const int steps = 100;
        var pixelsList = new List<Point>();
        for (var i = 0; i <= steps; i++)
        {
            var t = i / (double)steps;
            pixelsList.Add(GetPoint(t));
        }
        return pixelsList;
    }
}