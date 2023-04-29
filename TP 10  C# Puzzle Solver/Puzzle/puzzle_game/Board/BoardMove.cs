using System;
using System.Collections.Generic;
using System.Linq;

namespace puzzle_game
{
    public partial class Board
    {
        // Returns an array of possible directions
        public Direction[] GetPossibleDirections()
        {
            var result = new List<Direction>();

            var i = FindEmptyPos();

            // 2D coordinates
            var x = i / Width;
            var y = i - x * Width;

            if (!(x == 0))
                result.Add(Direction.UP);

            if (!(x == Width - 1))
                result.Add(Direction.DOWN);

            if (!(y == 0))
                result.Add(Direction.LEFT);

            if (!(y == Width - 1))
                result.Add(Direction.RIGHT);

            return result.ToArray();
        }

        // Swap two tiles in board 
        private void SwapTile(int i1, int i2)
        {
            if (Board1[i1].Type == Board1[i2].Type)
                throw new ArgumentException();

            var tmp = Board1[i1];
            Board1[i1] = Board1[i2];
            Board1[i2] = tmp;
        }

        // Moves the tiles by the direction
        // Returns true if it can move and false otherwise
        // If the board is already solved don't touch it
        public bool MoveDirection(Direction direct)
        {
            if (Solved)
                return false;

            var tile = Board1[0];

            var i = FindEmptyPos();


            // 2D coordinates
            var x = i / Width;
            var y = i - x * Width;


            var directions = GetPossibleDirections();

            if (!directions.Contains(direct))
                return false;

            y += direct == Direction.LEFT ? -1 : direct == Direction.RIGHT ? 1 : 0;
            x += direct == Direction.UP ? -1 : direct == Direction.DOWN ? 1 : 0;


            // Swaping the tiles
            SwapTile(i, x * Width + y);

            return true;
        }


        public void Shuffle(int nbr)
        {
            if (nbr <= 0)
                throw new ArgumentException("nbr need to be strictly positive !");

            var random = new Random();

            for (var i = 0; i < nbr; i++)
            {
                var possibleDirections = GetPossibleDirections();

                if (possibleDirections.Length <= 0)
                    continue;

                var index = random.Next(0, possibleDirections.Length);

                MoveDirection(possibleDirections[index]);
            }
        }
    }
}
