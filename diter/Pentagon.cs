// namespace diter;
//
// public class Pentagon(Color color): Shape(color)
// {
//     public override void SetCornersPoints(Point topLeft, Point topRight, Point bottomLeft, Point bottomRight)
//     {
//         PointsList.Clear();
//         PointsList.Add(new Point((int)Math.Round(topLeft.X + (topRight.X - topLeft.X) / 2f), topLeft.Y));
//         PointsList.Add(new Point(topLeft.X, (int)Math.Round((1 - 0.4f) * topLeft.Y + 0.4f * bottomLeft.Y)));
//         PointsList.Add(new Point((int)Math.Round((1 - 0.25f) * bottomLeft.X + 0.25f * bottomRight.X), bottomLeft.Y));
//         PointsList.Add(new Point((int)Math.Round((1 - 0.75f) * bottomLeft.X + 0.75f * bottomRight.X), bottomLeft.Y));
//         PointsList.Add(new Point(topRight.X, (int)Math.Round((1 - 0.4f) * topRight.Y + 0.4f * bottomRight.Y)));
//     }
// }