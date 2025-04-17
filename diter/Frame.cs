using System.Drawing.Drawing2D;

namespace diter;

public class Frame(Point start, Shape shape)
{
    private Shape _shape = shape;
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private Line[] _borderLinesList = new Line[4];
    
    public void Draw(Graphics g)
    {
        foreach (var line in this._borderLinesList)
        {
            line.Draw(g);
        }
        
        _shape.Draw(g);
    }

    public void EditFrame(Point newEnd)
    {
        this.SetBorderPoints(newEnd);
        this.SetBorderLines();
        this._shape.SetPoints(this._topLeft, this._topRight, this._bottomLeft, this._bottomRight);
    }

    public void SetBorderPoints(Point newEnd)
    {
        var topLeftX = Math.Min(start.X, newEnd.X);
        var topLeftY = Math.Min(start.Y, newEnd.Y);
        var bottomRightX = Math.Max(start.X, newEnd.X);
        var bottomRightY = Math.Max(start.Y, newEnd.Y);

        this._topLeft = new Point(topLeftX, topLeftY);
        this._topRight = new Point(bottomRightX, topLeftY);
        this._bottomLeft = new Point(topLeftX, bottomRightY);
        this._bottomRight = new Point(bottomRightX, bottomRightY);
    }

    private void SetBorderLines()
    {
        var newBordersList = new Line[4];
        newBordersList[0] = new Line(this._topLeft, this._topRight, Color.Black);
        newBordersList[1] = new Line(this._topRight, this._bottomRight, Color.Black);
        newBordersList[2] = new Line(this._bottomRight, this._bottomLeft, Color.Black);
        newBordersList[3] = new Line(this._bottomLeft, this._topLeft, Color.Black);

        this._borderLinesList = newBordersList;
    }
}