using System;
using System.Linq;
using GTA;
using GTA.Math;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Inventory;
using GTAZ.Peds;
using GTAZ.Props;
using GTAZ.Vehicles;

namespace GTAZ.Population
{
    public class ControllablePopulator
    {
        public const int AirdropWoodenCrateModel = -1513883840;
        public const int AirdropCrateModel = -1586104172;
        public const int ItemStackBoxModel = 1388415578;

        public readonly PedHash[] ZombieModels =
        {
            PedHash.Zombie01,
            PedHash.Corpse01,
            PedHash.Corpse02
        };

        private readonly ControlManager _manager;

        private readonly int _pedCapacity;
        private readonly int _vehicleCapacity;

        private readonly float _pedDespawnRange;
        private readonly float _vehicleDespawnRange;
        private readonly float _propDespawnRange;

        public ControllablePopulator(ControlManager manager, int pedCapacity, int vehicleCapacity, float pedDespawnRange, float vehicleDespawnRange)
        {
            _manager = manager;
            _pedCapacity = pedCapacity;
            _vehicleCapacity = vehicleCapacity;
            _pedDespawnRange = pedDespawnRange;
            _vehicleDespawnRange = vehicleDespawnRange;
        }

        public void DespawnOutOfRange()
        {
            _manager.GetLivingEntities().ToList().ForEach(e =>
            {
                if (e.Keep)
                    return;

                var despawnRange = 0f;

                if (e.Entity is Ped)
                {
                    despawnRange = _pedDespawnRange;
                }
                else if (e.Entity is Vehicle)
                {
                    despawnRange = _vehicleDespawnRange;
                }
                else if (e.Entity is Prop)
                {
                    despawnRange = _propDespawnRange;
                }

                if (!(e.Entity.Position.DistanceTo(Main.Player.Character.Position) >= despawnRange))
                    return;

                _manager.Remove(e);
                e.RemoveEntity();

            });

        }

        public void SpawnItemStack(ItemStack stack, Vector3 position)
        {
            Main.ControlManager.CreateProp(new DroppedItemStackProp(Main.ControlManager.EntityCount, stack), new Model(ItemStackBoxModel), position, Vector3.WorldNorth, true, true);
        }

        public void PopulateWithAbandonedVehicle(Vector3 position, int min, int max, Random rand)
        {
            var prob = rand.Next(1, 101);
            VehicleHash model;

            if (prob <= 25)
            {
                model = VehicleHash.Tornado2;
            }
            else if (prob <= 25)
            {
                model = VehicleHash.Emperor2;
            }
            else if (prob <= 5)
            {
                model = VehicleHash.Rhapsody;
            }
            else if (prob <= 5)
            {
                model = VehicleHash.Journey;
            }
            else if (prob <= 5)
            {
                model = VehicleHash.Surfer;
            }
            else
            {
                model = VehicleHash.Ingot;
            }

            PopulateWithVehicle(new AbandonedVehicle(_manager.GetEntities().Count), model, position, min, max, rand);
        }

        public void PopulateWithVehicle(ControllableVehicle vehicle, VehicleHash model, Vector3 position, int min, int max, Random rand)
        {
            if (!(_manager.GetLivingVehicles().ToList().Count + 1 <= _vehicleCapacity && min >= 0 && max >= min))
                return;

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = position.Around(varRandom1);

            var veh = _manager.CreateVehicle(vehicle, model, varPosition1, rand.Next(0, 360));
            veh.PlaceOnNextStreet();
        }

        public void PopulateWithZombie(int index, ZombiePed zped, Vector3 position, int min, int max, Random rand)
        {
            var model = ZombieModels[index];
            PopulateWithPed(zped, model, position, min, max, rand);
        }

        public void PopulateWithRandomZombie(ZombiePed zped, Vector3 position, int min, int max, Random rand)
        {
            PopulateWithZombie(rand.Next(0, ZombieModels.Length - 1), zped, position, min, max, rand);
        }

        /*
            SET_PED_COMBAT_ATTRIBUTES
            17 - FLEE
            5 - ATTACK ON RANGE
            8 - RUN QUICKLY
            9 - RUN SLOWLY
            0 - NOTHING
        */

        public void PopulateWithPed(ControllablePed ped, PedHash model, Vector3 position, int min, int max, Random rand)
        {
            if (!(_manager.GetLivingPeds().ToList().Count + 1 <= _pedCapacity && min >= 0 && max >= min))
                return;

            var varRandom1 = rand.Next(min, max);
            var varPosition1 = position.Around(varRandom1);

            var varPed = _manager.CreatePed(ped, model, varPosition1);

            // PED::APPLY_PED_BLOOD(l_68, 3, 0f, 0f, 0f, "wound_sheet");
            // PED::APPLY_PED_DAMAGE_PACK(a_0, "HOSPITAL_0", 0.0, 1.0);

            Function.Call(Hash.APPLY_PED_DAMAGE_PACK, varPed, "HOSPITAL_0", 0.0, 1.0);

            Function.Call(Hash.SET_PED_COMBAT_RANGE, varPed, max);
            Function.Call(Hash.SET_PED_SEEING_RANGE, varPed, max / 2);
            Function.Call(Hash.SET_PED_HEARING_RANGE, varPed, max / 4);

            Function.Call(Hash.SET_PED_COMBAT_ATTRIBUTES, varPed, 26, true);
            Function.Call(Hash.SET_PED_SUFFERS_CRITICAL_HITS, varPed, true);
        }
    }
}
