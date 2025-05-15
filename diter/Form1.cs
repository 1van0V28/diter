namespace diter;

public sealed partial class Form1 : Form
{
    private readonly DrawingController _drawingController = new();
    private readonly ToolSelector _toolSelector = new();
    
    public Form1()
    {
        InitializeComponent();
        
        DoubleBuffered = true;
        SplitContainer1.Panel2.GetType()
            .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(SplitContainer1.Panel2, true);
    }

    private void DrawPanel_Paint(object sender, PaintEventArgs e)
    {
        e.Graphics.Clear(SplitContainer1.Panel2.BackColor);
        foreach (var editRect in _drawingController.GetFramesList())
        {
            editRect.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        _drawingController.MouseDownAction(e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        _drawingController.MouseMoveAction(e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        _drawingController.MouseUpAction(e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void ControlPanel_ToolButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ToolButtonClick(sender);
        _drawingController.SetToolType(_toolSelector.ToolType);
    }

    private void ControlPanel_ShapeButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ShapeButtonClick(sender);
        _drawingController.SetShapeType(_toolSelector.ShapeType);
        _drawingController.SetToolType(_toolSelector.ShapeType);
    }

    private void ControlPanel_ColorOtherButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ColorOtherButtonClick(pnlColorView);
        _drawingController.SetCurrentColor(_toolSelector.CurrentColor);
        SplitContainer1.Panel1.Invalidate();
    }
    
    private void ControlPanel_ColorButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ColorButtonClick(sender, pnlColorView);
        _drawingController.SetCurrentColor(_toolSelector.CurrentColor);
        SplitContainer1.Panel1.Invalidate();
    }
}