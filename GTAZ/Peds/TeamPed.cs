using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds
{
    public class TeamPed : ControllablePed
    {
        public TeamPed(int uid) : base(uid, "TEAM_PED", 100f, new PedProperties
        {
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
        })
        { }
    }
}
