using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCS_SBP_HKDXX6.Models
{
    public class Polygon(int n)
    {
        public Point[] Points { get; set; } = new Point[n];

        public Point this[int i] => Points[i];

        public int PointCount => Points.Length;

        public double Perimeter() => Points.Select((t, i) => t.Distance(Points[(i + 1) % Points.Length])).Sum();

        public override string ToString() => Program.PointArrayToString(Points);
    }
}
