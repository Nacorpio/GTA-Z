using GTA;
using GTA.Native;

namespace GTAZ.Controllable {

    public class ZombiePed : ControllablePed {

        public ZombiePed(int uid, string sid) : base(uid, sid,
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
            Ped.Task.FightAgainst(Main.player.Character);

            Function.Call(Hash.REQUEST_CLIP_SET, "move_m@drunk@verydrunk");

            Function.Call(Hash.SET_PED_MOVEMENT_CLIPSET, Ped, "move_m@drunk@verydrunk", 0x3e800000);
            

        }

        protected override void OnPedDeadUpdate(int tick) {

        }

        protected override void OnPedAliveUpdate(int tick) {
            
        }

    }

}
