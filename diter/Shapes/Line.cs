namespace diter.Shapes;

public class Line(Point start, Point end, Color color, bool isDashed = false)
{
    protected List<Point> PixelsList => GetPixelsList();

    public virtual void Draw(Graphics g)
    {
        if (isDashed)
        {
            DrawDashed(g);
        }
        else
        {
            DrawLine(g);
        }
    }

    private void DrawDashed(Graphics g)
    {
        const int gapLength = 5;
        var isGap = false;
        for (var i = 0; i < PixelsList.Count; i++)
        {
            if (i % gapLength == 0 && i != 0)
            { 
                isGap = !isGap;
            }

            if (!isGap)
            {
                var pixel = PixelsList[i];
                g.FillRectangle(new SolidBrush(color), pixel.X, pixel.Y, 1, 1);
            }
        }
    }

    private void DrawLine(Graphics g)
    {
        foreach (var pixel in PixelsList)
        {
            g.FillRectangle(new SolidBrush(color), pixel.X, pixel.Y, 1, 1);
        }
    }
    
    protected virtual List<Point> GetPixelsList() // алгоритм Брезенхема
    {
        var start1 = new Point(start.X, start.Y); 
        var dx = Math.Abs(end.X - start.X); 
        var dy = Math.Abs(end.Y - start.Y); 
        var sx = start.X < end.X ? 1 : -1; 
        var sy = start.Y < end.Y ? 1 : -1; 
        var error = dx - dy;
        
        var pixelsList = new List<Point>(); 
        while (true) 
        { 
            pixelsList.Add(new Point(start1.X, start1.Y)); 
            if (start1.X == end.X && start1.Y == end.Y) 
            { 
                break;
            }
            var e2 = 2 * error;
            if (e2 > -dy)
            {
                error -= dy;
                start1.X += sx;
            }
            if (e2 < dx)
            {
                error += dx;
                start1.Y += sy;
            }
        }
        return pixelsList;
    }
}