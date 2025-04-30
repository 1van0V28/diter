namespace diter.Shapes;

public class Line(Point start, Point end, Color color)
{
    protected List<Point> PixelsList => GetBrezenhamPixels();

    public void Draw(Graphics g)
    {
        foreach (var pixel in PixelsList) 
        { 
            g.FillRectangle(new SolidBrush(color), pixel.X, pixel.Y, 1, 1);
        }
    }
    
    private List<Point> GetBrezenhamPixels()
    {
        var start1 = new Point(start.X, start.Y); 
        var dx = Math.Abs(end.X - start.X); 
        var dy = Math.Abs(end.Y - start.Y); 
        var sx = start.X < end.X ? 1 : -1; 
        var sy = start.Y < end.Y ? 1 : -1; 
        var error = dx - dy;
        
        var pixels = new List<Point>(); 
        while (true) 
        { 
            pixels.Add(new Point(start1.X, start1.Y)); 
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
        
        return pixels;
    }
}