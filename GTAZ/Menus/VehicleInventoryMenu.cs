using System;
using System.Drawing;
using System.Security.Cryptography.X509Certificates;
using GTA;
using GTAZ.Controllable;
using GTAZ.Inventory;
using GTAZ.Vehicles;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class VehicleInventoryMenu : BaseMenu {

        public class VehicleLootItems : BaseMenu {

            private readonly Inventory.Inventory _inventory;

            public VehicleLootItems(Inventory.Inventory inventory) {

                _inventory = inventory;

                TitleText = "Inventory";
                TitleColor = Color.White;
                CustomThemeColor = Color.Green;

                TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

            }

            public override void InitControls() {

                base.InitControls();

                _inventory.Items.ForEach(i => {
                    
                    var btn = new Button(i.GetDisplayName() + " x" + i.Size);
                    btn.OnPress += (sender, args) => {
                        Main.WindowManager.AddMenu(new VehicleLootMenu(_inventory, i));
                    };

                    AddMenuItem(btn);

                });

            }
        }

        public class VehicleLootMenu : BaseMenu {

            private readonly Inventory.Inventory _inventory;
            private readonly ItemStack _stack;

            public VehicleLootMenu(Inventory.Inventory inventory, ItemStack stack) {

                _inventory = inventory;
                _stack = stack;

                TitleText = "Manage Item (" + _stack.GetDisplayName() + ")";
                TitleColor = Color.White;
                CustomThemeColor = Color.Green;

                TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

            }

            public override void InitControls() {

                base.InitControls();

                var buttonLoot = new Button("Loot item");
                buttonLoot.OnPress += (sender, args) => {

                    _inventory.RemoveItem(_stack);
                    Main.PlayerInventory.AddItem(_stack);
                    Main.WindowManager.CloseAllMenus();

                    UI.Notify("You just looted " + _stack.GetDisplayName() + " x" + _stack.Size);

                };

                var buttonUse = new Button("Use item");
                buttonUse.OnPress += (sender, args) => {

                    _stack.UseItem(Main.Player, Main.Player.Character);

                    if (_stack.Size == 0) {
                        _inventory.RemoveItem(_stack);
                    } 

                    UI.Notify("You just used one " + _stack.GetDisplayName());

                };

                AddMenuItem(buttonUse);
                AddMenuItem(buttonLoot);

            }
        }

        private readonly Inventory.Inventory _inventory;

        public VehicleInventoryMenu(Inventory.Inventory inventory) {

            _inventory = inventory;

            TitleText = "Abandoned Vehicle";
            TitleColor = Color.White;
            CustomThemeColor = Color.Green;

            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();

            var dividerContent = new Devider("Vehicle Content");
            var buttonInventory = new Button("Inventory (" + _inventory.Count() + " items)");

            buttonInventory.OnPress += (sender, args) => {
                Main.WindowManager.AddMenu(new VehicleLootItems(_inventory));
            };

            AddMenuItem(dividerContent);
            AddMenuItem(buttonInventory);

        }

        public override void OnClose() {
            base.OnClose();
        }
    }

}
