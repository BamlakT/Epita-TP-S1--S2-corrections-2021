using System;

namespace puzzle_game
{
    internal class Program
    {
        private static void TestBoard(Board board)
        {
            board.Print();

            Console.WriteLine("Is correct ? : " + board.IsCorrect());
            Console.WriteLine("Is solvable  ? : " + board.IsSolvable());
            Console.WriteLine("Heuristic of the puzzle : " + board.CalculateHeuristic());
        }


        public static void Main(string[] args)
        {
            var board = new Board(9);

            int[] array =
            {
                1, 2, 3,
                4, 0, 5,
                7, 8, 6
            };

            board.Fill(array);
            foreach (var direction in board.GetPossibleDirections())
                Console.WriteLine(direction);

            //int[] array = new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 0};
            //int[] array = new int[] {3, 5, 6, 4, 1, 2, 7, 8, 9, 10, 0, 12, 13, 14, 11 ,15};


            TestBoard(board);

            var steps = board.SolveBoardBonus();

            Console.WriteLine("Resolution finished");

            foreach (var step in steps)
                Console.Write($"{step} ");

            Console.Write("\n");

            TestBoard(board);


            Viewer.Parse(board, steps);
            Viewer.LaunchViewer();
            /* 
             PriorityQueue<String, Int32> heap = new PriorityQueue<string, int>();
             heap.Enqueue("POPO1", 1);
             heap.Enqueue("POPO4", 4);
             heap.Enqueue("POPO3", 3);
             heap.Enqueue("POPO2", 2);
 
            
             Console.WriteLine(heap.Dequeue());
             Console.WriteLine(heap.Dequeue());
             Console.WriteLine(heap.Dequeue());
             Console.WriteLine(heap.Dequeue());
             */

            /*
            SimpleHeap<HeapIntElement> result = new SimpleHeap<HeapIntElement>(10);
            
            result.Add(new HeapIntElement(2));
            result.Add(new HeapIntElement(-1));
            result.Add(new HeapIntElement(-3));
            result.Add(new HeapIntElement(6));
            result.Add(new HeapIntElement(3));
            result.Add(new HeapIntElement(0));

            Console.WriteLine(result.RemoveFirst().ToString());
            Console.WriteLine(result.RemoveFirst().ToString());
            Console.WriteLine(result.RemoveFirst().ToString());
            Console.WriteLine(result.RemoveFirst().ToString());
            */

            /*
            Board board = new Board(16);
            
            board.Fill();
            
            SimpleGraph<Board> node = new SimpleGraph<Board>(board);
            
            SimpleHeap<HeapElement> heap = new SimpleHeap<HeapElement>(10);
            

            board.Print();
            
            Board boardLeft = board.DeepCopy();
            boardLeft.MoveDirection(Direction.LEFT);
            
            Board boardRight = board.DeepCopy();
            boardRight.MoveDirection(Direction.RIGHT);
            
            node.AddChild(boardLeft);
            node.AddChild(boardRight);

            foreach (var sibling in node.getChilds()) {
                Console.WriteLine("Child : ");
                sibling.Value.Print();
                HeapElement heapElement = new HeapElement(sibling, sibling.Value.CalculateHeuristic());

                heap.Add(heapElement);
            }
            
            
            Console.Write("Board with minimum ");
            var element = heap.RemoveFirst().Node.Value;
            Console.WriteLine($" Heuristic : {element.CalculateHeuristic()}" );
            element.Print();
            */
        }
    }
}
