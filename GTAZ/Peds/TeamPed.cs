using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class TeamPed : ControllablePed {

        public TeamPed(int uid) : base(uid, "TEAM_PED", 100f, new PedProperties {

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

        #region MyRegion
        //protected override void OnEntityAlive() {}

        //protected override void OnEntityDead() {
        //    UI.Notify("A team member died!");
        //}

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

        //protected override void OnEntityPedNearbyUpdate(Ped ped1, int tick) {}

        //protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        //protected override void OnEntityPedNearby(Ped ped1) {}

        //protected override void OnEntityPlayerNearby() {}

        //protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        //protected override void OnPlayerMenuOpen() {}

        //protected override void OnEntityAliveUpdate(int tick) {}

        //protected override void OnEntityInitialize() {} 
        #endregion

    }

}
