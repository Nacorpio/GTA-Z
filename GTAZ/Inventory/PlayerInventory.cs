using GTA;
using GTAZ.Menus;
using mlgthatsme.GUI;

namespace GTAZ.Inventory {

    public class PlayerInventory : Inventory {

        public PlayerInventory() : base("Player Inventory", 16) {}

        protected override BaseMenu GetMenu() {
            return new ManageItemMenu(this);
        }
    }

}
