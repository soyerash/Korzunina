using System;
using System.IO;
using System.Linq;

namespace Korzunina.Visualization.Logic
{
    public class Matrix
    {
        private int _n, _m;
        private double[,] _matr;

        public Matrix(int N, int M)
        {
            _n = N;
            _m = M;
            _matr = new double[N, M];
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    if (i != j) _matr[i, j] = 0;
                    else _matr[i, j] = 1;
        }
        public Matrix(double[,] a)
        {
            _n = a.GetLength(0);
            _m = a.GetLength(1);
            _matr = new double[_n, _m];
            for (int i = 0; i < _n; i++)
                for (int j = 0; j < _m; j++)
                    _matr[i, j] = a[i, j];
        }
        public Matrix(string namefile)
        {
            StreamReader fs = new StreamReader(namefile);
            var modelInfo = fs.ReadToEnd().Split(' ', '\r', '\n').ToList(); //modelInfo - это список всех слов, чисел и символов, заключённых между пробелами и переводами строк
            _n = Convert.ToInt32(modelInfo[0]);
            _m = Convert.ToInt32(modelInfo[1]);
            _matr = new double[_n, _m];
            int k = 3;
            for (var i = 0; i < _n; i++)
                for (var j = 0; j < _m; j++)
                {
                    while (modelInfo[k] == "") k++;
                    _matr[i, j] = Convert.ToInt32(modelInfo[k++]);
                }
            modelInfo.RemoveRange(0, modelInfo.Count);
        }
        public Matrix(double[,] a, int N, int M)
        {
            _matr = new double[N,M];
            _n = N;
            _m = M;
            for (int i = 0; i < N; i++)
                for (int j = 0; j < M; j++)
                    _matr[i, j] = a[i, j];
        }

        public int getN()
        {
            return _n;
        }
        public int getM()
        {
            return _m;
        }
        public double getElem(int i, int j)
        {
            return _matr[i, j];
        }
        public void setElem(int i, int j, double a)
        {
            this._matr[i, j] = a;
        }

        public void Show()
        {
            for (int i = 0; i < _n; i++)
            {
                for (int j = 0; j < _m; j++)
                    Console.Write(_matr[i, j] + " ");
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
                    c._matr[i, j] = Convert.ToInt32(modelInfo[k++]);
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
                        v += a._matr[i, k] * b._matr[k, j];
                    }
                    c._matr[i, j] = v;
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
                    c._matr[i, j] = a._matr[i,j]  * b;
                }
            }
            return c;
        }
        public static double[] operator *(Matrix a, double[] b)
        {
            double[] c = new double[a._n];
            for (int i = 0; i < a._n; i++)
            {
                for (int j = 0; j < a._m; j++)
                {
                    c[i] += a._matr[i, j] * b[j];
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
                    c._matr[i, j] = a._matr[i, j] + b._matr[i, j];
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
                    c._matr[i, j] = a._matr[i, j] - b._matr[i, j];
                }
            }
            return c;
        }
    }
}
