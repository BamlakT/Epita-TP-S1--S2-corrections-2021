using System.Collections.Generic;

namespace Reference
{
    public class Flask : Machine
    {
        private readonly uint [] upgrades = { 300 };
        private readonly int maxLevel = 2;

        // TODO
        public Flask()
        {
            this.type = MachineType.Flask;
            this.capacity = 20;
            this.items = new List <Item>();
            this.level = 1;
        }

        // TODO
        public override bool Upgrade(ref long money)
        {
            if (this.level == maxLevel || upgrades[this.level - 1] > money)
                return false;

            this.capacity += (this.Level == 1) ? 4 : this.capacity;
            money -= upgrades[this.level - 1];
            this.level++;
            return true;
        }
        
        // TODO
        public override bool Produce(uint count, ref long money)
        {
            if (count == 0)
                return false;

            long total = money / (int)ItemType.Flask;
            while (total > 0 && count > 0 && this.items.Count < this.capacity)
            {
                this.items.Add(new Item(ItemType.Flask));
                money -= (int)ItemType.Flask;
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
            this.capacity = 20;
            return (uint)MachinePrice.Flask / 3;
        }
    }
}