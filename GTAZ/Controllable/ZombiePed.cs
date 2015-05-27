﻿using System.Windows.Forms;
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

        protected override void OnEntityAlive() {
            
        }

        protected override void OnEntityDead() {
           
        }

        protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {
            
        }

        protected override void OnEntityPlayerNearbyUpdate(int tick) {

        }

        protected override void OnEntityPedNearby(Ped ped) {

        }

        protected override void OnEntityPlayerNearby() {
        }

        protected override void OnEntityAliveUpdate(int tick) {

        }

        protected override void OnPlayerKeyDown(KeyEventArgs e) {
        }

        protected override void OnEntityInitialize() {
            Ped.AlwaysKeepTask = true;
            Ped.Task.FightAgainst(Main.Player.Character);
        }
    }

}
