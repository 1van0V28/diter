namespace diter;

public class Marker(Point pos)
{
    private readonly Point _topLeft = new Point(pos.X - 15, pos.Y - 15);
    private readonly Point _bottomRight = new Point(pos.X + 15, pos.Y + 15);

    public void Draw(Graphics g)
    {
        g.FillRectangle(new SolidBrush(Color.Black), pos.X - 15, pos.Y - 15, 30, 30);
    }

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = _topLeft.X < mousePos.X && mousePos.X < _bottomRight.X;
        var isMouseDownY = _topLeft.Y < mousePos.Y && mousePos.Y < _bottomRight.Y;

        return (isMouseDownX && isMouseDownY);
    }

}