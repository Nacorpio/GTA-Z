using System;
using GTA;
using GTA.Math;

namespace GTAZ.Inventory {

    public class ItemStack {

        private readonly Item _item;
        private int _size;

        private bool _decrementOnUse = true;

        public ItemStack(Item item, int size) {
            _item = item;
            _size = size;
        }

        /// <summary>
        /// Uses the Item of this ItemStack as the specified trigger and on the specified target Ped.
        /// </summary>
        /// <param name="trigger">The Player of which triggered this action.</param>
        /// <param name="target">The Ped of which is the target of this action.</param>
        public void UseItem(Player trigger, Ped target) {

            if (!(_size - 1 >= 0)) {
                return;
            }

            _item.Use(trigger, target);
           
            if (_decrementOnUse) _size--;
            if (_size == 0) Main.PlayerInventory.RemoveItem(this);

        }

        public void SetDecrementOnUse(bool value) {
            _decrementOnUse = value;
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
