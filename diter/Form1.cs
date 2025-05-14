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
        foreach (var editRect in _drawingController.FramesList)
        {
            editRect.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        _drawingController.MouseDownAction(e, _toolSelector.Color, _toolSelector.ShapeType);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        _drawingController.MouseMoveAction(e);
        SplitContainer1.Panel2.Invalidate();
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        _drawingController.MouseUpAction();
        SplitContainer1.Panel2.Invalidate();
    }

    private void ControlPanel_ShapeButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ShapeButtonClick(sender, e);
    }

    private void ControlPanel_ColorOtherButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ColorOtherButtonClick(pnlColorView); 
        SplitContainer1.Panel1.Invalidate();
    }
    
    private void ControlPanel_ColorButtonClick(object sender, EventArgs e)
    {
        _toolSelector.ColorButtonClick(sender, pnlColorView);
        SplitContainer1.Panel1.Invalidate();
    }

    private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button2_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button10_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button17_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button14_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button20_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button21_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void button18_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void btnBlack_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void btnPolyline_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void btnOther_Click(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
    {
        throw new System.NotImplementedException();
    }
}