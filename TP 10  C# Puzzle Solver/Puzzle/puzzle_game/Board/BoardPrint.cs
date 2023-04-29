using System;

namespace puzzle_game
{
    public partial class Board
    {
        public string PadCenter(string s, int width, char c)
        {
            if (s == null ||
                width <= s.Length)
                return s;

            var padding = width - s.Length;
            return s.PadRight(s.Length + padding / 2, c).PadLeft(width, c);
        }

        private void PrintLine(int i, int width, int longest_number)
        {
            if (i == 0)
                Console.Write('╔');
            else if (i == width)
                Console.Write("╚");
            else
                Console.Write('╠');

            for (var ligne_index = 0; ligne_index < width; ligne_index++)
            {
                for (var p = 0; p < longest_number + 2; p++)
                    Console.Write("═");

                if (ligne_index == width - 1)
                    break;

                if (i == width)
                    Console.Write("╩");
                else if (i == 0)
                    Console.Write("╦");
                else
                    Console.Write("╬");
            }


            if (i == 0)
                Console.WriteLine('╗');
            else if (i == width)
                Console.WriteLine("╝");
            else
                Console.WriteLine('╣');
        }

        // Pretty print the board
        public void Print()
        {
            // Determining the longest index
            var longest_number = 1;

            foreach (var tile in Board1)
            {
                var length_of_elt = (int) Math.Floor(Math.Log10(tile.Value)) + 1;
                longest_number = length_of_elt > longest_number ? length_of_elt : longest_number;
            }


            for (var i = 0; i < Width; i++)
            {
                PrintLine(i, Width, longest_number);


                for (var j = 0; j < Width; j++)
                {
                    var index = Board1[i * Width + j].Type == TileType.FULL
                        ? Board1[i * Width + j].Value.ToString()
                        : "X";

                    Console.Write("║" + PadCenter(index, longest_number + 2, ' '));
                    if (!(j < Width - 1))
                        Console.Write("║\n");
                }
            }

            PrintLine(Width, Width, longest_number);
        }
    }
}
