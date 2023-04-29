namespace Ref.Token
{
    public class TokenChar : Token
    {
        protected override string AllowedChars =>
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ,;:![]{}_";

        public TokenChar(char c)
            : base(c)
        {
        }

        protected override bool Update(Dot dot)
        {
            if (Value is 'a' or '_' && dot.CurrentEnvironment is Environment.Output)
            {
                dot.Enqueue(this);
                return true;
            }
            return base.Update(dot);
        }
    }
}
