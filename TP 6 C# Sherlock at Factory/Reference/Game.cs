
namespace Reference
{
    // This class is just a wrapper to call Factory
    public class Game
    {
        private int nbRound;
        private int round;
        public Factory factory;

        // TODO
        public int NbRound => nbRound;
        public int Round => round;
        public long Money => this.factory.Money;

        // TODO
        public Game(int nbRound, long initialMoney)
        {
            this.nbRound = nbRound;
            this.round = 1;
            this.factory = new Factory(initialMoney);
        }
        
        // TODO
        /**
         * Launch the whole game process. Starts the bot, and performs update
         * up to nbRound. Then end ends the bot.
         * Returns the total score (the factory's money).
         * <param name="bot">The bot that will play the game</param>
         */
        public long Launch(Bot bot)
        {
            bot.Start(this);
            
            while (this.round <= this.nbRound)
            {
                bot.Update(this);
                this.round++;
            }
            
            bot.End(this);
            return this.factory.Money;
        }

        // TODO
        /**
         * Calls the Build method on its factory.
         * <param name="type">The type of the machine to build</param>
         */
        public bool Build(MachineType type)
        {
            return this.factory.Build(type);
        }

        // TODO
        /**
         * Calls the Produce method on its factory.
         * <param name="type">The type of machine to search for</param>
         * <param name="count">The number of items to produce</param>
         */
        public bool Produce(MachineType type, uint count)
        {
            return this.factory.Produce(type, (int)count);
        }

        // TODO
        /**
         * Calls the UpgradeMatch method on its factory.
         * <param name="type">The type of the machines to upgrade</param>
         * <param name="level">The level of the machines to upgrade</param>
         * <param name="count">The number of machine to upgrade</param>
         */
        public bool UpgradeMatch(MachineType type, int level, int count)
        {
            return this.factory.UpgradeMatch(type, level, count);
        }

        // TODO
        /**
         * Calls the UpgradeAll method on its factory.
         */
        public bool UpgradeAll()
        { 
            return this.factory.UpgradeAll();
        }

        // TOOD
        /**
         * Calls the SellMatch method on its factory with the specified type.
         * <param name="type">The type of machine to sell items</param>
         */
        public void UpdateMoneyMatch(MachineType type)
        {
            this.factory.SellMatch(type);
        }

        // TODO
        /**
         * Calls the SellAll method on its factory.
         */
        public void UpdateMoneyAll()
        {
            this.factory.SellAll();
        }

        // TODO
        /**
         * Calls the DestroyMatch method on its factory.
         */
        public void DestroyMatch(MachineType type)
        {
            this.factory.DestroyMatch(type);
        }

        // TODO
        /**
         * Calls the DestroyAll method on its factory.
         */
        public void DestroyAll()
        {
            this.factory.DestroyAll();
        }
    }
}