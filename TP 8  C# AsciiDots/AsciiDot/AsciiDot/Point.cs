using System;

namespace Ref
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }

        // Column
        public int X { get; private set; }

        // Row
        public int Y { get; private set; }

        /**
         * <summary>Move the dot by changing his coordinates X and Y according to <c>direction</c></summary>
         * <returns>Return the actual Point of the dot</returns>
         */
        protected Point Step(Direction direction)
        {
            // TODO use this version
            X += DirUtils.DeltaX(direction);
            Y += DirUtils.DeltaY(direction);
            return this;
        }

        // It's strange but this is a call to Point's constructor
        public Point Clone() => new(X, Y);


        public override string ToString() =>
            $"Point({X}, {Y})";

        public override int GetHashCode() =>
            HashCode.Combine(X, Y);

        public Point MoveTo(Direction direction) =>
            Clone().Step(direction);

        public override bool Equals(object obj) =>
            obj is Point point
                ? point.X == X && point.Y == Y
                : base.Equals(obj);
    }
}
