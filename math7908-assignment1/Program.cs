using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace math7908_assignment1
{
    class Program
    {

        static void Main(string[] args)
        {
            string fileName = "./data/mq4data17.csv";

            Console.WriteLine("Reading from " + fileName + "...");

            List<MatrixInputData> dataSets = ReadFileInput(fileName);

            Console.WriteLine("Found " + dataSets.Count + " data sets:");

            for (int i = 0; i < dataSets.Count; i++)
            {
                Console.WriteLine("{0}) {1}x{2}", i + 1, dataSets[i].matrix.GetLength(0), dataSets[i].matrix.GetLength(1));
            }

            Console.WriteLine("===================");

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine();

                SolveMatrix(dataSets[i]);

                Console.WriteLine();
            }

            Console.ReadKey();
        }

        private static string FormatAsQuizAnswer(float[,] m)
        {
            string s = "";

            s += "[";

            for (int i = 0; i < m.GetLength(0); i++)
            {
                for (int j = 0; j < m.GetLength(1); j++)
                {
                    s += (int) Math.Round(m[i, j]);

                    if (i != m.GetLength(0) - 1)
                    {
                        s += ",";
                    }
                }
            }

            s += "]";

            return s;
        }

        private static void SolveMatrix(MatrixInputData input)
        {
            Console.WriteLine("Processing {0}x{1}...", input.matrix.GetLength(0), input.matrix.GetLength(1));

            Stopwatch watch = Stopwatch.StartNew();

            float[,] inverse = new PerformanceMatrix().Inverse(input.matrix);

            float[,] answer = Matrix.Multiply(inverse, input.rhs);

            watch.Stop();

            Console.WriteLine();

           Console.WriteLine("Answer=\n" + FormatAsQuizAnswer(answer));

            Console.WriteLine("Finished in " + watch.ElapsedMilliseconds + "ms");
        }

        private static List<MatrixInputData> ReadFileInput(string fileName)
        {
            List<MatrixInputData> dataSets = new List<MatrixInputData>();

            StreamReader stream = new StreamReader(fileName);

            string line;

            while ((line = stream.ReadLine()) != null)
            {
                if (line[0] == '#')
                {
                    // convert to numbers ... example: "#4x4" -> "4x4" -> ["4", "4"]
                    string[] size = line.Substring(1).Split('x');

                    int rows = int.Parse(size[0]);

                    int cols = int.Parse(size[0]);

                    MatrixInputData data = new MatrixInputData();

                    data.matrix = new float[rows, cols];

                    data.rhs = new float[rows, 1];

                    // read data 
                    for (int i = 0; i < rows; i++)
                    {
                        line = stream.ReadLine();

                        string[] values = line.Split(','); 

                        // read entire row matrix values
                        for (int j = 0; j < cols; j++)
                        {
                            data.matrix[i, j] = float.Parse(values[j]);
                        }

                        // read the last column as rhs
                        data.rhs[i, 0] = float.Parse(values[cols]);
                    }

                    dataSets.Add(data);
                }
            }

            return dataSets;
        }

        private class MatrixInputData
        {
            public float[,] matrix;

            public float[,] rhs;
        }
    }
}
