namespace diter.Shapes;

public static class CurveLine
// возможно нет смысла наследоваться от Line, ведь данный класс можно использовать только для расчётов точек
{
    private static Point Lerp(Point a, Point b, double t)
    {
        return new Point(
            (int)((1 - t) * a.X + t * b.X),
            (int)((1 - t) * a.Y + t * b.Y)
        );
    }

    private static Point GetPoint(List<Point> originalVerticesList, double t) // алгоритм де Кастельжо
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

    public static List<Point> GetPixelsList(List<Point> originalVerticesList)
    {
        const int steps = 1000;
        var pixelsList = new List<Point>();
        for (var i = 0; i <= steps; i++)
        {
            var t = i / (double)steps;
            pixelsList.Add(GetPoint(originalVerticesList, t));
        }
        return pixelsList;
    }
}