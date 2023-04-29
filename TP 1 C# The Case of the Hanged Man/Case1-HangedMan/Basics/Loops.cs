using System;

namespace Case1_HangedMan.Basics
{
    public static class Loops
    {
        public static ulong Factorial(ulong n)
        {
            ulong result = 1;
            while (n > 0)
            {
                result *= n;
                n--;
            }

            return result;
        }

        public static int Power(int a, int b)
        {
            // computes a^b, b>0 (error case)
            if (b < 0)
            {
                Console.Error.WriteLine("N must be positive or null");
                return 0;
            }

            int result = 1;
            while (b > 0)
            {
                result *= a;
                b--;
            }

            return result;
        }

        public static int DivisorSum(int n)
        {
            if (n <= 0)
            {
                Console.Error.WriteLine("N must be positive");
                return -1;
            }
            if (n == 2)
                return 1;
            if (n == 1)
                return 0;
            int result = 0;
            for (int i = 2; Power(i, 2) <= n; i++)
            {
                if (n % i == 0)
                {
                    if (i == n / i)
                        result += i;
                    else
                        result += i + n / i;
                }
            }

            return result + 1;
        }

        public static bool PerfectNumber(int c)
        {
            // Perfect number exercise
            return DivisorSum(c) == c;
        }

        public static int DecodeBinary(string s)
        {
            // transforms a binary string into number
            int result = 0;
            // for each easier for correction but feasible with while
            foreach (var c in s)
            {
                result <<= 1;
                result |= c % '0';
            }

            return result;
        }

        // Sherlock is trying to unlock a safe but the code needs to satisfy some condition
        // this is a simple not optimized bruteforce algorithm for the example
        public static int CrackTheCode(string code)
        {
            // decode the binary string
            int nb = DecodeBinary(code);
            nb -= nb % 2;
            while (nb >= 0 && !PerfectNumber(nb))
            {
                nb -= 2;
            }

            return nb;
        }
    }
}