using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenInsertor : Token
    {
        public Direction Direction { get; }
        protected override string AllowedChars => "^>v<";

        public TokenInsertor(char c)
            : base(c)
        {
            Direction = c switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new LexerError(c)
            };
        }

        protected override List<Dot> Action(Dot dot)
        {
            // If the direction of the dot is perpendicular to Direction
            if (dot.Direction != DirUtils.Invert(Direction))
                dot.Direction = Direction;

            return new List<Dot> {dot};
        }
    }
}
