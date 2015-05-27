using System.Windows.Forms;
using GTA;
using GTA.Native;

namespace GTAZ.Controllable {

    public class TeamPed : ControllablePed {

        public TeamPed(int uid) : base(uid, "TEAM", new PedProperties {

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
            RecordKeys = true

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

        protected override void OnPlayerKeyDown(KeyEventArgs e) {
            switch (e.KeyCode) {
                case Keys.E:
                    UI.Notify("It works, of course.");
                    break;
            }
        }

        protected override void OnEntityAliveUpdate(int tick) {

        }

        protected override void OnEntityInitialize() {

        }

    }

}
