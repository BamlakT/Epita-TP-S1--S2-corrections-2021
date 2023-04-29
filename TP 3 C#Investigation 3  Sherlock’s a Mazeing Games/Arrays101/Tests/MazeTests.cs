// ReSharper disable StringLiteralTypo

using System;
using System.IO;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [Timeout(100)]
    public class MazeTests
    {
        #region Print

        public class bPrintTest
        {
            [Test]
            public void PrintMazeEmpty()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                char[,] maze = { };
                Maze.Maze.Print(maze);
                string expected = $"{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }

            [Test]
            public void PrintWithPath()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', 'o', '#', ' ', ' ', '#'},
                    {'#', 'o', 'o', ' ', ' ', '#'},
                    {'#', '#', 'o', '#', ' ', '#'},
                    {'#', 'o', 'o', '#', ' ', '#'},
                    {'#', 'o', '#', '#', '#', '#'},
                    {'#', 'o', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '@', '#'},
                };
                Maze.Maze.Print(maze);
                string expected = $"##xx########{Environment.NewLine}" +
                                  $"##oo##    ##{Environment.NewLine}" +
                                  $"##oooo    ##{Environment.NewLine}" +
                                  $"####oo##  ##{Environment.NewLine}" +
                                  $"##oooo##  ##{Environment.NewLine}" +
                                  $"##oo########{Environment.NewLine}" +
                                  $"##oooooooo##{Environment.NewLine}" +
                                  $"########@@##{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }

            [Test]
            public void PrintNoEnd()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', ' ', '#', ' ', ' ', '#', ' ', ' ', '#'},
                    {'#', ' ', ' ', ' ', '#', '#', '#', ' ', '#'},
                    {'#', '#', '#', '#', '#', ' ', ' ', ' ', '#'},
                    {'#', ' ', '#', ' ', ' ', ' ', '#', ' ', '#'},
                    {'#', ' ', '#', ' ', '#', ' ', '#', '#', '#'},
                    {'#', ' ', ' ', ' ', '#', ' ', ' ', ' ', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', ' ', '#'},
                    {'#', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#'},
                    {'#', '@', '#', '#', '#', '#', '#', '#', '#'},
                };
                Maze.Maze.Print(maze);
                string expected = $"##xx##############{Environment.NewLine}" +
                                  $"##  ##    ##    ##{Environment.NewLine}" +
                                  $"##      ######  ##{Environment.NewLine}" +
                                  $"##########      ##{Environment.NewLine}" +
                                  $"##  ##      ##  ##{Environment.NewLine}" +
                                  $"##  ##  ##  ######{Environment.NewLine}" +
                                  $"##      ##      ##{Environment.NewLine}" +
                                  $"##############  ##{Environment.NewLine}" +
                                  $"##              ##{Environment.NewLine}" +
                                  $"##@@##############{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }
        }

        #endregion

        #region FindStart

        public class aFindStartTest
        {

            [Test]
            public void FindStartEmpty()
            {
                var x = 0;
                var y = 0;
                char[,] maze = { };

                var actual = Maze.Maze.FindStart(maze, ref x, ref y);
                Assert.False(actual);
            }

            [Test]
            public void FindStartNotExisting()
            {
                var x = 0;
                var y = 0;
                char[,] maze = {{'#', '#', '#'}, {'#', ' ', '#'}, {'#', 'x', '#'}};

                var actual = Maze.Maze.FindStart(maze, ref x, ref y);
                Assert.False(actual);
            }

            [Test]
            public void FindStartExisting()
            {
                var x = 0;
                var y = 0;
                char[,] maze = {{'#', 'x', '#'}, {'#', ' ', '#'}, {'#', '@', '#'}};

                var actual = Maze.Maze.FindStart(maze, ref x, ref y);
                Assert.True(actual);
                Assert.AreEqual(1, x);
                Assert.AreEqual(2, y);
            }
        }

        #endregion

        #region IsPathValid

        public class cIsPathValiedTest
        {
            [Test]
            [Timeout(100)]
            public void IsPathValidEmpty()
            {
                char[,] maze = { };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.False(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidNoStart()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', 'o', '#', ' ', ' ', '#'},
                    {'#', 'o', 'o', ' ', ' ', '#'},
                    {'#', '#', 'o', '#', ' ', '#'},
                    {'#', 'o', 'o', '#', ' ', '#'},
                    {'#', 'o', '#', '#', '#', '#'},
                    {'#', 'o', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '#', '#'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.False(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidNoEnd()
            {
                char[,] maze =
                {
                    {'#', '#', '#', '#', '#', '#'},
                    {'#', 'o', '#', ' ', ' ', '#'},
                    {'#', 'o', 'o', ' ', ' ', '#'},
                    {'#', '#', 'o', '#', ' ', '#'},
                    {'#', 'o', 'o', '#', ' ', '#'},
                    {'#', 'o', '#', '#', '#', '#'},
                    {'#', 'o', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '@', '#'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.False(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidNoPath()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', ' ', '#', ' ', ' ', '#'},
                    {'#', ' ', ' ', ' ', ' ', '#'},
                    {'#', '#', ' ', '#', ' ', '#'},
                    {'#', ' ', ' ', '#', ' ', '#'},
                    {'#', ' ', '#', '#', '#', '#'},
                    {'#', ' ', ' ', ' ', ' ', '#'},
                    {'#', '#', '#', '#', '@', '#'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.False(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidAlmost()
            {
                char[,] maze =
                {
                    {'#', '@', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', 'o', '#', 'o', 'o', 'o', 'o', 'o', '#'},
                    {'#', 'o', 'o', 'o', '#', '#', '#', 'o', '#'},
                    {'#', '#', '#', '#', '#', 'o', '#', 'o', '#'},
                    {'#', ' ', '#', ' ', ' ', 'o', '#', ' ', '#'},
                    {'#', ' ', '#', ' ', '#', 'o', '#', '#', '#'},
                    {'#', ' ', ' ', ' ', '#', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', 'o', '#'},
                    {'#', 'o', 'o', 'o', 'o', 'o', 'o', 'o', '#'},
                    {'#', 'x', '#', '#', '#', '#', '#', '#', '#'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.False(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidValid()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#', '#', '#', '#'},
                    {'#', 'o', '#', 'o', 'o', 'o', 'o', 'o', '#'},
                    {'#', 'o', 'o', 'o', '#', '#', '#', 'o', '#'},
                    {'#', '#', '#', '#', '#', 'o', 'o', 'o', '#'},
                    {'#', ' ', '#', ' ', ' ', 'o', '#', ' ', '#'},
                    {'#', ' ', '#', ' ', '#', 'o', '#', '#', '#'},
                    {'#', ' ', ' ', ' ', '#', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '#', '#', '#', 'o', '#'},
                    {'#', 'o', 'o', 'o', 'o', 'o', 'o', 'o', '#'},
                    {'#', '@', '#', '#', '#', '#', '#', '#', '#'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.True(actual);
            }

            [Test]
            [Timeout(100)]
            public void IsPathValidFilled()
            {
                char[,] maze =
                {
                    {'o', 'x', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                    {'o', '@', 'o', 'o', 'o', 'o', 'o', 'o', 'o'},
                };

                var actual = Maze.Maze.IsPathValid(maze);
                Assert.True(actual);
            }
        }

        #endregion

        #region FindPath

        public class dFindPathTest
        {
            [Test]
            [Timeout(100)]
            public void FindPathEmpty()
            {
                char[,] maze = { };
                var expected = (char[,]) maze.Clone();

                Maze.Maze.FindPath(maze);
                Assert.AreEqual(expected, maze);
            }

            [Test]
            [Timeout(100)]
            public void FindPathNoStart()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', 'o', '#', ' ', ' ', '#'},
                    {'#', 'o', 'o', ' ', ' ', '#'},
                    {'#', '#', 'o', '#', ' ', '#'},
                    {'#', 'o', 'o', '#', ' ', '#'},
                    {'#', 'o', '#', '#', '#', '#'},
                    {'#', 'o', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '#', '#'},
                };
                var expected = (char[,]) maze.Clone();

                Maze.Maze.FindPath(maze);
                Assert.AreEqual(expected, maze);
            }

            [Test]
            [Timeout(100)]
            public void FindPathNoEnd()
            {
                char[,] maze =
                {
                    {'#', '@', '#', '#', '#', '#'},
                    {'#', 'o', '#', ' ', ' ', '#'},
                    {'#', 'o', 'o', ' ', ' ', '#'},
                    {'#', '#', 'o', '#', ' ', '#'},
                    {'#', 'o', 'o', '#', ' ', '#'},
                    {'#', 'o', '#', '#', '#', '#'},
                    {'#', 'o', 'o', 'o', 'o', '#'},
                    {'#', '#', '#', '#', '#', '#'},
                };
                var expected = (char[,]) maze.Clone();

                Maze.Maze.FindPath(maze);
                Assert.AreEqual(expected, maze);
            }

            [Test]
            [Timeout(100)]
            public void FindPathNoPath()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', ' ', '#', ' ', ' ', '#'},
                    {'#', ' ', ' ', ' ', ' ', '#'},
                    {'#', '#', '#', '#', ' ', '#'},
                    {'#', ' ', ' ', '#', ' ', '#'},
                    {'#', ' ', '#', '#', '#', '#'},
                    {'#', ' ', ' ', ' ', ' ', '#'},
                    {'#', '#', '#', '#', '@', '#'},
                };
                var expected = (char[,]) maze.Clone();

                Maze.Maze.FindPath(maze);
                Assert.AreEqual(expected, maze);
            }

            [Test]
            [Timeout(100)]
            public void FindPathSimple()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', ' ', '#', ' ', '#', ' '},
                    {'#', ' ', '#', ' ', '#', ' '},
                    {'#', ' ', '#', ' ', '#', ' '},
                    {'#', ' ', ' ', ' ', ' ', ' '},
                    {'#', ' ', '#', '#', '#', ' '},
                    {'#', ' ', ' ', '#', ' ', ' '},
                    {'#', '#', '#', '#', '@', '#'},
                };

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }

            [Test]
            [Timeout(100)]
            public void FindPathLoop()
            {
                char[,] maze =
                {
                    {'#', 'x', '#', '#', '#', '#'},
                    {'#', ' ', '#', ' ', ' ', ' '},
                    {'#', ' ', ' ', ' ', '#', ' '},
                    {'#', ' ', '#', ' ', '#', ' '},
                    {'#', ' ', ' ', ' ', ' ', ' '},
                    {'#', ' ', '#', '#', '#', ' '},
                    {'#', ' ', ' ', '#', ' ', ' '},
                    {'#', '#', '#', '#', '@', '#'},
                };

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }

            [Test]
            [Timeout(100)]
            public void FindPathFreeMaze()
            {
                char[,] maze =
                {
                    {' ', 'x', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', ' ', ' '},
                    {' ', ' ', ' ', ' ', '@', ' '},
                };

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }
        }

        #endregion

        #region Generate

        [Timeout(100)]
        public class eGenerateTest
        {
            [Test]
            public void GenerateSmall()
            {
                const int width = 4;
                const int height = 4;
                var maze = Maze.Maze.Generate(width, height);

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }

            [Test]
            public void GenerateOdd()
            {
                const int width = 17;
                const int height = 17;
                var maze = Maze.Maze.Generate(width, height);

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }

            [Test]
            public void GenerateEven()
            {
                const int width = 20;
                const int height = 20;
                var maze = Maze.Maze.Generate(width, height);

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }

            [Test]
            public void GenerateLarge()
            {
                const int width = 100;
                const int height = 100;
                var maze = Maze.Maze.Generate(width, height);

                Maze.Maze.FindPath(maze);
                Assert.True(Maze.Maze.IsPathValid(maze));
            }
        }

        #endregion
    }
}
