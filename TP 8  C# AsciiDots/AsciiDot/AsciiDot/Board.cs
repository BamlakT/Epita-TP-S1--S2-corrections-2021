using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ref.Token;

namespace Ref
{
    public class Board
    {
        /**
         * <summary>Constructor of Board</summary>
         * <param name="path">
         *      A path of a file in programming language based on ascii art: Ascii Dots
         * </param>
         */
        public Board(string path)
        {
            // Load the content of the file
            var content = LoadContent(path);

            // Yes it is ugly but we can do SQL in C#
            // https://docs.microsoft.com/fr-fr/dotnet/framework/data/adonet/sql/linq/linq-to-sql-queries
            var table =
                // iterate over each lines in reverse 
                (from row in content
                     .Split('\n')
                     .Reverse()
                 // remove comment (start with 2 back-quote ``)
                 select row.Split("``")[0]
                     .ToCharArray())
                .ToArray();

            var width = table.Max(row => row.Length);
            var height = table.Length;

            Matrix = new Token.Token[width, height];

            for (var y = 0; y < height; ++y)
                for (var x = 0; x < width; ++x)
                    Matrix[x, y] = Lexer.Lex(table, x, y);
        }

        // rows / columns
        public Token.Token[,] Matrix { get; }

        public int Width => Matrix.GetLength(0);
        public int Height => Matrix.GetLength(1);

        /**
         * <summary>Load the file path</summary>
         * <returns>Return the content of the file</returns>
         */
        private static string LoadContent(string path)
        {
            return new StreamReader(path).ReadToEnd();
        }

        /**
         * <summary>Print the board in string</summary>
         * <example>
         * <code>Console.WriteLine(board);</code>
         * </example>
         */
        public override string ToString()
        {
            var content = "";

            for (var y = Height - 1; y >= 0; --y)
            {
                for (var x = 0; x < Width; x++)
                    content += Matrix[x, y].Value;

                content += "\n";
            }

            return content;
        }

        private bool IsInside(Point point)
        {
            return IsInside(point.X, point.Y);
        }

        /**
         * <summary>Check if the position (x, y) is in Matrix</summary>
         * <param name="x">The xth row in <c>Matrix</c>. Can be negative!</param>
         * <param name="y">The yth column in <c>Matrix</c>. Can be negative!</param>
         */
        private bool IsInside(int x, int y)
        {
            return x >= 0 && x < Width &&
                   y >= 0 && y < Height;
        }

        public Token.Token Get(Point point)
        {
            return Get(point.X, point.Y);
        }

        public Token.Token Get(int x, int y) =>
            IsInside(x, y)
                ? Matrix[x, y]
                : new TokenEmpty();

        public Token.Token Set(Point point, Token.Token token)
        {
            return Set(point.X, point.Y, token);
        }

        public Token.Token Set(int x, int y, Token.Token token)
        {
            return Matrix[x, y] = token;
        }

        /**
         * <summary>
         *     Search all dots in <c>Matrix</c>
         *     and add the dot in a new list of type <c>Dot</c>
         * </summary>
         * <returns>Return a list of dots find in <c>Matrix</c></returns>
         */
        public List<Dot> StartDots()
        {
            var dots = new List<Dot>();

            for (var x = 0; x < Width; ++x)
                for (var y = 0; y < Height; ++y)
                    if (Matrix[x, y] is TokenStart)
                    {
                        // Find the correct orientation to start for the dot
                        var dot = new Dot(x, y, Direction.Up);
                        // add it only if there is a correct path to start
                        if (FindStartDirection(dot))
                            dots.Add(dot);
                    }

            return dots;
        }

        /**
         * <summary>
         *     Find the starting direction of the dot
         *     Check all direction in closewise: check first Direction.Up
         * </summary>
         * <param name="dot">A new dot</param>
         * <returns>Return true if find a direction, false if not</returns>
         */
        public bool FindStartDirection(Dot dot)
        {
            foreach (var direction in EnumUtil.GetValues<Direction>())
                if (IsStartToken(direction, Get(dot.MoveTo(direction))))
                {
                    dot.Direction = direction;
                    return true;
                }

            return false;
        }

        /**
         * <summary>Check if the starting direction is correct</summary>
         * <param name="startDirection">The starting direction of a fresh dot</param>
         * <param name="token">
         *     The token obtained in Matrix by the movement of the fresh dot by
         *     <c>startDirection</c>
         * </param>
         * <returns>
         *     Return true if the fresh dot can be move on the direction
         *     <c>startDirection</c>, false if not
         * </returns>
         */
        private static bool IsStartToken(Direction startDirection, Token.Token token)
        {
            return token switch
            {
                TokenPath path => path.CanTravel(startDirection),
                TokenInsertor => true,
                TokenEnd or TokenEmpty or TokenStart => false,
                _ => false
            };
        }
    }
}
