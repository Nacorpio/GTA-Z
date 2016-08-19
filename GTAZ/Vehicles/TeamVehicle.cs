using GTA;
using GTAZ.Controllable;

namespace GTAZ.Vehicles {

    public class TeamVehicle : ControllableVehicle
    {
        public TeamVehicle(int uid) : base(uid, "TEAM_VEHICLE", new VehicleProperties
        {
            Teleport = false,
            AttachBlip = true,

            BlipColor = BlipColor.Blue,
            IsDrivable = true,

            PrimaryColor = VehicleColor.MetallicBlack,
            SecondaryColor = VehicleColor.MetallicBlack,

            NumberPlateText = "",
            WindowTint = VehicleWindowTint.PureBlack,
            IsEngineRunning = true,
            SmashWindows = false,
            EngineHealth = 100,
            BurstTires = false
        })
        { }
    }
}
