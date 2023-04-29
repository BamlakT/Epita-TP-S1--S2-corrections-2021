using System.Data.SqlTypes;

namespace Ref.Token
{
    public class TokenQuote : Token
    {
        public TokenQuote(char c)
            : base(c)
        {
            Single = c is '\'';
        }

        public bool Single { get; }
        protected override string AllowedChars => "'\"";

        protected override bool Update(Dot dot)
        {
            if (dot.CurrentEnvironment is Environment.Default)
                dot.Flush();
            else if (dot.CurrentEnvironment is Environment.Affectation)
            {
                dot.Flush();
                dot.CurrentEnvironment = Environment.Default;
            }
            else
            {
                dot.Enqueue(this);
                switch (dot.CurrentEnvironment)
                {
                    case Environment.Output:
                        dot.CurrentEnvironment = Single
                            ? Environment.SingleQuote
                            : Environment.DoubleQuote;
                        break;
                    case Environment.SingleQuote when Single:
                    case Environment.DoubleQuote when !Single:
                        dot.Flush();
                        dot.CurrentEnvironment = Environment.Default;
                        break;
                }
            }

            return true;
        }
    }
}
