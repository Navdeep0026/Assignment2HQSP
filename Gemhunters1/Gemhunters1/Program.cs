using System;

namespace GemHunters
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Player
    {
        public string Name { get; set; }
        public Position Position { get; set; }
        public int GemCount { get; set; }

        public Player(string name, Position position)
        {
            Name = name;
            Position = position;
            GemCount = 0;
        }
    }

    public class Board
    {
        private const int Size = 6;
        private char[,] board = new char[Size, Size];
        private Random random = new Random();

        public Board()
        {
            InitializeBoard();
            PlaceGems(5);
            PlaceObstacles(3);
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        private void PlaceGems(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PlaceRandom('G');
            }
        }

        private void PlaceObstacles(int count)
        {
            for (int i = 0; i < count; i++)
            {
                PlaceRandom('O');
            }
        }

        private void PlaceRandom(char item)
        {
            int x, y;
            do
            {
                x = random.Next(Size);
                y = random.Next(Size);
            } while (board[x, y] != '-');

            board[x, y] = item;
        }

        public void Display()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gem Hunters Game!");
            Board board = new Board();
            board.Display();
        }
    }
}
