using System.Drawing;
using GTAZ.Inventory;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class PlayerInventoryMenu : BaseMenu {

        public PlayerInventoryMenu() {

            TitleText = "Player Inventory";
            TitleColor = Color.White;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");
            CustomThemeColor = Color.Gray;

        }

        public override void InitControls() {

            base.InitControls();

            var dividerContent = new Devider("Content");

            var buttonAllItems = new Button("All Items");
            buttonAllItems.OnPress += (sender, args) => {
                Main.PlayerInventory.ShowInventory();
            };

            var dividerCategories = new Devider("Categories");

            var buttonWeapons = new Button("Weapons");
            var buttonAmmo = new Button("Ammunition");
            var buttonFood = new Button("Food");
            var buttonUtilities = new Button("Utilities");
            var buttonOwnerships = new Button("Ownerships");

            AddMenuItem(dividerContent);

            AddMenuItem(buttonAllItems);

            AddMenuItem(dividerCategories);

            AddMenuItem(buttonWeapons);
            AddMenuItem(buttonAmmo);
            AddMenuItem(buttonFood);
            AddMenuItem(buttonUtilities);
            AddMenuItem(buttonOwnerships);

        }

    }

}
