namespace diter;

public abstract class Shape
{
    protected Color Color;

    protected Shape(Color color = default)
    {
        this.Color = color == default ? Color.Black : color;
    }

    public abstract void Draw(Graphics g);

    public abstract void ChangeEndCoors(Point newEnd);

    public abstract void CompleteDraw();
}