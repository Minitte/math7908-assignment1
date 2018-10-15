using System;
using System.Threading;
using System.Threading.Tasks;

namespace math7908_assignment1
{
    class PerformanceMatrix
    {
        public PerformanceMatrix() { }

        /// <summary>
        /// Inverse a square matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public float[,] Inverse(float[,] a)
        {
            float det = Determinant(a);

            float[,] adj = Adjugate(a);

            return Matrix.Multiply(adj, 1f / det);
        }

        /// <summary>
        /// Calculates the Adjugate matrix
        /// </summary>
        /// <param name="a">Untranposed matrix</param>
        /// <returns></returns>
        public float[,] Adjugate(float[,] a)
        {
            float[,] t = Matrix.Transpose(a);

            float[,] adj = new float[t.GetLength(0), t.GetLength(0)];

            for (int i = 0; i < t.GetLength(0); i++)
            {
                float[][,] minors = Matrix.LineMinors(t, i);

                for (int j = 0; j < t.GetLength(0); j++)
                {
                    adj[i, j] = Determinant(minors[j]) * Matrix.CheckerBoard(i, j);
                }
            }

            return adj;
        }

        /// <summary>
        /// Finds the Determinant of the matrix.
        /// Must be square
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public float Determinant(float[,] a)
        {
            int size = a.GetLength(0);

            if (size == 2)
            {
                return Matrix.Determinant2x2(a);
            }

            float sum = 0;

            float[][,] minors = Matrix.LineMinors(a, 0);

            int i = 0;

            Parallel.ForEach(minors, minor =>
            {
                if (a[0, i] != 0)
                {
                    sum += a[0, i] * Matrix.CheckerBoard(0, i) * Matrix.Determinant(minor);
                }

                i++;
            });

            return sum;
        }
    }
}
