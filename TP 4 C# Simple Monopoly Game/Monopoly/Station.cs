namespace Monopoly
{
    public class Station : Property
    {
        public Station(string name, int price,
            int position) : base(name, price, position, 150)
        {
            //TODO
        }

        public override string ToString()
        {
            return "[STATION]" + base.ToString();
        }
    }
}