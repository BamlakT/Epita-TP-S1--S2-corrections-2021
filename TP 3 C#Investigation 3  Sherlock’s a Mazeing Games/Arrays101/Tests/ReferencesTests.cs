using Basics;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    [Timeout(100)]
    public class ReferencesTests
    {
        #region Swap

        public class aSwapTest
        {
            [Test]
            public void SwapBasic()
            {
                var a = -1;
                var b = 2;
                References.Swap(ref a, ref b);

                Assert.AreEqual(a, 2);
                Assert.AreEqual(b, -1);
            }

            [Test]
            public void SwapSame()
            {
                var a = 42;
                var b = 42;
                References.Swap(ref a, ref b);

                Assert.AreEqual(a, 42);
                Assert.AreEqual(b, 42);
            }
        }

        #endregion

        #region EuclideanDivision

        [Timeout(100)]
        public class bEuclideanDivisionTest
        {
            [Test]
            public void EuclideanDivisionZero()
            {
                var a = 0;
                const int b = 13;

                const int expected = 0;
                var actual = References.EuclideanDivision(ref a, b);

                Assert.AreEqual(expected, actual);
                Assert.AreEqual(a, 0);
            }

            [Test]
            public void EuclideanDivisionDivisible()
            {
                var a = 126;
                const int b = 18;

                const int expected = 7;
                var actual = References.EuclideanDivision(ref a, b);

                Assert.AreEqual(expected, actual);
                Assert.AreEqual(a, 0);
            }

            [Test]
            public void EuclideanDivisionPrimes()
            {
                var a = 131;
                const int b = 47;

                const int expected = 2;
                var actual = References.EuclideanDivision(ref a, b);

                Assert.AreEqual(expected, actual);
                Assert.AreEqual(a, 37);
            }
        }

        #endregion

        #region ComplexSquare

        public class cComplexSquareTest
        {
            [Test]
            public void ComplexSquareZero()
            {
                const int expectedRe = 0;
                const int expectedIm = 0;

                var re = 0;
                var im = 0;
                References.ComplexSquare(ref re, ref im);

                Assert.AreEqual(expectedRe, re);
                Assert.AreEqual(expectedIm, im);
            }

            [Test]
            public void ComplexSquareReal()
            {
                const int expectedRe = 81;
                const int expectedIm = 0;

                var re = 9;
                var im = 0;
                References.ComplexSquare(ref re, ref im);

                Assert.AreEqual(expectedRe, re);
                Assert.AreEqual(expectedIm, im);
            }

            [Test]
            public void ComplexSquareImaginary()
            {
                const int expectedRe = -144;
                const int expectedIm = 0;

                var re = 0;
                var im = 12;
                References.ComplexSquare(ref re, ref im);

                Assert.AreEqual(expectedRe, re);
                Assert.AreEqual(expectedIm, im);
            }

            [Test]
            public void ComplexSquareComplex()
            {
                const int expectedRe = -24;
                const int expectedIm = 70;

                var re = 5;
                var im = 7;
                References.ComplexSquare(ref re, ref im);

                Assert.AreEqual(expectedRe, re);
                Assert.AreEqual(expectedIm, im);
            }
        }

        #endregion
    }
}
