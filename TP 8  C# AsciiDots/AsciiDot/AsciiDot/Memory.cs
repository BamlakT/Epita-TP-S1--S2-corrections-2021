using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Ref
{
    public enum Environment
    {
        Default,
        Output,
        SingleQuote,
        DoubleQuote,
        Affectation
    }
    
    public class Memory
    {
        public double Value;
        
        public Environment CurrentEnvironment;
        public List<Token.Token> Queue;

        public Memory()
        {
            Value = 0;
            CurrentEnvironment = Environment.Default;
            Queue = new();
        }

        private Memory(Memory memory)
        {
            Value = memory.Value;
            CurrentEnvironment = memory.CurrentEnvironment;
            Queue = new List<Token.Token>(memory.Queue);
        }

        public Memory Clone() => new Memory(this);

        public void Enqueue(Token.Token token)
        {
            Queue.Add(token);
        }
        
        public void Assignment(string str)
        {
            str = str.TrimStart('#');
            
            if (str.StartsWith('?'))
            {
                Console.Write("Please input a value: ");
                str = Console.ReadLine();
            }

            Value = 0;
            if (double.TryParse(str, out var newValue))
                Value = newValue;
        }

        private void Display(string str, bool ascii, bool newline)
        {
            if (str.StartsWith('a'))
            {
                str = str.TrimStart('a');
                Display(str, true, newline);
            }
            else if (str.StartsWith('_'))
            {
                str = str.TrimStart('_');
                Display(str, ascii, false);
            }
            else
            {
                string value;
            
                if (str.StartsWith('"'))
                    value = str.Trim('"');
                else if (str.StartsWith('\''))
                    value = str.Trim('\'');
                else if (str.StartsWith('#'))
                    value = ascii
                        ? Convert.ToChar((int) Value).ToString()
                        : Value.ToString(CultureInfo.InvariantCulture);
                else // never in this case...
                    return;

                Console.Write(value);
                if (newline)
                    Console.WriteLine();
            }
        }

        public void Display(string str)
        {
            str = str.TrimStart('$');
            Display(str, false, true);
        }

        public void Flush()
        {
            if (Queue.Count == 0)
                return;
            
            var first = Queue.First();
            var str = string.Join("", Queue.Select(token => token.Value));
            switch (first.Value)
            {
                case '#':
                    Assignment(str);
                    break;
                case '$':
                    Display(str);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(first.Value.ToString());
            }

            Queue.Clear();
        }
    }
}