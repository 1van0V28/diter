using diter.Shapes;

namespace diter;

public class DrawingController
{
    private bool _isAddLine = false;
    private bool _isEditLine;
    private Frame? _editFrame;
    public Stack<Frame> FramesList { get; } = [];
    private bool _isMouseDown;
    
    public void MouseDownAction(MouseEventArgs e)
    {
        if (!_isEditLine) // !!! нужно настроить выбор логики обработки событий мыши в зависимости от вида фигуры
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
        else if (e.Button == MouseButtons.Right)
        {
            _editFrame?.StopEdit();
            _editFrame = null;
            _isEditLine = false;
        } else 
        {
            AddNewCorner(e.Location);
        }
    }

    public void MouseMoveAction(MouseEventArgs e)
    {
        if (!_isEditLine) // !!! нужно настроить выбор логики обработки событий мыши в зависимости от вида фигуры
        {
            if (_isMouseDown && _editFrame != null)
            { 
                _editFrame.EditFrame(e.Location);
            }
        }
        else
        {
            _editFrame?.EditFrame(e.Location);
        }
    }

    public void MouseUpAction()
    {
        if (!_isEditLine && _isMouseDown) // !!! нужно настроить выбор логики обработки событий мыши в зависимости от вида фигуры 
        {
            _editFrame?.StopEdit();
            _isMouseDown = false;
        }
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
        _editFrame.FillShape(Color.Aquamarine);
        _isMouseDown = true; 
    }
    
    private void StartAddFrame(Point mousePos)
    {
        Shape newShape = _isAddLine ? 
            new Polyline([mousePos, mousePos], Color.Crimson) : 
            new Pentagon(Color.Crimson);
        var newFrame = new Frame(mousePos, newShape); 
        FramesList.Push(newFrame);
        _editFrame = newFrame;
        _editFrame.StartEdit();

        if (_isAddLine)
        {
            _editFrame.StartAddNewCorner();
            _isEditLine = true;
        }
        _isMouseDown = true;
    }

    private void AddNewCorner(Point mousePos)
    {
        _editFrame?.AddNewCorner(mousePos);
    }
}