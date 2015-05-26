using System.Collections.Generic;
using System.Linq;
using GTA;

namespace GTAZ.Controllable {

    public class ZombieCollection {

        private readonly List<ZombiePed> _zombies = new List<ZombiePed>(); 

        public ZombieCollection(params ZombiePed[] zpeds) {
            zpeds.ToList().ForEach(z => _zombies.Add(z));
        }

        public ZombieCollection() {}

        public ZombieCollection Add(params ZombiePed[] zped) {
            foreach (var ped in zped) {
                if (!Contains(ped))
                    _zombies.Add(ped);
            }
            return this;
        }

        public ZombiePed GetIndex(int index) {
            return _zombies[index];
        }

        public ZombiePed Get(Ped ped) {
            return _zombies.Where(z => z.Ped == ped).ToArray()[0];
        }

        public ZombiePed Get(string sid) {
            return _zombies.Where(z => z.StringId == sid).ToArray()[0];
        }

        public ZombiePed Get(int uid) {
            return _zombies.Where(z => z.UniqueId == uid).ToArray()[0];
        }

        public bool Contains(params ZombiePed[] zped) {
            return zped.Where(z => Zombies().Contains(z)).ToArray().Length > 0;
        }

        public bool Contains(Ped ped) {
            return _zombies.Where(z => z.Ped == ped).ToArray().Length > 0;
        }

        public bool Contains(string sid) {
            return _zombies.Where(z => z.StringId == sid).ToArray().Length > 0;
        }

        public bool Contains(int uid) {
            return _zombies.Where(z => z.UniqueId == uid).ToString().Length > 0;
        }

        public List<ZombiePed> Zombies() {
            return _zombies;
        } 

        public int Count {
            get { return _zombies.Count; }
        }

    }

}
