using diter.Shapes;

namespace diter;

public class DrawingController
{
    private Frame? _editFrame;
    public Stack<Frame> FramesList { get; } = [];
    private bool _isMouseDown;
    
    public void MouseDownAction(object sender, MouseEventArgs e)
    {
        for (var i = 0; i < FramesList.Count; i++) // переводим фигуру по клику в состояние редактирования
        {
            var frame = FramesList.ElementAt(i);
            if (frame.GetIsMouseDown(e.Location))
            {
                if (!frame.IsEdit) // чтобы начать редактировать, нужно сначала перевести в состояние редактирования
                {
                    _editFrame?.StopEdit();
                    frame.StartEdit();
                    return;
                }
                break;
            }
        }

        for (var i = 0; i < FramesList.Count; i++) // редактируем фигуру, если она в состоянии редактирования
        {
            var frame = FramesList.ElementAt(i);
            if (frame.IsEdit)
            {
                var editMarkerIndex = frame.GetMouseDownMarkerIndex(e.Location);
                if (editMarkerIndex is >= 0 and <= 3)
                {
                    StartResizeFrame(frame, editMarkerIndex, e.Location); 
                    return;
                }
                if (editMarkerIndex == 4)
                {
                    StartRotateFrame(frame, e.Location); 
                    return;
                }
                if (frame.GetIsMouseDown(e.Location))
                {
                    StartDragFrame(frame, e.Location);
                    return;
                }
            }
        }
        StartAddFrame(e.Location); // добавляем новую фигуру, если клик не по фигуре
    }

    public void MouseMoveAction(object sender, MouseEventArgs e)
    {
        if (_isMouseDown && _editFrame != null)
        {
            _editFrame.EditFrame(e.Location);
        }
    }

    public void MouseUpAction(object sender, MouseEventArgs e)
    {
        if (_editFrame != null)
        {
            _editFrame.StopEdit();
        }
        else
        {
            _editFrame = null;
        }
        _isMouseDown = false;
    }
    
    private void StartResizeFrame(Frame frame, int editMarkerIndex, Point mousePos)
    {
        _editFrame = frame;
        _editFrame.StartResize(editMarkerIndex, mousePos);
        _isMouseDown = true;
    }
    
    private void StartRotateFrame(Frame frame, Point mousePos)
    {
        _editFrame = frame;
        _editFrame.StartRotate(mousePos);
        _isMouseDown = true;
    }
    
    private void StartDragFrame(Frame frame, Point mousePos) 
    { 
        _editFrame = frame; 
        _editFrame.StartDrag(mousePos); 
        _isMouseDown = true; 
    }
    
    private void StartAddFrame(Point mousePos)
    { 
        var newShape = new Triangle(Color.Crimson); 
        var newFrame = new Frame(mousePos, newShape); 
        FramesList.Push(newFrame); 
        _editFrame = newFrame; 
        _isMouseDown = true;
    }
}