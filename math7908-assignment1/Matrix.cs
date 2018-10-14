using System;
using System.Collections.Generic;
using System.Text;

using System.Diagnostics;

namespace math7908_assignment1
{
    class Matrix
    {

        private Matrix() { }

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
