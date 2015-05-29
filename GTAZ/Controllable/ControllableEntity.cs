using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAV_purge_mod;

namespace GTAZ.Controllable {

    public abstract class ControllableEntity : Updater {

        private Entity _entity;
        private float _interactionDistance = 2f;

        protected ControllableEntity(int uid, string groupId) {
            UniqueId = uid;
            GroupId = groupId;
        }

        //

        public bool Keep { get; set; }

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
        /// Sets the distance of which the player has to be to this entity to fire the KeyDown event.
        /// </summary>
        /// <param name="distance">The distance units.</param>
        public void SetInteractionDistance(float distance) {
            if (distance >= 0)
                _interactionDistance = distance;
        }

        /// <summary>
        /// Places this ControllableEntity on the closest StreetNode.
        /// </summary>
        public void PlaceOnNextStreet() {

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
        public ControllableEntity KeyDown(KeyEventArgs e) {

            if (e == null)
                return null;

            if (IsActive && Entity.IsInRangeOf(Main.Player.Character.Position, _interactionDistance) && !Main.Player.Character.IsInVehicle())
                OnEntityPlayerKeyDown(e);
            
            return this;

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
        private int _playerNearbyTicks, _pedNearbyTicks;

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

            if (_entity.IsAlive) {

                OnEntityAliveUpdate(_aliveTicks);

                if (_aliveTicks == 0)
                    OnEntityAlive();

                _aliveTicks++;

                foreach (var entity in Main.ControlManager.LivingPeds) {

                    if (Entity.IsInRangeOf(entity.Entity.Position, _interactionDistance)) {

                        OnEntityPedNearbyUpdate((Ped) entity.Entity, _pedNearbyTicks);

                        if (_pedNearbyTicks == 0) {
                            OnEntityPedNearby((Ped) entity.Entity);
                        }

                        _pedNearbyTicks++;

                    } else {

                        _pedNearbyTicks = 0;

                    }

                }

                if (Entity.IsInRangeOf(Main.Player.Character.Position, _interactionDistance)) {
                  
                    OnEntityPlayerNearbyUpdate(_playerNearbyTicks);

                    if (_playerNearbyTicks == 0) {
                        OnEntityPlayerNearby();
                    }

                    _playerNearbyTicks++;

                } else {

                    _playerNearbyTicks = 0;

                }

                OnEntityInitialize();

            } else if (_entity.IsDead) {

                _aliveTicks = 0;

                OnEntityDead();
                RemoveEntity();

            }

        }

        /// <summary>
        /// Apply the changes to the wrapped Entity.
        /// </summary>
        protected abstract void ApplyChanges();

        //

        /// <summary>
        /// This event has to be fired every update where a Ped is nearby the wrapped Entity.
        /// </summary>
        /// <param name="ped">The Ped that is nearby.</param>
        /// <param name="tick">The current tick.</param>
        protected abstract void OnEntityPedNearbyUpdate(Ped ped, int tick);

        /// <summary>
        /// This event has to be fired every update where a Player is nearby the wrapped Entity.
        /// </summary>
        /// <param name="tick">The current tick.</param>
        protected abstract void OnEntityPlayerNearbyUpdate(int tick);

        /// <summary>
        /// This event has to be fired when a Ped is nearby the wrapped Entity.
        /// </summary>
        /// <param name="ped">The Ped that is nearby.</param>
        protected abstract void OnEntityPedNearby(Ped ped);

        /// <summary>
        /// This event has to be fired when a Player is nearby the wrapped Entity.
        /// </summary>
        protected abstract void OnEntityPlayerNearby();

        //

        /// <summary>
        /// This has event has to be fired when a Player is nearby and presses a key.
        /// </summary>
        /// <param name="e">The KeyEventArgs for the KeyDown event.</param>
        protected abstract void OnEntityPlayerKeyDown(KeyEventArgs e);

        //

        /// <summary>
        /// This event has to be fired when an Entity is going to be initialized.
        /// </summary>
        protected abstract void OnEntityInitialize();

        /// <summary>
        /// This event has to be fired every update where the wrapped Entity is alive.
        /// </summary>
        /// <param name="tick">The current tick.</param>
        protected abstract void OnEntityAliveUpdate(int tick);

        //

        /// <summary>
        /// This event has to be fired when the wrapped Entity is alive.
        /// </summary>
        protected abstract void OnEntityAlive();

        /// <summary>
        /// This event has to be fired when the wrapped Entity is dead.
        /// </summary>
        protected abstract void OnEntityDead();

        /// <summary>
        /// Remove the wrapped Entity. 
        /// </summary>
        public void RemoveEntity() {

            if (Entity.CurrentBlip != null) {
                Entity.CurrentBlip.Remove();
            }

            Main.ControlManager.Remove(this);

        }

    }

}
