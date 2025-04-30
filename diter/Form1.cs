namespace diter;

public sealed partial class Form1 : Form
{
    private readonly DrawingController _drawingController = new();
    
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
        foreach (var editRect in _drawingController.FramesList)
        {
            editRect.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        _drawingController.MouseDownAction(sender, e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        _drawingController.MouseMoveAction(sender, e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        _drawingController.MouseUpAction(sender, e);
        SplitContainer1.Panel2.Invalidate();
    }
}