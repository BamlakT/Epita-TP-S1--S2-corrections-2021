using System;
using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenPath : Token
    {
        public bool Vertical { get; }
        public bool Horizontal { get; }

        protected override string AllowedChars => "|+-";

        public TokenPath(char c)
            : base(c)
        {
            Vertical = c is '|' or '+';
            Horizontal = c is '-' or '+';
        }

        protected override List<Dot> Action(Dot dot)
        {
            var newListDots = new List<Dot>();

            if (CanTravel(dot.Direction))
                newListDots.Add(dot);

            return newListDots;
        }

        public bool CanTravel(Direction direction)
        {
            return direction switch
            {
                Direction.Up or Direction.Down => Vertical,
                Direction.Right or Direction.Left => Horizontal,
                _ => throw new ArgumentOutOfRangeException(direction.ToString())
            };
        }
    }
}
