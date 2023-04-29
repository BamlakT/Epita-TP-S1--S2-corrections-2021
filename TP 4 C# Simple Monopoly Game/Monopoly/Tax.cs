namespace Monopoly
{
    public class Tax : Special
    {
        private int amount;

        public int Amount => this.amount;

        public Tax(int amount, int position)
        {
            //TODO
            this.amount = amount;
            this.position = position;
        }

        public bool TaxPlayer(Player player)
        {
            //TODO
            return this.ModifyBudget(player, -amount);
        }

        public override string ToString()
        {
            return "[Tax]: " + amount + "£";
        }
    }
}