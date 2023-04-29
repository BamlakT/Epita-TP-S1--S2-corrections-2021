using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenEmpty : Token
    {
        protected override string AllowedChars => " ";

        public TokenEmpty(char c)
            : base(c)
        {
        }

        public TokenEmpty()
            : base(' ')
        {
        }

        protected override List<Dot> Action(Dot dot) =>
            new();
    }
}
