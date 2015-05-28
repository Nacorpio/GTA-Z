using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAZ.Controllable {

    public class ControlManager {

        private List<ControllableEntity> elements;

        public ControlManager() {
            elements = new List<ControllableEntity>();
        }

        public ControlManager(int capacity) {
            elements = new List<ControllableEntity>(capacity);
        }

        public ControlManager CreatePed(ControllablePed cped, PedHash model, Vector3 position) {

            var var1 = World.CreatePed(model, position);
            var var2 = cped.Control(var1);

            if (var2 == null) {
                return null;
            }

            return Add(var2);

        }

        public ControlManager CreateVehicle(ControllableVehicle cveh, VehicleHash model, Vector3 position) {

            var var1 = World.CreateVehicle(model, position);
            var var2 = cveh.Control(var1);

            if (var2 == null) {
                return null;
            }

            return Add(var2);

        }

        public ControlManager KeyDown(KeyEventArgs e) {
            Entities.ForEach(entity => entity.KeyDown(e));
            return this;
        }

        public bool Contains(ControllableEntity cped) {
            return Contains(cped.Entity) && Contains(cped.GroupId) && Contains(cped.UniqueId);
        }

        public bool Contains(Entity entity) {
            return elements.Any(e => e.Entity == entity);
        }

        public bool Contains(string groupId) {
            return elements.Any(e => e.GroupId == groupId);
        }

        public bool Contains(int uid) {
            return elements.Any(e => e.UniqueId == uid);
        }

        public IEnumerable<ControllableEntity> Get(string groupId) {
            return elements.Where(e => e.GroupId == groupId);
        }

        public ControllableEntity Get(int uid) {
            return elements.Where(e => e.UniqueId == uid).ToArray()[0];
        }

        public IEnumerable<ControllableEntity> ActiveEntities {
            get { return elements.Where(e => e.IsActive); }
        }

        public IEnumerable<ControllableEntity> InactiveEntities {
            get { return elements.Where(e => !e.IsActive); }
        }

        public IEnumerable<ControllableEntity> LivingEntities {
            get { return elements.Where(e => e.IsActive && e.Entity.IsAlive); }
        }

        public IEnumerable<ControllableEntity> DeadEntities {
            get { return elements.Where(e => e.IsActive && e.Entity.IsDead); }
        }

        public IEnumerable<ControllableEntity> LivingPeds {
            get {
                return elements.Where(e => e is ControllablePed);
            }
        }

        public IEnumerable<ControllableEntity> LivingVehicles {
            get {
                return elements.Where(e => e is ControllableVehicle);
            }
        }

        public ControlManager Add(ControllableEntity entity) {
            if (!Contains(entity))
                elements.Add(entity);
            return this;
        }

        public ControlManager Remove(ControllableEntity entity) {
            if (Contains(entity))
                elements.Remove(entity);
            return this;
        }

        public ControlManager Remove(int uid) {
            if (Contains(uid))
                elements.Remove(elements.Where(e => e.UniqueId == uid).ToArray()[0]);
            return this;
        }

        public bool AliveForMilliseconds(int uid, float milliseconds) {
            return elements.Where(e => e.UniqueId == uid).ToArray()[0].SecondsAlive >= milliseconds;
        }

        public bool AliveForSeconds(int uid, float seconds) {
            return AliveForMilliseconds(uid, seconds * 1000);
        }

        public IEnumerable<ControllablePed> Peds {
            get { return (IEnumerable<ControllablePed>) Entities.Where(e => e is ControllablePed); }
        }

        public IEnumerable<ControllableVehicle> Vehicles {
            get { return (IEnumerable<ControllableVehicle>) Entities.Where(e => e is ControllableVehicle); }
        } 

        public List<ControllableEntity> Entities {
            get { return elements; }
        } 

        public int Count {
            get { return Entities.Count; }
        }
        
        public void Tick() {
            Entities.ForEach(e => e.OnTick());
        }

    }

}
