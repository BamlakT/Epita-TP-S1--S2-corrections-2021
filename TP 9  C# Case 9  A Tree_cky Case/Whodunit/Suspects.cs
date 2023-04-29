using System;
using System.IO;
using System.Collections.Generic;

namespace CurseCase
{
    public class Suspects
    {
        private Tuple<string, int>[] data;
        public Tuple<string, int>[] Data
        {
            get => data;
            set => data = value;
        }

        public Suspects(GenTree tree, int size)
        {
            Queue<GenTree> q = new Queue<GenTree>();
            q.Enqueue(tree);
            Tuple<string, int>[] data = new Tuple<string, int>[size];
            for (int i = 0; q.Count > 0; i++)
            {
                GenTree root = q.Dequeue();
                Tuple<string, int> el = new Tuple<string, int>(root.Name, root.Suspicion);
                data[i] = el;

                if (root.Left != null)
                    q.Enqueue(root.Left);
                if (root.Right != null)
                    q.Enqueue(root.Right);
            }

            this.data = data;
        }

        private static void Swap(Tuple<string, int>[] data, int i, int j)
        {
            (data[i], data[j]) = (data[j], data[i]);
        }

        private static void Heapify(Tuple<string, int>[] data, int i, int end)
        {
            if (i >= end)
                return;

            int node = i;
            int child = 2 * node + 1;

            while (child <= end)
            {
                if (child < end && data[child].Item2 < data[child + 1].Item2)
                    child++;

                if (data[node].Item2 < data[child].Item2)
                {
                    Swap(data, node, child);
                    node = child;
                    child = 2 * node + 1;
                }
                else
                    break;
            }
        }

        public void HeapSort()
        {
            for (int i = this.data.Length / 2; i >= 0; i--)
                Heapify(this.data, i, this.data.Length - 1);

            for (int i = this.data.Length - 1; i >= 0; i--)
            {
                Swap(this.data, 0, i);
                Heapify(this.data, 0, i - 1);
            }
        }

        public override string ToString()
        {
            return string.Join<Tuple<string, int>>(" | ", this.data);
        }
    }
}
