using System;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ.Population {

    public class ControllablePopulator {

        private readonly ControlManager _manager;
        private readonly int _count;
        private readonly float _despawnRange;

        public ControllablePopulator(ControlManager manager, int count, float despawnRange) {
            _manager = manager;
            _count = count;
            _despawnRange = despawnRange;
        }

        public void DespawnPeds() {

            _manager.LivingEntities.ToList().ForEach(e => {

                if (!(e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= _despawnRange)) {
                    return;
                }

                _manager.Remove(e);
                e.RemoveEntity();

            });

        }

        public void PopulatePeds(ControllablePed ped, PedHash model, Vector3 around, int min, int max, Random rand) {

            if (!(_manager.Count + 1 <= _count && min >= 0 && max >= min)) {
                return;
            }

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = around.Around(varRandom1);

            _manager.CreatePed(ped, model, varPosition1);

        }

    }

}
