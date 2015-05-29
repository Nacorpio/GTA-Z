using GTA;

namespace GTAZ.Inventory {

    public class PedInventory : Inventory {

        public PedInventory(int capacity = 16) : base("PedInventory", null, capacity) {}

        public override void OnItemUse(Player player, int index) {}

        public override void OnItemUse(Ped ped, int index) {
            
        }

        public override void OnInventoryOpen() {}

        public override void OnInventoryClose() {}

    }

}
