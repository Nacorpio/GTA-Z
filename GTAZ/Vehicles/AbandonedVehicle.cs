using System;
using System.Windows.Forms;
using GTA;
using GTAZ.Controllable;
using GTAZ.Inventory;
using GTAZ.Menus;

namespace GTAZ.Vehicles
{
    public class AbandonedVehicle : ControllableVehicle
    {
        public static AbandonedVehicle VehicleLooted;

        public AbandonedVehicle(int uid) : base(uid, "ABANDONED_VEHICLE", new VehicleProperties
        {
            Teleport = false,
            AttachBlip = true,
            IsDrivable = false,

            BlipColor = BlipColor.Blue,

            ModifyEngine = false,
            ModifyColors = false
        })
        {
            _hoodMenu = new VehicleHoodMenu(this);

            PlayerKeyDown += OnPlayerKeyDown;
            Initialize += OnInitialize;

            TrunkOpened += OnTrunkOpened;
            TrunkClosed += OnTrunkClosed;

            HoodOpened += OnHoodOpened;
            HoodClosed += OnHoodClosed;
        }

        private readonly VehicleHoodMenu _hoodMenu;

        private void OnHoodClosed(object sender, EventArgs eventArgs)
        {
            _hoodMenu.Close();   
        }
       
        private void OnHoodOpened(object sender, EventArgs eventArgs)
        {
            Main.WindowManager.AddMenu(_hoodMenu);
        }

        private void OnTrunkClosed(object sender, EventArgs eventArgs)
        {
            var inventory = (VehicleInventory) GetPart("Inventory");
            inventory.CloseInventory();
        }

        private void OnTrunkOpened(object sender, EventArgs eventArgs)
        {
            VehicleLooted = this;

            var inventory = (VehicleInventory) GetPart("Inventory");
            inventory.ShowInventory();
        }

        private void OnInitialize(object sender, EventArgs eventArgs)
        {
            SetInteractionDistance(4f);
        }

        private void OnPlayerKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.E:
                    ToggleTrunk();
                    break;

                case Keys.T:
                    ToggleHood();
                    break;
            }
        }
    }
}
