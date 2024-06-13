// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
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
            case 'U':
                Position.Y -= 1;
                break;
            case 'D':
                Position.Y += 1;
                break;
            case 'L':
                Position.X -= 1;
                break;
            case 'R':
                Position.X += 1;
                break;
        }
    }
}
public class Cell
{
    public string Occupant { get; set; }

    public Cell(string occupant)
    {
        Occupant = occupant;
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
            if (Board.IsValidMove(CurrentTurn, move))
            {
                Board.UpdatePlayerPosition(CurrentTurn, move);
                Board.CollectGem(CurrentTurn);
                SwitchTurn();
                TotalTurns++;
            }
            else
            {
                Console.WriteLine("Invalid move. Try again.");
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
        {
            Console.WriteLine("Player 1 Wins!");
        }
        else if (Player2.GemCount > Player1.GemCount)
        {
            Console.WriteLine("Player 2 Wins!");
        }
        else
        {
            Console.WriteLine("It's a tie!");
        }
    }
}