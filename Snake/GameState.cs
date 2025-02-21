namespace Snake;

public class GameState
{
    public int Rows { get; }
    public int Columns { get; }
    public GridValue[,] Grid { get; }
    public Direction Dir { get; private set; }
    public int Score { get; private set; }
    public bool GameOver { get; set; }

    private DateTime lastRareFoodSpawnTime = DateTime.MinValue; // track when rareFood last spawned
    private DateTime rareFoodSpawnTime; // track when current rareFood was spawned
    private GridPosition? rareFoodPosition = null;
    private readonly LinkedList<GridPosition> snakePosition = new LinkedList<GridPosition>();
    private readonly Random random = new Random(); // where food will spawn
    private readonly LinkedList<Direction> dirChanges = new LinkedList<Direction>();

    public GameState(int rows, int columns)
    {
        Rows = rows;
        Columns = columns;
        Grid = new GridValue[rows, columns];
        Dir = Direction.Right;
        
        AddSnake();
        AddFood();
        AddRareFood();
    }
    
    public void Move()
    {
        if (dirChanges.Count > 0)
        {
            Dir = dirChanges.First.Value;
            dirChanges.RemoveFirst();
        }
        
        GridPosition newHeadPosition = HeadPosition().Translate(Dir);
        GridValue hit = WillHit(newHeadPosition);

        if (hit == GridValue.Outside || hit == GridValue.Snake)
        {
            GameOver = true;
        }
        else if (hit == GridValue.Empty)
        {
            RemoveTail();
            AddHead(newHeadPosition);
        }
        else if (hit == GridValue.Food)
        {
            AddHead(newHeadPosition);
            Score++;
            AddFood();
        }
        else if (hit == GridValue.RareFood) // 2 points for rare food
        {
            AddHead(newHeadPosition);
            Score+=2;
            AddRareFood();
        }
        
        AddRareFood();
    }

    private Direction GetLastDirection()
    {
        if (dirChanges.Count == 0)
        {
            return Dir;
        }

        return dirChanges.Last.Value;
    }

    private bool CanChangeDirection(Direction newDir)
    {
        if (dirChanges.Count == 2) // buffer size for ChangeDirection
        {
            return false;
        }

        Direction lastDir = GetLastDirection();
        return newDir != lastDir && newDir != lastDir.Opposite();
    }

    public void ChangeDirection(Direction dir)
    {
        if(CanChangeDirection(dir))
        {
            dirChanges.AddLast(dir);
        }
    }
    
    public GridPosition HeadPosition()
    {
        return snakePosition.First.Value;
    }

    private GridPosition TailPosition()
    {
        return snakePosition.Last.Value;
    }

    public IEnumerable<GridPosition> SnakePosition()
    {
        return snakePosition;
    }

    private bool OutsideGrid(GridPosition position)
    {
        return position.Row < 0 || position.Row >= Rows || position.Column < 0 || position.Column >= Columns;
    }

    private GridValue WillHit(GridPosition newHeadPosition)
    {
        if (OutsideGrid(newHeadPosition))
        {
            return GridValue.Outside;
        }

        if (newHeadPosition == TailPosition())
        {
            return GridValue.Empty;
        }

        return Grid[newHeadPosition.Row, newHeadPosition.Column];
    }

    private void AddHead(GridPosition position)
    {
        snakePosition.AddFirst(position);
        Grid[position.Row, position.Column] = GridValue.Snake;
    }

    private void RemoveTail()
    {
        GridPosition tail = snakePosition.Last.Value;
        Grid[tail.Row, tail.Column] = GridValue.Empty;
        snakePosition.RemoveLast();
    }

    private void AddSnake() // start position of snake
    {
        var startingRow = Rows / 2;

        for (int startingCol = 1; startingCol <= 3; startingCol++)
        {
            Grid[startingRow, startingCol] = GridValue.Snake;
            snakePosition.AddFirst(new GridPosition(startingRow, startingCol));
        }
    }

    private void AddFood()
    {
        var empty = new List<GridPosition>(EmptyPositions());
        
        if (empty.Count == 0)
        {
            return;
        }

        GridPosition position = empty[random.Next(empty.Count)];
        Grid[position.Row, position.Column] = GridValue.Food;
    }
    
    /*
        AddRareFood will spawn food at rarer intervals 
        This food will add 2 points to the score
        It can spawn alongside normal food, but there should be no more than 1 of each available at the same time
        This food will also only be available for 8 seconds (test to assess)
    */
    
    private void AddRareFood() // spawn rareFood 
    
    // ### Issue: Body block set to background block when rareFood consumed
    // ### Issue: If rareFood spawns when game starts, it doesn't persist for 2 seconds
    {
        RemoveRareFood();

        if ((DateTime.Now - rareFoodSpawnTime).TotalSeconds >= 15) // has it been 35 seconds since last spawn - need to randomise this
        {
            // Make rare food spawn less frequently
            if (random.NextDouble() > 0.45) // 45% chance per tick
            {
                var empty = new List<GridPosition>(EmptyPositions()); // get all empty positions on the grid{
                if (empty.Count > 0)
                {
                    GridPosition position = empty[random.Next(empty.Count)]; // get empty random position
                    Grid[position.Row, position.Column] = GridValue.RareFood; // spawn RareFood on it

                    rareFoodPosition = position;
                    rareFoodSpawnTime = DateTime.Now;
                }
            }
        }
        lastRareFoodSpawnTime = DateTime.Now;
    }

    private void RemoveRareFood()
    {
        if (rareFoodPosition is not null && (DateTime.Now - rareFoodSpawnTime).TotalSeconds >= 2) // remove after 2 seconds
        {
            Grid[rareFoodPosition.Row, rareFoodPosition.Column] = GridValue.Empty;
            rareFoodPosition = null;
        }
    }

    private IEnumerable<GridPosition> EmptyPositions() // get empty positions to add food to
    {
        for (int r = 0; r < Rows; r++)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (Grid[r, c] == GridValue.Empty)
                {
                    yield return new GridPosition(r, c);
                }
            }
        }
    }
}