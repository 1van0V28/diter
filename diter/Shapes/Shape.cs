namespace diter.Shapes;

public class Shape(Color color)
{
    private bool _isEdit;
    private readonly List<Line> _borderLinesList = [];
    protected readonly List<Point> PointsList = [];
    
    public void Draw(Graphics g)
    {
        if (_isEdit)
        {
            SetBordersLines();
        }
        foreach (var line in _borderLinesList)
        {
            line.Draw(g);
        }
    }
    
    public virtual void SetCornersPoints(Point[] editRectCornersList) {}

    private void SetBordersLines()
    {
        _borderLinesList.Clear();
        for (var i = 0; i < PointsList.Count; i++)
        {
            var startPoint = PointsList[i];
            var endPoint = PointsList[(i + 1) % PointsList.Count];
            _borderLinesList.Add(new Line(startPoint, endPoint, color));
        }
    }
    
    public void StartEdit()
    {
        _isEdit = true;
    }

    public void StopEdit()
    {
        _isEdit = false;
    }
}