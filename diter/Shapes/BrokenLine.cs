namespace diter.Shapes;

public class BrokenLine(List<Point> originalVerticesList, Color borderColor, bool isEditable = false, bool isClosed = false)
{
    public List<Point> OriginalVerticesList { get; private set; } = originalVerticesList;
    private readonly List<Point> _verticesList = [..originalVerticesList];
    private readonly List<Line> _linesList = [];

    private readonly BitMask _bitMask = new BitMask();
    private bool _isEdit = true;
    private Color _fillColor = Color.Transparent;
    public void Draw(Graphics g)
    {
        if (_isEdit)
        {
            UpdateLines();
            if (_verticesList.Count != 0 && _fillColor != Color.Transparent && isClosed && !isEditable)
            {
                _bitMask.UpdateBitMask(GetEndPoints(), _verticesList);
            }
        }
        
        if (_fillColor != Color.Transparent && isClosed && !isEditable)
        { 
            _bitMask.Draw(g, GetEndPoints(), _fillColor);
        }
        
        foreach (var line in _linesList)
        {
            line.Draw(g);
        }
    }

    public void SetOriginalVerticesList()
    {
        OriginalVerticesList = [.._verticesList];
    }
    
    private void UpdateLines()
    {
        _linesList.Clear();
        var verticesListCount = isClosed ? _verticesList.Count : _verticesList.Count - 1;
        for (var i = 0; i < verticesListCount; i++) 
        { 
            var startPoint = _verticesList[i]; 
            var endPointIndex = isClosed ? (i + 1) % verticesListCount : i + 1;
            var endPoint = _verticesList[endPointIndex];
            var line = isEditable ?
                new EditLine(startPoint, endPoint, borderColor) :
                new Line(startPoint, endPoint, borderColor);
            
            _linesList.Add(line);
        }
    }

    public int GetMouseDownMarkerIndex(Point mousePos)
    {
        if (!isEditable)
        {
            return -1;
        }
        
        for (var i = 0; i < _linesList.Count; i++) 
        { 
            var line = _linesList[i] as EditLine;
            if (line!.GetIsMouseDownMarker(mousePos)) 
            { 
                return i;
            }
        } 
        return -1;
    } 
    
    public Point[] GetEndPoints()
    {
        var minX = _verticesList.Min(point => point.X);
        var minY = _verticesList.Min(point => point.Y);
        var maxX = _verticesList.Max(point => point.X);
        var maxY = _verticesList.Max(point => point.Y);
        
        return [new Point(minX, minY), new Point(maxX, maxY)];
    }

    public void Resize(List<Point> originalVerticesList, List<Point> verticesList, int firstPointIndex, int secondPointIndex)
    {
        // Вычисляем центры прямоугольников
        var oldCenterX = originalVerticesList.Average(p => p.X);
        var oldCenterY = originalVerticesList.Average(p => p.Y);
        var newCenterX = verticesList.Average(p => p.X);
        var newCenterY = verticesList.Average(p => p.Y);

        var sideIndex = firstPointIndex;
        var nextIndex = secondPointIndex;
        var oppSideIndex = (sideIndex + 2) % 4;

        // Вектор стороны (ширина)
        double oldDxWidth = originalVerticesList[nextIndex].X - originalVerticesList[sideIndex].X;
        double oldDyWidth = originalVerticesList[nextIndex].Y - originalVerticesList[sideIndex].Y;
        var oldWidth = Math.Sqrt(oldDxWidth * oldDxWidth + oldDyWidth * oldDyWidth);

        // Вектор высоты
        double oldDxHeight = originalVerticesList[oppSideIndex].X - originalVerticesList[sideIndex].X;
        double oldDyHeight = originalVerticesList[oppSideIndex].Y - originalVerticesList[sideIndex].Y;
        var perpDx = -oldDyWidth;
        var perpDy = oldDxWidth;
        var perpLength = Math.Sqrt(perpDx * perpDx + perpDy * perpDy);
        if (perpLength == 0) perpLength = 1;
        var oldHeight = Math.Abs((oldDxHeight * perpDx + oldDyHeight * perpDy) / perpLength);

        // Новые размеры
        double newDxWidth = verticesList[nextIndex].X - verticesList[sideIndex].X;
        double newDyWidth = verticesList[nextIndex].Y - verticesList[sideIndex].Y;
        var newWidth = Math.Sqrt(newDxWidth * newDxWidth + newDyWidth * newDyWidth);

        double newDxHeight = verticesList[oppSideIndex].X - verticesList[sideIndex].X;
        double newDyHeight = verticesList[oppSideIndex].Y - verticesList[sideIndex].Y;
        var newPerpDx = -newDyWidth;
        var newPerpDy = newDxWidth;
        var newPerpLength = Math.Sqrt(newPerpDx * newPerpDx + newPerpDy * perpDy);
        if (newPerpLength == 0) newPerpLength = 1;
        var newHeight = Math.Abs((newDxHeight * newPerpDx + newDyHeight * newPerpDy) / newPerpLength);

        // Масштабы
        var scaleX = oldWidth != 0 ? newWidth / oldWidth : 1.0;
        var scaleY = oldHeight != 0 ? newHeight / oldHeight : 1.0;

        // Угол поворота
        var angle = Math.Atan2(oldDyWidth, oldDxWidth);
        var cosAngle = Math.Cos(angle);
        var sinAngle = Math.Sin(angle);

        // Границы нового прямоугольника
        double minX = verticesList.Min(p => p.X);
        double maxX = verticesList.Max(p => p.X);
        double minY = verticesList.Min(p => p.Y);
        double maxY = verticesList.Max(p => p.Y);

        // Масштабируем вершины ломаной
        _verticesList.Clear();
        foreach (var point in OriginalVerticesList)
        {
            // Смещение и преобразование в локальную систему
            var offsetX = point.X - oldCenterX;
            var offsetY = point.Y - oldCenterY;
            var localX = offsetX * cosAngle + offsetY * sinAngle;
            var localY = -offsetX * sinAngle + offsetY * cosAngle;

            // Масштабирование
            var scaledLocalX = localX * scaleX;
            var scaledLocalY = localY * scaleY;

            // Обратное преобразование в глобальную систему
            var globalX = scaledLocalX * cosAngle - scaledLocalY * sinAngle;
            var globalY = scaledLocalX * sinAngle + scaledLocalY * cosAngle;

            // Смещение к новому центру
            var newX = newCenterX + globalX;
            var newY = newCenterY + globalY;

            // Ограничиваем координаты внутри нового прямоугольника
            newX = Math.Max(minX, Math.Min(maxX, newX));
            newY = Math.Max(minY, Math.Min(maxY, newY));

            // Добавляем вершину
            _verticesList.Add(new Point((int)Math.Round(newX), (int)Math.Round(newY)));
        }
        UpdateLines();
    }

    public void Rotate(Point center, double angleRadians)
    {
        for (var i = 0; i < _verticesList.Count; i++)
        {
            _verticesList[i] = GetRotatedPoint(OriginalVerticesList[i], center, angleRadians);
        }
        UpdateLines();
    }

    private static Point GetRotatedPoint(Point originalPoint, Point center, double angleRadians)
    {
        double relativeX = originalPoint.X - center.X;
        double relativeY = originalPoint.Y - center.Y;

        var cosTheta = Math.Cos(angleRadians);
        var sinTheta = Math.Sin(angleRadians);

        // Вычисления с double для большей точности
        var newX = center.X + (relativeX * cosTheta - relativeY * sinTheta);
        var newY = center.Y + (relativeX * sinTheta + relativeY * cosTheta);

        // Приведение к int с округлением
        return new Point((int)Math.Round(newX), (int)Math.Round(newY));
    }

    public void Drag(int deltaX, int deltaY)
    {
        for (var i = 0; i < _verticesList.Count; i++)
        {
            _verticesList[i] = new Point(
                    OriginalVerticesList[i].X + deltaX,
                    OriginalVerticesList[i].Y + deltaY
                    );
        }
        UpdateLines();
    }

    public void StartEdit()
    {
        _isEdit = true;
    }

    public void StopEdit()
    {
        _isEdit = false;
    }

    public void SetFillColor(Color color)
    {
        _fillColor = color;
    }
}