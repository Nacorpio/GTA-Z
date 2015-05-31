using System;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTAZ.Assembly;
using GTAZ.Inventory;

namespace GTAZ.Controllable {

    public delegate void VehicleFeatureEventHandler(object sender, EventArgs e);
    public delegate void VehicleFeatureChangedEventHandler(bool state, object sender, EventArgs e);

    public abstract class ControllableVehicle : EntityAssembly {

        protected event VehicleFeatureChangedEventHandler HoodChanged;
        protected event VehicleFeatureChangedEventHandler TrunkChanged;

        protected event VehicleFeatureEventHandler TrunkOpened;
        protected event VehicleFeatureEventHandler TrunkClosed;

        protected event VehicleFeatureEventHandler HoodOpened;
        protected event VehicleFeatureEventHandler HoodClosed;

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

            public bool ModifyEngine;
            public bool ModifyColors;

            public bool IsEngineRunning;
            public float EngineHealth;

            public bool AttachBlip;
            public BlipColor BlipColor;

        }

        private bool _isTrunkOpen, _isHoodOpen;
        private readonly VehicleProperties _vehicleProperties;

        protected ControllableVehicle(int uid, string groupId, VehicleProperties vehicleProperties) : base(uid, groupId, 100f) {
            _vehicleProperties = vehicleProperties;
        }

        public Vehicle Vehicle {
            get { return (Vehicle) Entity; }
        }

        public void ToggleTrunk() {

            if (_isTrunkOpen) {

                Vehicle.CloseDoor(VehicleDoor.Trunk, false);
                _isTrunkOpen = false;

                if (TrunkClosed != null) TrunkClosed(this, EventArgs.Empty);
                if (TrunkChanged != null) TrunkChanged(_isTrunkOpen, this, EventArgs.Empty);

            } else {
              
                Vehicle.OpenDoor(VehicleDoor.Trunk, false, false);
                _isTrunkOpen = true;

                if (TrunkOpened != null) TrunkOpened(this, EventArgs.Empty);
                if (TrunkChanged != null) TrunkChanged(_isTrunkOpen, this, EventArgs.Empty);

            }

        }

        public void ToggleHood() {

            if (_isHoodOpen) {

                Vehicle.CloseDoor(VehicleDoor.Hood, false);
                _isHoodOpen = false;

                if (HoodClosed != null)
                    HoodClosed(this, EventArgs.Empty);
                if (HoodChanged != null)
                    HoodChanged(_isHoodOpen, this, EventArgs.Empty);

            } else {

                Vehicle.OpenDoor(VehicleDoor.Hood, false, false);
                _isHoodOpen = true;

                if (HoodOpened != null)
                    HoodOpened(this, EventArgs.Empty);
                if (HoodChanged != null)
                    HoodChanged(_isHoodOpen, this, EventArgs.Empty);

            }

        }

        protected override void InitializeAssembly() {

            // Initialize the Parts every wrapped vehicle should have in its Assembly.
            var inventory = new VehicleInventory();
            inventory.AddItem(ItemsDef.ItemExample);

            AddPart("Inventory", inventory);

        }

        protected override void ApplyChanges() {

            if (Entity == null) {
                return;
            }

            var vehicle = (Vehicle) Entity;
            InitializeAssembly();

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

        #region

        //protected override void OnEntityPedNearbyUpdate(Ped ped, int tick) {

        //}

        ////

        //protected sealed override void OnEntityPlayerKeyDown(KeyEventArgs e) {

        //    if (e == null)
        //        return;

        //    OnPlayerKeyDown(e);

        //}

        // protected abstract void OnPlayerKeyDown(KeyEventArgs e);

        // protected abstract override void OnEntityAliveUpdate(int tick);

        // protected abstract override void OnEntityInitialize();

        #endregion

    }

}
