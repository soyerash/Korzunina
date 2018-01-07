using System;
using System.Drawing;
using System.Windows.Forms;

namespace Korzunina.Visualization.Logic
{
    class Draw
    {
        public double L = -6, R = 5, B = -5, T = 5;                 // Мировые координаты границ окна
        public int W, H;                                            // Разрешение рабочей области окна
        public double DX, DY;                                       // Ширина и высота одного пикселя
        public double X_Pos, Y_Pos;                                 // Позиция графического курсора в мировых координатах

        public int toScreenX(double X)                              // Переход от мировых координат к экранным (для абсциссы)
        {
            int newX = (int)((X - L) / (R - L) * W);
            return newX;
        }
        public int toScreenY(double Y)                              // Переход от мировых координат к экранным (для ординаты)
        {
            int newY = (int)((T - Y) / (T - B) * H);
            return newY;
        }
        public double toWorldX(int X)                               // Переход от экранных координат к мировым (для абсциссы)
        {
            double newX = L + (R - L) * (X + 0.5) / (double)(W);
            return newX;
        }
        public double toWorldY(int Y)                               // Переход от экранных координат к мировым (для ординаты)
        {
            double newY = T - (T - B) * (Y + 0.5) / (double)(H);
            return newY;
        }

        public void setPixelH()
        {
            DX = (R - L) / W;
            DY = (T - B) / H;
        }

        public void drawAxis(object sender, PaintEventArgs e, Camera camera)             // Отрисовка координатных осей
        {
            Matrix z = new Matrix(new double[,] { { 0, 0 }, { 0, 0 }, { 0, 7 }, { 1, 1 } });
            z = camera.CreatProjectMatrix(z);
            try
            {
                e.Graphics.DrawLine(new Pen(Brushes.Green), toScreenX(z.getElem(0, 0) / z.getElem(2, 0)), toScreenY(z.getElem(1, 0) / z.getElem(2,0)), toScreenX(z.getElem(0, 1) / z.getElem(2, 1)), toScreenY(z.getElem(1, 1) / z.getElem(2, 1)));
            }
            catch (Exception) { }
            Matrix x = new Matrix(new double[,] { { 0, 7 }, { 0, 0 }, { 0, 0 }, { 1, 1 } });
            x = camera.CreatProjectMatrix(x);
            try
            {
                e.Graphics.DrawLine(new Pen(Brushes.Red), toScreenX(x.getElem(0, 0) / x.getElem(2, 0)), toScreenY(x.getElem(1, 0) / x.getElem(2, 0)), toScreenX(x.getElem(0, 1) / x.getElem(2, 1)), toScreenY(x.getElem(1, 1) / x.getElem(2, 1)));
            }
            catch (Exception) { }
            Matrix y = new Matrix(new double[,] { { 0, 0 }, { 0, 7 }, { 0, 0 }, { 1, 1 } });
            y = camera.CreatProjectMatrix(y);
            try
            {
                e.Graphics.DrawLine(new Pen(Brushes.Purple), toScreenX(y.getElem(0, 0) / y.getElem(2, 0)), toScreenY(y.getElem(1, 0) / y.getElem(2, 0)), toScreenX(y.getElem(0, 1) / y.getElem(2, 1)), toScreenY(y.getElem(1, 1) / y.getElem(2, 1)));
            }
            catch (Exception) { }
        }

        public void drawModel(object sender, PaintEventArgs e, Matrix map, Matrix edges) //Отрисовка модели
        {
            Pen pen = new Pen(Color.Black, 1);
            for (int i = 0; i < edges.getN(); i++)
            {
                int dot1 = Convert.ToInt16(edges.getElem(i, 0));
                int dot2 = Convert.ToInt16(edges.getElem(i, 1));

                Point p1 = new Point(toScreenX(map.getElem(0, dot1) / map.getElem(2, dot1)), toScreenY(map.getElem(1, dot1) / map.getElem(2, dot1)));
                Point p2 = new Point(toScreenX(map.getElem(0, dot2) / map.getElem(2, dot2)), toScreenY(map.getElem(1, dot2) / map.getElem(2, dot2)));
                e.Graphics.DrawLine(pen, p1, p2);
            }
        }

        public void Move(MouseEventArgs e)
        {
            L += DX * (X_Pos - e.X);
            R += DX * (X_Pos - e.X);
            T += -DY * (Y_Pos - e.Y);
            B += -DY * (Y_Pos - e.Y);
            X_Pos = e.X;
            Y_Pos = e.Y;
        }

        public void SetResolution() //изменение размеров рабочей области
        {
            setPixelH();
            B = T - H * DX;
            /*
            T = H * DX / 2;
            B = -T;
            */
            double newT = T, newB = B;
            T = newT + (T - B - newT + newB) / 2;
            B = newB - (T - B - newT + newB) / 2;
        }
    }
}