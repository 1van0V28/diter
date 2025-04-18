namespace diter;

public class Frame(Point start, Shape shape)
{
    private bool _isDrag;
    private int? _editMarkerIndex;
    private Point? _dragStart;
    private Point _topLeft;
    private Point _topRight;
    private Point _bottomLeft;
    private Point _bottomRight;
    private readonly EditLine[] _borderLinesList = new EditLine[4];
    
    public void Draw(Graphics g)
    {
        if (_isDrag || _editMarkerIndex != null)
        {
            foreach (var line in _borderLinesList)
            { 
                line.Draw(g);
            }
        }
        
        shape.Draw(g);
    }

    private void DragFrame(Point newEnd)
    {
        _dragStart ??= newEnd;

        var deltaX = newEnd.X - _dragStart.Value.X;
        var deltaY = newEnd.Y - _dragStart.Value.Y;
        _dragStart = newEnd;

        _topLeft.X += deltaX;
        _topLeft.Y += deltaY;
        _topRight.X += deltaX;
        _topRight.Y += deltaY;
        _bottomRight.X += deltaX;
        _bottomRight.Y += deltaY;
        _bottomLeft.X += deltaX;
        _bottomLeft.Y += deltaY;
    }

    private void ResizeFrame(Point newEnd)
    {
        _dragStart ??= newEnd;

        if (_editMarkerIndex != null)
        {
            switch (_editMarkerIndex)
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
        if (_dragStart != null)
        {
            var deltaY = newEnd.Y - _dragStart.Value.Y;
            _dragStart = newEnd;

            if (_topLeft.Y + deltaY + 1 < _bottomLeft.Y)
            {
                _topLeft.Y += deltaY; 
                _topRight.Y += deltaY;
            }
        }
    }
    
    private void ResizeRight(Point newEnd)
    {
        if (_dragStart != null)
        {
            var deltaX = newEnd.X - _dragStart.Value.X;
            _dragStart = newEnd;

            if (_topRight.X + deltaX - 1 > _topLeft.X)
            {
                _topRight.X += deltaX;
                _bottomRight.X += deltaX;
            }
        }
    }
    
    private void ResizeBottom(Point newEnd)
    {
        if (_dragStart != null)
        {
            var deltaY = newEnd.Y - _dragStart.Value.Y;
            _dragStart = newEnd;

            if (_bottomLeft.Y + deltaY - 1 > _topLeft.Y)
            {
                _bottomLeft.Y += deltaY;
                _bottomRight.Y += deltaY;
            }
        }
    }
    
    private void ResizeLeft(Point newEnd)
    {
        if (_dragStart != null)
        {
            var deltaX = newEnd.X - _dragStart.Value.X;
            _dragStart = newEnd;

            if (_topLeft.X + deltaX + 1 < _topRight.X)
            {
                _topLeft.X += deltaX;
                _bottomLeft.X += deltaX;
            }
        }
    }

    private void SetCornersPoints(Point newEnd)
    {
        var topLeftX = Math.Min(start.X, newEnd.X);
        var topLeftY = Math.Min(start.Y, newEnd.Y);
        var bottomRightX = Math.Max(start.X, newEnd.X);
        var bottomRightY = Math.Max(start.Y, newEnd.Y);

        _topLeft = new Point(topLeftX, topLeftY);
        _topRight = new Point(bottomRightX, topLeftY);
        _bottomLeft = new Point(topLeftX, bottomRightY);
        _bottomRight = new Point(bottomRightX, bottomRightY);
    }

    private void SetBordersLines()
    {
        _borderLinesList[0] = new EditLine(_topLeft, _topRight, Color.Black);
        _borderLinesList[1] = new EditLine(_topRight, _bottomRight, Color.Black);
        _borderLinesList[2] = new EditLine(_bottomRight, _bottomLeft, Color.Black);
        _borderLinesList[3] = new EditLine(_bottomLeft, _topLeft, Color.Black);
    }
    
    public void StartDrag()
    {
        _isDrag = true;
    }

    public void StartResize(int editMarkerIndex)
    {
        _editMarkerIndex = editMarkerIndex;
    }

    public void StopEdit()
    {
        _isDrag = false;
        _editMarkerIndex = null;
        _dragStart = null;
        shape.StopEdit();
    }
    
    public void EditFrame(Point newEnd)
    {
        shape.StartEdit();
        if (_isDrag) 
        {
            DragFrame(newEnd);
        } else if (_editMarkerIndex != null)
        {
            ResizeFrame(newEnd); 
        }
        else
        {
            SetCornersPoints(newEnd);
        }
        SetBordersLines();
        shape.SetCornersPoints(_topLeft, _topRight, _bottomLeft, _bottomRight);
    }

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = _topLeft.X < mousePos.X && mousePos.X < _topRight.X;
        var isMouseDownY = _topLeft.Y < mousePos.Y && mousePos.Y < _bottomLeft.Y;

        return (isMouseDownX && isMouseDownY);
    }

    public int GetMouseDownMarkerIndex(Point mousePos)
    {
        for (var i = 0; i < _borderLinesList.Length; i++)
        {
            if (_borderLinesList[i].GetIsMouseDownMarker(mousePos))
            {
                return i;
            }
        }
        
        return -1;
    }
}