namespace diter;

public class Square(Point start, Color colorRect) : Rect(start, colorRect)
{
    public override void Draw(Graphics g)
    {
        int deltaX = base.End.X - base.Start.X;
        int deltaY = base.End.Y - base.Start.Y;
        int sideLength = Math.Min(Math.Abs(deltaX), Math.Abs(deltaY)) + 1;
        int topLeftX = deltaX >= 0 ? base.Start.X : base.Start.X - sideLength;
        int topLeftY = deltaY >= 0 ? base.Start.Y : base.Start.Y - sideLength;

        if (deltaX < 0)
        {
            topLeftX = base.Start.X - sideLength;
        }
        if (deltaY < 0)
        {
            topLeftY = base.Start.Y - sideLength;
        }
        
        g.FillRectangle(new SolidBrush(base.ColorRect), topLeftX, topLeftY, sideLength, sideLength);
    }
}