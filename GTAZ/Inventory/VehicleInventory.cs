using GTA;

namespace GTAZ.Inventory {

    public class VehicleInventory : Inventory {

        public VehicleInventory(int capacity = 32) : base("VehicleInventory", null, capacity) {}

        public override void OnItemUse(Player player, int index) {}

        public override void OnItemUse(Ped ped, int index) {}

        public override void OnInventoryClose() {}

        public override void OnInventoryOpen() {}

    }

}
