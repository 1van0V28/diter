namespace diter.Shapes;

public class Marker(Point pos) // в перспективе можно рассматривать как наследник shape
{
    private const int Width = 5;
    private readonly BrokenLine _bordersLines = new ([
        new Point(pos.X - Width, pos.Y - Width),
        new Point(pos.X + Width, pos.Y - Width),
        new Point(pos.X + Width, pos.Y + Width),
        new Point(pos.X - Width, pos.Y + Width)
    ], Color.Black, false, true);

    public void Draw(Graphics g)
    {
        _bordersLines.StartEdit();
        _bordersLines.SetFillColor(Color.White);
        _bordersLines.Draw(g);
    }

    public bool GetIsMouseDown(Point mousePos)
    {
        var isMouseDownX = pos.X - Width * 2 < mousePos.X && mousePos.X < pos.X + Width * 2;
        var isMouseDownY = pos.Y - Width * 2 < mousePos.Y && mousePos.Y < pos.Y + Width * 2;

        return (isMouseDownX && isMouseDownY);
    }
}