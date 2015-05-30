using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTAZ.Assembly;

namespace GTAZ.Inventory {

    public delegate void InventoryChangedEventHandler(object sender, EventArgs e);
    public delegate void InventoryItemUseEventHandler(int index, Player trigger, Ped target, object sender, EventArgs e);
    public delegate void InventoryItemAddedEventHandler(ItemStack add, object sender, EventArgs e);

    /// <summary>
    /// Represents an inventory, storing ItemStacks.
    /// </summary>
    public abstract class Inventory : EntityPart {

        protected event InventoryChangedEventHandler Shown, Closed;

        protected event InventoryItemUseEventHandler ItemUsed;
        protected event InventoryItemAddedEventHandler ItemAdded;

        private readonly string _name;

        private readonly int _capacity;
        private readonly List<ItemStack> _items;

        private object _menu;

        protected Inventory(string name, object menu, int capacity = 16) : base(name) {
            _name = name;
            _items = new List<ItemStack>(capacity);
            _capacity = capacity;
            _menu = menu;
        }

        //

        /// <summary>
        /// Shows the menu of this inventory.
        /// </summary>
        public void ShowInventory() {

            // TODO: Show the inventory as a menu.
            if (Shown != null) Shown(this, EventArgs.Empty);

        }

        /// <summary>
        /// Closes the menu of this inventory.
        /// </summary>
        public void CloseInventory() {

            // TODO: Close the inventory.
            if (Closed != null) Closed(this, EventArgs.Empty);

        }


        //

        protected void UseItem(int index, Player trigger, Ped target) {
            _items[index].UseItem(trigger, target);
            if (ItemUsed != null) ItemUsed(index, trigger, target, this, EventArgs.Empty);
        }

        public void AddItem(ItemStack item) {
            if (_items.Count + 1 <= _capacity) {
                _items.Add(item);
            }
        }

        public void AddItem(Item item, int size = 1) {
            AddItem(new ItemStack(item, size));
        }

        public void AddItem(int index, ItemStack item) {
            if (_items.Count + 1 <= _capacity) {
                _items[index] = item;
                if (ItemAdded != null) ItemAdded(item, this, EventArgs.Empty);
            }
        }

        public bool ContainsItem(ItemStack item) {
            return _items.Any(i => i == item);
        }

        public bool ContainsItem(int index, ItemStack item) {
            return _items[index] == item;
        }

        public void RemoveItem(ItemStack item) {
            _items.Remove(item);
        }

        public void RemoveItem(int index) {
            _items.RemoveAt(index);
        }

        public string Name {
            get { return _name; }
        }

        public int Capacity {
            get { return _capacity; }
        }

        public List<ItemStack> Items {
            get { return _items; }
        }

        public int Count(Item item) {
            return _items.Count(i => i.Item.Id == item.Id);
        }

        public int Count() {
            return _items.Count;
        }

    }

}
