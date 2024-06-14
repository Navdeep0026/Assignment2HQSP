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

        public bool Equals(Position other)
        {
            return X == other.X && Y == other.Y;
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

        public void Move(char direction)
        {
            switch (direction)
            {
                case 'U': Position.Y = Math.Max(0, Position.Y - 1); break;
                case 'D': Position.Y = Math.Min(5, Position.Y + 1); break;
                case 'L': Position.X = Math.Max(0, Position.X - 1); break;
                case 'R': Position.X = Math.Min(5, Position.X + 1); break;
            }
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

        public bool IsGem(Position position)
        {
            return board[position.X, position.Y] == 'G';
        }

        public bool IsObstacle(Position position)
        {
            return board[position.X, position.Y] == 'O';
        }

        public void RemoveGem(Position position)
        {
            if (board[position.X, position.Y] == 'G')
            {
                board[position.X, position.Y] = '-';
            }
        }

        public void Display(Player p1, Player p2)
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if (p1.Position.X == i && p1.Position.Y == j)
                    {
                        Console.Write("P1 ");
                    }
                    else if (p2.Position.X == i && p2.Position.Y == j)
                    {
                        Console.Write("P2 ");
                    }
                    else
                    {
                        Console.Write(board[i, j] + " ");
                    }
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

            Player player1 = new Player("P1", new Position(0, 0));
            Player player2 = new Player("P2", new Position(5, 5));
            Board board = new Board();

            for (int i = 0; i < 30; i++)
            {
                board.Display(player1, player2);
                Player currentPlayer = i % 2 == 0 ? player1 : player2;
                Console.WriteLine($"{currentPlayer.Name}'s turn. Enter move (U, D, L, R):");
                char move = Console.ReadKey().KeyChar;
                Console.WriteLine();

                Position oldPosition = new Position(currentPlayer.Position.X, currentPlayer.Position.Y);
                currentPlayer.Move(move);

                if (board.IsObstacle(currentPlayer.Position))
                {
                    currentPlayer.Position = oldPosition;
                    Console.WriteLine("Move blocked by an obstacle!");
                }
                else if (board.IsGem(currentPlayer.Position))
                {
                    currentPlayer.GemCount++;
                    board.RemoveGem(currentPlayer.Position);
                }

                Console.Clear();
            }

            Console.WriteLine("Game Over!");
            Console.WriteLine($"Player 1 Gems: {player1.GemCount}");
            Console.WriteLine($"Player 2 Gems: {player2.GemCount}");
            if (player1.GemCount > player2.GemCount)
            {
                Console.WriteLine("Player 1 Wins!");
            }
            else if (player2.GemCount > player1.GemCount)
            {
                Console.WriteLine("Player 2 Wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
        }
    }
}
