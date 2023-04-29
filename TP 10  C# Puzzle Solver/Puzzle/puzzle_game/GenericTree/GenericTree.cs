using System.Collections.Generic;

namespace puzzle_game.SimpleGraph
{
    public class SimpleGraph<T>
    {
        public Direction directionFromParent;

        public int heightGraph;

        public SimpleGraph<T> parent;

        public SimpleGraph(T value)
        {
            Value = value;
            Child = null;
            Next = null;
            parent = null;
            directionFromParent = Direction.NONE;
            heightGraph = 0;
        }

        public T Value { get; }

        public SimpleGraph<T> Next { get; set; }

        public SimpleGraph<T> Child { get; set; }

        public void AddChild(T newChild, Direction direction)
        {
            var newNode = new SimpleGraph<T>(newChild);
            newNode.Next = Child;
            Child = newNode;
            newNode.parent = this;
            newNode.directionFromParent = direction;
            newNode.heightGraph = heightGraph + 1;
        }

        public void AddChild(SimpleGraph<T> newNode, Direction direction)
        {
            newNode.Next = Child;
            Child = newNode;
            newNode.directionFromParent = direction;
            newNode.parent = this;
            newNode.heightGraph = heightGraph + 1;
        }

        public List<SimpleGraph<T>> getAdjsList()
        {
            var result = new List<SimpleGraph<T>>();

            var sibling = Next;

            while (sibling != null)
            {
                result.Add(sibling);
                sibling = sibling.Next;
            }

            return result;
        }

        public List<SimpleGraph<T>> getChilds()
        {
            if (Child != null)
            {
                var result = Child.getAdjsList();
                result.Add(Child);
                return result;
            }

            return null;
        }

        /* You are free to add methods here ...
         For example remove, find etc ... */
    }
}
