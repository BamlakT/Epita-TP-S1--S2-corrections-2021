using System;
using System.Collections.Generic;
using System.Linq;

namespace Maze
{
    public static class Maze
    {
        public const char SpaceChar = ' ';
        public const char WallChar = '#';
        public const char StartChar = '@';
        public const char EndChar = 'x';
        public const char PathChar = 'o';

        /**
         * <summary>Looks for the character '@' in a matrix.</summary>
         * <param name="maze">A characters matrix representing a maze.</param>
         * <param name="x">A reference to the x coordinate of '@'</param>
         * <param name="y">A reference to the y coordinate of '@'</param>
         * <returns>Returns <c>true</c> if the character was found, else <c>false</c></returns>
         */
        public static bool FindStart(char[,] maze, ref int x, ref int y)
        {
            for (y = 0; y < maze.GetLength(0); y++)
            {
                for (x = 0; x < maze.GetLength(1); x++)
                    if (maze[y, x] == StartChar)
                        return true;
            }

            return false;
        }

        /**
         * <summary>Writes a maze in the standard output</summary>
         * <example>
         * <code>
         * char[,] maze = {
         *      {'#', 'x', '#', '#', '#', '#'},
         *      {'#', ' ', '#', ' ', ' ', '#'},
         *      {'#', ' ', ' ', ' ', ' ', '#'},
         *      {'#', '#', ' ', '#', ' ', '#'},
         *      {'#', ' ', ' ', '#', ' ', '#'},
         *      {'#', ' ', '#', '#', '#', '#'},
         *      {'#', ' ', ' ', ' ', ' ', '#'},
         *      {'#', '#', '#', '#', '@', '#'},
         * };
         * Maze.Print(maze);
         * </code>
         * Outputs the following:
         * <code>
         * ##xx########
         * ##  ##    ##
         * ##        ##
         * ####  ##  ##
         * ##    ##  ##
         * ##  ########
         * ##        ##
         * ########@@##
         * </code>
         * </example>
         */
        public static void Print(char[,] maze)
        {
            for (var y = 0; y < maze.GetLength(0); y++)
            {
                for (var x = 0; x < maze.GetLength(1); x++)
                {
                    // I love colors - these are obviously not mandatory.
                    switch (maze[y, x])
                    {
                        case PathChar:
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case StartChar:
                        case EndChar:
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                    }

                    Console.Write("{0}{0}", maze[y, x]);
                    Console.ResetColor();
                }

                if (y != maze.GetLength(0) - 1)
                    Console.WriteLine();
            }

            Console.WriteLine();
        }

        /**
         * <summary>Checks if a maze contains a valid path,
         * i.e. a path from the starting point to the destination.</summary>
         * <param name="maze">A characters matrix representing a maze.</param>
         * <returns>True if <c>maze</c> contains a valid path.</returns>
         */
        public static bool IsPathValid(char[,] maze)
        {
            var x = 0;
            var y = 0;

            var visited = new bool[maze.GetLength(0), maze.GetLength(1)];
            for (y = 0; y < maze.GetLength(0); y++)
            {
                for (x = 0; x < maze.GetLength(1); x++)
                    visited[y, x] = false;
            }

            return FindStart(maze, ref x, ref y) &&
                   FollowPath(maze, x, y, visited);
        }

        private static bool FollowPath(char[,] maze, int x, int y,
            bool[,] visited)
        {
            visited[y, x] = true;

            (int, int)[] dirs = {(1, 0), (0, 1), (-1, 0), (0, -1)};
            foreach (var (dx, dy) in dirs)
            {
                var newX = x + dx;
                var newY = y + dy;

                if (IsOutOfBounds(maze, newX, newY))
                    continue;

                if (maze[newY, newX] == EndChar)
                    return true;

                if (!visited[newY, newX] && maze[newY, newX] == PathChar &&
                    FollowPath(maze, newX, newY, visited))
                    return true;
            }

            return false;
        }

        /**
         * <summary>Finds a valid path from the starting point to the
         * destination and mark it in the matrix with the character 'o'.
         * If their is no such path, the maze is left unchanged.</summary>
         * <param name="maze">A characters matrix representing a maze.</param>
         */
        public static void FindPath(char[,] maze)
        {
            var x = 0;
            var y = 0;

            var visited = new bool[maze.GetLength(0), maze.GetLength(1)];
            for (y = 0; y < maze.GetLength(0); y++)
            {
                for (x = 0; x < maze.GetLength(1); x++)
                    visited[y, x] = false;
            }

            if (!FindStart(maze, ref x, ref y))
                return;

            WalkToEnd(maze, x, y, visited);
        }

        private static bool WalkToEnd(char[,] maze, int x, int y,
            bool[,] visited)
        {
            visited[y, x] = true;

            (int, int)[] dirs = {(1, 0), (0, 1), (-1, 0), (0, -1)};
            foreach (var (dx, dy) in dirs)
            {
                var newX = x + dx;
                var newY = y + dy;

                if (IsOutOfBounds(maze, newX, newY))
                    continue;

                if (maze[newY, newX] == EndChar)
                    return true;

                if (visited[newY, newX] || maze[newY, newX] != SpaceChar ||
                    !WalkToEnd(maze, newX, newY, visited)) continue;

                maze[newY, newX] = PathChar;
                return true;
            }

            return false;
        }

        /**
         * <summary>Generate a maze.</summary>
         * <param name="width">The width of the output maze - at least 4.</param>
         * <param name="height">The height of the output maze - at least 4.</param>
         * <returns>Returns a matrix of size <c>width</c>x<c>height</c>.</returns>
         *
         * BONUS
         */
        public static char[,] Generate(int width, int height)
        {
            var maze = new char[height, width];
            // Initialize a maze in which all cells (x, y)
            // where x or y is even are walls.
            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        maze[y, x] = '#';
                    else
                        maze[y, x] = ' ';
                }
            }

            CarveMaze(maze);

            return maze;
        }

        private static void CarveMaze(char[,] maze)
        {
            var visited = new bool[maze.GetLength(0), maze.GetLength(1)];
            for (var j = 0; j < maze.GetLength(0); j++)
            {
                for (var i = 0; i < maze.GetLength(1); i++)
                    visited[j, i] = false;
            }

            var (x, y) = (1, 1);
            maze[y, x] = StartChar;
            var cells = new Stack<(int, int)>();
            cells.Push((x, y));
            while (cells.Count != 0)
            {
                (x, y) = cells.Pop();

                var dirs = GetAvailableDirection(maze, x, y, visited);
                foreach (var (dx, dy) in dirs)
                {
                    maze[y + dy, x + dx] = ' ';
                    visited[y + 2 * dy, x + 2 * dx] = true;
                    cells.Push((x + 2 * dx, y + 2 * dy));
                }
            }

            x = 2 * (maze.GetLength(1) - 1) / 2 - 1;
            y = 2 * (maze.GetLength(0) - 1) / 2 - 1;
            maze[y, x] = EndChar;
        }

        private static (int, int)[] GetAvailableDirection(char[,] maze, int x,
            int y,
            bool[,] visited)
        {
            (int, int)[] candidates = {(1, 0), (0, 1), (-1, 0), (0, -1)};
            var dirs = candidates.Where(dir =>
            {
                var (dx, dy) = dir;
                return !IsOutOfBounds(maze, x + 2 * dx, y + 2 * dy) &&
                       !visited[y + 2 * dy, x + 2 * dx];
            }).ToArray();

            var random = new Random((int) DateTime.Now.Ticks);

            for (var i = dirs.Length - 1; i >= 0; i--)
            {
                var j = random.Next(dirs.Length);
                (dirs[i], dirs[j]) = (dirs[j], dirs[i]);
            }

            return dirs;
        }

        private static bool IsOutOfBounds(char[,] maze, int x, int y)
        {
            return x < 0 || y < 0 | x >= maze.GetLength(1) ||
                   y >= maze.GetLength(0);
        }
    }
}