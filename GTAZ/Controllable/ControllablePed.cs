using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAZ.Assembly;

namespace GTAZ.Controllable {

    public abstract class ControllablePed : EntityAssembly {

        protected struct PedProperties {

            public bool Teleport;
            public int X, Y, Z;

            public bool AttachBlip;
            public BlipColor BlipColor;

            public int Health;
            public int MaxHealth;

            public bool SpawnRandomWeapons;
            public WeaponHash[] Weapons;
            public WeaponHash[] RandomWeapons;

            public WeaponHash PreferredWeapon;

            public int Accuracy;
            public int Armor;

            public bool IsFriendly;
            public bool IsZombie;

            public bool RecordKeys;

            public bool HasMenu;
            public Keys MenuKey;

        }

        private readonly PedProperties _props;

        protected ControllablePed(int uid, string group, PedProperties props) : base(uid, group) {
            _props = props;
        }

        public Ped Ped {
            get { return (Ped) Entity; }
        }

        private void AttachBlip(Entity ped) {
            if (_props.AttachBlip) {

                ped.AddBlip()
                    .Color = _props.BlipColor;

            }
        }

        //

        protected override void ApplyChanges() {

            if (Entity == null) {
                return;
            }

            var ped = (Ped) Entity;

            ped.IsPersistent = true;

            if (_props.IsFriendly) {

                var playerGroup = Function.Call<int>(Hash.GET_PLAYER_GROUP, Main.Player);
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, playerGroup);

            } else {

                ped.IsEnemy = true;

            }

            ped.RelationshipGroup = (_props.IsFriendly ? Main.PlayerGroup : (_props.IsZombie ? Main.ZombieGroup : Main.EnemyGroup));

            var randomWeapon = WeaponHash.Unarmed;
            if (_props.Teleport) {

                ped.Position = new Vector3(_props.X, _props.Y, _props.Z);

            }

            AttachBlip(ped);

            if (_props.SpawnRandomWeapons) {

                var rand = new Random(Game.GameTime);
                var randomIndex = rand.Next(0, _props.RandomWeapons.Length - 1);

                randomWeapon = _props.RandomWeapons[randomIndex];
                ped.Weapons.Give(randomWeapon, 100, true, true);

            }

            ped.Accuracy = _props.Accuracy;
            ped.Armor = _props.Armor;
            ped.MaxHealth = _props.MaxHealth;
            ped.Health = _props.Health;

            if (_props.Weapons != null && _props.PreferredWeapon != WeaponHash.Unarmed) {

                _props.Weapons.ToList().ForEach(w => ped.Weapons.Give(w, 100, true, true));
                ped.Weapons.Select(ped.Weapons[_props.PreferredWeapon]);

            } else {

                ped.Weapons.Select(ped.Weapons[randomWeapon]);

            }

        }

        protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {
            
        }

        //

        protected override void InitializeAssembly() {
            // Initialize the Parts every wrapped ped should have in its Assembly.

        }

        protected sealed override void OnEntityPlayerKeyDown(KeyEventArgs e) {

            if (e == null || !_props.RecordKeys) {
                return;
            }

            if (_props.HasMenu && e.KeyCode == _props.MenuKey) {
                OnPlayerMenuOpen();
            }

            OnPlayerKeyDown(e);

        }

        protected abstract void OnPlayerMenuOpen();

        protected abstract void OnPlayerKeyDown(KeyEventArgs e);

        //

        protected override void OnEntityAliveUpdate(int tick) {
            
        }

        protected abstract override void OnEntityInitialize();

    }

}
