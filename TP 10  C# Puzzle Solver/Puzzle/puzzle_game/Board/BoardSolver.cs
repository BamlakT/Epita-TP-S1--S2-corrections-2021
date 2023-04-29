using System;
using System.Collections.Generic;
using puzzle_game.SimpleGraph;

namespace puzzle_game
{
    public partial class Board
    {
        private int ManhattanDistance(int i1, int i2)
        {
            var x1 = i1 / Width;
            var y1 = i1 - x1 * Width;

            var x2 = i2 / Width;
            var y2 = i2 - x2 * Width;


            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }


        public int CalculateHeuristic()
        {
            var total_heuri = 0;

            for (var i = 1; i < Size + 1; i++)
            {
                var tile = Board1[i - 1];

                if (tile.Value != i &&
                    tile.Type == TileType.FULL)
                {
                    var tile_heuristic = ManhattanDistance(i - 1, tile.Value - 1);

                    total_heuri += tile_heuristic;
                }
            }

            return total_heuri;
        }


        private Direction reverseDirection(Direction direction)
        {
            var lookUpTable = new List<Direction>
            {
                Direction.DOWN,
                Direction.UP,
                Direction.RIGHT,
                Direction.LEFT,
                Direction.NONE
            };

            return lookUpTable[(int) Convert.ChangeType(direction, direction.GetTypeCode())];
        }

        public List<Direction> SolveBoard()
        {
            var SolvingBoard = DeepCopy();

            //On choisi le minimum a chaque fois et on poursuit

            var result = new List<Direction>();
            var prevMovement = Direction.NONE;

            // While the board isn't correct we continue... 
            for (var steps = 0; !SolvingBoard.IsCorrect() || SolvingBoard.CalculateHeuristic() != 0; steps++)
            {
                var possibleDirection = SolvingBoard.GetPossibleDirections();

                var minDirection = Direction.UP;
                // Maximum value 
                var min_heu = Size * Size * (Width + 1);

                // Looping through all the possible directions
                foreach (var direction in possibleDirection)
                {
                    var tmp_board = SolvingBoard.DeepCopy();

                    tmp_board.MoveDirection(direction);

                    //Console.WriteLine($"Heuristic : {tmp_board.CalculateHeuristic()}");

                    var tmpHeuristic = tmp_board.CalculateHeuristic();

                    // Getting the direction of the minimum heuristic value, it can't be the previous part
                    if (tmpHeuristic <= min_heu &&
                        prevMovement != direction)
                    {
                        min_heu = tmpHeuristic;
                        minDirection = direction;
                    }
                }

                // Applying the direction to the actual board
                SolvingBoard.MoveDirection(minDirection);
                result.Add(minDirection);
                prevMovement = reverseDirection(minDirection);
            }

            return result;
        }

        public void ApplyMovements(List<Direction> directions)
        {
            foreach (var direction in directions)
                MoveDirection(direction);
        }

        /* BONUS */
        public List<Direction> SolveBoardBonus()
        {
            var toSolve = DeepCopy();

            var graph = new SimpleGraph<Board>(toSolve);

            var minHeap = new MinHeap<SimpleGraph<Board>>(toSolve.Size * toSolve.Size);

            //PriorityQueue<SimpleGraph<Board>, Int32> minHeap = new PriorityQueue<SimpleGraph<Board>, int>();

            while (!graph.Value.IsCorrect())
            {
                var directions = graph.Value.GetPossibleDirections();
                foreach (var possibleDirection in directions)
                {
                    var tmp = graph.Value.DeepCopy();

                    tmp.MoveDirection(possibleDirection);

                    var newGraphNode = new SimpleGraph<Board>(tmp);


                    var newNode =
                        new HeapElement<SimpleGraph<Board>>(newGraphNode, tmp.CalculateHeuristic() + graph.heightGraph);

                    graph.AddChild(newGraphNode, possibleDirection);

                    minHeap.Enqueue(newNode);
                }

                graph = minHeap.Dequeue().Node;
            }

            /* Retrieve Path */

            var result = new List<Direction>();
            while (graph != null)
            {
                result.Insert(0, graph.directionFromParent);
                graph = graph.parent;
                if (graph.directionFromParent == Direction.NONE)
                    break;
            }

            return result;
        }
    }
}
