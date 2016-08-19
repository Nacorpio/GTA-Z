using System.Collections.Generic;
using System.Linq;

namespace GTAZ.Shopping
{
    public abstract class Store
    {
        private readonly List<StoreItemStack> _items = new List<StoreItemStack>(); 

        protected Store(string name, params StoreItemStack[] items)
        {
            Name = name;
            items.ToList().ForEach(i => _items.Add(i));
        }

        public string Name { get; }

        public string GetDisplayName()
        {
            return Name;
        }

        public bool HasItemStackFor(StoreItem item)
        {
            return _items.Any(i => i.GetStoreItem() == item);
        }

        public void AddItem(StoreItem item, int size = 1)
        {
            AddItemStack(new StoreItemStack(item, size));
        }

        public void AddItemStack(StoreItemStack item)
        {
            if (HasItemStackFor(item.GetStoreItem()))
            {
                var itemStack = GetItemStack(item.GetStoreItem());
                itemStack.SetSize(itemStack.GetSize() + item.GetSize());
                return;
            }

            _items.Add(item);
        }

        public StoreItemStack GetItemStack(StoreItem item)
        {
            return _items.Where(i => i.GetStoreItem() == item).ToArray()[0];
        }
    }
}
