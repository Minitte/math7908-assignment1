using System;

namespace math7908_assignment1
{
    class Program
    {
        static void Main(string[] args)
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

            Console.ReadKey();
        }
    }
}
