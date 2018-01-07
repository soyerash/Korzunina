using System;

namespace Korzunina.Visualization.Logic
{
    static class VectorWork
    {

        public static double Norma(double[] a)
        {
            double norma = 0;
            for (int i = 0; i < a.GetLength(0); i++)
                norma += Math.Pow(a[i], 2);
            return Math.Sqrt(norma);
        }

        public static double[] VectorMultiply(double[] a, double[] b)
        {
            double[] c = new double[3];
            c[0] = a[1] * b[2] - a[2] * b[1];
            c[1] = a[2] * b[0] - a[0] * b[2];
            c[2] = a[0] * b[1] - a[1] * b[0];
            return c;
        }

        public static double q(double[] a, double[] b)
        {
            double qq = 0;
            for (int i = 0; i < a.GetLength(0); i++)
                qq += Math.Pow(b[i] - a[i], 2);
            return Math.Sqrt(qq);
        }
    }
}
