using System.Windows.Forms;
using GTA;
using GTA.Math;

namespace GTAZ.Controllable {

    public abstract class ControllableVehicle : ControllableEntity {

        protected struct VehicleProperties {

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
            public bool IsEngineRunning;
            public float EngineHealth;

            public bool AttachBlip;
            public BlipColor BlipColor;

        }

        private readonly VehicleProperties _vehicleProperties;

        protected ControllableVehicle(int uid, string groupId, VehicleProperties vehicleProperties) : base(uid, groupId) {
            _vehicleProperties = vehicleProperties;
        }

        public Vehicle Vehicle {
            get { return (Vehicle) Entity; }
        }

        protected override void ApplyChanges() {

            if (Entity == null) {
                return;
            }

            var vehicle = (Vehicle) Entity;

            if (_vehicleProperties.Teleport) {
                vehicle.Position = new Vector3(_vehicleProperties.X, _vehicleProperties.Y, _vehicleProperties.Z);
            }

            vehicle.NumberPlate = _vehicleProperties.NumberPlateText;

            vehicle.PrimaryColor = _vehicleProperties.PrimaryColor;
            vehicle.SecondaryColor = _vehicleProperties.SecondaryColor;
            vehicle.WindowTint = _vehicleProperties.WindowTint;

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
            vehicle.EngineHealth = _vehicleProperties.EngineHealth;
            vehicle.EngineRunning = _vehicleProperties.IsEngineRunning;

            if (_vehicleProperties.AttachBlip) {
                vehicle.AddBlip().Color = _vehicleProperties.BlipColor;
            }

        }

        protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {
            throw new System.NotImplementedException();
        }

        protected override void OnEntityDeadUpdate(int tick) {

            var vehicle = (Vehicle)Entity;

            if (_vehicleProperties.AttachBlip && vehicle.CurrentBlip != null) {
                vehicle.CurrentBlip.Remove();
            }

        }

        //

        protected override void OnEntityPlayerKeyDown(KeyEventArgs e) {

            if (e == null)
                return;

            OnPlayerKeyDown(e);

        }

        protected abstract void OnPlayerKeyDown(KeyEventArgs e);

        protected abstract override void OnEntityAliveUpdate(int tick);

        protected abstract override void OnEntityInitialize();

    }

}
