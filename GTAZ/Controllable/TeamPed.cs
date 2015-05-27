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

            Teleport = false

        }) {}

        protected override void OnPedAliveUpdate(int tick) {
        }

        protected override void OnPedInRangeOfPlayer(int tick) {

        }

        protected override void OnPedDeadUpdate(int tick) {
            if (tick == 0)
                UI.Notify("A team member just died!");
        }

        protected override void OnPedInitialize() {
        }

    }

}
