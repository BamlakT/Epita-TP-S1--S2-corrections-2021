namespace Monopoly
{
    public abstract class Property : Cell
    {
        //TODO
        private Player owner = null;
        protected int price;
        protected int rentCost;
        protected string name;

        //TODO
        public int Price => price;
        public int RentCost => rentCost;
        public string Name => name;

        public Player Owner
        {
            get => owner;
            set => owner = value;
        }
       
        public Property(string name, int price, int position, int rentCost)
        {
            //TODO
            this.name = name;
            this.price = price;
            this.position = position;
            this.rentCost = rentCost;
            this.owner = null;
        }

        public override string ToString()
        {
            return name + "; " + price + "; " + position + "; " + rentCost;
        }
    }
}