using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS_SBP_HKDXX6.Models
{
    public class SbpProblem(Point[] points, int polygonPointCount)
    {
        public static double Goal(Polygon solution) => solution.Perimeter();

        public double Constraint(Polygon solution)
        {
            double sum = 0;
            foreach (var p in points)
            {
                double min = 0;
                for (var i = 0; i < solution.PointCount; i++)
                {
                    var dist = p.DistanceFromLine(solution[i], solution[(i + 1) % solution.PointCount]);
                    if (dist < min)
                    {
                        min = dist;
                    }
                }
                if (min < 0)
                {
                    sum += min;
                }
            }
            return -1*sum;
        }

        public Polygon GetRandomNeighbour(Polygon solution, double distance, Random r)
        {
            Polygon? newSolution = null;
            while (newSolution == null)
            {
                //create deep copy
                newSolution = new Polygon(polygonPointCount)
                {
                    Points = solution.Points.Select(p => new Point(p.X, p.Y)).ToArray()
                };

                foreach (var t in newSolution.Points)
                {
                    var angle = r.NextDouble();
                    t.X += Math.Cos(angle) * distance;
                    t.Y += Math.Sin(angle) * distance;
                }
            }
            return newSolution;
        }
        public Polygon GenerateRandomSolution(Random r)
        {
            var poly = new Polygon(polygonPointCount);

            var center = new Point(
                points.Average(p => p.X),
                points.Average(p => p.Y));

            var radius = points.Max(p => p.Distance(center)) + 1;

            (Point point, double angle)[] pointsAndAngles = new (Point, double)[polygonPointCount];

            while (poly[0] == null || Constraint(poly) > 0)
            {
                for (var i = 0; i < polygonPointCount; i++)
                {
                    var angle = r.NextDouble() * 2 * Math.PI;
                    pointsAndAngles[i] = (new Point(
                            center.X + radius * Math.Cos(angle),
                            center.Y + radius * Math.Sin(angle)),
                        angle);
                }

                poly.Points = pointsAndAngles.OrderByDescending(p => p.angle).Select(p => p.point).ToArray();
            }

            return poly;

        }
    }
}
