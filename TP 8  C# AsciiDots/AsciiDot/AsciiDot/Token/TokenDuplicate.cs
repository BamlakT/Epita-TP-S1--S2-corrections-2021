using System.Collections.Generic;

namespace Ref.Token
{
    public class TokenDuplicate : Token
    {
        protected override string AllowedChars => "*";

        public TokenDuplicate(char c)
            : base(c)
        {
        }

        protected override List<Dot> Action(Dot dot)
        {
            List<Dot> newListDots = new List<Dot>{dot};
            
            Direction dir1 = DirUtils.Rotate(dot.Direction);
            Direction dir2 = DirUtils.Invert(dir1);

            newListDots.Add(new Dot(dot, dir1));
            newListDots.Add(new Dot(dot, dir2));

            return newListDots;
        }
    }
}