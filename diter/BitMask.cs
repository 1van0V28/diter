namespace diter;

public class BitMask
{
    private int[,] _bitMask = new int[,] { };
    
    public void Draw(Graphics g, Point[] endPoints, Color color)
    {
        var topLeft = endPoints[0];
        var bitMaskWidth = _bitMask.GetLength(1);
        var bitMaskLength = _bitMask.GetLength(0);

        // Создаём Bitmap для оптимизированной отрисовки
        using var bitmap = new Bitmap(bitMaskWidth, bitMaskLength);
        for (var i = 0; i < bitMaskLength; i++)
        {
            for (var j = 0; j < bitMaskWidth; j++)
            {
                if (_bitMask[i, j] == 1)
                {
                    bitmap.SetPixel(j, i, color);
                }
            }
        }
        g.DrawImage(bitmap, topLeft.X, topLeft.Y);
    }
    
    public void UpdateBitMask(Point[] endPoints, List<Point> verticesList)
    {
        var topLeft = endPoints[0];
        var bottomRight = endPoints[1];

        var bitMaskWidth = bottomRight.X - topLeft.X + 1;
        var bitMaskLength = bottomRight.Y - topLeft.Y + 1;

        if (bitMaskWidth <= 0 || bitMaskLength <= 0)
        {
            _bitMask = new int[0, 0];
            return;
        }

        var bitMask = new int[bitMaskLength, bitMaskWidth];

        // Алгоритм scanline
        for (var y = topLeft.Y; y <= bottomRight.Y; y++)
        {
            // Находим пересечения с рёбрами
            var intersections = new List<double>();
            for (int i = 0; i < verticesList.Count; i++)
            {
                var p1 = verticesList[i]; 
                var p2 = verticesList[(i + 1) % verticesList.Count];

                double y1 = p1.Y;
                double y2 = p2.Y;
                if ((y1 <= y && y2 > y) || (y2 <= y && y1 > y))
                {
                    double x1 = p1.X;
                    double x2 = p2.X;
                    if (Math.Abs(y1 - y2) < 1e-6) continue; // Пропускаем горизонтальные рёбра
                    var t = (y - y1) / (y2 - y1);
                    var x = x1 + t * (x2 - x1);
                    intersections.Add(x);
                }
            }

            // Сортируем пересечения
            intersections.Sort();

            // Закрашиваем области между парами пересечений
            for (var i = 0; i < intersections.Count - 1; i += 2)
            {
                var xStart = (int)Math.Ceiling(intersections[i]);
                var xEnd = (int)Math.Floor(intersections[i + 1]);
                for (int x = xStart; x <= xEnd; x++)
                {
                    var bitX = x - topLeft.X;
                    var bitY = y - topLeft.Y;
                    if (bitX >= 0 && bitX < bitMaskWidth && bitY >= 0 && bitY < bitMaskLength)
                    {
                        bitMask[bitY, bitX] = 1;
                    }
                }
            }
        }
        _bitMask = bitMask;
    }
}