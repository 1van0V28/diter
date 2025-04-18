namespace diter;

public class Line(Point start, Point end, Color color)
{
    private Point _start = start;
    private Point _end = end;
    protected List<Point>? PixelsList;
    private Color _color = color;

    public void Draw(Graphics g)
    {
        this.SetBrezenhamPixels();
        if (this.PixelsList != null)
        {
            foreach (var pixel in this.PixelsList)
            {
                g.FillRectangle(new SolidBrush(this._color), pixel.X, pixel.Y, 1, 1);
            }
        }
    }
    
    protected void SetBrezenhamPixels()
    {
        var start1 = new Point(this._start.X, this._start.Y); 
        var dx = Math.Abs(this._end.X - this._start.X); 
        var dy = Math.Abs(this._end.Y - this._start.Y); 
        var sx = this._start.X < this._end.X ? 1 : -1; 
        var sy = this._start.Y < this._end.Y ? 1 : -1; 
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
        this.PixelsList = pixels;
    }
}