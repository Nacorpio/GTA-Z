using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTAZ.Assembly;

namespace GTAZ.Controllable {

    public abstract class ControllableVehicle : EntityAssembly {

        public struct VehicleProperties {

            public bool Teleport;
            public int X, Y, Z;

            public string NumberPlateText;

            public VehicleColor PrimaryColor, SecondaryColor;
            public VehicleWindowTint WindowTint;

            public bool SmashWindows;
            public VehicleWindow[] WindowsToSmash;

            public bool BurstTires;
            public int[] TiresToBurst;

            public bool IsDrivable;

            public bool ModifyEngine;
            public bool ModifyColors;

            public bool IsEngineRunning;
            public float EngineHealth;

            public bool AttachBlip;
            public BlipColor BlipColor;

        }

        private readonly VehicleProperties _vehicleProperties;

        protected ControllableVehicle(int uid, string groupId, VehicleProperties vehicleProperties) : base(uid, groupId, 100f) {
            _vehicleProperties = vehicleProperties;
        }

        public Vehicle Vehicle {
            get { return (Vehicle) Entity; }
        }

        //

        protected override void InitializeAssembly() {

            // Initialize the Parts every wrapped vehicle should have in its Assembly.
            AddPart("Battery", null);
            AddPart("Inventory", null);

        }

        protected override void ApplyChanges() {

            if (Entity == null) {
                return;
            }

            var vehicle = (Vehicle) Entity;

            if (_vehicleProperties.Teleport) {
                vehicle.Position = new Vector3(_vehicleProperties.X, _vehicleProperties.Y, _vehicleProperties.Z);
            }

            if (!string.IsNullOrWhiteSpace(_vehicleProperties.NumberPlateText)) {
                vehicle.NumberPlate = _vehicleProperties.NumberPlateText;
            }

            if (_vehicleProperties.ModifyColors) {
                vehicle.PrimaryColor = _vehicleProperties.PrimaryColor;
                vehicle.SecondaryColor = _vehicleProperties.SecondaryColor;
                vehicle.WindowTint = _vehicleProperties.WindowTint;
            }

            if (_vehicleProperties.SmashWindows) {
                foreach (var window in _vehicleProperties.WindowsToSmash) {
                    vehicle.SmashWindow(window);
                }
            }

            if (_vehicleProperties.BurstTires) {
                foreach (var tire in _vehicleProperties.TiresToBurst) {
                    vehicle.BurstTire(tire);
                }
            }

            vehicle.IsDriveable = _vehicleProperties.IsDrivable;

            if (_vehicleProperties.ModifyEngine) {
                vehicle.EngineHealth = _vehicleProperties.EngineHealth;
                vehicle.EngineRunning = _vehicleProperties.IsEngineRunning;
            }

            if (_vehicleProperties.AttachBlip) {
                vehicle.AddBlip().Color = _vehicleProperties.BlipColor;
            }

        }

        protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {

        }

        //

        protected sealed override void OnEntityPlayerKeyDown(KeyEventArgs e) {

            if (e == null)
                return;

            OnPlayerKeyDown(e);

        }

        protected abstract void OnPlayerKeyDown(KeyEventArgs e);

        protected abstract override void OnEntityAliveUpdate(int tick);

        protected abstract override void OnEntityInitialize();

    }

}
