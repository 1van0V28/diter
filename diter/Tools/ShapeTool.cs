using diter.Shapes;

namespace diter.Tools;

public class ShapeTool(ToolContext toolContext): ITool
{
    public void MouseDownAction(MouseEventArgs e)
    {
        if (!toolContext.IsEditLine)
        {
            for (var i = toolContext.FramesList.Count - 1; i >= 0; i--) // переводим фигуру по клику в состояние редактирования
            { 
                var frame = toolContext.FramesList.ElementAt(i); 
                if (frame.GetIsMouseDown(e.Location)) 
                { 
                    if (!frame.IsEdit) // чтобы начать редактировать, нужно сначала перевести в состояние редактирования
                    { 
                        toolContext.StartEditFrame(frame);
                        return;
                    }
                    break;
                }
            }
            
            for (var i = toolContext.FramesList.Count - 1; i >= 0; i--) // редактируем фигуру, если она в состоянии редактирования
            { 
                var frame = toolContext.FramesList.ElementAt(i); 
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
                        StartDragFrame(frame, e.Location, toolContext.CurrentColor); 
                        return; 
                    }
                }
            }
            StartAddFrame(e.Location, toolContext.CurrentColor, toolContext.ShapeType); // добавляем новую фигуру, если клик не по фигуре
        }
        else if (e.Button == MouseButtons.Right)
        {
            toolContext.StopAddLine();
        } else 
        {
            AddNewCorner(e.Location);
        }
    }

    public void MouseMoveAction(MouseEventArgs e)
    {
        if (!toolContext.IsEditLine)
        {
            if (toolContext.IsMouseDown && toolContext.EditFrame != null)
            { 
                toolContext.EditFrame.EditFrame(e.Location);
            }
        }
        else
        {
            toolContext.EditFrame?.EditFrame(e.Location);
        }
    }

    public void MouseUpAction(MouseEventArgs e)
    {
        if (!toolContext.IsEditLine && toolContext.IsMouseDown) 
        {
            toolContext.StopEdit();
        }
    }
    
    private void StartResizeFrame(Frame frame, int editMarkerIndex, Point mousePos)
    {
        toolContext.StartResizeFrame(frame, editMarkerIndex, mousePos);
    }
    
    private void StartRotateFrame(Frame frame, Point mousePos)
    {
        toolContext.StartRotateFrame(frame, mousePos);
    }

    private void StartDragFrame(Frame frame, Point mousePos, Color currentColor)
    {
        toolContext.StartDragFrame(frame, mousePos, currentColor);
    }

    private void StartAddFrame(Point mousePos, Color currentColor, Type shapeType)
    {
        toolContext.StartAddFrame(mousePos, currentColor, shapeType);
    }
    
    private void AddNewCorner(Point mousePos)
    {
        toolContext.EditFrame?.AddNewCorner(mousePos);
    }
}