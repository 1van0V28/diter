using diter.Shapes;

namespace diter.Tools;

public class ToolContext
{
    public Type ShapeType { get; private set; } = typeof(Polyline);
    public Color CurrentColor { get; private set; } = Color.Black;
    private bool _isAddLine;
    public bool IsEditLine { get; private set; }
    public bool IsMouseDown { get; private set; }
    public Frame? EditFrame { get; private set; }
    public readonly List<Frame> FramesList = [];

    private void SetFrameToTop(Frame editFrame)
    {
        var editFrameIndex = FramesList.IndexOf(editFrame);
        if (editFrameIndex == FramesList.Count - 1) return;

        var temp = FramesList.Last();
        FramesList[^1] = editFrame;
        FramesList[editFrameIndex] = temp;
    }
    
    public void StartEditFrame(Frame frame)
    {
        EditFrame?.StopEdit();
        EditFrame = frame;
        SetFrameToTop(frame);
        frame.StartEdit();
    }
    
    public void StartResizeFrame(Frame frame, int editMarkerIndex, Point mousePos)
    {
        EditFrame = frame;
        EditFrame.StartResize(editMarkerIndex, mousePos);
        IsMouseDown = true;
    }
    
    public void StartRotateFrame(Frame frame, Point mousePos)
    {
        EditFrame = frame;
        EditFrame.StartRotate(mousePos);
        IsMouseDown = true;
    }
    
    public void StartDragFrame(Frame frame, Point mousePos) 
    { 
        EditFrame = frame; 
        EditFrame.StartDrag(mousePos);
        IsMouseDown = true; 
    }
    
    private Shape GetShape(Point mousePos, Color currentColor)
    {
        if (ShapeType == typeof(Polyline))
        {
            return new Polyline([mousePos, mousePos], currentColor);
        } 
        if (ShapeType == typeof(BezierCurve))
        {
            return new BezierCurve([mousePos, mousePos], currentColor);
        }
        if (ShapeType == typeof(Ellipse))
        {
            return new Ellipse(currentColor);
        }
        if (ShapeType == typeof(Rect))
        {
            return new Rect(currentColor);
        }
        if (ShapeType == typeof(Triangle))
        {
            return new Triangle(currentColor);
        }
        if (ShapeType == typeof(Pentagon))
        {
            return new Pentagon(currentColor);
        }
        return new Trapezoid(currentColor);
    }

    private void SetIsAddLine(Type shapeType)
    {
        if (shapeType == typeof(Polyline) || shapeType == typeof(BezierCurve))
        {
            _isAddLine = true;
        }
        else
        {
            _isAddLine = false;
        }
    }
    
    public void StartAddFrame(Point mousePos, Color currentColor, Type shapeType)
    {
        EditFrame?.StopEdit();
        var newShape = GetShape(mousePos, currentColor);
        var newFrame = new Frame(mousePos, newShape); 
        FramesList.Add(newFrame);
        EditFrame = newFrame;
        EditFrame.StartEdit();

        SetIsAddLine(shapeType);
        if (_isAddLine)
        {
            EditFrame.StartAddNewCorner();
            IsEditLine = true;
        }
        IsMouseDown = true;
    }

    public void StopAddLine()
    {
        EditFrame?.StopEdit();
        EditFrame = null;
        IsEditLine = false;
    }

    public void StopEdit()
    {
        EditFrame?.StopEdit();
        IsMouseDown = false;
    }

    public void SetCurrentColor(Color currentColor)
    {
        CurrentColor = currentColor;
    }

    public void SetShapeType(Type shapeType)
    {
        ShapeType = shapeType;
    }
}