using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS_SBP_HKDXX6.Models
{
    public class Point(double x, double y)
    {
        public double X { get; set; } = x;
        public double Y { get; set; } = y;

        public double Distance(Point p) => Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));

        /// <summary>
        /// Returns the distance from the point to the line defined by the two points (negative if on the left side, positive if on the right side)
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public double DistanceFromLine(Point p1, Point p2)
        {
            double a = p2.Y - p1.Y;
            double b = p1.X - p2.X;
            double c = p2.X * p1.Y - p1.X * p2.Y;

            return (a * X + b * Y + c) / Math.Sqrt(a * a + b * b);
        }

        public override string ToString() => "{" + $"{X},{Y}" + "}";
    }
}
