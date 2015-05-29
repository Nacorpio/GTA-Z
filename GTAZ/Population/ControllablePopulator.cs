using System;
using System.IO;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Peds;
using GTAZ.Vehicles;

namespace GTAZ.Population {

    public class ControllablePopulator {

        public readonly PedHash[] ZombieModels = new [] {
                PedHash.Zombie01
        };

        private readonly ControlManager _manager;

        private readonly int _pedCapacity;
        private readonly int _vehicleCapacity;

        private readonly float _pedDespawnRange;
        private readonly float _vehicleDespawnRange;

        public ControllablePopulator(ControlManager manager, int pedCapacity, int vehicleCapacity, float pedDespawnRange, float vehicleDespawnRange) {
            _manager = manager;
            _pedCapacity = pedCapacity;
            _vehicleCapacity = vehicleCapacity;
            _pedDespawnRange = pedDespawnRange;
            _vehicleDespawnRange = vehicleDespawnRange;
        }

        public void DespawnOutOfRange() {
            
            _manager.LivingEntities.ToList().ForEach(e => {

                if (e.Keep) {
                    // The entity is going to be kept.
                    return;
                } 

                float despawnRange;

                if (e.Entity is Ped) {
                    despawnRange = _pedDespawnRange;
                } else if (e.Entity is Vehicle) {
                    despawnRange = _vehicleDespawnRange;
                }

                if (!(e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= despawnRange)) {
                    return;
                }

                _manager.Remove(e);
                e.RemoveEntity();

            });

        }

        public void DespawnPeds() {

            _manager.LivingPeds.ToList().ForEach(e => {

                if (e.Keep) {
                    return;
                }

                if (!(e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= _pedDespawnRange)) {
                    return;
                }

                _manager.Remove(e);
                e.RemoveEntity();

            });

        }

        public void DespawnVehicles() {

            _manager.LivingVehicles.ToList().ForEach(e => {

                if (e.Keep) {
                    return;
                }

                if (!(e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= _vehicleDespawnRange)) {
                    return;
                }

                _manager.Remove(e);
                e.RemoveEntity();

            });

        }

        public void PopulateWithAbandonedVehicle(Vector3 position, int min, int max, Random rand) {

            var prob = rand.Next(1, 101);
            VehicleHash model;

            if (prob <= 25) {
                model = VehicleHash.Tornado2;
            } else if (prob <= 25) {
                model = VehicleHash.Emperor2;
            } else if (prob <= 5) {
                model = VehicleHash.Rhapsody;
            } else if (prob <= 5) {
                model = VehicleHash.Journey;
            } else if (prob <= 5) {
                model = VehicleHash.Surfer;
            } else {
                model = VehicleHash.Ingot;
            }

            PopulateWithVehicle(new AbandonedVehicle(_manager.LivingVehicles.ToList().Count), model, position, min, max, rand);

        }

        

        public void PopulateWithVehicle(ControllableVehicle vehicle, VehicleHash model, Vector3 position, int min, int max, Random rand) {

            if (!(_manager.LivingVehicles.ToList().Count + 1 <= _vehicleCapacity && min >= 0 && max >= min)) {
                return;
            }

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = position.Around(varRandom1);

            var veh = _manager.CreateVehicle(vehicle, model, varPosition1, rand.Next(0, 360));
            veh.PlaceOnNextStreet();

        }

        public void PopulateWithZombie(int index, ZombiePed zped, Vector3 position, int min, int max, Random rand) {
            var model = ZombieModels[index];
            PopulateWithPed(zped, model, position, min, max, rand);
        }

        public void PopulateWithRandomZombie(ZombiePed zped, Vector3 position, int min, int max, Random rand) {
            PopulateWithZombie(rand.Next(0, ZombieModels.Length - 1), zped, position, min, max, rand);
        }

        public void PopulateWithPed(ControllablePed ped, PedHash model, Vector3 position, int min, int max, Random rand) {

            if (!(_manager.LivingPeds.ToList().Count + 1 <= _pedCapacity && min >= 0 && max >= min)) {
                return;
            }

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = position.Around(varRandom1);

            _manager.CreatePed(ped, model, varPosition1);

        }

    }

}
