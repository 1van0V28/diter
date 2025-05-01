namespace diter.Shapes;

public class Shape(Color color)
{
    private bool _isEdit;
    protected readonly List<Line> BorderLinesList = [];
    protected List<Point> PointsList = [];
    
    public void Draw(Graphics g)
    {
        if (_isEdit)
        {
            SetBordersLines();
        }
        foreach (var line in BorderLinesList)
        {
            line.Draw(g);
        }
    }
    
    public virtual void SetCornersPoints(Point[] editRectCornersList) {}

    public virtual void SetCornersPoints(Point mousePos) {}

    public virtual void AddNewCorner(Point mousePos) {}

    protected virtual void SetBordersLines()
    {
        BorderLinesList.Clear();
        for (var i = 0; i < PointsList.Count; i++)
        {
            var startPoint = PointsList[i];
            var endPoint = PointsList[(i + 1) % PointsList.Count];
            BorderLinesList.Add(new Line(startPoint, endPoint, color));
        }
    }

    public Point[] GetEndPoints()
    {
        var minX = PointsList.Min(point => point.X);
        var minY = PointsList.Min(point => point.Y);
        var maxX = PointsList.Max(point => point.X);
        var maxY = PointsList.Max(point => point.Y);
        
        return [new Point(minX, minY), new Point(maxX, maxY)];
    }
    
    public void StartEdit()
    {
        _isEdit = true;
    }

    public virtual void StopEdit()
    {
        _isEdit = false;
    }
}