namespace BalloonsPopsGame
{
    using System;
    using System.Collections.Generic;

    class GamePlay
    {

        static void checkLeft(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column - 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0; checkLeft(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }

        static void checkRight(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row;
            int newColumn = column + 1;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkRight(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }
        static void checkUp(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row + 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkUp(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }
        }

        static void checkDown(byte[,] matrix, int row, int column, int searchedItem)
        {
            int newRow = row - 1;
            int newColumn = column;
            try
            {
                if (matrix[newRow, newColumn] == searchedItem)
                {
                    matrix[newRow, newColumn] = 0;
                    checkDown(matrix, newRow, newColumn, searchedItem);
                }
                else return;
            }
            catch (IndexOutOfRangeException)
            { return; }

        }
        static bool change(byte[,] matrixToModify, int rowAtm, int columnAtm)
        {
            if (matrixToModify[rowAtm, columnAtm] == 0)
            {
                return true;
            }
            byte searchedTarget = matrixToModify[rowAtm, columnAtm];
            matrixToModify[rowAtm, columnAtm] = 0;
            checkLeft(matrixToModify, rowAtm, columnAtm, searchedTarget);
            checkRight(matrixToModify, rowAtm, columnAtm, searchedTarget);


            checkUp(matrixToModify, rowAtm, columnAtm, searchedTarget);
            checkDown(matrixToModify, rowAtm, columnAtm, searchedTarget);
            return false;
        }

        static bool doit(byte[,] matrix)
        {
            bool isWinner = true;
            Stack<byte> stek = new Stack<byte>();
            int columnLenght = matrix.GetLength(0);
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                for (int i = 0; i < columnLenght; i++)
                {
                    if (matrix[i, j] != 0)
                    {
                        isWinner = false;
                        stek.Push(matrix[i, j]);
                    }
                }
                for (int k = columnLenght - 1; (k >= 0); k--)
                {
                    try
                    {
                        matrix[k, j] = stek.Pop();
                    }
                    catch (Exception)
                    {
                        matrix[k, j] = 0;
                    }
                }
            }
            return isWinner;
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
                Console.WriteLine("{2}.   {0} with {1} moves.", slot.name, slot.val, i + 1);
            }
            Console.WriteLine("----------------------------------");


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

        public static void ProcessGame(string temp, string[,] topFive, ref byte[,] matrix, ref int userMoves)
        {
            switch (temp)
            {
                case "RESTART":
                    StartGame.Start();
                    break;
                case "TOP":
                    sortAndPrintChartFive(topFive);
                    break;
                case "EXIT": Console.WriteLine("Good Bye!");
                    Environment.Exit(0);
                    break;
                default:
                    if ((temp.Length == 3) && (temp[0] >= '0' && temp[0] <= '9') && (temp[2] >= '0' && temp[2] <= '9')
                        && (temp[1] == ' ' || temp[1] == '.' || temp[1] == ','))
                    {
                        int userRow, userColumn;
                        userRow = int.Parse(temp[0].ToString());
                        if (userRow > 4)
                        {
                            Console.WriteLine("Invalid move or command!");
                            return;
                        }
                        userColumn = int.Parse(temp[2].ToString());

                        if (change(matrix, userRow, userColumn))
                        {
                            Console.WriteLine("Illegal move: cannot pop missing balloon!");
                            return;
                        }
                        userMoves++;
                        if (doit(matrix))
                        {
                            Console.WriteLine("Congratulations! You popped all baloons in {0} moves.", userMoves);
                            if (signIfSkilled(topFive, userMoves))
                            {
                                sortAndPrintChartFive(topFive);
                            }
                            else
                            {
                                Console.WriteLine("I am sorry you are not skillful enough for TopFive chart!");
                            }
                            matrix = MatrixUtils.CreateMatrix();
                            userMoves = 0;
                        }
                        MatrixUtils.PrintMatrix(matrix);
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
