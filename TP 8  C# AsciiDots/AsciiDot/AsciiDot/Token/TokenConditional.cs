using System.Collections.Generic;
using System.Linq;

namespace Ref.Token
{
    public class TokenConditional : Token
    {
        private readonly List<Dot> dotHorizontal = new();
        private readonly List<Dot> dotVertical = new();

        public TokenConditional(char c)
            : base(c)
        {
        }

        protected override string AllowedChars => "~";

        protected override List<Dot> Action(Dot dot)
        {
            if (DirUtils.SameAxis(dot.Direction, Direction.Up))
                dotVertical.Add(dot);
            else
                dotHorizontal.Add(dot);

            var newList = new List<Dot>();
            
            while (dotHorizontal.Count >= 1 && dotVertical.Count >= 1)
            {
                var horizontal = dotHorizontal.First();
                var vertical = dotVertical.First();
                
                if (vertical.Value != 0)
                    horizontal.Direction = Direction.Up;
                
                newList.Add(horizontal);
                dotHorizontal.Remove(horizontal);
                dotVertical.Remove(vertical);
            }

            return newList;
        }
    }
}