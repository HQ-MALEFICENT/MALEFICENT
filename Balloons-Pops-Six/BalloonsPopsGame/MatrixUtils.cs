namespace BalloonsPopsGame
{
    using System;

    public class MatrixUtils
    {
        public const byte Rows = 5;
        public const byte Columns = 10;

        public static byte[,] CreateMatrix()
        {
            byte[,] matrix = new byte[Rows, Columns];
            Random randNumber = new Random();
            for (byte row = 0; row < Rows; row++)
            {
                for (byte column = 0; column < Columns; column++)
                {
                    matrix[row, column] = (byte)randNumber.Next(1, 5);
                }
            }

            return matrix;
        }

        public static void PrintMatrix(byte[,] matrix)
        {
            Console.Write("{0, 4}", " ");
            for (byte column = 0; column < Columns; column++)
            {
                Console.Write(column + " ");
            }

            Console.Write("{0}{1, 3}", "\n", " ");
            Console.Write(new string('-', (Columns * 2) + 1));

            Console.WriteLine();

            for (byte row = 0; row < Rows; row++)
            {
                Console.Write(row + " | ");
                for (byte col = 0; col < Columns; col++)
                {
                    if (matrix[row, col] == 0)
                    {
                        Console.Write("{0, -2}", ".");
                        continue;
                    }

                    Console.Write(matrix[row, col] + " ");
                }

                Console.Write("|");
                Console.WriteLine();
            }

            Console.Write("{0, 3}", " ");
            Console.Write(new string('-', (Columns * 2) + 1));

            Console.WriteLine();
        }

        public static bool CheckMatrixIsEmpty(byte[,] matrix)
        {
            bool isEmpty = true;
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (matrix[row, col] != 0)
                    {
                        isEmpty = false;
                    }
                }
            }

            return isEmpty;
        }
    }
}