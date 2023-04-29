using System.Linq;

namespace Ref.Token
{
    public class TokenInput : Token
    {
        protected override string AllowedChars => "?";
        public TokenInput(char c)
            : base(c)
        {
        }

        protected override bool Update(Dot dot)
        {
            if (dot.CurrentEnvironment is Environment.SingleQuote or Environment.DoubleQuote)
            {
                dot.Enqueue(this);
                return true;
            }
            
            if (dot.Memory.Queue.Count == 1 && dot.Memory.Queue[0] is TokenValue
                && dot.CurrentEnvironment is Environment.Affectation)
                dot.Enqueue(this);
            
            dot.Flush();
            dot.CurrentEnvironment = Environment.Default;

            return true;
        }
    }
}