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