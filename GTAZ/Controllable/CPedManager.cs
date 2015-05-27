
using System.Collections.Generic;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;

namespace GTAZ.Controllable {

    public class CPedManager {

        private List<ControllablePed> elements;

        public CPedManager() {
            elements = new List<ControllablePed>();
        }

        public CPedManager(int capacity) {
            elements = new List<ControllablePed>(capacity);
        }

        public CPedManager Create(ControllablePed cped, PedHash model, Vector3 position) {

            var var1 = World.CreatePed(model, position);
            var var2 = cped.Control(var1);

            if (var2 == null) {
                return null;
            }

            return Add(var2);

        }

        public bool Contains(ControllablePed cped) {
            return Contains(cped.Ped) && Contains(cped.StringId) && Contains(cped.UniqueId);
        }

        public bool Contains(Ped ped) {
            return elements.Where(e => e.Ped == ped).ToArray().Length == 1;
        }

        public bool Contains(string sid) {
            return elements.Where(e => e.StringId == sid).ToArray().Length >= 1;
        }

        public bool Contains(int uid) {
            return elements.Where(e => e.UniqueId == uid).ToArray().Length == 1;
        }

        public IEnumerable<ControllablePed> Get(string sid) {
            return elements.Where(e => e.StringId == sid);
        }

        public ControllablePed Get(int uid) {
            return elements.Where(e => e.UniqueId == uid).ToArray()[0];
        }

        public ControllablePed Get(Ped ped) {
            return elements.Where(e => e.Ped == ped).ToArray()[0];
        }

        public IEnumerable<ControllablePed> ActivePeds {
            get { return elements.Where(e => e.IsActive); }
        }

        public IEnumerable<ControllablePed> InactivePeds {
            get { return elements.Where(e => !e.IsActive); }
        }

        public IEnumerable<ControllablePed> LivingPeds {
            get { return elements.Where(e => e.IsActive && e.Ped.IsAlive); }
        }

        public IEnumerable<ControllablePed> DeadPeds {
            get { return elements.Where(e => e.IsActive && e.Ped.IsDead); }
        }

        public CPedManager KillAllPeds() {
            elements.ForEach(e => {
                if (e.IsActive && e.Ped.IsAlive) e.Ped.Kill();
            });
            return this;
        }

        public CPedManager Add(ControllablePed ped) {
            if (!Contains(ped)) {
                elements.Add(ped);
            }
            return this;
        }

        public int Count {
            get { return elements.Count; }
        }

        public void Tick() {
            elements.ForEach(e => e.OnTick());
        }

    }

}
