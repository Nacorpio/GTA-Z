using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace GTAZ.Shopping {

    public abstract class Store {

        private readonly string _name;
        private readonly List<StoreItemStack> _items = new List<StoreItemStack>(); 

        protected Store(string name, params StoreItemStack[] items) {
            _name = name;
            items.ToList().ForEach(i => _items.Add(i));
        }

        public string Name {
            get { return _name; }
        }

        public string GetDisplayName() {
            return _name;
        }

        public bool HasItemStackFor(StoreItem item) {
            return _items.Any(i => i.GetStoreItem() == item);
        }

        public void AddItem(StoreItem item, int size = 1) {
            AddItemStack(new StoreItemStack(item, size));
        }

        public void AddItemStack(StoreItemStack item) {

            if (HasItemStackFor(item.GetStoreItem())) {
                var itemStack = GetItemStack(item.GetStoreItem());
                itemStack.SetSize(itemStack.GetSize() + item.GetSize());
                return;
            }

            _items.Add(item);

        }

        public StoreItemStack GetItemStack(StoreItem item) {
            return _items.Where(i => i.GetStoreItem() == item).ToArray()[0];
        }

    }

}
