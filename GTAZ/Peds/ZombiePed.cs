using System;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Peds {

    public class ZombiePed : ControllablePed {

        public ZombiePed(int uid) : base(uid, "ZOMBIE_PED", 100f,
            new PedProperties {

                IsFriendly = false,
                IsZombie = true,

                SpawnRandomWeapons = true,
                RandomWeapons =
                    new[] {
                        WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Dagger, WeaponHash.Crowbar, WeaponHash.Bat,
                        WeaponHash.Hammer, WeaponHash.Hatchet, WeaponHash.GolfClub
                    },

                Weapons = null,
                PreferredWeapon = WeaponHash.Unarmed,

                Armor = 0,
                Accuracy = 5,
                MaxHealth = 74,
                Health = 74,

                AttachBlip = true,
                BlipColor = BlipColor.Red,

                Teleport = false,
                HasMenu = false

            }) {
            
            Initialize += OnInitialize;

        }

        private void OnInitialize(object sender, EventArgs eventArgs) {

            Ped.AlwaysKeepTask = true;
            Ped.Task.FightAgainst(Main.Player.Character);

        }

    }

}
