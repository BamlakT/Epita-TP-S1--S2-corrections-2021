using System;

namespace Ref.Token
{
    public class TokenValue : Token
    {
        public TokenValue(char c)
            : base(c)
        {
        }

        protected override string AllowedChars => "#";

        protected override bool Update(Dot dot)
        {
            switch (dot.CurrentEnvironment)
            {
            case Environment.Default or Environment.Affectation:
                dot.Flush();
                dot.Enqueue(this);
                dot.CurrentEnvironment = Environment.Affectation;
                return true;
            case Environment.Output:
                dot.Enqueue(this);
                dot.Flush();
                dot.CurrentEnvironment = Environment.Default;
                return true;
            case Environment.SingleQuote or Environment.DoubleQuote:
                dot.Enqueue(this);
                return true;
            default:
                throw new ArgumentOutOfRangeException(dot.CurrentEnvironment.ToString());
            }
        }
    }
}
