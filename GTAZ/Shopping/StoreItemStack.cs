using GTA;

namespace GTAZ.Shopping
{
    public class StoreItemStack
    {
        private readonly StoreItem _item;
        private int _size;

        public StoreItemStack(StoreItem item, int size = 1)
        {
            _item = item;
            _size = size;
        }

        public void UseItem(Ped ped)
        {
            if (_size -1 >= 0)
                _item.OnStoreItemUse(ped);
                SetSize(_size - 1);
        }

        public StoreItem GetStoreItem()
        {
            return _item;
        }

        public void SetSize(int size)
        {
            if (size >= 0)
                _size = size;
        }

        public int GetSize()
        {
            return _size;
        }
    }
}
