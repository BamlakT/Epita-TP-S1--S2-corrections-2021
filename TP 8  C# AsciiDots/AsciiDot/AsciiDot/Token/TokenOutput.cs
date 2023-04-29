namespace Ref.Token
{
    public class TokenOutput : Token
    {
        protected override string AllowedChars => "$";
        public TokenOutput(char c)
            : base(c)
        {
        }

        protected override bool Update(Dot dot)
        {
            dot.Enqueue(this);
            
            switch (dot.CurrentEnvironment)
            {
                case Environment.Default:
                    dot.CurrentEnvironment = Environment.Output;
                    break;
                case Environment.Affectation:
                    dot.Flush();
                    dot.CurrentEnvironment = Environment.Output;
                    break;
            }

            return true;
        }
    }
}
