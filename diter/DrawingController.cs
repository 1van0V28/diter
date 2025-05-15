using diter.Shapes;
using diter.Tools;

namespace diter;

public class DrawingController
{
    private Type? _toolType;
    private ITool Tool => GetTool();
    private readonly ToolContext _toolContext = new();
    
    public void MouseDownAction(MouseEventArgs e)
    {
        Tool.MouseDownAction(e);
    }

    public void MouseMoveAction(MouseEventArgs e)
    {
        Tool.MouseMoveAction(e);
    }

    public void MouseUpAction(MouseEventArgs e)
    {
        Tool.MouseUpAction(e);
    }
    
    private ITool GetTool()
    {
        if (_toolType == typeof(FillTool))
        {
            return new FillTool(_toolContext);
        }
        if (_toolType == typeof(DeleteTool))
        {
            return new DeleteTool(_toolContext);
        }
        return new ShapeTool(_toolContext);
    }

    public List<Frame> GetFramesList()
    {
        return _toolContext.FramesList;
    }
    
    public void SetShapeType(Type shapeType)
    { 
        _toolContext.SetShapeType(shapeType);
    }
    
    public void SetCurrentColor(Color currentColor)
    {
        _toolContext.SetCurrentColor(currentColor);
    }
    
    public void SetToolType(Type toolType)
    {
        _toolType = toolType;
    }
}