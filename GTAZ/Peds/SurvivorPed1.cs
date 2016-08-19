using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds
{
    public class SurvivorPed1 : ControllablePed
    {
        public SurvivorPed1(int uid) : base(uid, "SURVIVOR_PED", 100f, new PedProperties
        {
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
        { }
    }
}
