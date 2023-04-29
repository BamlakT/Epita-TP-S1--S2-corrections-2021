using System;
using System.Collections.Generic;
using Ref.Token;

namespace Ref
{
    public class Dot : Point
    {
        public Direction Direction;

        public Memory Memory;

        public Environment CurrentEnvironment
        {
            get => Memory.CurrentEnvironment;
            set => Memory.CurrentEnvironment = value;
        }
        
        public double Value
        {
            get => Memory.Value;
            set => Memory.Value = value;
        }

        public static int Count { get; private set; }

        public int Id { get; }

        /**
         * <summary>Constructor of Dot</summary>
         * <param name="x">The position xth row in <c>Matrix</c></param>
         * <param name="y">The position yth row in <c>Matrix</c></param>
         * <param name="direction">The direction indicates the direction of the next move in <c>Matrix</c></param>
         */
        public Dot(int x, int y, Direction direction)
            : base(x, y)
        {
            Direction = direction;
            Memory = new Memory();
            Id = Count++;
        }

        public Dot(Point point, Direction direction) : base(point)
        {
            Direction = direction;
            Memory = new Memory();
            Id = Count++;
        }

        public Dot(Dot dot, Direction direction) : base(dot)
        {
            Direction = direction;
            Memory = dot.Memory.Clone();
            Id = Count++;
        }

        public void Step() =>
            Step(Direction);

        public void Enqueue(Token.Token token) =>
            Memory.Enqueue(token);

        public void Flush() =>
            Memory.Flush();
        
        public override string ToString() =>
            $"Dot({X}, {Y}, {Direction}, {Value}, {Memory.CurrentEnvironment}, [ {string.Join(',', Memory.Queue)} ])";
    }
}
