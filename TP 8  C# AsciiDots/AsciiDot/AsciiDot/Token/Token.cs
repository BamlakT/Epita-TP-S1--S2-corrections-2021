using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Ref.Token
{
    public abstract class Token
    {
        public char Value { get; }

        protected abstract string AllowedChars { get; }
        public virtual int PointInside => 0;

        protected Token(char c)
        {
            Value = AllowedChars is null || !AllowedChars.Contains(c)
                ? throw new LexerError(c)
                : c;
        }

        protected virtual bool Update(Dot dot)
        {
            if (dot.CurrentEnvironment is Environment.SingleQuote or Environment.DoubleQuote)
            {
                dot.Enqueue(this);
                return true;
            }

            dot.Flush();
            dot.CurrentEnvironment = Environment.Default;

            return false;
        }

        public List<Dot> Apply(Dot dot) =>
            Update(dot)
                ? new List<Dot> {dot}
                : Action(dot);

        protected virtual List<Dot> Action(Dot dot)
        {
            return new List<Dot> {dot};
        }
    }
}
