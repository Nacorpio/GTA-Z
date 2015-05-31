using System.Drawing;
using GTAZ.Controllable;
using mlgthatsme.GUI;

namespace GTAZ.Menus {

    public class VehicleHoodMenu : BaseMenu {

        private readonly ControllableVehicle _vehicle;

        public VehicleHoodMenu(ControllableVehicle vehicle) {

            _vehicle = vehicle;

            TitleText = "Vehicle Properties";
            TitleColor = Color.White;
            CustomThemeColor = Color.Blue;

            TitleOverlay = new SpriteDefinition("www_merryweathersecurity_com", "texturesheet_map");

        }

        public override void InitControls() {

            base.InitControls();

            var buttonEngine = new Button("Engine properties");
            var buttonBattery = new Button("Battery properties");
            var buttonFuel = new Button("Fuel status");

            AddMenuItem(buttonEngine);
            AddMenuItem(buttonBattery);
            AddMenuItem(buttonFuel);

        }
    }

}
