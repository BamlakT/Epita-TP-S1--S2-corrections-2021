using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace puzzle_game
{
    public class Viewer
    {
        public static string convert_to_string(List<Direction> directions)
        {
            var result = "";

            var lookup_table = new List<(Direction, string)>
            {
                (Direction.UP, "UP"),
                (Direction.DOWN, "DOWN"),
                (Direction.RIGHT, "RIGHT"),
                (Direction.LEFT, "LEFT")
            };

            foreach (var direction in directions)
            {
                foreach (var item in lookup_table)
                    if (item.Item1 == direction)
                    {
                        result += direction;
                        break;
                    }

                result += '\n';
            }

            return result;
        }

        // Asynchronous function for file writing
        public static async Task WriteInFile(string matrix, string steps)
        {
            await File.WriteAllTextAsync("../../../Viewer/steps.out", matrix + '\n' + steps);
        }

        public static void Parse(Board board, List<Direction> directions)
        {
            // Create matrix string
            var matrix = "";
            for (var i = 0; i < board.Size - 1; i++)
                matrix += board.Board1[i].Value + ";";
            matrix += board.Board1.Last().Value;
            var steps = convert_to_string(directions);
            WriteInFile(matrix, steps);
        }

        public static void LaunchViewer()
        {
            var viewer = new ProcessStartInfo();
            viewer.FileName = "python";
            viewer.Arguments = "../../../Viewer/main.py";

            viewer.WorkingDirectory = Directory.GetCurrentDirectory();
            Console.WriteLine(Directory.GetCurrentDirectory());
            viewer.UseShellExecute = false;
            viewer.RedirectStandardError = true;
            viewer.RedirectStandardOutput = true;

            using (var process = Process.Start(viewer))
            {
                using (var sr = process.StandardOutput)
                {
                    Console.Write(sr.ReadToEnd());
                }

                using (var sr = process.StandardError)
                {
                    Console.Write(sr.ReadToEnd());
                }
            }
        }
    }
}
