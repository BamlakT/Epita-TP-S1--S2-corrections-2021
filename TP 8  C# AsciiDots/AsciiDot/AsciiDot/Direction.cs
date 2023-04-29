using System;
using System.Collections.Generic;
using System.Linq;

namespace Ref
{
    /**
     * <summary>
     *     A small function to iterate over value of an enum (not taken from stake overflow):
     *     https://stackoverflow.com/questions/972307/how-to-loop-through-all-enum-values-in-c
     * </summary>
     */
    public static class EnumUtil
    {
        // Yes it is a dark magic (no problem if you don't understand now)
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public enum Direction
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3
    }

    public static class DirUtils
    {
        public static bool SameAxis(Direction d1, Direction d2) =>
            d1 == d2 || d1 == Invert(d2);

        public static Direction Invert(Direction direction) =>
            Rotate(Rotate(direction));

        // An enum is just int named so it can be used as it
        // so arithmetic is allow on it.
        public static Direction Rotate(Direction direction) =>
            (Direction) (((int) direction + 1) % 4);

        // algebra go brrrr
        public static int DeltaX(Direction direction) => 
            (int) direction % 2 * (1 - (int) direction / 2 * 2);

        // just a `+1` can make miracle
        public static int DeltaY(Direction direction) =>
            ((int) direction + 1) % 2 * (1 - (int) direction / 2 * 2);
    }
}
