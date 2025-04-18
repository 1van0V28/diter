namespace diter;

public class EditLine(Point start, Point end, Color color): Line(start, end, color)
{
    private Marker? Marker => GetMarker();

    public new void Draw(Graphics g)
    {
        base.Draw(g);
        
        Marker?.Draw(g);
    }
    
    private Marker? GetMarker()
    {
        if (PixelsList != null)
        {
            var markerPos = PixelsList[PixelsList.Count / 2];
            return new Marker(markerPos);
        }

        return null;
    }

    public bool GetIsMouseDownMarker(Point mousePos)
    {
        return Marker?.GetIsMouseDown(mousePos) ?? false;
    }
}