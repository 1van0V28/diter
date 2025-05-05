namespace diter.Shapes;

public class Shape(Color color)
{
    protected BrokenLine BordersLines = new([], color);

    public void StartEdit()
    {
        BordersLines.StartEdit();
    }

    public void StopEdit()
    {
        BordersLines.StopEdit();
    }

    public void FillShape(Color color)
    {
        BordersLines.SetFillColor(color);
    }
    
    public void Draw(Graphics g)
    {
        BordersLines.Draw(g);
    }

    public void SetOriginalVerticesList()
    {
        BordersLines.SetOriginalVerticesList();
    }

    public void Resize(List<Point> originalVerticesList, List<Point> verticesList, int firstPointIndex, int secondPointIndex)
    {
        BordersLines.Resize(originalVerticesList, verticesList, firstPointIndex, secondPointIndex);
    }

    public void Rotate(Point center, double angleRadians)
    {
        BordersLines.Rotate(center, angleRadians);
    }

    public void Drag(int deltaX, int deltaY)
    {
        BordersLines.Drag(deltaX, deltaY);
    }

    public virtual void SetBordersLines(List<Point> editRectCornersList)
    {
        throw new NotImplementedException("This shape is line");
    }

    public virtual void SetBordersLines(Point mousePos)
    {
        throw new NotImplementedException("This shape is parametric");
    }

    public virtual void AddNewCorner(Point mousePos)
    {
        throw new NotImplementedException("This shape is parametric");
    }

    public Point[] GetEndPoints()
    {
        return BordersLines.GetEndPoints();
    }
}