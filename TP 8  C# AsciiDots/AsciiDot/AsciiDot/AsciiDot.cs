using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ref.Token;

namespace Ref
{
    public class AsciiDot
    {
        private readonly Board board;
        public List<Dot> Dots { get; private set; }

        public List<(Point, Direction)> History = new();

        public AsciiDot(Board board)
        {
            this.board = board;
            Dots = board.StartDots();
        }

        private List<Dot> UpdateDot(Dot dot)
        {
            var previousDir = dot.Direction;
            var newListDotsAlive = board.Get(dot).Apply(dot);
            foreach (var newDot in newListDotsAlive)
            {
                var previousPos = newDot.Clone();
                newDot.Step();
                if (previousDir != newDot.Direction)
                    History.Add((previousPos, previousDir));
            }

            return newListDotsAlive;
        }

        public void UpdateGame() =>
            Dots = Dots
                .Select(UpdateDot)
                .SelectMany(x => x)
                .ToList();

        private bool IsRunning =>
            Dots.Any();

        /**
         * <summary>
         *     Launch the execution of the code AsciiDots.
         *     The execution is still running while there is dot can be move or do a action.
         *     Be careful how you read the list of the dots!
         *     If you add a new dot because of the action in the case contains token duplicate '*',
         *     you must apply the next action on the next execution and not on the current execution.
         * </summary>
         */
        public void Launch(bool print)
        {
            // TODO add in the subject how tu use dotnet run

            if (print)
            {
                Console.Clear();
                // black magic with bash code : https://tldp.org/HOWTO/Bash-Prompt-HOWTO/x361.html
                Console.Write("\x1b[1000A");
                Console.WriteLine(this);
                Console.WriteLine("\x1b[32m==== Program start ====\x1b[0m");
                Console.Write("\x1b[s");
            }

            while (IsRunning)
                try
                {
                    if (print)
                    {
                        Console.Write("\x1b[1000A");
                        Console.WriteLine(this);
                        Console.Write("\x1b[u");
                        UpdateGame();
                        Console.Write("\x1b[s");
                        Thread.Sleep(100);
                    }

                    UpdateGame();
                }
                catch (EndOfProgram)
                {
                    break;
                }

            if (print)
                Console.WriteLine("\x1b[31m==== Program end ====\x1b[0m");
        }


        public override string ToString()
        {
            var content = "";

            for (var y = 0; y < board.Height; ++y)
            {
                for (var x = 0; x < board.Width; x++)
                {
                    var point = new Point(x, board.Height - y - 1);
                    content += ColorNumber(point) + board.Get(point).Value;
                }

                content += "\x1b[0m\n";
            }

            return content;
        }

        private string ColorNumber(int x, int y) =>
            ColorNumber(new Point(x, y));

        private string ColorNumber(Point point) =>
            // use of bash code color: https://misc.flogisoft.com/bash/tip_colors_and_formatting
            CountDotsAt(point) switch
            {
                0 => "\x1b[0m",
                1 => "\x1b[31m",
                2 => "\x1b[32m",
                3 => "\x1b[33m",
                4 => "\x1b[34m",
                5 => "\x1b[35m",
                6 => "\x1b[36m",
                7 => "\x1b[37m",
                _ => "\x1b[30m",
            };

        private int CountDotsAt(Point point) =>
            Dots.Count(point.Equals) + board.Get(point).PointInside;
    }
}