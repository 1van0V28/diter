using diter.Shapes;

namespace diter;

public class ToolSelector
{
    public Type ShapeType { get; private set; } = typeof(Polyline);
    public Color Color { get; private set; } = Color.Black;

    public void ToolButtonClick(object sender, EventArgs e)
    {
        throw new NotImplementedException("ToolButtonClick");
    }

    public void ShapeButtonClick(object sender, EventArgs e)
    {
        var shapeButton = (Button)sender;
        ShapeType = shapeButton.Name switch
        {
            "btnPolyline" => typeof(Polyline),
            "btnBezierCurve" => typeof(BezierCurve),
            "btnEllipse" => typeof(Ellipse),
            "btnRect" => typeof(Rect),
            "btnTriangle" => typeof(Triangle),
            "btnPentagon" => typeof(Pentagon),
            "btnTrapezoid" => typeof(Trapezoid),
            _ => ShapeType
        };
    }

    public void ColorOtherButtonClick(Panel pnlColorView)
    {
        using (var colorDialog = new ColorDialog())
        {
            colorDialog.FullOpen = true;
            colorDialog.Color = Color;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color = colorDialog.Color;
                pnlColorView.BackColor = Color;
            }
        }
    }

    public void ColorButtonClick(object sender, Panel pnlColorView)
    {
        var colorButton = (Button)sender;
        Color = colorButton.BackColor;
        pnlColorView.BackColor = Color;
    }
}