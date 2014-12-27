namespace BalloonsPopsGame
{
    using System;

    public class MatrixUtils
    {
        private const byte Rows = 5;
        private const byte Columns = 10;

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
            PrintDash();

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
            PrintDash();

            Console.WriteLine();
        }

        private static void PrintDash()
        {
            for (byte column = 0; column < (Columns * 2) + 1; column++)
            {
                Console.Write("-");
            }
        }
    }
}