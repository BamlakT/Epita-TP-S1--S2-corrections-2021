using System.Collections.Generic;

namespace Monopoly
{
    public class Player
    {
        private List<Property> possessions;
        private int balance;
        private int position;
        private string name;
        public bool jailed;

        public int Balance => balance;
        public string Name => name;
        public List<Property> Possessions => possessions;
        public int Position => position;

        public Player(string name, int initialBalance, int initialPosition)
        {
            //TODO
            this.balance = initialBalance;
            this.position = initialPosition;
            this.name = name;
            this.jailed = false;
            this.possessions = new List<Property>();
        }

        public void ReceiveMoney(int amount)
        {
            //TODO
            this.balance += amount;
        }

        public bool RetrieveMoney(int amount)
        {
            //TODO
            if (amount > this.balance)
                return false;
            this.balance -= amount;
            return true;
        }

        public bool Buy(Property p)
        {
            //TODO
            if (!this.RetrieveMoney(p.Price))
                return false;
            this.possessions.Add(p);
            p.Owner = this;
            return true;
        }

        public bool Sell(Property p)
        {
            //TODO
            if (p.Owner != this)
                return false;
            this.balance += p.Price;
            p.Owner = null;
            this.possessions.Remove(p);
            return true;
        }

        public bool TransferTo(Player p, int amount)
        {
            //TODO
            if (!this.RetrieveMoney(amount))
                return false;
            p.ReceiveMoney(amount);
            return true;
        }

        public bool SellTo(Property p, Player player)
        {
            //TODO
            if (p.Owner != this || player.Balance < p.Price)
                return false;
            this.Sell(p);
            player.Buy(p);
            return true;
        }

        public void Move(int vector, int boardSize)
        {
            //TODO
            this.position = (this.position + vector) % boardSize;
        }

        public void SendToJail()
        {
            this.jailed = true;
        }

        public override string ToString()
        {
            string pos = this.possessions.Count > 0 ? this.possessions[0].Name : "";

            for (int i = 1; i < this.possessions.Count; i++)
                pos += ", " + this.possessions[i].Name;

            return $"player: '{this.name}'\n" +
                   $"balance: {this.balance} £\n" +
                   $"position: {this.position}\n" +
                   $"possessions:{pos}";
        }
    }
}