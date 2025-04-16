namespace diter;

public abstract class Shape
{
    public abstract void Draw(Graphics g);

    public abstract void ChangeEndCoors(Point newEnd);

    public abstract void CompleteEdit();

    public abstract Rectangle GetEditRectBorders();
    
    public abstract bool IsEdit();

    public abstract void StartEdit();
}