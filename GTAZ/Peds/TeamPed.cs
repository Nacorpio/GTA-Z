using System.Runtime.CompilerServices;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ;
using GTAZ.Controllable;
using Menu = GTA.Menu;

namespace GTAZ.Peds {

    public class TeamPed : ControllablePed {

        public TeamPed(int uid) : base(uid, "TEAM_PED", new PedProperties {

            IsFriendly = false,

            SpawnRandomWeapons = true,
            RandomWeapons = new [] {WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat, WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub},

            Weapons = null,
            PreferredWeapon = WeaponHash.Unarmed,

            Armor = 50,
            Accuracy = 50,
            MaxHealth = 125,
            Health = 125,

            AttachBlip = true,
            BlipColor = BlipColor.Red,

            Teleport = false,
            HasMenu = false

        }) {}

        protected override void OnEntityAlive() {}

        protected override void OnEntityDead() {
            UI.Notify("A team member died!");
        }

        protected override void OnEntityPedNearbyUpdate(Ped ped1, int tick) {}

        protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        protected override void OnEntityPedNearby(Ped ped1) {}

        protected override void OnEntityPlayerNearby() {}

        protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        protected override void OnPlayerMenuOpen() {}

        protected override void OnEntityAliveUpdate(int tick) {}

        protected override void OnEntityInitialize() {}

    }

}
