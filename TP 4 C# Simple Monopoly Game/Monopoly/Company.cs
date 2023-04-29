namespace Monopoly
{
    public class Company : Property
    {
        public Company(string name, int price,
            int position) : base(name, price, position, 100)
        {
            //TODO
            /*
             * Nothing to be done here, just calling the superclass constructor.
             */
        }

        public override string ToString()
        {
            return "[Company] " + base.ToString();
        }
    }
}