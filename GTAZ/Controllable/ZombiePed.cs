using GTA;
using GTA.Native;

namespace GTAZ.Controllable {

    public class ZombiePed : ControllablePed {

        public ZombiePed(int uid) : base(uid, "ZOMBIE",
        new PedProperties {

            IsFriendly = false,

            SpawnRandomWeapons = true,
            RandomWeapons = new []{WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat, WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub},

            Weapons = null,
            PreferredWeapon = WeaponHash.Unarmed,

            Armor = 50,
            Accuracy = 10,
            MaxHealth = 75,
            Health = 75,

            AttachBlip = true,
            BlipColor = BlipColor.Red,

            Teleport = false

        }) {}

        protected override void OnPedInitialize() {

            Ped.AlwaysKeepTask = true;
            
            Function.Call(Hash.SET_BLOCKING_OF_NON_TEMPORARY_EVENTS, Ped, 1);
            Function.Call(Hash.SET_PED_FLEE_ATTRIBUTES, Ped, 0, 0);
            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, Ped, 17, 1);

        }

        protected override void OnPedInRangeOfPlayer(int tick) {
            if (tick == 0)
                UI.Notify("A zombie is in range of the player (2 units).");
        }

        protected override void OnPedDeadUpdate(int tick) {

        }

        protected override void OnPedAliveUpdate(int tick) {
          
        }

    }

}
