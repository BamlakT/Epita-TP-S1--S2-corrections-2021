using System;

namespace Basics 
{
    public class Reference
    {
        public static void HelloWorld()
        {
            Console.WriteLine("Hello World!");
        }

        public static void Welcome()
        {
            Console.WriteLine("Hello, what's your name?");
            Console.WriteLine($"Welcome to 221B Baker Street, {Console.ReadLine()}!");
        }

        public static void ComputeAge()
        {
            Console.WriteLine("What's your year of birth?");
            Console.WriteLine($"Looks like you're around {DateTime.Now.Year - Int32.Parse(Console.ReadLine() ?? string.Empty)}!");
        }

        public static double Pow(double x, int n)
        {/*
            int i = 1;
            double res = x;
            if (n==0)
               return 1;

            if (x == 0)
               return 0;
            if (n>0)
            {
                while (i < n)
                {
                    res *= x;
                    i++;
                }
               
            }
            else
            {
                res = 1 / x;
                while (i < n*-1)
                {
                    res *= 1/x;
                    i++;
                }
            }
            return res;
            */
            if (n < 0)
                return 1 / x * Pow(x, -n - 1);
            if (n == 0)
                return 1;
            return x * Pow(x, n - 1);
        }
        

        public static uint Factorial(uint n)
        {
            /*
            uint i = 1;
            uint res = n;
            while (i<n)
            {
                res *= i;
                i++;
            }
            return res;
            
            if (n < 2)
                return 1;
            return n * Factorial(n - 1);
            int a = 0;
            int b = 0;
            int c = 1;
            int res ;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(a);
                res = a + b + c;
                a = b;
                b = c;
                c = res;
                Console.WriteLine("c---->" + c);
            }

            return (uint)a;*/
            uint i = 1;
            uint j = 1;
            
            while (i<=n)
            {
                j *= i;
                i++;
                
            }

            return j;
        }
        
        private static bool PrimeRec(uint i, uint j)
        {
            if (j * j > i)
                return true;
            if (i % j == 0)
                return false;
            return PrimeRec(i, j + 2);
        }

        public static bool IsPrime(uint n)
        {
            if (n == 2)
                return true;
            if (n < 2)
                return false;
            if (n % 2 == 0)
                return false;
            return PrimeRec(n, 3);
        }

        private static uint FiboRec(uint n, uint n1, uint n2)
        {
            if (n == 1)
                return n2;

            return FiboRec(n - 1, n1 + n2, n1);
        }
        
        public static uint Fibonacci(uint n)
        {/*
            if (n <= 1)
                return n;
            if (n == 2)
                return 1;
            return FiboRec(n, 1, 1);
            uint a = 0;
            uint b = 1;
            uint temp;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(a);
                temp = a;
                a = b;
                b += temp;
            }
            Console.WriteLine(a);
            return a;
         
            uint a = 0;
            uint b = 1;
            uint c = 1;
            uint ans;
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(a);
                ans = a + b + c;
                a = b;
                b = c;
                c = ans;
            }

            return a;*/
            uint a = 0;
            uint b = 1;
            uint temp;
            for (int i = 0; i < n; i++)
            {
                temp = a;
                
                a = b;
                Console.WriteLine(a);
                b += temp;
            }
            
        

            return a;
        }

        public static string SherlockHolmes(uint n)
        {
            /*
            if (n == 0)
                return "";
            if (n == 1)
                return "1";
            string result = " ";
            if (n % 15 == 0)
                result += "Sherlock Holmes";
            else if (n % 5 == 0)
                result += "Holmes";
            else if (n % 3 == 0)
                result += "Sherlock";
            else
                result += n;

            return SherlockHolmes(n - 1) + result;
            */

            uint i = 1;
            string res="";
            while (i<=n)
            {
                if (i%15==0)
                {
                    res += "sherlock homes ";
                }
                else if (i%5==0)
                {
                    res += "homes, ";
                }
                else if (i%3==0)
                {
                    res += "sherlock, ";
                }
                else
                {
                    res += i + ", ";
                }

                i++;
            }

            return res;
        }
        
    }
}
