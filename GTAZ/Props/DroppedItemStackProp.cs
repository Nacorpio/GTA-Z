using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTAZ.Assembly;
using GTAZ.Controllable;
using GTAZ.Inventory;

namespace GTAZ.Props
{
    public delegate void ItemPickupEventHandler(Ped newOwner, Vector3 pos, object sender, EventArgs e);

    public class DroppedItemStackProp : ControllableProp
    {
        public event ItemPickupEventHandler Pickup;

        private class ItemStackPart : EntityPart
        {
            private readonly ItemStack[] _itemStack;

            public ItemStackPart(params ItemStack[] stack) : base("ItemStack")
            {
                _itemStack = stack;
            }

            public ItemStack[] GetItemStack()
            {
                return _itemStack;
            }
        }

        private readonly ItemStack[] _itemStack;

        public DroppedItemStackProp(int uid, params ItemStack[] stack) : base(uid, "ITEM_STACK_PROP", 25f)
        {
            _itemStack = stack;
            SetInteractionDistance(4f);

            PlayerKeyDown += OnPlayerKeyDown;
        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs ke)
        {
            if (ke.KeyCode == Keys.E)
            {
                _itemStack.ToList().ForEach(i => Main.PlayerInventory.AddItem(i));

                Pickup?.Invoke(Main.Player.Character, Entity.Position, this, EventArgs.Empty);
                UI.Notify("The stack has been picked up");

                Main.WindowManager.RefreshAllMenus();

                RemoveEntity();
                Entity.Delete();
            }
        }

        protected override void InitializeAssembly()
        {
            AddPart("ItemStack", new ItemStackPart(_itemStack));
        }
    }
}
