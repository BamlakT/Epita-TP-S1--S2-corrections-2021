using System;
using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenMirror : Token
    {
        public bool Invert { get; } // true '/' false '\'

        protected override string AllowedChars => "/\\";

        public TokenMirror(char c)
            : base(c) =>
            Invert = c is '/';

        protected override List<Dot> Action(Dot dot)
        {
            dot.Direction = dot.Direction switch
            {
                Direction.Up => Invert ? Direction.Right : Direction.Left,
                Direction.Right => Invert ? Direction.Up : Direction.Down,
                Direction.Down => Invert ? Direction.Left : Direction.Right,
                Direction.Left => Invert ? Direction.Down : Direction.Up,
                _ => throw new ArgumentOutOfRangeException(dot.Direction.ToString())
            };

            return new List<Dot> {dot};
        }
    }
}
