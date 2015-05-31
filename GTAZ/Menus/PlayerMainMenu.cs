using System.Drawing;
using GTA;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class PlayerMainMenu : BaseMenu{

        public PlayerMainMenu() {

            TitleText = "Grand Theft Zombies";
            TitleColor = Color.White;
            TitleSize = 0.75f;

            CustomThemeColor = Color.Red;
            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();

            var divider0 = new Devider("Main Menu");                

            var tbxToggle1 = new TickBox("Toggle GTZ") {
                PrefixIcon = new SpriteDefinition("commonmenu", "mp_specitem_heroin"),
                PrefixIconColor = Color.White,
                PrefixIconSize = 16,
            };

            tbxToggle1.OnPress += (sender, args) => {
                Main.Toggle();
                if (Main.IsToggled) {
                    UI.Notify("GTZ is now toggled ON");
                } else {
                    UI.Notify("GTZ is now toggled OFF");
                }
            };

            var btnDeveloper = new Button("Developer");

            btnDeveloper.OnPress += (sender, args) => Main.WindowManager.AddMenu(new DeveloperMenu());

            var divider1 = new Devider("My Content");
            var btnInventory = new Button("Inventory");

            btnInventory.OnPress += (sender, args) => {
                Main.WindowManager.AddMenu(new PlayerInventoryMenu());
            };

            var btnWeapons = new Button("Weapons");
            var btnContacts = new Button("Contacts");

            AddMenuItem(divider0);

            AddMenuItem(tbxToggle1);
            AddMenuItem(btnDeveloper);

            AddMenuItem(divider1);

            AddMenuItem(btnInventory);
            AddMenuItem(btnWeapons);
            AddMenuItem(btnContacts);

        }

        public override void Update() {

            base.Update();

        }

    }

}
