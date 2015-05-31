using System;
using System.Drawing;
using GTA;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class DeveloperMenu : BaseMenu {

        private readonly SpriteDefinition[] _sprites = {
            new SpriteDefinition("04_b_sext_stripperinfernus", "04_b_sext_stripperinfernus"),
            new SpriteDefinition("05_a_sext_stripperjuliet", "05_a_sext_stripperjuliet"),
            new SpriteDefinition("05_c_sext_stripperjuliet", "05_c_sext_stripperjuliet"),
            new SpriteDefinition("06_a_sext_strippernikki", "06_a_sext_strippernikki"),
            new SpriteDefinition("06_c_sext_strippernikki", "06_c_sext_strippernikki"),
            new SpriteDefinition("10_a_sext_hitchergirl", "10_a_sext_hitchergirl"),
            new SpriteDefinition("10_c_sext_hitchergirl", "10_c_sext_hitchergirl"),
            new SpriteDefinition("11_b_sext_taxiliz", "11_b_sext_taxiliz"),
        };

        public DeveloperMenu() {

            var rand = new Random(Game.GameTime);

            TitleText = "Developer";
            TitleColor = Color.Purple;
            CustomThemeColor = Color.DeepPink;
            TitleOverlay = _sprites[rand.Next(0, _sprites.Length - 1)];

        }

        public override void InitControls() {

            base.InitControls();

            var dividerCategories = new Devider("Categories");

            var buttonZombies = new Button("Zombies");
            var buttonSurvivors = new Button("Survivors");
            var buttonEvents = new Button("Events");
            var buttonVehicles = new Button("Vehicles");
            var buttonLooting = new Button("Looting");

            var buttonItems = new Button("Items");
            buttonItems.OnPress += (sender, args) => {
                Main.WindowManager.AddMenu(new DeveloperItemsMenu());
            };

            var dividerEnvironment = new Devider("Environment");

            var buttonWeather = new Button("Weather");
            var buttonTime = new Button("Time");

            AddMenuItem(dividerCategories);

            AddMenuItem(buttonZombies);
            AddMenuItem(buttonSurvivors);
            AddMenuItem(buttonEvents);
            AddMenuItem(buttonVehicles);
            AddMenuItem(buttonLooting);
            AddMenuItem(buttonItems);

            AddMenuItem(dividerEnvironment);

            AddMenuItem(buttonWeather);
            AddMenuItem(buttonTime);

        }
    }

}
