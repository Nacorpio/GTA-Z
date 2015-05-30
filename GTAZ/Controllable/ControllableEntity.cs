using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAV_purge_mod;

namespace GTAZ.Controllable {

    public delegate void ChangedEventHandler(object sender, EventArgs e);
    public delegate void PedNearbyEventHandler(Ped ped, object sender, EventArgs e);
    public delegate void PlayerKeyDownEventHandler(object sender, KeyEventArgs ke);
    public delegate void EntityAliveUpdateEventHandler(int tick, object sender, EventArgs e);

    /// <summary>
    /// A wrapper providing a way to keep track of an Entity.
    /// </summary>
    public abstract class ControllableEntity : Updater {

        protected event ChangedEventHandler AirEnter, WaterEnter, UpsideDown, Attached, TouchPlayer, PlayerNearby, Dead, Initialize, Alive;

        protected event PedNearbyEventHandler PedNearby;
        protected event PlayerKeyDownEventHandler PlayerKeyDown;
        protected event EntityAliveUpdateEventHandler AliveUpdate;

        private Entity _entity;
        private float _interactionDistance = 2f;

        protected ControllableEntity(int uid, string groupId) {
            UniqueId = uid;
            GroupId = groupId;
        }

        //

        /// <summary>
        /// Returns whether this ControllableEntity should be kept in OutOfRange despawnings.
        /// </summary>
        public bool Keep { get; set; } = false;

        /// <summary>
        /// Returns the unique group identifier of this ControllableEntity.
        /// </summary>
        public string GroupId { get; private set; }

        /// <summary>
        /// Returns the unique identifier of this ControllableEntity.
        /// </summary>
        public int UniqueId { get; private set; }

        /// <summary>
        /// Returns the Entity that is being wrapped around in this ControllableEntity.
        /// </summary>
        public Entity Entity { get { return _entity; }}

        /// <summary>
        /// Returns the ControllableEntities that the wrapped Entity has been damaged by.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ControllableEntity> GetDamagedBy() {
            return Main.ControlManager.Entities.Where(e => Entity.HasBeenDamagedBy(e.Entity));
        } 

        /// <summary>
        /// Sets the distance of which the player has to be to this entity to fire the KeyDown event.
        /// </summary>
        /// <param name="distance">The distance units.</param>
        protected void SetInteractionDistance(float distance) {
            if (distance >= 0)
                _interactionDistance = distance;
        }

        /// <summary>
        /// Places this ControllableEntity on the closest StreetNode.
        /// </summary>
        protected void PlaceOnNextStreet() {

            var pos = Entity.Position;
            var outPos = new OutputArgument();

            for (var i = 0; i < 40; i++) {

                Function.Call(Hash.GET_NTH_CLOSEST_VEHICLE_NODE, pos.X, pos.Y, pos.Z, i, outPos, 1, 0x40400000, 0);
                var newPos = outPos.GetResult<Vector3>();

                if (Function.Call<bool>(Hash.IS_POINT_OBSCURED_BY_A_MISSION_ENTITY, newPos.X, newPos.Y, newPos.Z, 5.0f, 5.0f, 5.0f, 0)) {
                    return;
                }

                Entity.Position = newPos;
                break;

            }

        }

        //

        /// <summary>
        /// Controls the specified Entity with this ControllableEntity.
        /// </summary>
        /// <param name="entity">The Entity to wrap around.</param>
        /// <returns></returns>
        public ControllableEntity Control(Entity entity) {

            if (_entity == null)

                _entity = entity;
                ApplyChanges();
                

            return this;

        }

        /// <summary>
        /// Fires the KeyDown event.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public void KeyDown(KeyEventArgs e) {

            if (e == null)
                return;

            if (PlayerKeyDown == null)
                return;

            if (!(IsActive && Entity.IsInRangeOf(Main.Player.Character.Position, _interactionDistance) &&
                !Main.Player.Character.IsInVehicle())) {

                return;

            }
            
            PlayerKeyDown(this, e);

        }

        //

        /// <summary>
        /// This event has to be fired on every update of this ControllableEntity.
        /// </summary>
        /// <param name="tick">The current tick.</param>
        protected override void OnUpdate(int tick) {
            IsActive = _entity != null && (_entity.IsAlive || _entity.IsDead);
        }

        private int _aliveTicks;
        private int _playerNearbyTicks, _pedNearbyTicks, _playerTouchingTicks, _entityIsAttachedTicks,
                    _entityInAirTicks, _entityInWaterTicks, _entityUpsideDownTicks;

        //

        /// <summary>
        /// This event has to be fired on every active update of this ControllableEntity.
        /// </summary>
        /// <param name="activeTick">The current amount of ticks the wrapped Entity has been active for.</param>
        /// <param name="tick">The current tick.</param>
        protected override void OnActiveUpdate(int activeTick, int tick) {

            if (_entity == null) {
                Entity.Delete();
                return;
            }

            if (Entity.IsAlive) {

                if (AliveUpdate != null) AliveUpdate(_aliveTicks, this, EventArgs.Empty);

                if (Initialize == null)
                    return;

                if (_aliveTicks == 0)
                    if (Alive != null) Alive(this, EventArgs.Empty);

                _aliveTicks++;

                foreach (var entity in Main.ControlManager.LivingPeds) {

                    if (Entity.IsInRangeOf(entity.Entity.Position, _interactionDistance)) {

                        if (_pedNearbyTicks == 0) {
                            if (PedNearby != null) PedNearby((Ped) entity.Entity, this, EventArgs.Empty);
                        }

                        _pedNearbyTicks++;

                    } else {

                        _pedNearbyTicks = 0;

                    }

                }

                if (Entity.IsUpsideDown) {

                    if (_entityUpsideDownTicks == 0) {
                        if (UpsideDown != null)
                            UpsideDown(this, EventArgs.Empty);
                    }

                    _entityUpsideDownTicks++;

                } else {

                    _entityUpsideDownTicks = 0;

                }

                if (Entity.IsInWater) {

                    if (_entityInWaterTicks == 0) {
                        if (WaterEnter != null)
                            WaterEnter(this, EventArgs.Empty);
                    }

                    _entityInWaterTicks++;

                } else {

                    _entityInWaterTicks = 0;

                }

                if (Entity.IsInAir) {

                    if (_entityInAirTicks == 0) {
                        if (AirEnter != null)
                             AirEnter(this, EventArgs.Empty);
                    }

                    _entityInAirTicks++;

                } else {

                    _entityInAirTicks = 0;

                }

                if (Entity.IsAttached()) {

                    if (_entityIsAttachedTicks == 0) {
                        if (Attached != null)
                            Attached(this, EventArgs.Empty);
                    }

                    _entityIsAttachedTicks++;

                } else {

                    _entityIsAttachedTicks = 0;

                }

                if (Entity.IsTouching(Main.Player.Character)) {

                    if (_playerTouchingTicks == 0) {
                        if (TouchPlayer != null)
                            TouchPlayer(this, EventArgs.Empty);
                    }

                    _playerTouchingTicks++;

                } else {

                    _pedNearbyTicks = 0;

                }

                if (Entity.IsInRangeOf(Main.Player.Character.Position, _interactionDistance)) {
                  
                    if (_playerNearbyTicks == 0) {
                        if (PlayerNearby != null)
                            PlayerNearby(this, EventArgs.Empty);
                    }

                    _playerNearbyTicks++;

                } else {

                    _playerNearbyTicks = 0;

                }

                Initialize(this, EventArgs.Empty);    
            
            } else if (_entity.IsDead) {

                _aliveTicks = 0;

                UI.Notify("CHECK (DEAD)!");

                if (Dead != null)
                    Dead(this, EventArgs.Empty);

                RemoveEntity();

            }

        }

        /// <summary>
        /// Apply the changes to the wrapped Entity.
        /// </summary>
        protected abstract void ApplyChanges();

        /// <summary>
        /// Remove the wrapped Entity. 
        /// </summary>
        public void RemoveEntity() {

            if (_entity.CurrentBlip != null) {
                Entity.CurrentBlip.Remove();
            }

            Main.ControlManager.Remove(this);

        }

    }

}
