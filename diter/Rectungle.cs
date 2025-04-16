namespace diter;

public class Rectungle(Point start, Point end, Color color): Shape
{
    private Point _start = start;
    private Point _end = end;
    private Color _color = color;
    public override void Draw(Graphics g)
    {
        var topLeftX = Math.Min(this._start.X, this._end.X);
        var topLeftY = Math.Min(this._start.Y, this._end.Y);
        var width = Math.Abs(this._end.X - this._start.X) + 1;
        var height = Math.Abs(this._end.Y - this._start.Y) + 1;
        
        g.FillRectangle(new SolidBrush(this._color), topLeftX, topLeftY, width, height);
    }

    public override void ChangeEndCoors(Point newEnd)
    {
        this._end = newEnd;
    }

    public override void CompleteDraw()
    {
    }
}