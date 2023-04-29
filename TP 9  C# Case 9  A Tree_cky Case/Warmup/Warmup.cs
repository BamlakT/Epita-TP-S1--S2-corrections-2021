using System;

namespace Warmup
{
    class Warmup
    {
        public static bool IsPalindrome(string str)
        {
            if (str == null)
                throw new ArgumentNullException();

            str = str.ToLower();
            int i = 0;
            int j = str.Length - 1;
            while (i < j)
            {
                while (Char.IsLetter(str[i]) == false)
                    i++;
                while (Char.IsLetter(str[j]) == false)
                    j--;

                if (str[i] != str[j])
                    return false;
                i++;
                j--;
            }

            return true;
        }

        public static char RotChar(char c, int key)
        {
           
            key %= 26;
            char shift = c + (char)key;

            if (shift < 'A')
                shift = ('[' - ('A' - shift));
            else if (shift > 'Z')
                shift = ('@' + (shift - 'Z'));

            return shift;
        }

        public static string RotString(string s, int key)
        {
            if (s == null)
                throw new ArgumentNullException();

            string res = "";

            foreach (char c in s)
                    res += Char.IsLetter(c) ? RotChar(c, key) : c;

            return res;
        }

        public static int BinarySearch(int[] array, int elt)
        {
            if (array == null)
                throw new ArgumentNullException();

            int last_index = array.Length - 1;
            int left = 0;
            int right = last_index;

            while (right >= left)
            {
                int mid = left + (right - left) / 2;
                if (mid > last_index)
                    break;
                if (elt == array[mid])
                    return mid;

                if (elt < array[mid])
                    right = mid - 1;
                else
                    left = mid + 1;
            }

            return right + 1;
        }
    }
}
