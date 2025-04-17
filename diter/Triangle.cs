namespace diter;

public class Triangle(Color color): Shape
{
    private Point _top;
    private Point _bottomLeft;
    private Point _bottomRight;
    private Line[] _borderLinesList = new Line[3];
    private Color _color = color;
    
    public override void Draw(Graphics g)
    {
        this.SetBorderLines();
        foreach (var line in this._borderLinesList)
        {
            line.Draw(g);
        }
    }

    public override void SetPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
    {
        this._top = new Point(topLeft.X + (topRight.X - topLeft.X) / 2, topLeft.Y);
        this._bottomLeft = bottomLeft;
        this._bottomRight = bottomRight;
    }
    
    private void SetBorderLines()
    {
        var newBordersList = new Line[3];
        newBordersList[0] = new Line(this._bottomLeft, this._top, this._color);
        newBordersList[1] = new Line(this._top, this._bottomRight, this._color);
        newBordersList[2] = new Line(this._bottomRight, this._bottomLeft, this._color);

        this._borderLinesList = newBordersList;
    }
}