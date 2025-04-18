using System.Drawing.Drawing2D;

namespace diter;

public class Frame(Point start, Shape shape)
{
    private bool _isDrag = false;
    private Shape _shape = shape;
    private int? _editMarkerIndex;
    private Point? _dragStart;
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private EditLine[] _borderLinesList = new EditLine[4];
    
    public void Draw(Graphics g)
    {
        if (this._isDrag || this._editMarkerIndex != null)
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
        if (this._isDrag) 
        {
            DragFrame(newEnd);
        } else if (this._editMarkerIndex != null)
        {
           ResizeFrame(newEnd); 
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

    private void ResizeFrame(Point newEnd)
    {
        if (_dragStart == null)
        {
            _dragStart = newEnd;
        }

        if (this._editMarkerIndex != null)
        {
            switch (this._editMarkerIndex)
            {
                case 0: ResizeTop(newEnd);
                    return;
                case 1: ResizeRight(newEnd);
                    return;
                case 2: ResizeBottom(newEnd);
                    return;
                case 3: ResizeLeft(newEnd);
                    return;
            }
        }
    }

    private void ResizeTop(Point newEnd)
    {
        if (this._dragStart != null)
        {
            var deltaY = newEnd.Y - this._dragStart.Value.Y;
            this._dragStart = newEnd;

            if (this._topLeft.Y + deltaY + 1 < this._bottomLeft.Y)
            {
                this._topLeft.Y += deltaY; 
                this._topRight.Y += deltaY;
            }
        }
    }
    
    private void ResizeRight(Point newEnd)
    {
        if (this._dragStart != null)
        {
            var deltaX = newEnd.X - this._dragStart.Value.X;
            this._dragStart = newEnd;

            if (this._topRight.X + deltaX - 1 > this._topLeft.X)
            {
                this._topRight.X += deltaX;
                this._bottomRight.X += deltaX;
            }
        }
    }
    
    private void ResizeBottom(Point newEnd)
    {
        if (this._dragStart != null)
        {
            var deltaY = newEnd.Y - this._dragStart.Value.Y;
            this._dragStart = newEnd;

            if (this._bottomLeft.Y + deltaY - 1 > this._topLeft.Y)
            {
                this._bottomLeft.Y += deltaY;
                this._bottomRight.Y += deltaY;
            }
        }
    }
    
    private void ResizeLeft(Point newEnd)
    {
        if (this._dragStart != null)
        {
            var deltaX = newEnd.X - this._dragStart.Value.X;
            this._dragStart = newEnd;

            if (this._topLeft.X + deltaX + 1 < this._topRight.X)
            {
                this._topLeft.X += deltaX;
                this._bottomLeft.X += deltaX;
            }
        }
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
        var newBordersList = new EditLine[4];
        newBordersList[0] = new EditLine(this._topLeft, this._topRight, Color.Black);
        newBordersList[1] = new EditLine(this._topRight, this._bottomRight, Color.Black);
        newBordersList[2] = new EditLine(this._bottomRight, this._bottomLeft, Color.Black);
        newBordersList[3] = new EditLine(this._bottomLeft, this._topLeft, Color.Black);

        this._borderLinesList = newBordersList;
    }

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = this._topLeft.X < mousePos.X && mousePos.X < this._topRight.X;
        var isMouseDownY = this._topLeft.Y < mousePos.Y && mousePos.Y < this._bottomLeft.Y;

        return (isMouseDownX && isMouseDownY);
    }

    public int GetMouseDownMarkerIndex(Point mousePos)
    {
        for (var i = 0; i < this._borderLinesList.Length; i++)
        {
            if (this._borderLinesList[i].GetIsMouseDownMarker(mousePos))
            {
                return i;
            }
        }
        
        return -1;
    }

    public void StartDrag()
    {
        this._isDrag = true;
    }

    public void StartResize(int editMarkerIndex)
    {
        this._editMarkerIndex = editMarkerIndex;
    }

    public void StopEdit()
    {
        this._isDrag = false;
        this._editMarkerIndex = null;
        this._dragStart = null;
    }
}