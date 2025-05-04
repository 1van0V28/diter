namespace diter.Shapes;

public class Frame(Point start, Shape shape)
{ 
    public bool IsEdit { get; private set; }
    private bool _isResizing;
    private bool _isRotating;
    private bool _isDragging;
    private bool _isAddingNewCorner;
    private double _radiusCircumCircle;
    private int? _resizeMarkerIndex;
    private Point? _mouseStartPos;
    private Point _center;
    private List<Point> _originalVerticesList = [];
    private BrokenLine _frameBorder = new ([], Color.Black, true, true);
    private BrokenLine _frameRotateLever = new ([], Color.Black, true, true);
    
    public void Draw(Graphics g)
    {
        if (IsEdit)
        {
            _frameBorder.Draw(g);
            _frameRotateLever.Draw(g);
        }
        shape.Draw(g);
    }

    private void SetFrameBorder(Point[] endPoints)
    {
        var topLeftX = endPoints[0].X; 
        var topLeftY = endPoints[0].Y; 
        var bottomRightX = endPoints[1].X; 
        var bottomRightY = endPoints[1].Y;
        
        var originalVerticesList = new List<Point>
        {
            new (topLeftX, topLeftY),
            new (bottomRightX, topLeftY),
            new (bottomRightX, bottomRightY),
            new (topLeftX, bottomRightY),
        };
        _frameBorder = new BrokenLine(originalVerticesList, Color.Black, true, true);
    }

    private void SetCenterPoint()
    { 
        if (_isRotating) return;

        var cornersPointsList = _frameBorder.OriginalVerticesList;
        double centerX = Math.Abs(cornersPointsList[0].X + (cornersPointsList[2].X - cornersPointsList[0].X) / 2); 
        double centerY = Math.Abs(cornersPointsList[0].Y + (cornersPointsList[2].Y - cornersPointsList[0].Y) / 2);
        
        _center = new Point((int)centerX, (int)centerY);
    }
    
    private void SetFrameRotateLever()
    {
        var cornersPointsList = _frameBorder.OriginalVerticesList;
        var width = Math.Sqrt(
            Math.Pow(cornersPointsList[1].X - cornersPointsList[0].X, 2)
            + 
            Math.Pow(cornersPointsList[1].Y - cornersPointsList[0].Y, 2)
            ); 
        var height = Math.Sqrt(
            Math.Pow(cornersPointsList[3].X - cornersPointsList[0].X, 2)
            + 
            Math.Pow(cornersPointsList[3].Y - cornersPointsList[0].Y, 2)
        ); 
        _radiusCircumCircle = Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(height / 2, 2));
        _frameRotateLever = new BrokenLine([_center, new Point(_center.X, _center.Y - (int)_radiusCircumCircle)], Color.Black, true);
    }
        
    public bool GetIsMouseDown(Point mousePos)
    { 
        var cornersPointsList = _frameBorder.OriginalVerticesList;
        if (cornersPointsList.Count == 0) return false;
        // Определяем угол поворота по стороне V_0-V_1
        var dx = cornersPointsList[1].X - cornersPointsList[0].X;
        var dy = cornersPointsList[1].Y - cornersPointsList[0].Y;
        double angle = (float)Math.Atan2(dy, dx);
        double cosAngle = (float)Math.Cos(-angle); // Обратный поворот
        double sinAngle = (float)Math.Sin(-angle);

        // Преобразуем координаты мыши в локальную систему
        var offsetX = mousePos.X - _center.X;
        var offsetY = mousePos.Y - _center.Y;
        var localX = offsetX * cosAngle - offsetY * sinAngle;
        var localY = offsetX * sinAngle + offsetY * cosAngle;

        // Находим границы прямоугольника в локальной системе
        double minX = double.MaxValue, maxX = double.MinValue;
        double minY = double.MaxValue, maxY = double.MinValue;
        foreach (var vertex in cornersPointsList)
        {
            var vx = vertex.X - _center.X;
            var vy = vertex.Y - _center.Y;
            var localVx = vx * cosAngle - vy * sinAngle;
            var localVy = vx * sinAngle + vy * cosAngle;
            minX = Math.Min(minX, localVx);
            maxX = Math.Max(maxX, localVx);
            minY = Math.Min(minY, localVy);
            maxY = Math.Max(maxY, localVy);
        }

        // Проверяем, находится ли точка внутри прямоугольника
        return localX >= minX && localX <= maxX && localY >= minY && localY <= maxY;
    }
        
    public int GetMouseDownMarkerIndex(Point mousePos)
    {
        var mouseDownMarkerBorderIndex = _frameBorder.GetMouseDownMarkerIndex(mousePos);
        if (mouseDownMarkerBorderIndex != -1)
        {
            return mouseDownMarkerBorderIndex;
        }
        
        var mouseDownMarkerLeverIndex = _frameRotateLever.GetMouseDownMarkerIndex(mousePos);
        return mouseDownMarkerLeverIndex == -1 ? mouseDownMarkerLeverIndex : 4;
    }

    private void ResizeFrame(Point mousePos)
    { 
        switch (_resizeMarkerIndex) 
        { 
            case 0: ResizeSide(mousePos, 0, 1); 
                break;
            case 1: ResizeSide(mousePos, 1, 2); 
                break;
            case 2: ResizeSide(mousePos, 2, 3); 
                break;
            case 3: ResizeSide(mousePos, 3, 0); 
                break;
        }
    }
    
    private void ResizeSide(Point mousePos, int firstPointIndex, int secondPointIndex)
    { 
        if (_mouseStartPos == null) return; // Проверка, что масштабирование начато
        
        // Вектор перетягиваемой стороны
        var sideX = _originalVerticesList[secondPointIndex].X - _originalVerticesList[firstPointIndex].X;
        var sideY = _originalVerticesList[secondPointIndex].Y - _originalVerticesList[firstPointIndex].Y;

        // Перпендикулярный вектор (направление изменения размера)
        double normalX = sideY; // (y2 - y1)
        double normalY = -sideX; // -(x2 - x1)

        // Нормализуем перпендикулярный вектор
        var length = Math.Sqrt(normalX * normalX + normalY * normalY);
        if (length == 0) return; // Избегаем деления на ноль
        normalX /= length;
        normalY /= length;

        // Проецируем смещение мыши на перпендикулярное направление
        var dx = mousePos.X - _mouseStartPos.Value.X;
        var dy = mousePos.Y - _mouseStartPos.Value.Y;
        var projection = dx * normalX + dy * normalY;

        // Вычисляем знаковое расстояние между перетягиваемой и противоположной сторонами
        // Берем средние точки сторон
        var oppFirstPointIndex = (firstPointIndex + 3) % _originalVerticesList.Count;
        var oppSecondPointIndex = (secondPointIndex + 1) % _originalVerticesList.Count;
        double sideMidX = (_originalVerticesList[firstPointIndex].X + _originalVerticesList[secondPointIndex].X) / 2f;
        double sideMidY = (_originalVerticesList[firstPointIndex].Y + _originalVerticesList[secondPointIndex].Y) / 2f;
        double oppSideMidX = (_originalVerticesList[oppFirstPointIndex].X + _originalVerticesList[oppSecondPointIndex].X) / 2f;
        double oppSideMidY = (_originalVerticesList[oppFirstPointIndex].Y + _originalVerticesList[oppSecondPointIndex].Y) / 2f;

        // Вектор от противоположной стороны к перетягиваемой
        var distX = sideMidX - oppSideMidX;
        var distY = sideMidY - oppSideMidY;

        // Знаковое расстояние (проекция на нормаль)
        var signedDistance = distX * normalX + distY * normalY;

        // Ограничиваем проекцию только для уменьшения размера
        if (projection < 0) // Пользователь уменьшает размер
        {
            // Минимальное допустимое расстояние между сторонами
            const double minSize = 10f;
            // Ограничиваем отрицательное смещение, чтобы не пересекать противоположную сторону
            var maxNegativeProjection = -(Math.Abs(signedDistance) - minSize);
            projection = Math.Max(projection, maxNegativeProjection);
        }
        // Для положительного projection (увеличение размера) ограничений нет

        // Обновляем координаты вершин перетягиваемой стороны
        var firstPoint = new Point(
            (int)(_originalVerticesList[firstPointIndex].X + projection * normalX),
            (int)(_originalVerticesList[firstPointIndex].Y + projection * normalY)
        );
        var secondPoint = new Point(
            (int)(_originalVerticesList[secondPointIndex].X + projection * normalX),
            (int)(_originalVerticesList[secondPointIndex].Y + projection * normalY)
        );
        var cornersPointsArray = new Point[4];
        cornersPointsArray[firstPointIndex] = firstPoint;
        cornersPointsArray[secondPointIndex] = secondPoint;
        cornersPointsArray[oppFirstPointIndex] = _originalVerticesList[oppFirstPointIndex];
        cornersPointsArray[oppSecondPointIndex] = _originalVerticesList[oppSecondPointIndex];
        var cornersPointsList = cornersPointsArray.ToList();
        
        shape.Resize(_originalVerticesList, cornersPointsList, firstPointIndex, secondPointIndex);
        _frameRotateLever.Resize(_originalVerticesList, cornersPointsList, firstPointIndex, secondPointIndex);
        _frameBorder = new BrokenLine(cornersPointsList, Color.Black, true, true);
    }
    
    private void RotateFrame(Point mousePos)
    {
        if (_mouseStartPos == null) return; // Проверка, что вращение начато

        // Вычисление угла поворота относительно начальной позиции
        double dxStart = _mouseStartPos.Value.X - _center.X;
        double dyStart = _mouseStartPos.Value.Y - _center.Y;
        double dxEnd = mousePos.X - _center.X;
        double dyEnd = mousePos.Y - _center.Y;

        var angleStart = Math.Atan2(dyStart, dxStart);
        var angleEnd = Math.Atan2(dyEnd, dxEnd);
        var angleRadians = angleEnd - angleStart;

        shape.Rotate(_center, angleRadians);
        _frameRotateLever.Rotate(_center, angleRadians);
        _frameBorder.Rotate(_center, angleRadians);
    }
    
    private void DragFrame(Point mousePos)
    {
        if (_mouseStartPos == null) return; // Проверка, что перемещение начато
    
        var deltaX = mousePos.X - _mouseStartPos.Value.X;
        var deltaY = mousePos.Y - _mouseStartPos.Value.Y;
        
        shape.Drag(deltaX, deltaY);
        _frameBorder.Drag(deltaX, deltaY);
        _frameRotateLever.Drag(deltaX, deltaY);
    }
    
    private void SetFrame(Point mousePos)
    { 
        var topLeftX = Math.Min(start.X, mousePos.X); 
        var topLeftY = Math.Min(start.Y, mousePos.Y); 
        var bottomRightX = Math.Max(start.X, mousePos.X); 
        var bottomRightY = Math.Max(start.Y, mousePos.Y);
        
        var originalVerticesList = new List<Point>
        {
            new (topLeftX, topLeftY),
            new (bottomRightX, topLeftY),
            new (bottomRightX, bottomRightY),
            new (topLeftX, bottomRightY)
        };
        _frameBorder = new BrokenLine(originalVerticesList, Color.Black, true, true);
        shape.SetBordersLines(_frameBorder.OriginalVerticesList);
        SetFrameRotateLever();
    }

    private void AddNewCornerToFrame(Point mousePos)
    {
        // Обновляем последнюю точку ломаной и фрейм
        shape.SetBordersLines(mousePos);
        SetFrameBorder(shape.GetEndPoints());
        SetFrameRotateLever();
    }
    
    public void EditFrame(Point mousePos)
    {
        if (_isResizing)
        { 
            ResizeFrame(mousePos);
        } else if (_isRotating)
        { 
            RotateFrame(mousePos);
        } else if (_isDragging) 
        {
            DragFrame(mousePos);
        } 
        else if (_isAddingNewCorner)
        {
            AddNewCornerToFrame(mousePos);
        }
        else
        {
            SetFrame(mousePos);
        }
        SetCenterPoint();
    }
    
    public void StartEdit() 
    { 
        IsEdit = true;
    }

    public void StopEdit()
    {
        // Фиксируем изменения
        shape.SetOriginalVerticesList();
        _frameRotateLever.SetOriginalVerticesList();
        _frameBorder.SetOriginalVerticesList();
        
        IsEdit = false;
        _isResizing = false; 
        _isRotating = false; 
        _isDragging = false;
        _isAddingNewCorner = false;
        _resizeMarkerIndex = null; 
        _mouseStartPos = null; 
    }
    
    public void StartResize(int resizeMarkerIndex, Point mousePos) 
    { 
        _isResizing = true;
        _resizeMarkerIndex = resizeMarkerIndex; 
        _mouseStartPos = mousePos; 
        // Сохраняем исходные координаты вершин перед началом масштабирования
        _originalVerticesList = _frameBorder.OriginalVerticesList;
    }
    
    public void StartRotate(Point mousePos)
    { 
        _isRotating = true;
        _mouseStartPos = mousePos; 
    }
    
    public void StartDrag(Point mousePos)
    { 
        _isDragging = true; 
        _mouseStartPos = mousePos; 
    }

    public void StartAddNewCorner()
    {
        _isAddingNewCorner = true;
    }

    public void AddNewCorner(Point mousePos)
    {
        IsEdit = true;
        shape.AddNewCorner(mousePos);
    }
}