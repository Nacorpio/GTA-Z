using GTAZ.Assembly;
using GTAZ.Controllable;
using GTAZ.Inventory;

namespace GTAZ.Props {

    public class DroppedItemStackProp : ControllableProp {

        private class ItemStackPart : EntityPart {

            private readonly ItemStack _itemStack;

            public ItemStackPart(ItemStack stack) : base("ItemStack") {
                _itemStack = stack;
            }

            public ItemStack GetItemStack() {
                return _itemStack;
            }

        }

        private readonly ItemStack _itemStack;

        public DroppedItemStackProp(int uid, ItemStack stack) : base(uid, "ITEM_STACK_PROP", 25f) {
            _itemStack = stack;
        }

        protected override void InitializeAssembly() {
            AddPart("ItemStack", new ItemStackPart(_itemStack));
        }
    }

}
