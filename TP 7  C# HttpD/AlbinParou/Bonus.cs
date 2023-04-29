using System;

namespace HttpD
{
    public class Bonus
    {
        // Given
        public static readonly string b64_table = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";

        public static string StringToBase64(string s)
        {
            // TODO: Encode string to Base 64 
            var i = 0;
            var ret = "";
            
            var pad = 3 - s.Length % 3;
            
            while (i < s.Length)
            {
                var tmpBits = 0;
                for (var j = 0; j < 3 && i < s.Length; j++)
                {
                    var c = s[i++];
                    tmpBits += c << 8 * (2 - j);
                }

                var t1 = tmpBits >> 18;
                var t2 = tmpBits >> 12;
                var t3 = tmpBits >> 6;
                char[] b64Chars =
                {
                    b64_table[t1 & 63],
                    b64_table[t2 & 63],
                    b64_table[t3 & 63],
                    b64_table[tmpBits & 63],
                };
                if (pad != 3 && i == s.Length)
                    for (var j = 0; j < pad; j++)
                        b64Chars[3 - j] = '=';
                for (var j = 0; j < 4; j++)
                    ret += b64Chars[j];

            }

            return ret;
        }

        public static string Base64ToString(string b64)
        {
            // TODO: Decode Base 64 to string
            var i = 0;
            var decoded = "";
            while (i < b64.Length)
            {
                var tmpBits = 0;
                var j = 0;
                for (; j < 4 && i < b64.Length && b64_table.Contains(b64[i]); j++)
                {
                    var c = b64[i++];
                    tmpBits += b64_table.IndexOf(c) << (18 - 6 * j);
                }

                var pad = 3;
                if (j < 3)
                {
                    if (i == b64.Length || i < b64.Length - 2)
                        throw new Exception($"Invalid base64 expression.");
                    if (i <= b64.Length - 1)
                    {
                        if (b64[^1] != '=')
                            throw new Exception($"Invalid base64 expression.");
                        pad--;
                        i++;
                        if (i == b64.Length - 2)
                        {
                            if (b64[^2] != '=')
                                throw new Exception($"Invalid base64 expression.");
                            pad--;
                            i++;
                        }
                    }
                }

                var t1 = tmpBits >> 16;
                var t2 = tmpBits >> 8;
                char[] decodedChars =
                {
                    (char) (t1 & 255),
                    (char) (t2 & 255),
                    (char) (tmpBits & 255)
                };
                for (j = 0; j < pad; j++)
                    decoded += decodedChars[j];
            }

            return decoded;
        }
    }
}