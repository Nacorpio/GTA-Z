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

        public ControllablePopulator(ControlManager manager, int count) {
            _manager = manager;
            _count = count;
        }

        public void Tick() {
            PopulateZombies(Main.Player.Character.Position, 50, 300, new Random(Game.GameTime));
            DepopulateZombies();
        }

        private void DepopulateZombies() {

            _manager.LivingEntities.ToList().ForEach(e => {

                if (e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= 350) {
                    _manager.Remove(e);
                    e.RemoveEntity();
                }

            });

        }

        private void PopulateZombies(Vector3 around, int min, int max, Random rand) {

            if (!(_manager.Count + 1 <= _count && min >= 0 && max >= min)) {
                return;
            }

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = around.Around(varRandom1);

            _manager.CreatePed(new ZombiePed(_manager.Count), PedHash.Zombie01, varPosition1);

        }

    }

}
