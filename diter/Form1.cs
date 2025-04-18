namespace diter;

public partial class Form1 : Form
{
    private Frame? _editFrame;
    private readonly Stack<Frame> _framesList = [];
    private bool _isMouseDown;
    
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
        foreach (var editRect in _framesList)
        {
            editRect.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        foreach (var frame in _framesList)
        {
            if (frame.GetIsMouseDown(e.Location) && _editFrame == null)
            {
                StartDragFrame(frame);
                return;
            } 
            if (frame.GetMouseDownMarkerIndex(e.Location) != -1 && _editFrame == null)
            {
                StartResizeFrame(frame, e.Location);
                return;
            }
        }
        StartAddFrame(e.Location);
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDown && _editFrame != null)
        {
            _editFrame.EditFrame(e.Location);
            SplitContainer1.Panel2.Invalidate();
        }
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        if (_editFrame != null)
        {
            _editFrame.StopEdit();
            _editFrame = null;
        }
        _isMouseDown = false;
    }

    private void StartDragFrame(Frame frame)
    {
        _editFrame = frame;
        _editFrame.StartDrag();
        _isMouseDown = true; 
        SplitContainer1.Panel2.Invalidate();
    }
    
    private void StartResizeFrame(Frame frame, Point mousePos)
    {
        _editFrame = frame;
        _editFrame.StartResize(frame.GetMouseDownMarkerIndex(mousePos));
        _isMouseDown = true;
        SplitContainer1.Panel2.Invalidate();
    }

    private void StartAddFrame(Point mousePos)
    {
        var newShape = new Triangle(Color.Crimson);
        var newFrame = new Frame(mousePos, newShape);
        _framesList.Push(newFrame);
        _editFrame = newFrame;
        _isMouseDown = true;
    }
}