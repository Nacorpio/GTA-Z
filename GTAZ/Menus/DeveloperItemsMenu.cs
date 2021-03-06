﻿using System.Drawing;
using GTA;
using GTAZ.Inventory;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class DeveloperItemsMenu : BaseMenu {

        public DeveloperItemsMenu() {

            TitleText = "Items DEV";
            TitleColor = Color.Purple;
            CustomThemeColor = Color.DeepPink;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();

            var dividerOptions = new Devider("Inventory");

            var multiItems = new Multichoice() {
                Text = "Item",
                Choices = ItemsDef.ItemsToStrings(ItemsDef.Items)
            };

            var sliderQuantity = new Slider("Quantity (1)") {
                Value = 1,
                MaxValue = 16,
                IncrementValue = 1
            };

            sliderQuantity.ValueChanged += (sender, args) => {
                sliderQuantity.Text = "Quantity (" + sliderQuantity.Value + ")";
            };

            var multiWhere = new Multichoice() {
                Text = "Where",
                Choices = new [] {"Player Inventory", "On ground"}
            };

            var buttonSpawn = new Button("Spawn");
            buttonSpawn.OnPress += (sender, args) => {
                
                switch (multiWhere.GetSelectedChoice()) {
                    case "On ground":
                        Main.Populator.SpawnItemStack(new ItemStack(ItemsDef.Items[multiItems.GetChoiceIndex()], sliderQuantity.Value), Main.Player.Character.Position);
                        break;
                    case "Player Inventory":
                        Main.PlayerInventory.AddItem(ItemsDef.Items[multiItems.GetChoiceIndex()], sliderQuantity.Value);
                        break;
                }

                UI.Notify("The selected item has been spawned");

            };

            AddMenuItem(dividerOptions);
            AddMenuItem(multiItems);
            AddMenuItem(sliderQuantity);
            AddMenuItem(multiWhere);
            AddMenuItem(buttonSpawn);

        }
    }

}
