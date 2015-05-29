using System.Windows.Forms;
using GTA;
using GTAZ.Controllable;
using GTAZ.Inventory;

namespace GTAZ.Vehicles {

    public class AbandonedVehicle : ControllableVehicle {

        public AbandonedVehicle(int uid) : base(uid, "ABANDONED_VEHICLE", new VehicleProperties {
            
            Teleport = false,
            AttachBlip = true, 
            IsDrivable = false,

            BlipColor = BlipColor.Blue,

            ModifyEngine = false,
            ModifyColors = false

        }) {}

        private bool _isTrunkOpen;
        private bool _isHoodOpen;

        protected override void OnPlayerKeyDown(KeyEventArgs e) {

            if (e.KeyCode == Keys.E) {
                if (_isTrunkOpen) {

                    var inventory = (VehicleInventory) GetPart("Inventory");
                    inventory.CloseInventory();

                    Vehicle.CloseDoor(VehicleDoor.Trunk, false);
                    _isTrunkOpen = false;

                } else {

                    var inventory = (VehicleInventory) GetPart("Inventory");
                    inventory.ShowInventory();

                    Vehicle.OpenDoor(VehicleDoor.Trunk, false, false);
                    _isTrunkOpen = true;

                }
            } else if (e.KeyCode == Keys.T) {
                if (_isHoodOpen) {
                    Vehicle.CloseDoor(VehicleDoor.Hood, false);
                    _isHoodOpen = false;
                } else {
                    Vehicle.OpenDoor(VehicleDoor.Hood, false, false);
                    _isHoodOpen = true;
                }
            }

        }

        protected override void OnEntityAliveUpdate(int tick) {}

        protected override void OnEntityInitialize() {
            SetInteractionDistance(3f);
        }

        protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        protected override void OnEntityPedNearby(Ped ped) {}

        protected override void OnEntityPlayerNearby() {}

        protected override void OnEntityAlive() {}

        protected override void OnEntityDead() {}

    }

}
