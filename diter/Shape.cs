namespace diter;

public abstract class Shape
{
    public abstract void Draw(Graphics g);

    public abstract void SetPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight);
}