namespace diter.Shapes;

public class BrokenLine(List<Point> cornersPointsList, Color color): Shape(color)
{
    private bool _isClosed;
    private readonly Color _color = color;

    public override void SetCornersPoints(Point mousePos)
    {
        cornersPointsList[^1] = mousePos;
        PointsList = cornersPointsList;
    }

    public override void AddNewCorner(Point mousePos)
    {
        if (mousePos == PointsList[0])
        {
            _isClosed = true;
        }
        else
        {
            cornersPointsList[^1] = mousePos;
            cornersPointsList.Add(mousePos);
        }
    }

    protected override void SetBordersLines()
    {
        if (_isClosed)
        {
            base.SetBordersLines();
        }
        else
        {
            BorderLinesList.Clear();
            for (var i = 0; i < PointsList.Count - 1; i++)
            {
                var startPoint = PointsList[i];
                var endPoint = PointsList[(i + 1) % PointsList.Count];
                BorderLinesList.Add(new Line(startPoint, endPoint, _color));
            }
        }
    }

    public override void StopEdit()
    {
        base.StopEdit();
        cornersPointsList.Remove(PointsList[^1]);
        SetBordersLines();
    }
}