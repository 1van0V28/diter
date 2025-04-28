namespace diter;

public class Frame(Point start, Shape shape)
{
    private bool _isDrag; // т.к. при ротации фигуры меняется её ориентация, то необходимо поместить вершины в список
    private int _radiusCircumCircle;
    private int? _editMarkerIndex;
    private Point? _dragStart;
    private Point _center;
    private readonly Point[] _cornersPointsList = new Point[4];
    private readonly EditLine[] _borderLinesList = new EditLine[5];
    
    private Point[] _originalCorners; // Исходные координаты вершин
    
    public void Draw(Graphics g)
    {
        if (_isDrag || _editMarkerIndex != null)
        {
            foreach (var line in _borderLinesList)
            { 
                line.Draw(g);
            }
        }
        
        shape.Draw(g);
    }

    private void DragFrame(Point newEnd)
    {
        _dragStart ??= newEnd;

        var deltaX = newEnd.X - _dragStart.Value.X;
        var deltaY = newEnd.Y - _dragStart.Value.Y;
        _dragStart = newEnd;

        for (var i = 0; i < _cornersPointsList.Length; i++)
        {
            _cornersPointsList[i].X += deltaX;
            _cornersPointsList[i].Y += deltaY;
        }
    }

    public void RotateFrame(Point newEnd)
    {
        if (_dragStart == null) return; // Проверка, что вращение начато

        // Вычисление угла поворота относительно начальной позиции
        float dxStart = _dragStart.Value.X - _center.X;
        float dyStart = _dragStart.Value.Y - _center.Y;
        float dxEnd = newEnd.X - _center.X;
        float dyEnd = newEnd.Y - _center.Y;

        var angleStart = (float)Math.Atan2(dyStart, dxStart);
        var angleEnd = (float)Math.Atan2(dyEnd, dxEnd);
        var angleRadians = angleEnd - angleStart;

        // Поворот всех вершин относительно исходных координат
        for (var i = 0; i < _cornersPointsList.Length; i++)
        {
            _cornersPointsList[i] = GetRotatedPoint(_originalCorners[i], angleRadians);
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
    
    public void StartRotation(Point startPoint)
    {
        _dragStart = startPoint;
        // Сохраняем исходные координаты вершин перед началом вращения
        _originalCorners = (Point[])_cornersPointsList.Clone();
    }
    
    public void EndRotation()
    {
        _dragStart = null; // Сбрасываем состояние
        // Обновляем исходные координаты, чтобы зафиксировать новое положение
        _originalCorners = (Point[])_cornersPointsList.Clone();
    }

    private void ResizeFrame(Point newEnd) // при повороте происходит смещение ориентации, поэтому необходимо изменение происходило относительно центра
    {
        if (_editMarkerIndex != null)
        {
            switch (_editMarkerIndex)
            {
                case 0: ResizeSide(newEnd, 0, 1, 3, 2);
                    return;
                case 1: ResizeSide(newEnd, 1, 2, 0, 3);
                    return;
                case 2: ResizeSide(newEnd, 2, 3, 1, 0);
                    return;
                case 3: ResizeSide(newEnd, 3, 0, 2, 1);
                    return;
            }
        }
    }

    private void ResizeSide(Point newEnd, int firstPointIndex, int secondPointIndex, int oppFirstPointIndex, int oppSecondPointIndex)
    { 
        if (_dragStart == null) return;
        
        // Вектор перетягиваемой стороны
        float sideX = _originalCorners[secondPointIndex].X - _originalCorners[firstPointIndex].X;
        float sideY = _originalCorners[secondPointIndex].Y - _originalCorners[firstPointIndex].Y;

        // Перпендикулярный вектор (направление изменения размера)
        float normalX = sideY; // (y2 - y1)
        float normalY = -sideX; // -(x2 - x1)

        // Нормализуем перпендикулярный вектор
        float length = (float)Math.Sqrt(normalX * normalX + normalY * normalY);
        if (length == 0) return; // Избегаем деления на ноль
        normalX /= length;
        normalY /= length;

        // Проецируем смещение мыши на перпендикулярное направление
        float dx = newEnd.X - _dragStart.Value.X;
        float dy = newEnd.Y - _dragStart.Value.Y;
        float projection = dx * normalX + dy * normalY;

        // Вычисляем знаковое расстояние между перетягиваемой и противоположной сторонами
        // Берем средние точки сторон
        float sideMidX = (_originalCorners[firstPointIndex].X + _originalCorners[secondPointIndex].X) / 2f;
        float sideMidY = (_originalCorners[firstPointIndex].Y + _originalCorners[secondPointIndex].Y) / 2f;
        float oppSideMidX = (_originalCorners[oppFirstPointIndex].X + _originalCorners[oppSecondPointIndex].X) / 2f;
        float oppSideMidY = (_originalCorners[oppFirstPointIndex].Y + _originalCorners[oppSecondPointIndex].Y) / 2f;

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
            (int)(_originalCorners[firstPointIndex].X + projection * normalX),
            (int)(_originalCorners[firstPointIndex].Y + projection * normalY)
        );
        _cornersPointsList[secondPointIndex] = new Point(
            (int)(_originalCorners[secondPointIndex].X + projection * normalX),
            (int)(_originalCorners[secondPointIndex].Y + projection * normalY)
        );
    }

    private void SetCornersPoints(Point newEnd)
    {
        var topLeftX = Math.Min(start.X, newEnd.X);
        var topLeftY = Math.Min(start.Y, newEnd.Y);
        var bottomRightX = Math.Max(start.X, newEnd.X);
        var bottomRightY = Math.Max(start.Y, newEnd.Y);

        _cornersPointsList[0] = new Point(topLeftX, topLeftY);
        _cornersPointsList[1] = new Point(bottomRightX, topLeftY);
        _cornersPointsList[2] = new Point(bottomRightX, bottomRightY);
        _cornersPointsList[3] = new Point(topLeftX, bottomRightY);
    }

    private void SetCenterPoint()
    {
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

        var rotateMarkerPos = new Point(_center.X, _center.Y - _radiusCircumCircle); // нужен центр
        _borderLinesList[4] = new EditLine(_center, rotateMarkerPos, Color.Black, rotateMarkerPos);
    }

    private void SetRadiusCircumCircle()
    {
        var width = (double)_cornersPointsList[1].X - _cornersPointsList[0].X;
        var height = (double)_cornersPointsList[3].Y - _cornersPointsList[0].Y;
        
        _radiusCircumCircle = (int)Math.Sqrt(Math.Pow(width / 2, 2) + Math.Pow(height / 2, 2));
    }
    
    // public void StartDrag()
    // {
    //     _isDrag = true;
    // }
    
    public void StartDrag(Point startPoint)
    {
        _isDrag = true;
        _dragStart = startPoint;
        _originalCorners = (Point[])_cornersPointsList.Clone();
    }

    public void StartResize(int editMarkerIndex, Point mousePos)
    {
        _editMarkerIndex = editMarkerIndex;
        _dragStart = mousePos;
        _originalCorners = (Point[])_cornersPointsList.Clone();
    }
    

    public void StopEdit()
    {
        _isDrag = false;
        _editMarkerIndex = null;
        _dragStart = null;
        shape.StopEdit();
    }
    
    public void EditFrame(Point newEnd) // идея в том, чтобы вычислять угол изменения вектора от курсора к центру относительно его предыдущего положения
    {
        shape.StartEdit();
        if (_isDrag) 
        {
            DragFrame(newEnd);
        } 
        else if (_editMarkerIndex == 4)
        {
            RotateFrame(newEnd);
            SetBordersLines(); // обновляем границы
            shape.SetCornersPoints(_cornersPointsList); // обновляем вершины фигуры
            return;
        }
        else if (_editMarkerIndex != null)
        {
            ResizeFrame(newEnd); 
        }
        else
        {
            SetCornersPoints(newEnd);
        }
        SetCenterPoint(); // обновляем центр
        SetRadiusCircumCircle(); // обновляем радиус описанной окружности
        SetBordersLines(); // обновляем границы
        shape.SetCornersPoints(_cornersPointsList); // обновляем вершины фигуры
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
}