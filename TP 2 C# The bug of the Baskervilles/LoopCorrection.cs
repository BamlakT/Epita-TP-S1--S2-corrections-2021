using System;

namespace Exercise
{
    public class Loop
    {
        public static void PrintNaturals(int n)
        {
            for (int i = 1; i <= n; i++)
            {
                Console.Write(i);
                if (i + 1 <= n)
                    Console.Write(" ");
            }

            Console.WriteLine();
        }

        public static void PrintPrimes(int n)
        {
            bool isFirstPrint = true;
            for (int i = 2; i <= n; i++)
            {
                bool isPrime = true;
                for (int j = 2; j * j <= i && isPrime; j++) // was j <= Math.Sqrt(i)
                    if (i % j == 0)
                        isPrime = false;

                if (isPrime)
                {
                    if (isFirstPrint)
                        isFirstPrint = false;
                    else
                        Console.Write(" ");
                    Console.Write(i);
                }
            }

            Console.WriteLine();
        }

        public static long Fibonacci(long n)
        {
            if (n < 0)
            {
                Console.WriteLine("InvalidArg : Given parameter is negative");
                return -1;
            }
            long a = 0, b = 1, c = 0;
            if (n == 0 || n == 1)
                return n;
            for (int i = 1; i < n; i++)
            {
                c = a + b;
                a = b;
                b = c;
            }

            return c;
        }

        private static long Factorial(long n)
        {
            long res = 1;
            for (int i = 2; i <= n; i++)
                res *= i;
            return res;
        }

        public static void PrintStrong(int n)
        {
            
            if (n < 1)
            {
                Console.WriteLine("InvalidArg : Given parameter is negative or 0");
            }
            bool isFirstPrint = true;
            for (int i = 1; i <= n; ++i)
            {
                long sum = 0;
                for (int num = i; num != 0;)
                {
                    sum += Factorial(num % 10);
                    num /= 10;
                }

                if (sum == i)
                {
                    if (isFirstPrint)
                        isFirstPrint = false;
                    else
                        Console.Write(" ");
                    Console.Write(i);
                }
            }
            Console.WriteLine();
        }

        public static float Abs(float n)
        {
            return (n < 0) ? -n : n;
        }

        public static float Sqrt(float n)
        {
            const float eps = 0.001f;
            float guess = 1.0f;
            for (; Abs(guess * guess - n) >= eps;)
                guess = (n / guess + guess) / 2.0f;
            return guess;
        }

        public static double Power(long a, long b)
        {
            bool neg = false;
            if (b < 0)
            { 
                b *= -1;
                neg = true;
            }
                        
            double res = 1;
            for (; b > 0; b--)
                    res *= a;
                        
             return neg ? 1.0f/res : res;
        }

        private static void PrintLineWith(string s, int n)
        {
            for (int i = 0; i < n; i++)
                Console.Write(s);
        }

        public static void PrintTree(int n)
        {
            for (int i = 0; i < n; i++)
            {
                PrintLineWith(" ", n - i - 1);
                PrintLineWith("*", 2 * i + 1);
                Console.WriteLine();
            }

            int t = (n > 3) ? 2 : 1;
            for (int i = 0; i < t; i++)
            {
                PrintLineWith(" ", n - 1);
                Console.WriteLine("*");
            }
        }

        public static int Syracuse(int n)
        {
            int i = 0;
            for (; n != 1;)
            {
                if (n % 2 == 0)
                    n = n / 2;
                else
                    n = 3 * n + 1;
                i++;
            }
            return i;
        }

    }
}
