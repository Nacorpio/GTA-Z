using System.Windows.Forms;
using GTA;
using GTAV_purge_mod;

namespace GTAZ.Controllable {

    public abstract class ControllableEntity : Updater {

        private Entity _entity;
        public const float InteractionDistance = 2f;

        protected ControllableEntity(int uid, string groupId) {
            UniqueId = uid;
            GroupId = groupId;
        }

        //

        public string GroupId { get; private set; }

        public int UniqueId { get; private set; }

        public Entity Entity { get { return _entity; }}

        //

        public ControllableEntity Control(Entity entity) {

            if (_entity == null)

                _entity = entity;
                ApplyChanges();
                

            return this;

        }

        public ControllableEntity KeyDown(KeyEventArgs e) {

            if (e == null)
                return null;

            if (IsActive && Entity.IsInRangeOf(Main.Player.Character.Position, InteractionDistance))
                OnEntityPlayerKeyDown(e);
            
            return this;

        }

        //

        protected override void OnUpdate(int tick) {
            IsActive = _entity != null && (_entity.IsAlive || _entity.IsDead);
        }

        private int _aliveTicks;
        private int _playerNearbyTicks, _pedNearbyTicks;

        //

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

                    if (Entity.IsInRangeOf(entity.Entity.Position, InteractionDistance)) {

                        OnEntityPedNearbyUpdate((Ped) entity.Entity, _pedNearbyTicks);

                        if (_pedNearbyTicks == 0) {
                            OnEntityPedNearby((Ped) entity.Entity);
                        }

                        _pedNearbyTicks++;

                    } else {

                        _pedNearbyTicks = 0;

                    }

                }

                if (Entity.IsInRangeOf(Main.Player.Character.Position, InteractionDistance)) {
                  
                    OnEntityPlayerNearbyUpdate(_playerNearbyTicks);

                    if (_playerNearbyTicks == 0) {
                        OnEntityPlayerNearby();
                    }

                    _playerNearbyTicks++;

                } else {

                    _playerNearbyTicks = 0;

                }

            } else if (_entity.IsDead) {

                _aliveTicks = 0;

                OnEntityDead();
                RemoveEntity();

                return;

            }

            OnEntityInitialize();

        }

        protected abstract void ApplyChanges();

        //

        protected abstract void OnEntityPedNearbyUpdate(Ped ped, int tick);

        protected abstract void OnEntityPlayerNearbyUpdate(int tick);

        protected abstract void OnEntityPedNearby(Ped ped);

        protected abstract void OnEntityPlayerNearby();

        //

        protected abstract void OnEntityPlayerKeyDown(KeyEventArgs e);

        //

        protected abstract void OnEntityInitialize();

        protected abstract void OnEntityAliveUpdate(int tick);

        //

        protected abstract void OnEntityAlive();

        protected abstract void OnEntityDead();

        public void RemoveEntity() {
            if (Entity.CurrentBlip != null)
                Entity.CurrentBlip.Remove();
            Main.ControlManager.Remove(this);
        }

    }

}
