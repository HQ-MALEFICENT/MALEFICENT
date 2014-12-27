namespace BalloonsPopsGame
{
    using System;

    public class StartGame
    {
        static readonly string[,] TopFive = new string[5, 2];
        private static void Main()
        {
            Start();
        }

        public static void Start()
        {
            Console.WriteLine(
                "\nWelcome to \"Balloons Pops\" game. Please try to pop the balloons. Use 'top' to view the top scoreboard, 'restart' to start a new game and 'exit' to quit the game.");
            
            byte[,] matrix = MatrixUtils.CreateMatrix();

            MatrixUtils.PrintMatrix(matrix);
            string input;
            int userMoves = 0;

            while (true)
            {
                Console.Write("Enter a row and column: ");
                input = Console.ReadLine();
                input = input.ToUpper().Trim();

                GamePlay.ProcessGame(input, TopFive, ref matrix, ref userMoves);
            }
        }
    }
}