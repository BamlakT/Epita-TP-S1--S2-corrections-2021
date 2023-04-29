using System;
using System.Reflection;

namespace Ref.Token
{
    public class Lexer
    {
        private static readonly Type[] TokenTypes =
        {
            typeof(TokenStart),
            typeof(TokenEnd),
            typeof(TokenEmpty),
            typeof(TokenPath),
            typeof(TokenInsertor),
            typeof(TokenMirror),
            typeof(TokenReflector),
            typeof(TokenNumber),
            typeof(TokenChar),
            typeof(TokenQuote),
            typeof(TokenOutput),
            typeof(TokenValue),
            typeof(TokenInput),
            typeof(TokenDuplicate),
            typeof(TokenConditional)
        };

        public static Token Lex(char[][] table, int x, int y)
        {
            if (x - 1 >= 0 && x + 1 < table[y].Length)
            {
                if (table[y][x - 1] == '[' && table[y][x + 1] == ']')
                    return new TokenOperator(table[y][x], Direction.Up);
                if (table[y][x - 1] == '{' && table[y][x + 1] == '}')
                    return new TokenOperator(table[y][x], Direction.Right);
            }

            return Lex(x < table[y].Length ? table[y][x] : ' ');
        }

        public static Token Lex(char c)
        {
            foreach (var type in TokenTypes)
            {
                var constructor = type.GetConstructor(new[] {typeof(char)});
                try
                {
                    return constructor == null
                        ? throw new NotSupportedException("no constructor")
                        : (Token) constructor.Invoke(new object[] {c});
                }
                catch (TargetInvocationException invocation)
                {
                    if (invocation.InnerException is not LexerError)
                        throw;
                }
            }

            return new TokenChar(c);
        }
    }

    public class LexerError : Exception
    {
        private readonly char _c;

        public LexerError(char c)
        {
            _c = c;
        }

        public override string ToString()
        {
            return $"Invalid character: '{_c}'";
        }
    }
}
