using System.Collections.Generic;

namespace Factory06
{
    public class Factory
    {
        private long money;
        private readonly int maxNbMachine = 50;
        public List <Machine> machines;

        // TODO
        public long Money => money;

        // TODO
        public Factory(long initialMoney)
        {
            this.money = initialMoney;
            this.machines = new List <Machine>();
        }

        // TODO
        /**
         * Return a list of all the machines of the corresponding type.
         * <param name="type">The type of machine to search for</param>
         */
        public List <Machine> GetMatchMachines(MachineType type)
        {
            return this.machines.FindAll(m => m.Type == type);
        }

        // TODO
        /**
         * Returns a machine of the specified type which still has
         * some capacity left to produce an item.
         * <param name="type">The type of the machine to search for</param>
         */
        public Machine FindAvailableMachine(MachineType type)
        {
            List <Machine> matchMachines = GetMatchMachines(type);

            if (matchMachines.Count == 0)
                return null;

            foreach (Machine machine in matchMachines)
                if (machine.Items.Count < machine.Capacity)
                    return machine;

            return null;
        }

        // TODO
        /**
         * Build a new machine of the specified type if the factory
         * has enough money and places.
         * Returns true if built, false otherwise
         * <param name="type">The type of the machine to build</param>
         */
        public bool Build(MachineType type)
        {
            if (this.machines.Count == this.maxNbMachine)
                return false;

            int price;

            if (type == MachineType.Coat)
                price = (int) MachinePrice.Coat;
            else if (type == MachineType.Flask)
                price = (int)MachinePrice.Flask;
            else
                price = (int) MachinePrice.Hat;

            if (this.money < price)
                return false;

            this.money -= price;

            if (type == MachineType.Coat)
                this.machines.Add(new Coat());
            else if (type == MachineType.Flask)
                this.machines.Add(new Flask());
            else
                this.machines.Add(new Hat());

            return true;
        }

        // This is an auxiliary function. Not in the skeleton.
        /**
         * Check if the factory can produce an item form a machine of the
         * specified type.
         * Return true if possible, false otherwise.
         * <param name="type">The type of the machine to search for</param>
         */
        private bool CanProduce(MachineType type)
        {
            if (type == MachineType.Coat)
                return this.money > (long)ItemType.Coat;
            if (type == MachineType.Flask)
                return this.money > (long)ItemType.Flask;
            return this.money > (long)ItemType.Hat;
        }

        // TODO
        /**
         * Try to produce count items from machines of the specified type
         * in the factory.
         * Returns true if count items were produced, false otherwise.
         * <param name="type">The type of machine to search for</param>
         * <param name="count">The number of items to produce</param>
         */
        public bool Produce(MachineType type, int count)
        {
            Machine ready = FindAvailableMachine(type);

            if (ready is null || count < 0)
                return false;

            int nb = ready.Items.Count;

            if (ready.Produce((uint) count, ref this.money))
                return true;

            int newNb = ready.Items.Count;

            if (newNb - nb != count && CanProduce(type))
                return this.Produce(type, count - (newNb - nb));

            return count == 0;
        }

        // TODO
        /**
        * Upgrade all machine on the factory if enough money.
        * Returns true if all upgrade were done, false otherwise.
        */
        public bool UpgradeAll()
        {
            bool res = true;

            foreach (Machine machine in this.machines)
                res &= machine.Upgrade(ref this.money);

            // this.machines.ForEach(m => m.Upgrade(ref this.money));

            return res;
        }

        // TODO
        /**
        * Upgrade up to count machine on the factory of the specified type
        * and level if the factory has enough money.
        * Returns true if count upgrades were done, false otherwise
        * <param name="type">The type of the machines to upgrade</param>
        * <param name="level">The level of the machines to upgrade</param>
        * <param name="count">The number of machine to upgrade</param>
         */
        public bool UpgradeMatch(MachineType type, int level, int count)
        {
            List <Machine> matchTypeMachines = GetMatchMachines(type);
            List <Machine> matchMachines =
                matchTypeMachines.FindAll(m => m.Level == level);

            if (matchMachines.Count == 0 || count < 0)
                return false;

            foreach (Machine ready in matchMachines)
            {
                if (!ready.Upgrade(ref this.money))
                    return count == 0;

                if (--count == 0)
                    return true;
            }

            return count == 0;
        }

        // TODO
        /**
         * Destroy all the machines in the factory.
         * Returns the total money gained, and also updates the factory's money.
         */
        public uint DestroyAll()
        {
            uint total = 0;

            foreach (Machine machine in this.machines)
                total += machine.Destroy();

            this.machines.Clear();
            this.money += total;
            return total;
        }

        // TODO
        /**
         * Destroy all the machines in the factory of the specified type.
         * Returns the total money gained, and also updates the factory's money.
         * <param name="type">The type of machine to destroy</param>
         */
        public uint DestroyMatch(MachineType type)
        {
            uint total = 0;
            List <Machine> matches = this.GetMatchMachines(type);

            foreach (Machine machine in matches)
                total += machine.Destroy();

            this.machines.RemoveAll(m => matches.Contains(m));
            this.money += total;
            return total;
        }

        // TODO
        /**
         * Collect all the items on the factory.
         */
        public List <Item> CollectAll()
        {
            List <Item> allItems = new List <Item>();

            foreach (Machine machine in this.machines)
                allItems.AddRange(machine.Items);

            return allItems;
        }

        // TODO
        /**
         * Collect all the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to collect from</param>
         */
        public List <Item> CollectMatch(MachineType type)
        {
            List <Item> allItems = new List <Item>();

            foreach (Machine machine in this.GetMatchMachines(type))
                allItems.AddRange(machine.Items);

            return allItems;
        }

        // TODO
        /**
         * Sell all the machines' items on the factory.
         * Returns the total money gained, and updates the factory's money.
         */
        public uint SellAll()
        {
            List <Item> allItems = this.CollectAll();
            this.ClearAll();
            uint total = 0;

            foreach (Item item in allItems)
                total += item.Sell();

            this.money += total;
            return total;
        }

        // TODO
        /**
         * Sell all the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to sell items</param>
         */
        public uint SellMatch(MachineType type)
        {
            List <Item> allItems = this.CollectMatch(type);
            this.ClearMatch(type);
            uint total = 0;

            foreach (Item item in allItems)
                total += item.Sell();

            this.money += total;
            return total;
        }

        // TODO
        /**
         * Clear all machines items on the factory.
         */
        public void ClearAll()
        {
            foreach (Machine machine in this.machines)
                machine.Clear();
        }

        // TODO
        /**
         * Clear the items on the factory from the machine of the
         * corresponding type.
         * <param name="type">The type of machine to clear items</param>
         */
        public void ClearMatch(MachineType type)
        {
            foreach (Machine machine in this.GetMatchMachines(type))
                machine.Clear();
        }
    }
}
