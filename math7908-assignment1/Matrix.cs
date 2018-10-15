using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.CompilerServices;

using System.Diagnostics;

namespace math7908_assignment1
{
    class Matrix
    {

        private Matrix() { }

        /// <summary>
        /// Inverse a square matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float[,] Inverse(float[,] a)
        {
            Debug.Assert(a.GetLength(0) == a.GetLength(1), "Inverse: matrix not square!");

            float det = Determinant(a);

            float[,] adj = Adjugate(a);

            return Multiply(adj, 1f / det);
        }

        /// <summary>
        /// Finds the Determinant of the matrix.
        /// Must be square
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float Determinant(float[,] a)
        {
            int size = a.GetLength(0);

            if (size == 2)
            {
                return Determinant2x2(a);
            }

            float sum = 0;

            float[][,] minors = LineMinors(a, 0);

            int i = 0;

            foreach (float[,] minor in minors)
            {
                if (a[0, i] != 0)
                {
                    sum += a[0, i] * CheckerBoard(0, i) * Determinant(minor);
                }

                i++;
            }

            return sum;
        }

        /// <summary>
        /// Calculates the Adjugate matrix
        /// </summary>
        /// <param name="a">Untranposed matrix</param>
        /// <returns></returns>
        public static float[,] Adjugate(float[,] a)
        {
            float[,] t = Transpose(a);

            float[,] adj = new float[t.GetLength(0), t.GetLength(0)];

            for (int i = 0; i < t.GetLength(0); i++)
            {
                float[][,] minors = LineMinors(t, i);

                for (int j = 0; j < t.GetLength(0); j++)
                {
                    adj[i, j] = Determinant(minors[j]) * CheckerBoard(i, j);
                }
            }

            return adj;
        }

        /// <summary>
        /// Checker board pattern
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float CheckerBoard(int row, int col)
        {
            return (float)Math.Pow(-1, row + col);
        }

        /// <summary>
        /// Determinant of a 2x2 matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Determinant2x2(float[,] a)
        {
            return (a[0, 0] * a[1, 1]) - (a[1, 0] * a[0, 1]);
        }

        /// <summary>
        /// Gets all of the minors of the first row
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float[][,] LineMinors(float[,] a, int row)
        {
            int size = a.GetLength(0);

            float[][,] minors = new float[size][,];

            for (int i = 0; i < size; i++)
            {
                minors[i] = Minor(a, row, i);
            }

            return minors;
        }

        /// <summary>
        /// Returns the minor of matrix a
        /// </summary>
        /// <param name="a"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public static float[,] Minor(float[,] a, int row, int col)
        {
            Debug.Assert(a.GetLength(0) == a.GetLength(1), "Minor: matrix not square!");

            int size = a.GetLength(0);

            // minor matrix
            float[,] m = new float[size - 1, size - 1];

            // minor index
            int mi = 0;
            int mj = 0;

            for (int i = 0; i < size; i++)
            {
                // skip if on same row
                if (i == row)
                {
                    continue;
                }

                for (int j = 0; j < size; j++)
                {
                    // skip if on same column
                    if (j == col)
                    {
                        continue;
                    }

                    m[mi, mj] = a[i, j];
                    mj++;
                }

                mj = 0;
                mi++;
            }

            return m;
        }

        /// <summary>
        /// Multiples first and second matrix together if compatitable
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static float[,] Multiply(float[,] first, float[,] second)
        {
            // check for compatibility
            Debug.Assert(first.GetLength(1) == second.GetLength(0), "Multiply: first and second sizes are not compatiable!");

            float[,] result = new float[first.GetLength(0), second.GetLength(1)];

            for (int i = 0; i < first.GetLength(0); i++)
            {
                for (int j = 0; j < second.GetLength(1); j++)
                {

                    for (int x = 0; x < first.GetLength(0); x++)
                    {
                        result[i, j] += first[i, x] * second[x, j];
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// Multiplies the matrix with a scale/scalar
        /// </summary>
        /// <param name="m"></param>
        /// <param name="scale"></param>
        /// <returns></returns>
        public static float[,] Multiply(float[,] m, float scale)
        {
            float[,] result = new float[m.GetLength(0), m.GetLength(1)];

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    result[i, j] = m[i, j] * scale;
                }
            }

            return result;
        }

        /// <summary>
        /// Transposes the matrix
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static float[,] Transpose(float[,] a)
        {
            float[,] t = new float[a.GetLength(1), a.GetLength(0)];

            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    t[j, i] = a[i, j];
                }
            }

            return t;
        }

        /// <summary>
        /// Converts the matrix to a more readable form
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string ToString(float[,] m)
        {
            string s = "";

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    s += m[i, j];

                    if (j != m.GetLength(0) - 1)
                    {
                        s += ", ";
                    }
                }
                s += "\n";
            }

            return s;
        }
    }
}
