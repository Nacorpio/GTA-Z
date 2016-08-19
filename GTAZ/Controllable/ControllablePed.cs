using System;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAZ.Assembly;
using GTAZ.Inventory;

namespace GTAZ.Controllable {

    public delegate void MenuOpenEventHandler(object sender, EventArgs e);

    public abstract class ControllablePed : EntityAssembly
    {
        protected event MenuOpenEventHandler MenuOpen;

        protected struct PedProperties
        {
            public bool Teleport;
            public int X, Y, Z;

            public bool AttachBlip;
            public BlipColor BlipColor;

            public bool UseBlipSprite;
            public BlipSprite BlipSprite;

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

        protected ControllablePed(int uid, string group, float weightCapacity, PedProperties props) : base(uid, group, weightCapacity)
        {
            _props = props;
            PlayerKeyDown += OnPlayerKeyDown;
        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs e)
        {
            if (e == null || !_props.RecordKeys)
                return;

            if (_props.HasMenu && e.KeyCode == _props.MenuKey)
                MenuOpen?.Invoke(this, EventArgs.Empty);
        }

        protected Ped Ped => (Ped) Entity;

        private void AttachBlip(Entity ped)
        {
            if (!_props.AttachBlip) return;

            var blip = ped.AddBlip();
            blip.Color = _props.BlipColor;

            if (_props.UseBlipSprite)
                blip.Sprite = _props.BlipSprite;
        }

        protected override void ApplyChanges()
        {
            if (Entity == null)
                return;

            var ped = (Ped) Entity;
            ped.IsPersistent = true;

            PlaceOnNextStreet();

            if (_props.IsFriendly)
            {
                var playerGroup = Function.Call<int>(Hash.GET_PLAYER_GROUP, Main.Player);
                Function.Call(Hash.SET_PED_AS_GROUP_MEMBER, ped, playerGroup);
            }
            else
            {
                ped.IsEnemy = true;
            }

            ped.RelationshipGroup = (_props.IsFriendly ? Main.PlayerGroup : (_props.IsZombie ? Main.ZombieGroup : Main.EnemyGroup));

            var randomWeapon = WeaponHash.Unarmed;
            if (_props.Teleport)
                ped.Position = new Vector3(_props.X, _props.Y, _props.Z);

            AttachBlip(ped);

            if (_props.SpawnRandomWeapons)
            {
                var rand = new Random(Game.GameTime);
                var randomIndex = rand.Next(0, _props.RandomWeapons.Length - 1);

                randomWeapon = _props.RandomWeapons[randomIndex];
                ped.Weapons.Give(randomWeapon, 100, true, true);
            }

            ped.Accuracy = _props.Accuracy;
            ped.Armor = _props.Armor;
            ped.MaxHealth = _props.MaxHealth;
            ped.Health = _props.Health;

            if (_props.Weapons != null && _props.PreferredWeapon != WeaponHash.Unarmed)
            {
                _props.Weapons.ToList().ForEach(w => ped.Weapons.Give(w, 100, true, true));
                ped.Weapons.Select(ped.Weapons[_props.PreferredWeapon]);
            }
            else
            {
                ped.Weapons.Select(ped.Weapons[randomWeapon]);
            }
        }

        protected override void InitializeAssembly()
        {
            // Initialize the Parts every wrapped ped should have in its Assembly.
            AddPart("Inventory", new PlayerInventory());
        }
    }
}
