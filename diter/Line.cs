namespace diter;

public class Line(Point start, Point end, Color color) : Shape // Добавить линию для отрисовки и линию для редактирования
{
    private Point _start = start;
    private Point _end = end;
    private List<Point>? _pixelsList = null;
    private Color _color = color;

    public override void Draw(Graphics g)
    {
        this.SetBrezenhamPixels();
        foreach (var pixel in this._pixelsList)
        {
            g.FillRectangle(new SolidBrush(this._color), pixel.X, pixel.Y, 1, 1);
        }
    }

    public override void SetPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        throw new NotImplementedException();
    }
    
    private void SetBrezenhamPixels()
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
        this._pixelsList = pixels;
    }
}