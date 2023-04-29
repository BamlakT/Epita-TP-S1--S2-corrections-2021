using System;
using System.Linq;

namespace puzzle_game
{
    public partial class Board
    {
        // Board constructor
        public Board(int size)
        {
            if (size <= 0)
                throw new ArgumentException("Size needs to be positive !");

            // Checking if size is a perfect square
            if (Math.Sqrt(size) % 1 != 0)
                throw new ArgumentException("Size needs to be a perfect square !");

            Size = size;
            Width = Convert.ToInt32(Math.Sqrt(Size));
            Board1 = new Tile[size];
            Solved = false;
        }


        public bool Solved { get; }

        public int Size { get; }

        public int Width { get; }

        public Tile[] Board1 { get; private set; }

        public Board DeepCopy()
        {
            var copy = (Board) MemberwiseClone();

            copy.Board1 = new Tile[copy.Size];

            for (var i = 0; i < copy.Size; i++)
                copy.Board1[i] = Board1[i].DeepCopy();

            return copy;
        }

        private bool AreConsecutive(int[] arr)
        {
            if (arr.Length < 1)
                return false;

            var min = arr.Min();
            var max = arr.Max();

            if (max - min + 1 != arr.Length)
                return false;

            var visited = new bool[arr.Length];

            foreach (var e in arr)
            {
                if (visited[e - min])
                    return false;
                visited[e - min] = true;
            }

            return true;
        }


        public void Fill(int[] array)
        {
            // The 0 represent the empty TILE
            if (array.Length != Size)
                throw new ArgumentException("Size need to match !");

            if (!AreConsecutive(array))
                throw new ArgumentException("Array should be composed consecutive elements !");

            var holes = 0;

            for (var i = 0; i < Size; i++)
            {
                var item = array[i];

                holes += item == 0 ? 1 : 0;

                Board1[i] = new Tile(item, item == 0);
            }

            if (holes != 1)
                throw new ArgumentException("There's more than one hole !");
        }

        // Fill from nbr
        public void Fill()
        {
            for (var i = 1; i < Size; i++)
                Board1[i - 1] = new Tile(i, false);

            Board1[Size - 1] = new Tile(0, true);
            //Shuffle(this.size);
        }
    }
}
