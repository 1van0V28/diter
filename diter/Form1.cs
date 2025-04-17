namespace diter;

public partial class Form1 : Form
{
    private Frame _editFrame = null;
    private List<Frame> _framesList = new List<Frame>();
    private bool _isMouseDown = false;
    
    public Form1()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
    }

    private void DrawPanel_Paint(object sender, PaintEventArgs e)
    {
        foreach (var editRect in this._framesList)
        {
            editRect.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        var startPixel = new Point(e.Location.X, e.Location.Y);
        var newShape = new Rect(Color.Crimson);
        var newEditRect = new Frame(startPixel, newShape);
        _framesList.Add(newEditRect);
        this._isMouseDown = true;
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (this._isMouseDown)
        {
            var editRect = this._framesList.Last();
            editRect.EditFrame(new Point(e.Location.X, e.Location.Y));
            SplitContainer1.Panel2.Invalidate();
        }
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        this._isMouseDown = false;
    }
}