namespace Monopoly
{
    public abstract class Special : Cell
    {
        protected bool ModifyBudget(Player player, int amount)
        {
            //TODO
            if (amount < 0)
                return player.RetrieveMoney(-amount);
            player.ReceiveMoney(amount);
            return true;
        }
    }
}