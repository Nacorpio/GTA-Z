using GTA;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory
{
    public class PedInventory : Inventory
    {
        public PedInventory(int capacity = 16) : base("PedInventory", capacity)
        { }

        protected override BaseMenu GetMenu()
        {
            throw new System.NotImplementedException();
        }
    }
}
