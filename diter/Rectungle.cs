namespace diter;

public class Rectungle(Point start, Point end, Color colorRect): Shape
{
    protected Point Start = start;
    protected Point End = end;
    protected Color ColorRect = colorRect;
    public override void Draw(Graphics g)
    {
        var topLeftX = Math.Min(this.Start.X, this.End.X);
        var topLeftY = Math.Min(this.Start.Y, this.End.Y);
        var width = Math.Abs(this.End.X - this.Start.X) + 1;
        var height = Math.Abs(this.End.Y - this.Start.Y) + 1;
        
        g.FillRectangle(new SolidBrush(this.ColorRect), topLeftX, topLeftY, width, height);
    }

    public override void ChangeEndCoors(Point newEnd)
    {
        this.End = newEnd;
    }

    public override void CompleteDraw()
    {
    }
}