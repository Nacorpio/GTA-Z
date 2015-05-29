using GTA;

namespace GTAZ.Inventory {

    public class PlayerInventory : Inventory {

        public PlayerInventory(int capacity = 16) : base("PlayerInventory", capacity) {}

        public override void OnInventoryOpen() {
            throw new System.NotImplementedException();
        }

        public override void OnInventoryClose() {
            throw new System.NotImplementedException();
        }

        public override void OnItemUse(Ped ped, int index) {
            UseItem(index, ped);
        }

        public override void OnItemUse(Player player, int index) {
            OnItemUse(player.Character, index);
        }

    }

}
