using System;
using System.Linq;

namespace Basics
{
    public static class Arrays
    {
        /**
         * <summary>Swaps two elements in an array.</summary>
         */
        public static void Swap(int[] arr, int i, int j)
        {
            References.Swap(ref arr[i], ref arr[j]);
        }

        /**
         * <summary>Prints an array of integers.</summary>
         * <param name="arr"> The array of integers - might be empty.</param>
         */
        public static void Print(int[] arr)
        {
            
            Console.Write("[ ");
            if (arr.Length == 0)
            {
                Console.WriteLine("]");
                return;
            }

            for (var i = 0; i < arr.Length - 1; i++)
                Console.Write("{0} | ", arr[i]);

            Console.WriteLine("{0} ]", arr.Last());
        }

        /**
         * <summary>Returns the second greatest element of an array.</summary>
         * <param name="arr">An array of size at least 2.</param>
         * <returns>The second greatest element in <c>arr</c>.</returns>
         */
        public static int ViceMax(int[] arr)
        {
            var max = int.MinValue;
            var viceMax = int.MinValue;

            foreach (var element in arr)
            {
                if (element >= max)
                {
                    viceMax = max;
                    max = element;
                }
                else if (element > viceMax)
                {
                    viceMax = element;
                }
            }

            return viceMax;
        }

        /**
         * <summary>Reverses the array <code>arr</code> in place.</summary>
         * <param name="arr">An array of integers.</param>
         */
        public static void Reverse(int[] arr)
        {
            for (var i = 0; i < arr.Length / 2; i++)
                Swap(arr, i, arr.Length - i - 1);
        }

        /**
         * <summary>Concatenates to arrays.</summary>
         * <param name="lhs">The left part of the resulting array.</param>
         * <param name="rhs">The right part of the resulting array?</param>
         * <returns>Returns the concatenation of <c>lhs</c> and <c>rhs</c></returns>
         */
        public static int[] Concat(int[] lhs, int[] rhs)
        {
            var result = new int[lhs.Length + rhs.Length];

            int i;
            for (i = 0; i < lhs.Length; i++)
                result[i] = lhs[i];

            for (var j = 0; j < rhs.Length; i++, j++)
                result[i] = rhs[j];

            return result;
        }

        /**
         * <summary>Determines whether a given array is sorted in ascending order.</summary>
         * <param name="arr">An array of integers.</param>
         */
        public static bool IsSorted(int[] arr)
        {
            for (var i = 1; i < arr.Length; i++)
                if (arr[i - 1] > arr[i])
                    return false;
            return true;
        }
        
        /**
         * <summary>Sorts an array in-place in ascending order using insertion sort.</summary>
         * <param name="arr">An array of integers.</param>
         */
        public static void InsertionSort(int[] arr)
        {
            for (var i = 0; i < arr.Length; i++)
            {
                for (var j = i; j > 0 && arr[j - 1] > arr[j]; j--)
                    Swap(arr, j, j - 1);
            }
        }

        /**
         * <summary>Sorts an array in ascending order using any sorting algorithm.</summary>
         * <param name="arr">An array of integers.</param>
         *
         * BONUS
         */
        public static void OtherSort(int[] arr)
        {
            BuildHeap(arr, arr.Length);
            for (var i = arr.Length - 1; i > 0; i--)
            {
                Swap(arr, 0, i);
                Heapify(arr, 0, i);
            }
        }

        private static void BuildHeap(int[] arr, int length)
        {
            for (var i = length / 2; i >= 0; i--)
                Heapify(arr, i, length);
        }

        private static void Heapify(int[] arr, int root, int length)
        {
            while (true)
            {
                var largest = root;
                if (2 * root + 1 < length && arr[2 * root + 1] > arr[largest])
                    largest = 2 * root + 1;
                if (2 * root + 2 < length && arr[2 * root + 2] > arr[largest])
                    largest = 2 * root + 2;

                if (largest == root) return;
                Swap(arr, root, largest);
                root = largest;
            }
        }
    }
}