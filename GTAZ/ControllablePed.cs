using System;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAV_purge_mod;

namespace GTAZ {

    public abstract class ControllablePed : Updater {

        public struct PedProperties {

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

        }

        private Ped _ped;
        private readonly PedProperties _props;

        protected ControllablePed(int uid, string sid, PedProperties props) {

            UniqueId = uid;
            StringId = sid;
            _props = props;

        }

        /// <summary>
        /// Takes control of the specified ped.
        /// </summary>
        /// <param name="ped">The ped to take cntrol over.</param>
        /// <param name="pp">The properties to set to the ped.</param>
        public ControllablePed Control(Ped ped) {

            if (_ped == null && !IsControlling)

                _ped = ped;
                ApplyChanges();
                IsControlling = true;

            return this;

        }

        private void ApplyChanges() {

            if (_ped == null) {
                return;
            }

            if (_props.IsFriendly) {

                var playerGroup = Function.Call<int>(Hash.GET_PLAYER_GROUP, Main.player);
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, _ped, playerGroup);

            } else {

                Ped.IsEnemy = true;

            }

            RelationshipGroup = (_props.IsFriendly ? Main.PLAYER_GROUP : Main.ENEMY_GROUP);
            Ped.RelationshipGroup = RelationshipGroup;

            var randomWeapon = WeaponHash.Unarmed;
            if (_props.Teleport) {

                _ped.Position = new Vector3(_props.X, _props.Y, _props.Z);

            }

            if (_props.AttachBlip) {

                _ped.AddBlip()
                    .Color = _props.BlipColor;

            }

            if (_props.SpawnRandomWeapons) {

                var rand = new Random(Game.GameTime);
                var randomIndex = rand.Next(0, _props.RandomWeapons.Length - 1);

                randomWeapon = _props.RandomWeapons[randomIndex];
                _ped.Weapons.Give(randomWeapon, 100, true, true);

            }

            _ped.Accuracy = _props.Accuracy;
            _ped.Armor = _props.Armor;
            _ped.MaxHealth = _props.MaxHealth;
            _ped.Health = _props.Health;

            if (_props.Weapons != null && _props.PreferredWeapon != WeaponHash.Unarmed) {

                _props.Weapons.ToList().ForEach(w => _ped.Weapons.Give(w, 100, true, true));
                _ped.Weapons.Select(_ped.Weapons[_props.PreferredWeapon]);

            } else {

                _ped.Weapons.Select(_ped.Weapons[randomWeapon]);

            }

            OnPedInitialize();

        }

        protected override void OnUpdate(int tick) {
            IsActive = (_ped != null && (_ped.IsAlive || _ped.IsDead));
        }

        public void TeleportTo(Vector3 posVector3) {
            if (_ped != null)
                _ped.Position = posVector3;
        }

        private int _deadTicks = 0;
        private int _aliveTicks = 0;

        protected override void OnActiveUpdate(int activeTick, int tick) {

            if (_ped == null) {
                return;
            }

            if (_ped.IsAlive) {

                // The ped is alive.
                OnPedAliveUpdate(_aliveTicks);
                _aliveTicks++;

            } else if (_ped.IsDead) {

                if (_props.AttachBlip && _ped.CurrentBlip != null) {
                    _ped.CurrentBlip.Remove();
                }

                // The ped is dead.
                OnPedDeadUpdate(_deadTicks);
                _deadTicks++;

            }

        }

        public Ped Ped {
            get { return _ped; }
            private set { _ped = value; }
        }

        public int RelationshipGroup { get; set; }

        public bool IsControlling { get; set; }

        public int UniqueId {
            get; set;
        }

        public string StringId {
            get; set;
        }

        protected abstract void OnPedInitialize();

        /// <summary>
        /// This event fires every update where the controllable ped is dead.
        /// </summary>
        /// <param name="tick">The current tick.</param>
        protected abstract void OnPedDeadUpdate(int tick);

        /// <summary>
        /// This event fires every update where the controllable ped is alive.
        /// </summary>
        /// <param name="tick">The current tick.</param>
        protected abstract void OnPedAliveUpdate(int tick);


    }

}
