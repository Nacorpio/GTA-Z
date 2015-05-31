using System;
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTAZ.Assembly;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public delegate void InventoryChangedEventHandler(object sender, EventArgs e);
    public delegate void InventoryItemUseEventHandler(int index, Player trigger, Ped target, object sender, EventArgs e);
    public delegate void InventoryItemAddedEventHandler(ItemStack add, object sender, EventArgs e);

    public delegate void ItemDropEventHandler(Ped oldHolder, Vector3 pos, object sender, EventArgs e);
    public delegate void ItemPickupEventHandler(Ped newOwner, Vector3 pos, object sender, EventArgs e);

    /// <summary>
    /// Represents an inventory, storing ItemStacks.
    /// </summary>
    public abstract class Inventory : EntityPart {

        protected event ItemDropEventHandler Dropped;
        protected event ItemPickupEventHandler Pickup;

        protected event InventoryChangedEventHandler Shown, Closed;

        protected event InventoryItemUseEventHandler ItemUsed;
        protected event InventoryItemAddedEventHandler ItemAdded;

        private readonly string _name;

        private readonly int _capacity;
        private readonly List<ItemStack> _items;

        protected Inventory(string name, int capacity = 16) : base(name) {
            _name = name;
            _items = new List<ItemStack>(capacity);
            _capacity = capacity;
        }

        //

        protected abstract BaseMenu GetMenu();

        protected void UseItem(int index, Player trigger, Ped target) {
            _items[index].UseItem(trigger, target);
            if (ItemUsed != null)
                ItemUsed(index, trigger, target, this, EventArgs.Empty);
        }

        public void DropItem(int index) {
            
        }

        /// <summary>
        /// Shows the menu of this inventory.
        /// </summary>
        public void ShowInventory() {

            // TODO: Show the inventory as a menu.
            Main.WindowManager.AddMenu(GetMenu());
            if (Shown != null) Shown(this, EventArgs.Empty);

        }

        /// <summary>
        /// Closes the menu of this inventory.
        /// </summary>
        public void CloseInventory() {

            // TODO: Close the inventory.
            GetMenu().Close();
            if (Closed != null) Closed(this, EventArgs.Empty);

        }

        public void AddItem(ItemStack item) {

            if (_items.Count + 1 <= _capacity) {

                if (ContainsItem(item.Item)) {

                    var stacks = Get(item.Item).ToArray();
                    var stack = stacks[stacks.Length - 1];

                    if (stack.Size + item.Size >= item.Item.GetMaxStackSize()) {

                        var diff = (stack.Size + item.Size) - item.Item.GetMaxStackSize();
                        var diff1 = stack.Size - diff;

                        stack.SetSize(diff1);
                        _items.Add(new ItemStack(item.Item, diff));

                    }

                    stack.SetSize(stack.Size + item.Size);
                    return;

                }

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

        public IEnumerable<ItemStack> Get(Item item) {
            return _items.Where(i => i.Item == item);
        }

        public bool ContainsItem(Item item) {
            return _items.Any(i => i.Item == item);
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

        //

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
