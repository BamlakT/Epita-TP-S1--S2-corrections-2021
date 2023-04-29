using System.Collections.Generic;

namespace Factory06
{
    public class Coat : Machine
    {
        private readonly uint [] upgrades = { 200, 500 };
        private readonly int maxLevel = 3;

        // TODO
        public Coat()
        {
            this.type = MachineType.Coat;
            this.capacity = 30;
            this.items = new List <Item>();
            this.level = 1;
        }

        // TODO
        public override bool Upgrade(ref long money)
        {
            if (this.level == maxLevel || upgrades[this.level - 1] > money)
                return false;

            this.capacity += (this.Level == 1) ? 10 : this.capacity;
            money -= upgrades[this.level - 1];
            this.level++;
            return true;
        }

        // TODO
        public override bool Produce(uint count, ref long money)
        {
            if (count == 0)
                return false;

            long total = money / (int)ItemType.Coat;
            while (total > 0 && count > 0 && this.items.Count < this.capacity)
            {
                this.items.Add(new Item(ItemType.Coat));
                money -= (int)ItemType.Coat;
                total--;
                count--;
            }

            return count == 0;
        }

        // TODO
        public override void Clear()
        {
            this.items.Clear();
        }
        
        // TODO
        public override uint Destroy()
        {
            this.Clear();
            this.level = 1;
            this.capacity = 30;
            return (uint)MachinePrice.Coat / 3;
        }
    }
}