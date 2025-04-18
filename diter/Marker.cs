namespace diter;

public class Marker(Point pos)
{
    private Point _topLeft = new Point(pos.X - 15, pos.Y - 15);
    private Point _bottomRight = new Point(pos.X + 15, pos.Y + 15);

    public void Draw(Graphics g)
    {
        g.FillRectangle(new SolidBrush(Color.Black), pos.X - 15, pos.Y - 15, 30, 30);
    }

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = this._topLeft.X < mousePos.X && mousePos.X < this._bottomRight.X;
        var isMouseDownY = this._topLeft.Y < mousePos.Y && mousePos.Y < this._bottomRight.Y;

        return (isMouseDownX && isMouseDownY);
    }

}