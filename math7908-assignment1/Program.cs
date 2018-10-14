using System;

namespace math7908_assignment1
{
    class Program
    {
        static void Main(string[] args)
        {
            TestDeterminant();

            Console.ReadKey();
        }

        public static void TestDeterminant()
        {
            float[,] m1 = new float[3, 3];

            m1[0, 0] = 15;
            m1[0, 1] = 22;
            m1[0, 2] = 31;

            m1[1, 0] = 47;
            m1[1, 1] = 52;
            m1[1, 2] = 62;

            m1[2, 0] = 76;
            m1[2, 1] = 83;
            m1[2, 2] = 95;

            float det = Matrix.Determinant(m1);

            Console.WriteLine(Matrix.ToString(m1));
            Console.WriteLine();

            Console.WriteLine(det);
        }

        public static void TestMinor()
        {
            float[,] m1 = new float[3, 3];

            m1[0, 0] = 15;
            m1[0, 1] = 22;
            m1[0, 2] = 31;

            m1[1, 0] = 47;
            m1[1, 1] = 52;
            m1[1, 2] = 62;

            m1[2, 0] = 76;
            m1[2, 1] = 83;
            m1[2, 2] = 95;

            float[][,] minors = Matrix.LineMinors(m1);

            Console.WriteLine(Matrix.ToString(m1));
            Console.WriteLine();

            foreach (float[,] minor in minors)
            {
                Console.WriteLine(Matrix.ToString(minor));
                Console.WriteLine();
            }
        }

        public static void TestMultiply()
        {
            float[,] m1 = new float[2, 2];

            m1[0, 0] = 5;
            m1[0, 1] = 1;
            m1[1, 0] = 7;
            m1[1, 1] = 2;

            float[,] m2 = new float[2, 2];

            m2[0, 0] = 8;
            m2[0, 1] = 2;
            m2[1, 0] = 6;
            m2[1, 1] = 4;

            float[,] m3 = Matrix.Multiply(m1, m2);

            Console.WriteLine(Matrix.ToString(m3));
        }
    }
}
