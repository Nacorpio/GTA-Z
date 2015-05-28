using System.Windows.Forms;
using GTA;
using GTAZ.Controllable;

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

        protected override void OnPlayerKeyDown(KeyEventArgs e) {}

        protected override void OnEntityAliveUpdate(int tick) {}

        protected override void OnEntityInitialize() {}

        protected override void OnEntityPlayerNearbyUpdate(int tick) {}

        protected override void OnEntityPedNearby(Ped ped) {}

        protected override void OnEntityPlayerNearby() {}

        protected override void OnEntityAlive() {}

        protected override void OnEntityDead() {}

    }

}
