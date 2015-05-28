using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class SurvivorPed1 : ControllablePed {

        public SurvivorPed1(int uid) : base(uid, "SURVIVOR_PED", new PedProperties {

            IsFriendly = true,
            SpawnRandomWeapons = false,

            Weapons = new [] {WeaponHash.AssaultRifle, WeaponHash.CombatPistol, WeaponHash.Bat},
            PreferredWeapon = WeaponHash.AssaultRifle,

            Armor = 100,
            Accuracy = 50,

            MaxHealth = 100,
            Health = 100,

            AttachBlip = true,
            BlipColor = BlipColor.Green,

            Teleport = false,
            HasMenu = false

        })
        {
            
        }

        protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        protected override void OnEntityPedNearby(Ped ped) {}

        protected override void OnEntityPlayerNearby() {}

        protected override void OnEntityAlive() {}

        protected override void OnEntityDead() {}

        protected override void OnPlayerMenuOpen() {}

        protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        protected override void OnEntityInitialize() {}

    }

}
