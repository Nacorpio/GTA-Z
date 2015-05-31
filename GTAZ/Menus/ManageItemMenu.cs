using System;
using System.Drawing;
using GTA;
using GTAZ.Inventory;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class ManageItemMenu : InventoryMenu {

        protected class ManageItemSub : BaseMenu {

            private ItemStack _stack;

            public ManageItemSub(Inventory.Inventory inventory, ItemStack stack) {

                _stack = stack;

                TitleText = "Manage Item (" + stack.GetDisplayName() + ")";
                TitleColor = Color.White;
                CustomThemeColor = Color.Red;

                TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

            }

            public override void InitControls() {

                base.InitControls();

                var btnUse = new Button("Use Item");
                btnUse.OnPress += (sender, args) => {

                    _stack.UseItem(Main.Player, Main.Player.Character);
                    UI.Notify("Used 1x " + _stack.GetDisplayName());

                    Manager.RefreshAllMenus();
                    Manager.CloseCurrentWindow();

                };

                var btnDrop = new Button("Drop Item");

                AddMenuItem(btnUse);
                AddMenuItem(btnDrop);

            }
        }

        public ManageItemMenu(Inventory.Inventory inventory) : base(inventory) {}

        protected override void BtnItemOnOnPress(Inventory.Inventory inventory, ItemStack stack, object sender, EventArgs eventArgs) {
            Main.WindowManager.AddMenu(new ManageItemSub(inventory, stack));
        }

    }

}
