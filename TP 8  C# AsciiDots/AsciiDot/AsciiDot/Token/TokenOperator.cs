using System;
using System.Collections.Generic;
using System.Linq;

namespace Ref.Token
{
    public class TokenOperator : Token
    {
        private readonly List<Dot> _dotQueue = new();
        private Direction _direction;

        public override int PointInside =>
            _dotQueue.Count;

        public TokenOperator(char c, Direction direction)
            : base(c)
        {
            _direction = direction;
        }

        protected override string AllowedChars => "*/÷+-%^&ox>≥<≤=≠";

        protected override List<Dot> Action(Dot dot)
        {
            var otherDot = (
                from other in _dotQueue
                where !DirUtils.SameAxis(dot.Direction, other.Direction)
                select other
            ).FirstOrDefault();

            if (otherDot is null)
            {
                _dotQueue.Add(dot);
                return new List<Dot>();
            }
            else
            {
                _dotQueue.Remove(otherDot);

                if (DirUtils.SameAxis(otherDot.Direction, _direction))
                    (dot, otherDot) = (otherDot, dot);

                dot.Value = Compute(dot.Value, otherDot.Value);

                return new List<Dot> {dot};
            }
        }

        private const double Tolerance = 0.001;

        private double Compute(double lhs, double rhs) =>
            // I chose not to support the unary operator NOT
            Value switch
            {
                '*' => lhs * rhs,
                '/' or '÷' when rhs != 0 => lhs / rhs,
                '+' => lhs + rhs,
                '-' => lhs - rhs,
                '%' => lhs % rhs,
                '^' => Math.Pow(lhs, rhs),
                '&' => lhs != 0 && rhs != 0 ? 1 : 0,
                'o' => lhs != 0 || rhs != 0 ? 1 : 0,
                'x' => lhs != 0 ^ rhs != 0 ? 1 : 0,
                '>' => lhs > rhs ? 1 : 0,
                '≥' => lhs >= rhs ? 1 : 0,
                '<' => lhs < rhs ? 1 : 0,
                '≤' => lhs <= rhs ? 1 : 0,
                '=' => Math.Abs(lhs - rhs) < Tolerance ? 1 : 0,
                '≠' => Math.Abs(lhs - rhs) > Tolerance ? 1 : 0,
                _ => throw new NotSupportedException()
            };
    }
}
