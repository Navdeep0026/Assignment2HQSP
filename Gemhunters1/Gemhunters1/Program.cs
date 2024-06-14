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

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Gem Hunters Game!");
        }
    }
}
