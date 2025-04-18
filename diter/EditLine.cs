namespace diter;

public class EditLine(Point start, Point end, Color color): Line(start, end, color)
{
    private Marker? _marker;

    public new void Draw(Graphics g)
    {
        base.Draw(g);
        
        this.SetMarker();
        if (_marker != null)
        {
            this._marker.Draw(g);
        }
    }
    
    private void SetMarker()
    {
        base.SetBrezenhamPixels();
        if (base.PixelsList != null)
        {
            var markerPos = base.PixelsList[base.PixelsList.Count / 2];
            this._marker = new Marker(markerPos);
        }
    }

    public bool GetIsMouseDownMarker(Point mousePos)
    {
        return this._marker?.GetIsMouseDown(mousePos) ?? false;
    }
}