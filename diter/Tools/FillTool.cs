namespace diter.Tools;

public class FillTool(ToolContext toolContext): ITool
{
    public void MouseDownAction(MouseEventArgs e)
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
                toolContext.EditFrame?.FillShape(toolContext.CurrentColor);
            }
        }
    }

    public void MouseMoveAction(MouseEventArgs e) {}

    public void MouseUpAction(MouseEventArgs e) {}
}