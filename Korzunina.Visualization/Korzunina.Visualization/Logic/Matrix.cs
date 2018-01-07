using System;
using System.IO;
using System.Linq;

namespace Korzunina.Visualization.Logic
{
    class Matrix
    {
        private int n, m;
        private double[,] matr;

        public Matrix(int N, int M)
        {
            n = N;
            m = M;
            matr = new double[N, M];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    if (i != j) matr[i, j] = 0;
                    else matr[i, j] = 1;
        }
        public Matrix(double[,] a)
        {
            n = a.GetLength(0);
            m = a.GetLength(1);
            matr = new double[n, m];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < m; j++)
                    matr[i, j] = a[i, j];
        }
        public Matrix(string namefile)
        {
            StreamReader fs = new StreamReader(namefile);
            var modelInfo = fs.ReadToEnd().Split(' ', '\r', '\n').ToList(); //modelInfo - это список всех слов, чисел и символов, заключённых между пробелами и переводами строк
            n = Convert.ToInt32(modelInfo[0]);
            m = Convert.ToInt32(modelInfo[1]);
            matr = new double[n, m];
            int k = 3;
            for (var i = 0; i < n; i++)
                for (var j = 0; j < m; j++)
                {
                    while (modelInfo[k] == "") k++;
                    matr[i, j] = Convert.ToInt32(modelInfo[k++]);
                }
            modelInfo.RemoveRange(0, modelInfo.Count);
        }
        public Matrix(double[,] a, int N, int M)
        {
            matr = new double[N,M];
            n = N;
            m = M;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    matr[i, j] = a[i, j];
        }

        public int getN()
        {
            return n;
        }
        public int getM()
        {
            return m;
        }
        public double getElem(int i, int j)
        {
            return matr[i, j];
        }
        public void setElem(int i, int j, double a)
        {
            this.matr[i, j] = a;
        }

        public void Show()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                    Console.Write(matr[i, j] + " ");
                Console.WriteLine();
            }
        }

        private Matrix readFromFile(string namefile)
        {
            StreamReader fs = new StreamReader(namefile);
            var modelInfo = fs.ReadToEnd().Split(' ', '\r', '\n').ToList(); //modelInfo - это список всех слов, чисел и символов, заключённых между пробелами и переводами строк
            int N = Convert.ToInt16(modelInfo[0]);
            int M = Convert.ToInt16(modelInfo[1]);
            Console.WriteLine(M);
            Matrix c = new Matrix(N, M);
            int k = 3;
            for (var i = 0; i < N; i++)
                for (var j = 0; j < M; j++)
                {
                    while (modelInfo[k] == "") k++;
                    c.matr[i, j] = Convert.ToInt32(modelInfo[k++]);
                }
            modelInfo.RemoveRange(0, modelInfo.Count);
            return c;
        }

        public static Matrix operator *(Matrix a, Matrix   b)
        {
            Matrix c = new Matrix(a.getN(), b.getM());
            for (int i = 0; i < a.getN(); i++)

            {

                for (int j = 0; j < b.getM(); j++)
                {
                    double v = 0;
                    for (int k = 0; k < a.getM(); k++)
                    {
                        v += a.matr[i, k] * b.matr[k, j];
                    }
                    c.matr[i, j] = v;
                }
            }
            return c;
        }
        public static Matrix operator *(double b, Matrix a)
        {
            Matrix c = new Matrix(a.getN(), a.getM());
            for (int i = 0; i < a.getN(); i++)
            {
                for (int j = 0; j < a.getM(); j++)
                {
                    c.matr[i, j] = a.matr[i,j]  * b;
                }
            }
            return c;
        }
        public static double[] operator *(Matrix a, double[] b)
        {
            double[] c = new double[a.n];
            for (int i = 0; i < a.n; i++)
            {
                for (int j = 0; j < a.m; j++)
                {
                    c[i] += a.matr[i, j] * b[j];
                }
            }
            return c;
        }
        public static Matrix operator +(Matrix a, Matrix   b)
        {
            Matrix c = new Matrix(a.getM(), a.getN());
            for (int i = 0; i < a.getM(); i++)
            {
                for (int j = 0; j < a.getN(); j++)
                {
                    c.matr[i, j] = a.matr[i, j] + b.matr[i, j];
                }
            }
            return c;
        }
        public static Matrix operator -(Matrix a, Matrix   b)
        {
            Matrix c = new Matrix(a.getM(), a.getN());
            for (int i = 0; i < a.getM(); i++)
            {
                for (int j = 0; j < a.getN(); j++)
                {
                    c.matr[i, j] = a.matr[i, j] - b.matr[i, j];
                }
            }
            return c;
        }
    }
}
