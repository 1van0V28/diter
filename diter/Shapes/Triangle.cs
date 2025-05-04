// using diter.Shapes;
//
// namespace diter;
//
// public class Triangle(Color color): Shape(color)
// {
//     public override void SetCornersPoints(Point[] editRectCornersList)
//     {
//         PointsList.Clear();
//         PointsList.Add(new Point(
//             Math.Abs(editRectCornersList[0].X + (editRectCornersList[1].X - editRectCornersList[0].X) / 2),
//             Math.Abs(editRectCornersList[0].Y + (editRectCornersList[1].Y - editRectCornersList[0].Y) / 2)
//             ));
//         PointsList.Add(editRectCornersList[2]);
//         PointsList.Add(editRectCornersList[3]);
//     }
// }