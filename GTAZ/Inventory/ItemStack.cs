using System;
using GTA;
using GTA.Math;

namespace GTAZ.Inventory {

    public delegate void ItemDropEventHandler(Ped oldHolder, Vector3 pos, object sender, EventArgs e);
    public delegate void ItemPickupEventHandler(Ped newOwner, Vector3 pos, object sender, EventArgs e);

    public class ItemStack {

        protected event ItemDropEventHandler Dropped;
        protected event ItemPickupEventHandler Pickup;

        private readonly Item _item;
        private int _size;

        public ItemStack(Item item, int size) {
            _item = item;
            _size = size;
        }

        public void UseItem(Player trigger, Ped target) {

            if (!(_size - 1 >= 0)) {
                return;
            }

            _item.Use(trigger, target);
            _size--;

        }

        /// <summary>
        /// Set a new size to this ItemStack.
        /// </summary>
        /// <param name="size">The new size of the ItemStack.</param>
        public void SetSize(int size) {
            if (size >= 0)
                _size = size;
        }

        /// <summary>
        /// Returns the display name of this ItemStack.
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName() {
            return _item.Name;
        }

        /// <summary>
        /// Returns the Item of this ItemStack.
        /// </summary>
        public Item Item {
            get { return _item; }
        }

        /// <summary>
        /// Returns the size of this ItenStack.
        /// </summary>
        public int Size {
            get { return _size; }
        }

        public override string ToString() {
            return Item.Name + "x" + Size;
        }
    }

}
