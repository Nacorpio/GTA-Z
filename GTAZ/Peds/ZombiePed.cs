using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class ZombiePed : ControllablePed {

        public ZombiePed(int uid) : base(uid, "ZOMBIE_PED",
        new PedProperties {

            IsFriendly = false,
            IsZombie = true,

            SpawnRandomWeapons = true,
            RandomWeapons = new [] {WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat, WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub},

            Weapons = null,
            PreferredWeapon = WeaponHash.Unarmed,

            Armor = 50,
            Accuracy = 5,
            MaxHealth = 75,
            Health = 75,

            AttachBlip = true,
            BlipColor = BlipColor.Red,

            Teleport = false,
            HasMenu = false

        }) {}

        protected override void OnEntityDead() {}

        protected override void OnEntityAlive() {}

        protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {}

        protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        protected override void OnEntityPedNearby(Ped ped) {}

        protected override void OnEntityPlayerNearby() {}

        protected override void OnEntityAliveUpdate(int tick) {}

        protected override void OnPlayerMenuOpen() {}

        protected override void OnPlayerKeyDown(KeyEventArgs e) {
        }

        protected override void OnEntityInitialize() {

            Function.Call(Hash.APPLY_PED_BLOOD);

            Ped.AlwaysKeepTask = true;
            Ped.Task.GuardCurrentPosition();

        }

    }

}
