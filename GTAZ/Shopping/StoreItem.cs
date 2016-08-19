using GTA;

namespace GTAZ.Shopping
{
    public abstract class StoreItem
    {
        protected readonly string Name;
        private readonly int _id;

        private bool _available = true;

        protected StoreItem(int id, string name, float price)
        {
            _id = id;
            Name = name;
            Price = price;
        }

        public abstract void OnStoreItemPurchase(Player player);    

        public abstract void OnStoreItemUse(Player player);

        public abstract void OnStoreItemUse(Ped ped);

        public float Price
        {
            get; set;
        }

        public void SetAvailable(bool value)
        {
            _available = value;
        }

        public int GetId()
        {
            return _id;
        }

        public string GetDisplayName()
        {
            return Name;
        }
    }
}
