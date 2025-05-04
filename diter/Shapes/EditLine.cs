namespace diter.Shapes;

public class EditLine(Point start, Point end, Color color, Point? markerPos = null): Line(start, end, color)
{
    private Marker Marker => GetMarker();

    public override void Draw(Graphics g)
    {
        base.Draw(g);
        
        Marker.Draw(g);
    }
    
    private Marker GetMarker()
    {
        return new Marker(markerPos ?? PixelsList[PixelsList.Count / 2]);
    }

    public bool GetIsMouseDownMarker(Point mousePos)
    {
        return Marker.GetIsMouseDown(mousePos);
    }
}