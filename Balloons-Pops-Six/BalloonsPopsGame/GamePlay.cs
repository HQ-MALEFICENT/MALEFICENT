namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;

    public class GamePlay
    {
        static void CheckLeftNeighbor(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckLeftNeighbor(matrix, newRow, newColumn, searchedItem);
                }
            }

            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckRightNeighbor(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckRightNeighbor(matrix, newRow, newColumn, searchedItem);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckUpNeighbor(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckUpNeighbor(matrix, newRow, newColumn, searchedItem);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void CheckDownNeighbor(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    CheckDownNeighbor(matrix, newRow, newColumn, searchedItem);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return;
            }
        }

        static void PopBalloon(byte[,] matrix, int row, int column)
        {
            if (matrix[row, column] == 0)
            {
                Console.WriteLine("Illegal move: cannot pop missing balloon!");
            }
            else
            {
                byte searchedItem = matrix[row, column];
                matrix[row, column] = 0;
                CheckLeftNeighbor(matrix, row, column, searchedItem);
                CheckRightNeighbor(matrix, row, column, searchedItem);
                CheckUpNeighbor(matrix, row, column, searchedItem);
                CheckDownNeighbor(matrix, row, column, searchedItem);
            }
        }

        static void FallingDownBalloons(byte[,] matrix)
        {
            byte[,] newMatrix = matrix;
            Stack<byte> stack = new Stack<byte>();
            for (int col = 0; col < MatrixUtils.Columns; col++)
            {
                for (int row = 0; row < MatrixUtils.Rows; row++)
                {
                    if (matrix[row, col] != 0)
                    {
                        stack.Push(matrix[row, col]);
                    }
                }

                for (int row = MatrixUtils.Rows - 1; row >= 0; row--)
                {
                    try
                    {
                        newMatrix[row, col] = stack.Pop();
                    }
                    catch (Exception)
                    {
                        matrix[row, col] = 0;
                    }
                }
            }

            MatrixUtils.PrintMatrix(newMatrix);
        }

        static void sortAndPrintChartFive(string[,] tableToSort)
        {
            if (tableToSort[0, 0] == null)
            {
                Console.WriteLine("The scoreboard is empty.");
                return;
            }

            List<NameValuePair> klasirane = new List<NameValuePair>();

            for (int i = 0; i < 5; ++i)
            {
                if (tableToSort[i, 0] == null)
                {
                    break;
                }

                klasirane.Add(new NameValuePair(int.Parse(tableToSort[i, 0]), tableToSort[i, 1]));

            }

            klasirane.Sort();
            Console.WriteLine("---------TOP FIVE CHART-----------");
            for (int i = 0; i < klasirane.Count; ++i)
            {
                NameValuePair slot = klasirane[i];
                Console.WriteLine("{2}. {0} with {1} moves.", slot.username, slot.moves, i + 1);
            }
            Console.WriteLine(new string('-', 34));


        }

        static bool signIfSkilled(string[,] Chart, int points)
        {
            bool Skilled = false;
            int worstMoves = 0;
            int worstMovesChartPosition = 0;
            for (int i = 0; i < 5; i++)
            {
                if (Chart[i, 0] == null)
                {
                    Console.Write("Please enter your name for the top scoreboard: ");
                    string tempUserName = Console.ReadLine();
                    Chart[i, 0] = points.ToString();
                    Chart[i, 1] = tempUserName;
                    Skilled = true;
                    break;
                }
            }
            if (Skilled == false)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (int.Parse(Chart[i, 0]) > worstMoves)
                    {
                        worstMovesChartPosition = i;
                        worstMoves = int.Parse(Chart[i, 0]);
                    }
                }
            }
            if (points < worstMoves && Skilled == false)
            {
                Console.Write("Please enter your name for the top scoreboard: ");
                string tempUserName = Console.ReadLine();
                Chart[worstMovesChartPosition, 0] = points.ToString();
                Chart[worstMovesChartPosition, 1] = tempUserName;
                Skilled = true;
            }
            return Skilled;
        }

        public static void ProcessGame(string input, string[,] topFive, ref byte[,] matrix, ref int userMoves)
        {
            switch (input)
            {
                case "RESTART":
                    StartGame.Start();
                    break;
                case "TOP":
                    sortAndPrintChartFive(topFive);
                    break;
                case "EXIT":
                    Console.WriteLine("Good Bye!");
                    Environment.Exit(0);
                    break;
                default:
                    if ((input.Length == 3) && (input[0] >= '0' && input[0] <= '4') && (input[2] >= '0' && input[2] <= '9')
                        && (input[1] == ' ' || input[1] == '.' || input[1] == ','))
                    {
                        int userRow = (int)char.GetNumericValue(input[0]);
                        int userColumn = (int)char.GetNumericValue(input[2]);

                        PopBalloon(matrix, userRow, userColumn);
                        userMoves++;

                        if (MatrixUtils.CheckMatrixIsEmpty(matrix))
                        {
                            Console.WriteLine("Congratulations! You popped all balloons in {0} moves.", userMoves);
                            if (signIfSkilled(topFive, userMoves))
                            {
                                sortAndPrintChartFive(topFive);
                            }
                            else
                            {
                                Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                            }
                        }
                        else
                        {
                            FallingDownBalloons(matrix);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid move or command!");
                    }

                    break;
            }
        }
    }
}