namespace diter;

public class Pentagon(Color color): Shape
{
    private Point _top;
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private Line[] _borderLinesList = new Line[5];
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
        this._top = new Point((int)Math.Round(topLeft.X + (topRight.X - topLeft.X) / 2f), topLeft.Y);
        this._topLeft = new Point(topLeft.X, (int)Math.Round((1 - 0.4f) * topLeft.Y + 0.4f * bottomLeft.Y));
        this._bottomLeft = new Point((int)Math.Round((1 - 0.25f) * bottomLeft.X + 0.25f * bottomRight.X), bottomLeft.Y);
        this._bottomRight = new Point((int)Math.Round((1 - 0.75f) * bottomLeft.X + 0.75f * bottomRight.X), bottomLeft.Y);
        this._topRight = new Point(topRight.X, (int)Math.Round((1 - 0.4f) * topRight.Y + 0.4f * bottomRight.Y));
    }
    
    private void SetBorderLines()
    {
        var newBordersList = new Line[5];
        newBordersList[0] = new Line(this._top, this._topRight, this._color);
        newBordersList[1] = new Line(this._topRight, this._bottomRight, this._color);
        newBordersList[2] = new Line(this._bottomRight, this._bottomLeft, this._color);
        newBordersList[3] = new Line(this._bottomLeft, this._topLeft, this._color);
        newBordersList[4] = new Line(this._topLeft, this._top, this._color);

        this._borderLinesList = newBordersList;
    }
}