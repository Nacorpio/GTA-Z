using GTA;

namespace GTAZ.Inventory {

    public class PlayerInventory : Inventory {

        public PlayerInventory(int capacity = 16) : base("PlayerInventory", null, capacity) {}

        public override void OnInventoryOpen() {}

        public override void OnInventoryClose() {}

        public override void OnItemUse(Ped ped, int index) {
            UseItem(index, ped);
        }

        public override void OnItemUse(Player player, int index) {
            OnItemUse(player.Character, index);
        }

    }

}
