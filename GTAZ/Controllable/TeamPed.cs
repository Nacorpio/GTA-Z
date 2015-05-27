using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using Menu = GTA.Menu;

namespace GTAZ.Controllable {

    public class TeamPed : ControllablePed {

        public static GTA.Menu Menu;

        public TeamPed(int uid) : base(uid, "TEAM_PED", new PedProperties {

            IsFriendly = true,

            Weapons = new[] {WeaponHash.CarbineRifle, WeaponHash.MicroSMG, WeaponHash.Bat},
            PreferredWeapon = WeaponHash.CarbineRifle,

            Armor = 100,
            Accuracy = 50,
            MaxHealth = 150,
            Health = 150,

            AttachBlip = true,
            BlipColor = BlipColor.Green,

            Teleport = false,
            RecordKeys = true,

            HasMenu = true,
            MenuKey = Keys.E,

        }) {}

        protected override void OnEntityAlive() {
           
        }

        protected override void OnEntityDead() {
            UI.Notify("A team member died!");
        }

        protected override void OnEntityPedNearbyUpdate(Ped ped1, int tick) {

        }

        protected override void OnEntityPlayerNearbyUpdate(int tick) {

        }

        protected override void OnEntityPedNearby(Ped ped1) {

        }

        protected override void OnEntityPlayerNearby() {
        }

        protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        protected override void OnPlayerMenuOpen() {

            

            if (Menu == null)

                Menu = new Menu("Team Member (" + UniqueId + ")", new GTA.MenuItem[] {
                    new MenuLabel("Seconds alive: " + Tick, false), 
                });

                Main.Viewport.AddMenu(Menu);

        }

        protected override void OnEntityAliveUpdate(int tick) {
            
        }

        protected override void OnEntityInitialize() {

        }

    }

}
