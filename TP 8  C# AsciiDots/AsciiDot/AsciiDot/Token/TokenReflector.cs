using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenReflector : Token
    {
        public TokenReflector(char c)
            : base(c)
        {
            Direction =
                c switch
                {
                    '(' => Direction.Left,
                    ')' => Direction.Right,
                    _ => throw new LexerError(c)
                };
        }

        // direction of the mirror
        public Direction Direction { get; }

        protected override string AllowedChars => "()";

        protected override List<Dot> Action(Dot dot)
        {
            dot.Direction = dot.Direction == Direction
                ? DirUtils.Invert(dot.Direction)
                : dot.Direction;

            return new List<Dot> {dot};
        }
    }
}
