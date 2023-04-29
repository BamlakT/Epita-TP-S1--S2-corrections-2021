using System.Collections.Generic;

namespace Reference
{
    public class Hat : Machine
    {
        private readonly uint [] upgrades = { 200, 300, 400 };
        private readonly int maxLevel = 4;

        // TODO
        public Hat()
        {
            this.type = MachineType.Hat;
            this.capacity = 300;
            this.items = new List <Item>();
            this.level = 1;
        }

        // TODO
        public override bool Upgrade(ref long money)
        {
            if (this.level == maxLevel || upgrades[this.level - 1] > money)
                return false;

            this.capacity += (this.Level == 1) ? 300 : this.capacity;
            money -= upgrades[this.level - 1];
            this.level++;
            return true;
        }
        
        // TODO
        public override bool Produce(uint count, ref long money)
        {
            if (count == 0)
                return false;

            long total = money / (int)ItemType.Hat;
            while (total > 0 && count > 0 && this.items.Count < this.Capacity)
            {
                this.items.Add(new Item(ItemType.Hat));
                money -= (int)ItemType.Hat;
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
            this.capacity = 300;
            return (uint)MachinePrice.Hat / 3;
        }
    }
}