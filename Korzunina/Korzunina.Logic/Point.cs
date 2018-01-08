using System;
using System.Collections.Generic;
using System.Text;

namespace korzunina
{
    class Point
    {
        private double x, y, z;

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public double X
        {
            get { return x; }
        }

        public double Y
        {
            get { return y; }
        }

        public double Z
        {
            get { return z; }
        }
    }
}
