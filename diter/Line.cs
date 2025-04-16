namespace diter;

public class Line(Point start, Point end, Color color = default) : Shape
{
    private Point _end = end;
    private List<Point>? _pixelsList = null;
    private Color _color = color == default ? Color.Black : color;

    public override void Draw(Graphics g)
    {
         var color = _pixelsList == null ? Color.Gray : _color;
         var pixelsList = _pixelsList ?? GetBrezenhamPixels();
         
         foreach (var pixel in pixelsList)
         {
             g.FillRectangle(new SolidBrush(color), pixel.X, pixel.Y, 1, 1);
         }
    }
    
    public override void ChangeEndCoors(Point newEnd) 
    {
        this._end = newEnd;
    }

    public override void CompleteDraw()
    {
        this._pixelsList = GetBrezenhamPixels();
    }
    
    private List<Point> GetBrezenhamPixels()
    {
        var start1 = new Point(start.X, start.Y); 
        var dx = Math.Abs(this._end.X - start.X); 
        var dy = Math.Abs(this._end.Y - start.Y); 
        var sx = start.X < this._end.X ? 1 : -1; 
        var sy = start.Y < this._end.Y ? 1 : -1; 
        var error = dx - dy;
        
        var pixels = new List<Point>(); 
        while (true) 
        { 
            pixels.Add(new Point(start1.X, start1.Y)); 
            if (start1.X == this._end.X && start1.Y == this._end.Y) 
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