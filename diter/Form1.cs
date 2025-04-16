namespace diter;

public partial class Form1 : Form
{
    private bool _isMouseDawn = false;
    private Shape? _editShape = null;
    private List<Shape> _shapesList = new List<Shape>();
    public Form1()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
    }

    private void DrawPanel_Paint(object sender, PaintEventArgs e)
    {
        foreach (var shape in this._shapesList)
        {
            shape.Draw(e.Graphics);
        }
    }

    private void DrawPanel_MouseDown(object sender, MouseEventArgs e)
    {
        foreach (var shape in _shapesList)
        {
            if (shape.GetEditRectBorders().Contains(e.Location))
            {
                _editShape = shape;
                _isMouseDawn = true;
                shape.StartEdit();
                SplitContainer1.Panel2.Invalidate();
                return;
            }
        }
        
        var startPixel = new Point(e.Location.X, e.Location.Y);
        // var line = new Line(startPixel, startPixel, Color.Crimson);
        var rect = new Rect(startPixel, Color.Crimson);
        _editShape = rect;
        // var square = new Square(startPixel, startPixel, Color.Crimson);
        _isMouseDawn = true;
        _shapesList.Add(rect); // изменено для теста
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDawn)
        {
            if (_editShape != null)
            {
                var newEnd = new Point(e.Location.X, e.Location.Y);
                _editShape.ChangeEndCoors(newEnd);
                SplitContainer1.Panel2.Invalidate();
            }
        }
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        if (_editShape != null)
        {
            _editShape.CompleteEdit();
            _editShape = null;
            _isMouseDawn = false; 
            SplitContainer1.Panel2.Invalidate();
        }
    }
}