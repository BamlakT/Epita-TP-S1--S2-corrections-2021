using System;

namespace Reference
{
    public class Item
    {
        private readonly uint price;
        private ItemType type;
        
        // TODO
        public uint Price => price;
        public ItemType Type => type;

        // TODO
        public Item(ItemType type)
        {
            this.price = (uint) type;
            this.type = type;
        }

        // TODO
        /**
         * Sell the item.
         * A hat is worth 3 times its price.
         * A coat is worth 4 times its price.
         * A flask is worth 6 times its price.
         */
        public uint Sell()
        {
            uint value = this.price;

            if (this.type == ItemType.Hat)
                value *= 3;
            else if (this.type == ItemType.Coat)
                value *= 4;
            else if (this.type == ItemType.Flask)
                value *= 6;

            return value;
        }
    }
}