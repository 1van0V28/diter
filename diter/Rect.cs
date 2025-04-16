namespace diter;

public class Rect(Point start, Color colorRect): Shape
{
    private bool _isEdit = true;
    private EditRect _editRect = new EditRect(start);
    protected Point Start = start;
    protected Point End = start;
    protected Color ColorRect = colorRect;
    
    public override void Draw(Graphics g)
    {
        if (_isEdit)
        {
            _editRect.Draw(g);
        }
        
        var topLeftX = Math.Min(this.Start.X, this.End.X);
        var topLeftY = Math.Min(this.Start.Y, this.End.Y);
        var width = Math.Abs(this.End.X - this.Start.X) + 1;
        var height = Math.Abs(this.End.Y - this.Start.Y) + 1;
        
        g.FillRectangle(new SolidBrush(this.ColorRect), topLeftX, topLeftY, width, height);
    }

    public override void ChangeEndCoors(Point newEnd)
    {
        _editRect.SetBorders(this.Start, this.End);
        this.End = newEnd;
    }

    public override void CompleteEdit()
    {
        _isEdit = false;
    }

    public override Rectangle GetEditRectBorders()
    {
        return _editRect.GetBorders();
    }

    public override void StartEdit()
    {
        _isEdit = true;
    }

    public override bool IsEdit()
    {
        return _isEdit;
    }
}