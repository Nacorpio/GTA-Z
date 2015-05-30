using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class SurvivorPed1 : ControllablePed {

        public SurvivorPed1(int uid) : base(uid, "SURVIVOR_PED", 100f, new PedProperties {

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

        #region MyRegion
        //protected override void OnEntityUpsideDown() {}

        //protected override void OnEntityUpsideDownUpdate(int tick) {}

        //protected override void OnEntityInAir() {}

        //protected override void OnEntityInAirUpdate(int tick) {}

        //protected override void OnEntityInWater() {}

        //protected override void OnEntityInWaterUpdate(int tick) {}

        //protected override void OnEntityAttached() {}

        //protected override void OnEntityAttachedUpdate(int tick) {}

        //protected override void OnEntityPlayerIsTouching() {}

        //protected override void OnEntityPlayerIsTouchingUpdate(int tick) {}

        //protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        //protected override void OnEntityPedNearby(Ped ped) {}

        //protected override void OnEntityPlayerNearby() {}

        //protected override void OnEntityAlive() {}

        //protected override void OnEntityDead() {}

        //protected override void OnPlayerMenuOpen() {}

        //protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        //protected override void OnEntityInitialize() {} 
        #endregion

    }

}
