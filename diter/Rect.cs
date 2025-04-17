using System.Drawing.Drawing2D;

namespace diter;

public class Rect(Color color): Shape
{
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private Line[] _borderLinesList = new Line[4];
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
        this._topLeft = topLeft;
        this._topRight = topRight;
        this._bottomLeft = bottomLeft;
        this._bottomRight = bottomRight;
    }

    private void SetBorderLines()
    {
        var newBordersList = new Line[4];
        newBordersList[0] = new Line(this._topLeft, this._topRight, this._color);
        newBordersList[1] = new Line(this._topRight, this._bottomRight, this._color);
        newBordersList[2] = new Line(this._bottomRight, this._bottomLeft, this._color);
        newBordersList[3] = new Line(this._bottomLeft, this._topLeft, this._color);

        this._borderLinesList = newBordersList;
    }
}