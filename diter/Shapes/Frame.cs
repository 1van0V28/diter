namespace diter.Shapes;

public class Frame(Point start, Shape shape)
{ 
    public bool IsEdit { get; private set; }
    private bool _isResizing;
    private bool _isRotating;
    private bool _isDragging;
    private bool _isAddingNewCorner;
    private int _radiusCircumCircle;
    private int? _resizeMarkerIndex;
    private Point? _mouseStartPos;
    private Point _center;
    private Point[]? _originalCornersPointsList; // вершины на момент начала изменений
    private readonly Point[] _cornersPointsList = new Point[4];
    private readonly EditLine[] _borderLinesList = new EditLine[5];
    
    public void Draw(Graphics g)
    {
        if (IsEdit) // отображаем обводку
        {
            foreach (var line in _borderLinesList)
            { 
                line.Draw(g);
            }
        }
        shape.Draw(g);
    }

    private void UpdateFrameBorders(Point[] endPoints)
    {
        var topLeftX = endPoints[0].X; 
        var topLeftY = endPoints[0].Y; 
        var bottomRightX = endPoints[1].X; 
        var bottomRightY = endPoints[1].Y;
        
        _cornersPointsList[0] = new Point(topLeftX, topLeftY); 
        _cornersPointsList[1] = new Point(bottomRightX, topLeftY); 
        _cornersPointsList[2] = new Point(bottomRightX, bottomRightY); 
        _cornersPointsList[3] = new Point(topLeftX, bottomRightY);
    }

    private void SetCenterPoint()
    { 
        if (_isRotating) return;
        
        var centerX = Math.Abs(_cornersPointsList[0].X + (_cornersPointsList[2].X - _cornersPointsList[0].X) / 2); 
        var centerY = Math.Abs(_cornersPointsList[0].Y + (_cornersPointsList[2].Y - _cornersPointsList[0].Y) / 2);
        
        _center = new Point(centerX, centerY);
    }
    private void SetBordersLines()
    { 
        for (var i = 0; i < _cornersPointsList.Length; i++) 
        { 
            var startPoint = _cornersPointsList[i]; 
            var endPoint = _cornersPointsList[(i + 1) % _cornersPointsList.Length];
            
            _borderLinesList[i] = new EditLine(startPoint, endPoint, Color.Black);
        }
        
        var rotateMarkerPos = new Point(_center.X, _center.Y - _radiusCircumCircle);
        _borderLinesList[4] = new EditLine(_center, rotateMarkerPos, Color.Black, rotateMarkerPos);
    }
    
    private void SetRadiusCircumCircle() 
    { 
        var width = (double)_cornersPointsList[1].X - _cornersPointsList[0].X; 
        var height = (double)_cornersPointsList[3].Y - _cornersPointsList[0].Y;
        
        _radiusCircumCircle = (int)Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(height / 2, 2));
    }
        
    public bool GetIsMouseDown(Point mousePos) // переделать с учётом поворота
    { 
        var isMouseDownX = _cornersPointsList[0].X < mousePos.X && mousePos.X < _cornersPointsList[1].X; 
        var isMouseDownY = _cornersPointsList[0].Y < mousePos.Y && mousePos.Y < _cornersPointsList[3].Y;
        
        return (isMouseDownX && isMouseDownY);
    }
        
    public int GetMouseDownMarkerIndex(Point mousePos) 
    { 
        for (var i = 0; i < _borderLinesList.Length; i++) 
        { 
            if (_borderLinesList[i].GetIsMouseDownMarker(mousePos)) 
            { 
                return i;
            }
        } 
        return -1;
    }

    private void ResizeFrame(Point mousePos)
    { 
        switch (_resizeMarkerIndex) 
        { 
            case 0: ResizeSide(mousePos, 0, 1); 
                return;
            case 1: ResizeSide(mousePos, 1, 2); 
                return;
            case 2: ResizeSide(mousePos, 2, 3); 
                return;
            case 3: ResizeSide(mousePos, 3, 0); 
                return;
        }
    }
    
    private void ResizeSide(Point mousePos, int firstPointIndex, int secondPointIndex)
    { 
        if (_mouseStartPos == null || _originalCornersPointsList == null) return; // проверка, что масштабирование начато
        
        // Вектор перетягиваемой стороны
        float sideX = _originalCornersPointsList[secondPointIndex].X - _originalCornersPointsList[firstPointIndex].X;
        float sideY = _originalCornersPointsList[secondPointIndex].Y - _originalCornersPointsList[firstPointIndex].Y;

        // Перпендикулярный вектор (направление изменения размера)
        float normalX = sideY; // (y2 - y1)
        float normalY = -sideX; // -(x2 - x1)

        // Нормализуем перпендикулярный вектор
        float length = (float)Math.Sqrt(normalX * normalX + normalY * normalY);
        if (length == 0) return; // Избегаем деления на ноль
        normalX /= length;
        normalY /= length;

        // Проецируем смещение мыши на перпендикулярное направление
        float dx = mousePos.X - _mouseStartPos.Value.X;
        float dy = mousePos.Y - _mouseStartPos.Value.Y;
        float projection = dx * normalX + dy * normalY;

        // Вычисляем знаковое расстояние между перетягиваемой и противоположной сторонами
        // Берем средние точки сторон
        var oppFirstPointIndex = (firstPointIndex + 3) % _cornersPointsList.Length;
        var oppSecondPointIndex = (secondPointIndex + 1) % _cornersPointsList.Length;
        float sideMidX = (_originalCornersPointsList[firstPointIndex].X + _originalCornersPointsList[secondPointIndex].X) / 2f;
        float sideMidY = (_originalCornersPointsList[firstPointIndex].Y + _originalCornersPointsList[secondPointIndex].Y) / 2f;
        float oppSideMidX = (_originalCornersPointsList[oppFirstPointIndex].X + _originalCornersPointsList[oppSecondPointIndex].X) / 2f;
        float oppSideMidY = (_originalCornersPointsList[oppFirstPointIndex].Y + _originalCornersPointsList[oppSecondPointIndex].Y) / 2f;

        // Вектор от противоположной стороны к перетягиваемой
        float distX = sideMidX - oppSideMidX;
        float distY = sideMidY - oppSideMidY;

        // Знаковое расстояние (проекция на нормаль)
        float signedDistance = distX * normalX + distY * normalY;

        // Ограничиваем проекцию только для уменьшения размера
        if (projection < 0) // Пользователь уменьшает размер
        {
            // Минимальное допустимое расстояние между сторонами
            float minSize = 10f;
            // Ограничиваем отрицательное смещение, чтобы не пересекать противоположную сторону
            float maxNegativeProjection = -(Math.Abs(signedDistance) - minSize);
            projection = Math.Max(projection, maxNegativeProjection);
        }
        // Для положительного projection (увеличение размера) ограничений нет

        // Обновляем координаты вершин перетягиваемой стороны
        _cornersPointsList[firstPointIndex] = new Point(
            (int)(_originalCornersPointsList[firstPointIndex].X + projection * normalX),
            (int)(_originalCornersPointsList[firstPointIndex].Y + projection * normalY)
        );
        _cornersPointsList[secondPointIndex] = new Point(
            (int)(_originalCornersPointsList[secondPointIndex].X + projection * normalX),
            (int)(_originalCornersPointsList[secondPointIndex].Y + projection * normalY)
        );
    }
    
    private void RotateFrame(Point mousePos)
    {
        if (_mouseStartPos == null || _originalCornersPointsList == null) return; // Проверка, что вращение начато

        // Вычисление угла поворота относительно начальной позиции
        float dxStart = _mouseStartPos.Value.X - _center.X;
        float dyStart = _mouseStartPos.Value.Y - _center.Y;
        float dxEnd = mousePos.X - _center.X;
        float dyEnd = mousePos.Y - _center.Y;

        var angleStart = (float)Math.Atan2(dyStart, dxStart);
        var angleEnd = (float)Math.Atan2(dyEnd, dxEnd);
        var angleRadians = angleEnd - angleStart;

        // Поворот всех вершин относительно исходных координат
        for (var i = 0; i < _cornersPointsList.Length; i++)
        {
            _cornersPointsList[i] = GetRotatedPoint(_originalCornersPointsList[i], angleRadians);
        }
    }

    private Point GetRotatedPoint(Point point, float angleRadians)
    {
        float relativeX = point.X - _center.X;
        float relativeY = point.Y - _center.Y;

        var cosTheta = Math.Cos(angleRadians);
        var sinTheta = Math.Sin(angleRadians);

        // Вычисления с float для большей точности
        float newX = _center.X + (float)(relativeX * cosTheta - relativeY * sinTheta);
        float newY = _center.Y + (float)(relativeX * sinTheta + relativeY * cosTheta);

        // Приведение к int с округлением
        return new Point((int)Math.Round(newX), (int)Math.Round(newY));
    }
    
    private void DragFrame(Point mousePos)
    {
        if (_mouseStartPos == null || _originalCornersPointsList == null) return; // проверка, что перемещение начато
    
        var deltaX = mousePos.X - _mouseStartPos.Value.X;
        var deltaY = mousePos.Y - _mouseStartPos.Value.Y;
        
        for (var i = 0; i < _cornersPointsList.Length; i++) 
        { 
            _cornersPointsList[i].X = _originalCornersPointsList[i].X + deltaX; 
            _cornersPointsList[i].Y = _originalCornersPointsList[i].Y + deltaY;
        }
    }
    
    private void AddFrame(Point mousePos)
    { 
        var topLeftX = Math.Min(start.X, mousePos.X); 
        var topLeftY = Math.Min(start.Y, mousePos.Y); 
        var bottomRightX = Math.Max(start.X, mousePos.X); 
        var bottomRightY = Math.Max(start.Y, mousePos.Y);
        
        _cornersPointsList[0] = new Point(topLeftX, topLeftY); 
        _cornersPointsList[1] = new Point(bottomRightX, topLeftY); 
        _cornersPointsList[2] = new Point(bottomRightX, bottomRightY); 
        _cornersPointsList[3] = new Point(topLeftX, bottomRightY);
    }

    private void UpdateFrame()
    {
        SetCenterPoint();
        SetRadiusCircumCircle();
        SetBordersLines();
        shape.SetCornersPoints(_cornersPointsList);
    }
    
    public void EditFrame(Point mousePos)
    {
        shape.StartEdit();
        if (_isDragging) 
        {
            DragFrame(mousePos);
        } 
        else if (_isRotating)
        {
            RotateFrame(mousePos);
        }
        else if (_isResizing)
        {
            ResizeFrame(mousePos); 
        }
        else if (_isAddingNewCorner)
        {
            shape.SetCornersPoints(mousePos); // важен именно такой порядок, потому что обновляется последняя точка ломаной
            UpdateFrameBorders(shape.GetEndPoints());
            SetCenterPoint();
            SetRadiusCircumCircle();
            SetBordersLines();
            return;
        }
        else
        {
            AddFrame(mousePos);
        }
        UpdateFrame();
    }
    
    public void StartEdit() 
    { 
        IsEdit = true;
    }

    public void StopEdit() 
    { 
        _isResizing = false; 
        _isRotating = false; 
        _isDragging = false;
        _isAddingNewCorner = false;
        _resizeMarkerIndex = null; 
        _mouseStartPos = null; 
        shape.StopEdit(); // важен именно такой порядок, потому что этот метод удаляет последнюю точку
        UpdateFrameBorders(shape.GetEndPoints());
        SetCenterPoint();
        SetRadiusCircumCircle();
        SetBordersLines();
    }
    
    public void StartResize(int resizeMarkerIndex, Point mousePos) 
    { 
        _isResizing = true;
        
        _resizeMarkerIndex = resizeMarkerIndex; 
        _mouseStartPos = mousePos; 
        // Сохраняем исходные координаты вершин перед началом масштабирования
        _originalCornersPointsList = (Point[])_cornersPointsList.Clone();
    }
    
    public void StartRotate(Point mousePos)
    { 
        _isRotating = true;
        
        _mouseStartPos = mousePos; 
        // Сохраняем исходные координаты вершин перед началом вращения
        _originalCornersPointsList = (Point[])_cornersPointsList.Clone();
    }
    
    public void StartDrag(Point mousePos)
    { 
        _isDragging = true; 
        
        _mouseStartPos = mousePos; 
        // Сохраняем исходные координаты вершин перед началом перемещения
        _originalCornersPointsList = (Point[])_cornersPointsList.Clone();
    }

    public void StartAddNewCorner()
    {
        _isAddingNewCorner = true;
        // shape.SetCornersPoints(mousePos);
    }

    public void AddNewCorner(Point mousePos)
    {
        IsEdit = true;
        shape.AddNewCorner(mousePos);
    }
}