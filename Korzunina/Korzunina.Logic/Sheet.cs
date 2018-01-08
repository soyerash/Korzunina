using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace korzunina
{
    class Sheet
    {
        private const int tetrahedronCount = 6;

        private double hx, hy, hz;
        private int Nx, Ny, Nz;

        private int pointsCount, blocksCount, ZYPointsCount;

        private List<Point> coords;

        private int[,] adj;
        private Dictionary<int, List<int>> adjPoints;
        private int bandWidth;
        private double volume;
        
        public List<Point> Coordinates
        {
            get { return coords; }           
        }

        //Матрица смежности
        public int[,] AdjacencyMatrix
        {
            get { return adj; }           
        }

        //Соседние узлы каждой точки
        public Dictionary<int, List<int>> AdjacentPoints
        {
            get { return adjPoints; }
        }

        public int BandWidth
        {
            get { return bandWidth; }            
        }

        public double Volume
        {
            get { return volume; }
        }

        public int BlocksCount
        {
            get { return blocksCount; }
        }

        public int PointsCount
        {
            get { return pointsCount; }
        }

        public Sheet(double hx, double hy, double hz, int Nx, int Ny, int Nz)
        {
            this.hx = hx;
            this.hy = hy;
            this.hz = hz;
            this.Nx = Nx;
            this.Ny = Ny;
            this.Nz = Nz;

            pointsCount = (Nx + 1) * (Ny + 1) * (Nz + 1);
            blocksCount = Nx * Ny * Nz;
            ZYPointsCount = (Ny + 1) * (Nz + 1);
            volume = hx * hy * hz * blocksCount;

            coords = new List<Point>();
            adj = new int[blocksCount * tetrahedronCount, 4];
            adjPoints = new Dictionary<int, List<int>>(pointsCount);

            CalculatePointsAndAdjacencyMatrix();
            CalculateBandWidth();
        }      

        private void CalculatePointsAndAdjacencyMatrix()
        {
            int pointNumber = 0;
            int blockNumber = 0;

            for (int xIter = 0; xIter <= Nx; xIter++)
            {
                for (int yIter = 0; yIter <= Ny; yIter++)
                {
                    for (int zIter = 0; zIter <= Nz; zIter++)
                    {
                        coords.Add(new Point(hx * xIter, hy * yIter, hz * zIter));

                        if (zIter != Nz && yIter != Ny && xIter != Nx)
                        {
                            AddBlockToAdjacencyMatrix(pointNumber, blockNumber);
                            blockNumber++;
                        }

                        pointNumber++;
                    }
                }
            }
        }

        private void AddBlockToAdjacencyMatrix(int pointNumber, int blockNumber)
        {
            int i, j, l, k, p, q, s, r;

            p = pointNumber;    q = ZYPointsCount + p;
            i = p + 1;          j = ZYPointsCount + i;
            s = p + (Nz + 1);   r = ZYPointsCount + s;
            l = s + 1;          k = ZYPointsCount + l;

            int row = blockNumber * tetrahedronCount;

            adj[row, 0] = j; adj[row, 1] = l; adj[row, 2] = k; adj[row, 3] = q; ++row;
            adj[row, 0] = s; adj[row, 1] = r; adj[row, 2] = k; adj[row, 3] = q; ++row;
            adj[row, 0] = s; adj[row, 1] = k; adj[row, 2] = l; adj[row, 3] = q; ++row;
            adj[row, 0] = i; adj[row, 1] = l; adj[row, 2] = j; adj[row, 3] = p; ++row;
            adj[row, 0] = q; adj[row, 1] = j; adj[row, 2] = l; adj[row, 3] = p; ++row;
            adj[row, 0] = q; adj[row, 1] = l; adj[row, 2] = s; adj[row, 3] = p;

            AddAdjecentPoints(i, j, l, k, p, q, s, r);
        }

        private void AddAdjecentPoints(int i, int j, int l, int k, int p, int q, int s, int r)
        {
            ConnectPoints(p, new int[] { q, s, i });
            ConnectPoints(i, new int[] { l, j });
            ConnectPoints(s, new int[] { l, r });
            ConnectPoints(r, new int[] { q, k });
            ConnectPoints(l, new int[] { k });
            ConnectPoints(j, new int[] { k });
        }

        private void ConnectPoints(int point, int[] adjecentPoints)
        {
            List<int> points = new List<int>(6);
            if (adjPoints.ContainsKey(point))
                adjPoints.TryGetValue(point, out points);
            else adjPoints.Add(point, null);

            for (int i = 0; i < adjecentPoints.Length; i++)
            {
                if (!points.Contains(adjecentPoints[i])) points.Add(adjecentPoints[i]);
            }         

            points.TrimExcess();
            adjPoints[point] = points;
        }

        private void CalculateBandWidth()
        {
            int maxDifferenceInMatrix = 0;

            for (int i = 0; i < adj.GetLength(0); i++)
            {
                int maxDifferenceInRow = 0;

                for (int j = 0; j < adj.GetLength(1); j++)
                {                   
                    for (int k = j+1; k < adj.GetLength(1); k++)
                    {
                        if (Math.Abs(adj[i, j] - adj[i, k]) > maxDifferenceInRow)
                        {
                            maxDifferenceInRow = Math.Abs(adj[i, j] - adj[i, k]);
                        }
                    }
                }

                if (maxDifferenceInRow > maxDifferenceInMatrix)
                {
                    maxDifferenceInMatrix = maxDifferenceInRow;
                }
            }

            bandWidth = maxDifferenceInMatrix;
        }
    }
}
