namespace puzzle_game
{
    public enum TileType
    {
        EMPTY,
        FULL
    }

    public class Tile
    {
        public Tile(int value, bool empty)
        {
            Type = empty ? TileType.EMPTY : TileType.FULL;
            Value = value;
        }

        public TileType Type { get; private set; }

        public int Value { get; }

        public Tile DeepCopy()
        {
            var copy = (Tile) MemberwiseClone();
            copy.Type = Type;
            return copy;
        }
    }
}
