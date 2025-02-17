namespace Snake;

public class GridPosition
{
    public int Row { get; }
    public int Column { get; }

    public GridPosition(int row, int col)
    {
        Row = row;
        Column = col;
    }

    public GridPosition Translate(Direction dir)
    {
        return new GridPosition(Row + dir.RowOffset, Column + dir.ColumnOffset);
    }

    protected bool Equals(GridPosition other)
    {
        return Row == other.Row && Column == other.Column;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((GridPosition)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Column);
    }

    public static bool operator ==(GridPosition? left, GridPosition? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(GridPosition? left, GridPosition? right)
    {
        return !Equals(left, right);
    }
}