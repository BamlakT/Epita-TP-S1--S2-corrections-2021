namespace Ref.Token
{
    public class TokenNumber : TokenChar
    {
        public TokenNumber(char c)
            : base(c)
        {
        }

        protected override string AllowedChars => "0123456789";

        protected override bool Update(Dot dot)
        {
            if (dot.CurrentEnvironment is Environment.Affectation or Environment.SingleQuote or Environment.DoubleQuote)
                dot.Enqueue(this);

            return true;
        }
    }
}
