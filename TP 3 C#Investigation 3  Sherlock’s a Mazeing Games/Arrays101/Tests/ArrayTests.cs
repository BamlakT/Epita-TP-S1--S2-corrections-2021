using System;
using System.IO;
using NUnit.Framework;
using Basics;

namespace Tests
{
    [TestFixture]
    [Timeout(100)]
    public class ArrayTests
    {
        #region Swap

        public class ASwapTest
        {
            [Test]
            public void SwapFirstLast()
            {
                int[] expected = {1, 4, 5, 2};
                int[] actual = {2, 4, 5, 1};
                Arrays.Swap(actual, 0, 3);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SwapMiddle()
            {
                int[] expected = {1, 4, 5, 2};
                int[] actual = {1, 5, 4, 2};
                Arrays.Swap(actual, 1, 2);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void SwapSameArrays()
            {
                int[] expected = {1, 4, 5, 1};
                int[] actual = {1, 4, 5, 1};
                Arrays.Swap(actual, 0, 3);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region Print

        public class bPrintTest
        {
            [Test]
            public void PrintEmpty()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                var arr = Array.Empty<int>();
                Arrays.Print(arr);
                string expected = $"[ ]{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }

            [Test]
            public void PrintSingle()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                int[] arr = {1};
                Arrays.Print(arr);
                string expected = $"[ 1 ]{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }

            [Test]
            public void PrintSame()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                int[] arr = {42, 42, 42, 42, 42};
                Arrays.Print(arr);
                string expected = $"[ 42 | 42 | 42 | 42 | 42 ]{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }

            [Test]
            public void PrintRandom()
            {
                using var writer = new StringWriter();
                Console.SetOut(writer);

                int[] arr = {98, 54, 38, 95, 87, 3, 85, 94, 26, 83};
                Arrays.Print(arr);
                string expected =
                    $"[ 98 | 54 | 38 | 95 | 87 | 3 | 85 | 94 | 26 | 83 ]{Environment.NewLine}";
                Assert.AreEqual(expected, writer.ToString());
            }
        }

        #endregion

        #region ViceMax

        public class cViceMaxTest
        {
            [Test]
            public void ViceMaxTwo()
            {
                int[] arr = {1, 2};
                const int expected = 1;
                var actual = Arrays.ViceMax(arr);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ViceMaxTwoSame()
            {
                int[] arr = {42, 42};
                const int expected = 42;
                var actual = Arrays.ViceMax(arr);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ViceMaxSorted()
            {
                int[] arr = {1, 2, 3, 4};
                const int expected = 3;
                var actual = Arrays.ViceMax(arr);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ViceMaxReverseSorted()
            {
                int[] arr = {4, 3, 2, 1};
                const int expected = 3;
                var actual = Arrays.ViceMax(arr);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ViceMaxNotSorted()
            {
                int[] arr = {42, 20, 39, 17};
                const int expected = 39;
                var actual = Arrays.ViceMax(arr);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region Reverse

        public class dReverseTest
        {
            [Test]
            public void ReverseEmpty()
            {
                var expected = Array.Empty<int>();
                var actual = Array.Empty<int>();
                Arrays.Reverse(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ReverseSorted()
            {
                int[] expected = {5, 4, 3, 2, 1, 0};
                int[] actual = {0, 1, 2, 3, 4, 5};
                Arrays.Reverse(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ReverseReverseSorted()
            {
                int[] expected = {6, 5, 4, 3, 2, 1, 0};
                int[] actual = {0, 1, 2, 3, 4, 5, 6};
                Arrays.Reverse(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ReverseRandom()
            {
                int[] expected = {24, 26, 0, 2, 37, 43, 81, 84, 78, 89};
                int[] actual = {89, 78, 84, 81, 43, 37, 2, 0, 26, 24};
                Arrays.Reverse(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region Concat

        public class eConcatTest
        {

            [Test]
            public void ConcatEmpty()
            {
                var arr1 = Array.Empty<int>();
                var arr2 = Array.Empty<int>();

                var expected = Array.Empty<int>();
                var actual = Arrays.Concat(arr1, arr2);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ConcatEmptyLeft()
            {
                var arr1 = Array.Empty<int>();
                int[] arr2 = {5, 2, 3, 10};

                int[] expected = {5, 2, 3, 10};
                var actual = Arrays.Concat(arr1, arr2);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ConcatEmptyRight()
            {
                int[] arr1 = {5, 2, 3, 10};
                var arr2 = Array.Empty<int>();

                int[] expected = {5, 2, 3, 10};
                var actual = Arrays.Concat(arr1, arr2);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void ConcatRandom()
            {
                int[] arr1 = {33, 85, 9, 87, 86};
                int[] arr2 = {38, 42, 67, 13, 63};

                int[] expected = {33, 85, 9, 87, 86, 38, 42, 67, 13, 63};
                var actual = Arrays.Concat(arr1, arr2);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region IsSorted

        public class gIsSortedTest
        {
            [Test]
            public void IsSortedEmpty()
            {
                var arr = Array.Empty<int>();
                Assert.True(Arrays.IsSorted(arr));
            }

            [Test]
            public void IsSortedConstant()
            {
                int[] arr = {42, 42, 42, 42};
                Assert.True(Arrays.IsSorted(arr));
            }

            [Test]
            public void IsSortedTrue()
            {
                int[] arr = {-1, -1, -1, 0, 4, 4, 5, 42, 118};
                Assert.True(Arrays.IsSorted(arr));
            }

            [Test]
            public void IsSortedReverseSorted()
            {
                int[] arr = {1337, 9, -42};
                Assert.False(Arrays.IsSorted(arr));
            }

            [Test]
            public void IsSortedRandom()
            {
                int[] arr = {86, 73, 58, 70, 89};
                Assert.False(Arrays.IsSorted(arr));
            }
        }

        #endregion

        #region InsertionSort

        public class hInsertionSortTest
        {
            [Test]
            public void InsertionSortConstant()
            {
                int[] expected = {1, 1, 1, 1};
                int[] actual = {1, 1, 1, 1};
                Arrays.InsertionSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InsertionSortSorted()
            {
                int[] expected = {1, 2, 3, 4};
                int[] actual = {1, 2, 3, 4};
                Arrays.InsertionSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InsertionSortReverseSorted()
            {
                int[] expected = {1, 2, 3, 4};
                int[] actual = {4, 3, 2, 1};
                Arrays.InsertionSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InsertionSortEmpty()
            {
                var expected = Array.Empty<int>();
                var actual = Array.Empty<int>();
                Arrays.InsertionSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void InsertionSortRandom()
            {
                // Trust me, those numbers are random
                int[] expected = {4, 23, 36, 38, 50, 59, 76, 78, 87, 99};
                int[] actual = {87, 38, 59, 99, 50, 23, 78, 76, 36, 4};
                Arrays.InsertionSort(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion

        #region OtherSort

        public class iOtherSortTest
        {
            [Test]
            public void OtherSortConstant()
            {
                int[] expected = {1, 1, 1, 1};
                int[] actual = {1, 1, 1, 1};
                Arrays.OtherSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void OtherSortSorted()
            {
                int[] expected = {1, 2, 3, 4};
                int[] actual = {1, 2, 3, 4};
                Arrays.OtherSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void OtherSortReverseSorted()
            {
                int[] expected = {1, 2, 3, 4};
                int[] actual = {4, 3, 2, 1};
                Arrays.OtherSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void OtherSortEmpty()
            {
                var expected = Array.Empty<int>();
                var actual = Array.Empty<int>();
                Arrays.OtherSort(actual);
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void OtherSortRandom()
            {
                // Trust me, those numbers are random
                int[] expected = {4, 23, 36, 38, 50, 59, 76, 78, 87, 99};
                int[] actual = {87, 38, 59, 99, 50, 23, 78, 76, 36, 4};
                Arrays.OtherSort(actual);
                Assert.AreEqual(expected, actual);
            }
        }

        #endregion
    }
}