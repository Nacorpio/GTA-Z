using System;
using System.Drawing;
using GTA;
using mlgthatsme.GUI;
using GTAZ.Inventory;

namespace GTAZ.Menus {

    public abstract class InventoryMenu : BaseMenu {

        private class ItemButton : Button {

            public ItemButton(ItemStack stack) {
                Text = stack.GetDisplayName() + " x" + stack.Size;
                UserData = stack;
            }

        }

        private readonly Inventory.Inventory _inventory;

        protected InventoryMenu(Inventory.Inventory inventory) {

            _inventory = inventory;

            TitleText = inventory.Name;
            TitleColor = Color.White;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

            CustomThemeColor = Color.Gray;

        }

        public override void InitControls() {

            base.InitControls();

            if (_inventory.Items.Count > 0) {

                _inventory.Items.ForEach(i => {

                    var btnItem = new ItemButton(i);
                    btnItem.OnPress += (sender, args) => {
                        BtnItemOnOnPress(_inventory, i, sender, args);
                    };

                    AddMenuItem(btnItem);

                });

                AddMenuInfo("Press the item to manage.");

            } else {
              
                var labelNoItems = new Label("There are no items available");
                AddMenuItem(labelNoItems);
                  
            }
            
        }

        protected abstract void BtnItemOnOnPress(Inventory.Inventory inventory, ItemStack stack, object sender, EventArgs eventArgs);

        public Inventory.Inventory GetInventory() {
            return _inventory;
        }

    }

}
