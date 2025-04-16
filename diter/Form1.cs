namespace diter;

public partial class Form1 : Form
{
    private bool _isMouseDawn = false;
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
        var startPixel = new Point(e.Location.X, e.Location.Y);
        // var line = new Line(startPixel, startPixel, Color.Crimson);
        // var rect = new Rectungle(startPixel, startPixel, Color.Crimson);
        var square = new Square(startPixel, startPixel, Color.Crimson);
        _isMouseDawn = true;
        _shapesList.Add(square); // изменено для теста
    }

    private void DrawPanel_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMouseDawn)
        {
            var editableShape = _shapesList.Last();
            var newEnd = new Point(e.Location.X, e.Location.Y);
            editableShape.ChangeEndCoors(newEnd);
            SplitContainer1.Panel2.Invalidate();
        }
    }

    private void DrawPanel_MouseUp(object sender, MouseEventArgs e)
    {
        var editableShape = _shapesList.Last();
        editableShape.CompleteDraw();
        _isMouseDawn = false;
        SplitContainer1.Panel2.Invalidate();
    }
}