using System;

namespace Ref
{
    public static class Program
    {
        public static void Main()
        {
            // Init board
            // var board = new Board("../../../../TestSuite/boards/conditional.asciidot");
            // var board = new Board("../../../../TestSuite/boards/duplicate.asciidot");
            // var board = new Board("../../../../TestSuite/boards/input.asciidot");
            // var board = new Board("../../../../TestSuite/boards/move.asciidot");
            // var board = new Board("../../../../TestSuite/boards/operator.asciidot");
            // var board = new Board("../../../../TestSuite/boards/move.asciidot");
            // var board = new Board("../../../../TestSuite/boards/print.asciidot");
            var board = new Board("../../../../TestSuite/boards/print_asciinewline.asciidot");
            // var board = new Board("../../../../TestSuite/boards/print_value.asciidot");
            
            // Init Interpreter
            var game = new AsciiDot(board);

            // Now exec !
            game.Launch(false);

            // foreach (var (point, direction) in game.History)
            //     Console.Write($"(new Point({point.X}, {point.Y}), Direction.{direction}), ");
        }
    }
}
