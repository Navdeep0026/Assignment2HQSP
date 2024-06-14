using System;

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

    public void Move(char direction)
    {
        switch (direction)
        {
            case 'U': Position.Y -= 1; break;
            case 'D': Position.Y += 1; break;
            case 'L': Position.X -= 1; break;
            case 'R': Position.X += 1; break;
        }
    }
}

public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant = "-")
    {
        Occupant = occupant;
    }
}

public class Board
{
    private static Random rnd = new Random();
    public Cell[,] Grid { get; private set; }

    public Board(Player player1, Player player2)
    {
        Grid = new Cell[6, 6];
        InitializeBoard();
        PlaceEntity(player1.Position, player1.Name);
        PlaceEntity(player2.Position, player2.Name);
        PlaceObstacles();
        PlaceGems();
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < 6; i++)
            for (int j = 0; j < 6; j++)
                Grid[i, j] = new Cell();
    }

    private void PlaceEntity(Position pos, string entity)
    {
        Grid[pos.X, pos.Y].Occupant = entity;
    }

    private void PlaceObstacles()
    {
        PlaceRandomEntities("O", 5);
    }

    private void PlaceGems()
    {
        PlaceRandomEntities("G", 10);
    }

    private void PlaceRandomEntities(string entity, int count)
    {
        while (count > 0)
        {
            int x = rnd.Next(6);
            int y = rnd.Next(6);
            if (Grid[x, y].Occupant == "-")
            {
                Grid[x, y].Occupant = entity;
                count--;
            }
        }
    }

    public void Display()
    {
        for (int i = 0; i < 6; i++)
        {
            for (int j = 0; j < 6; j++)
                Console.Write(Grid[i, j].Occupant + " ");
            Console.WriteLine();
        }
    }

    public bool IsValidMove(Position position, char direction)
    {
        int newX = position.X, newY = position.Y;
        switch (direction)
        {
            case 'U': newY -= 1; break;
            case 'D': newY += 1; break;
            case 'L': newX -= 1; break;
            case 'R': newX += 1; break;
        }
        return newX >= 0 && newX < 6 && newY >= 0 && newY < 6 && Grid[newX, newY].Occupant != "O";
    }

    public void CollectGem(Player player)
    {
        if (Grid[player.Position.X, player.Position.Y].Occupant == "G")
        {
            player.GemCount++;
            Grid[player.Position.X, player.Position.Y].Occupant = "-";
        }
    }

    public void UpdatePlayerPosition(Player player, char direction)
    {
        Grid[player.Position.X, player.Position.Y].Occupant = "-";
        player.Move(direction);
        Grid[player.Position.X, player.Position.Y].Occupant = player.Name;
    }
}

public class Game
{
    private Board Board { get; set; }
    private Player Player1 { get; set; }
    private Player Player2 { get; set; }
    private Player CurrentTurn { get; set; }
    private int TotalTurns { get; set; }

    public Game()
    {
        Player1 = new Player("P1", new Position(0, 0));
        Player2 = new Player("P2", new Position(5, 5));
        Board = new Board(Player1, Player2);
        CurrentTurn = Player1;
        TotalTurns = 0;
    }

    public void Start()
    {
        while (!IsGameOver())
        {
            Console.Clear();
            Board.Display();
            Console.WriteLine($"{CurrentTurn.Name}'s Turn. Enter Move (U, D, L, R): ");
            char move = Console.ReadKey().KeyChar;
            Console.WriteLine();
            if (Board.IsValidMove(CurrentTurn.Position, move))
            {
                Board.UpdatePlayerPosition(CurrentTurn, move);
                Board.CollectGem(CurrentTurn);
                SwitchTurn();
                TotalTurns++;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
                System.Threading.Thread.Sleep(1000);
            }
        }
        AnnounceWinner();
    }

    private void SwitchTurn()
    {
        CurrentTurn = CurrentTurn == Player1 ? Player2 : Player1;
    }

    private bool IsGameOver()
    {
        return TotalTurns >= 30;
    }

    private void AnnounceWinner()
    {
        Console.Clear();
        Board.Display();
        Console.WriteLine($"Game Over! P1 Gems: {Player1.GemCount}, P2 Gems: {Player2.GemCount}");
        if (Player1.GemCount > Player2.GemCount)
            Console.WriteLine("Player 1 Wins!");
        else if (Player2.GemCount > Player1.GemCount)
            Console.WriteLine("Player 2 Wins!");
        else
            Console.WriteLine("It's a tie!");
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}