using System.Drawing.Drawing2D;

namespace diter;

public class EditRect(Point start)
{
    private Rectangle _borders = new Rectangle(start.X, start.Y, 1, 1);
    
    public void Draw(Graphics g)
    {
        var pen = new Pen(Color.Black, 4);
        pen.DashStyle = DashStyle.Dash;
        
        g.DrawRectangle(pen, _borders);
    }

    public void SetBorders(Point start, Point end)
    {
        var topLeftX = Math.Min(start.X, end.X);
        var topLeftY = Math.Min(start.Y, end.Y);
        var width = Math.Abs(end.X - start.X) + 1;
        var height = Math.Abs(end.Y - start.Y) + 1;
        
        this._borders = new Rectangle(topLeftX, topLeftY, width, height);
    }

    public Rectangle GetBorders()
    {
        return _borders;
    }
}