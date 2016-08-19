using System;
using GTA;

namespace GTAZ.Inventory
{
    public delegate void ItemChangedEventHandler(object sender, EventArgs e);
    public delegate void ItemUseEventHandler(Player trigger, Ped target, object sender, EventArgs e);
    
    public abstract class Item
    {
        protected event ItemUseEventHandler Used;

        private int _maxStackSize;

        protected Item(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Use(Player trigger, Ped target)
        {
            if (trigger == null || target == null) return;
            Used?.Invoke(trigger, target, this, EventArgs.Empty);
        }
                    
        /// <summary>
        /// Returns the unique identifier of this Item.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Returns the unique name of this Item.
        /// </summary>
        public string Name { get; }

        public int GetMaxStackSize()
        {
            return _maxStackSize;
        }

        public Item SetStackSize(int size)
        {
            _maxStackSize = size;
            return this;
        }

        public override string ToString()
        {
            return  Id + " ("+  Name + ")";
        }
    }

}
