using System;
using System.Windows.Forms;
using GTA;
using GTAZ.Controllable;
using GTAZ.Inventory;

namespace GTAZ.Vehicles {

    public class AbandonedVehicle : ControllableVehicle {

        private bool _isTrunkOpen;
        private bool _isHoodOpen;

        public AbandonedVehicle(int uid) : base(uid, "ABANDONED_VEHICLE", new VehicleProperties {

            Teleport = false,
            AttachBlip = true,
            IsDrivable = false,

            BlipColor = BlipColor.Blue,

            ModifyEngine = false,
            ModifyColors = false

        }) {

            PlayerKeyDown += OnPlayerKeyDown;
            Initialize += OnInitialize;

            TrunkOpened += OnTrunkOpened;
            TrunkClosed += OnTrunkClosed;

        }

        private void OnTrunkClosed(object sender, EventArgs eventArgs) {

            var inventory = (VehicleInventory)GetPart("Inventory");
            inventory.CloseInventory();

            UI.Notify("The inventory was closed!");

        }

        private void OnTrunkOpened(object sender, EventArgs eventArgs) {

            var inventory = (VehicleInventory)GetPart("Inventory");
            inventory.ShowInventory();

            UI.Notify("The inventory was shown!");

        }

        private void OnInitialize(object sender, EventArgs eventArgs) {
            SetInteractionDistance(4f);
        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs e) {

            switch (e.KeyCode) {
                
                case Keys.E:

                    ToggleTrunk();
                    break;

                case Keys.T:

                    ToggleHood();
                    break;

            }

        }

        #region "Commented"

        //protected override void OnEntityUpsideDown() {}

        //protected override void OnEntityUpsideDownUpdate(int tick) {}

        //protected override void OnEntityInAir() {}

        //protected override void OnEntityInAirUpdate(int tick) {}

        //protected override void OnEntityInWater() {}

        //protected override void OnEntityInWaterUpdate(int tick) {}

        //protected override void OnEntityAttached() {}

        //protected override void OnEntityAttachedUpdate(int tick) {}

        //protected override void OnEntityPlayerIsTouching() {}

        //protected override void OnEntityPlayerIsTouchingUpdate(int tick) {}

        //protected override void OnPlayerKeyDown(KeyEventArgs e) {

        //    if (e.KeyCode == Keys.E) {

        //        if (_isTrunkOpen) {

        //            var inventory = (VehicleInventory) GetPart("Inventory");
        //            inventory.CloseInventory();

        //            Vehicle.CloseDoor(VehicleDoor.Trunk, false);
        //            _isTrunkOpen = false;

        //        } else {

        //            var inventory = (VehicleInventory) GetPart("Inventory");
        //            inventory.ShowInventory();

        //            Vehicle.OpenDoor(VehicleDoor.Trunk, false, false);
        //            _isTrunkOpen = true;

        //        }

        //    } else if (e.KeyCode == Keys.T) {

        //        if (_isHoodOpen) {

        //            Vehicle.CloseDoor(VehicleDoor.Hood, false);
        //            _isHoodOpen = false;

        //        } else {

        //            Vehicle.OpenDoor(VehicleDoor.Hood, false, false);
        //            _isHoodOpen = true;

        //        }

        //    }

        //}

        //protected override void OnEntityAliveUpdate(int tick) {}

        //protected override void OnEntityInitialize() {
        //    SetInteractionDistance(4f);
        //}

        //protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        //protected override void OnEntityPedNearby(Ped ped) {}

        //protected override void OnEntityPlayerNearby() {
        //    UI.Notify("You are close to a vehicle!");
        //}

        //protected override void OnEntityAlive() {}

        //protected override void OnEntityDead() {}

        #endregion

    }

}
