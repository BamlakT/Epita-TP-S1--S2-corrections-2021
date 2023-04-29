using System;
using System.Collections.Generic;

namespace Ref.Token
{
    public class EndOfProgram : Exception
    {
    }

    public class TokenEnd : Token
    {
        protected override string AllowedChars => "&";

        public TokenEnd(char c)
            : base(c)
        {
        }

        protected override List<Dot> Action(Dot dot)
        {
            throw new EndOfProgram();
        }
    }
}
