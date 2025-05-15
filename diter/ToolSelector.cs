using diter.Shapes;
using diter.Tools;

namespace diter;

public class ToolSelector
{
    public Type ToolType { get; private set; } = typeof(ShapeTool);
    public Type ShapeType { get; private set; } = typeof(Polyline);
    public Color CurrentColor { get; private set; } = Color.Black;

    public void ToolButtonClick(object sender, EventArgs e)
    {
        var toolButton = (Button)sender;
        ToolType = toolButton.Name switch
        {
            "btnDelete" => typeof(DeleteTool),
            "btnFill" => typeof(FillTool),
            _ => ToolType
        };
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
            colorDialog.Color = CurrentColor;

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                CurrentColor = colorDialog.Color;
                pnlColorView.BackColor = CurrentColor;
            }
        }
    }

    public void ColorButtonClick(object sender, Panel pnlColorView)
    {
        var colorButton = (Button)sender;
        CurrentColor = colorButton.BackColor;
        pnlColorView.BackColor = CurrentColor;
    }
}