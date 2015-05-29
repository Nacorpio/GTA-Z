using System.Collections.Generic;
using System.Linq;
using GTA;
using GTAZ.Assembly;

namespace GTAZ.Inventory {

    /// <summary>
    /// Represents an inventory, storing ItemStacks.
    /// </summary>
    public abstract class Inventory : EntityPart {

        private readonly string _name;

        private readonly int _capacity;
        private readonly List<ItemStack> _items;

        protected Inventory(string name, int capacity = 16) : base(name) {
            _name = name;
            _items = new List<ItemStack>(capacity);
            _capacity = capacity;
        }

        //

        public void ShowInventory() {

            // TODO: Show the inventory as a menu.
            OnInventoryOpen();

        }

        public void CloseInventory() {

            // TODO: Close the inventory.
            OnInventoryClose();

        }

        //

        public abstract void OnInventoryOpen();

        public abstract void OnInventoryClose();

        //

        public abstract void OnItemUse(Player player, int index);

        public abstract void OnItemUse(Ped ped, int index);

        //

        protected void UseItem(int index, params object[] args) {
            _items[index].UseItem(args);
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
