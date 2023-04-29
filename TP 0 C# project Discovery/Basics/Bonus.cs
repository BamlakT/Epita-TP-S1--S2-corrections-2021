using System;
using System.Threading.Channels;

namespace Basics
{
    public class Bonus
    {
        public static char CaesarChar(char c, int n)
        {
            if (n<0)
            {
                n %= 26;
                  n += 26;
            }
            Console.WriteLine(Convert.ToChar(96));
            char res = (char) (c + n % 26);
            if (char.IsLower(c))
            {
                return res > 'z' ? (char) (res - 'z' + '`') : res;
            }
            
            return res > 'Z' ? (char) (res - 'Z' + '@') : res;
            


        }
        
        // Juste une fonction pour nous, il n'ont pas a la faire.
        // C'est la fonction avec laquelle on a fait le clue 
        public static Char rotleft(char str, uint n)
        {

            if (n < 0)
            {
                n %= 26;
                n += 26;
            }

            char res = (char) (str +n % 26);
            
            if (Char.IsLower(str))
                return res > 'z' ? (char)(res - 'z' + '`') : res;
            return res > 'Z' ? (char) (res - 'Z' + '@') : res;
            
            
            
            
            
           /* n %= 26;
            char[] s = str.ToCharArray();
            for (int i = 0; i < s.Length; i++)
            {
                 s[i] = (char) (s[i] - n);
            }

            return s.ToString();*/
        }

    }
}