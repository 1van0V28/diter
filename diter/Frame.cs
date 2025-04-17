using System.Drawing.Drawing2D;

namespace diter;

public class Frame(Point start, Shape shape)
{
    private bool _isEdit = false;
    private Shape _shape = shape;
    private Point? _dragStart;
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private Line[] _borderLinesList = new Line[4];
    
    public void Draw(Graphics g)
    {
        if (_isEdit)
        {
            foreach (var line in this._borderLinesList)
            { 
                line.Draw(g);
            }
        }
        
        _shape.Draw(g);
    }

    public void EditFrame(Point newEnd)
    {
        if (_isEdit)
        {
            DragFrame(newEnd);
        }
        else
        {
            this.SetBorderPoints(newEnd);
        }
        this.SetBorderLines();
        this._shape.SetPoints(this._topLeft, this._topRight, this._bottomLeft, this._bottomRight);
    }

    private void DragFrame(Point newEnd)
    {
        if (_dragStart == null)
        {
            _dragStart = newEnd;
        }

        var deltaX = newEnd.X - this._dragStart.Value.X;
        var deltaY = newEnd.Y - this._dragStart.Value.Y;
        this._dragStart = newEnd;

        this._topLeft.X += deltaX;
        this._topLeft.Y += deltaY;
        this._topRight.X += deltaX;
        this._topRight.Y += deltaY;
        this._bottomRight.X += deltaX;
        this._bottomRight.Y += deltaY;
        this._bottomLeft.X += deltaX;
        this._bottomLeft.Y += deltaY;
    }

    private void SetBorderPoints(Point newEnd)
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

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = this._topLeft.X < mousePos.X && mousePos.X < this._topRight.X;
        var isMouseDownY = this._topLeft.Y < mousePos.Y && mousePos.Y < this._bottomLeft.Y;

        return (isMouseDownX && isMouseDownY);
    }

    public void StartEdit()
    {
        this._isEdit = true;
    }

    public void StopEdit()
    {
        this._isEdit = false;
        this._dragStart = null;
    }
}