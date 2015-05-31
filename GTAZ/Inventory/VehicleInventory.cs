using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public class VehicleInventory : Inventory {

        public VehicleInventory(int capacity = 32) : base("VehicleInventory", capacity) {}

        protected sealed override BaseMenu GetMenu() {
            return new VehicleInventoryMenu(this);
        }
    }

}
