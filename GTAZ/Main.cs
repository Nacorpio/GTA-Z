using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Inventory;
using GTAZ.Menus;
using GTAZ.Peds;
using GTAZ.Population;
using mlgthatsme.GUI;

namespace GTAZ
{

    public class Main : Script {

        public static PlayerInventory PlayerInventory = new PlayerInventory();

        public static WindowManager WindowManager;
        public static bool IsToggled;

        public static int PlayerGroup;
        public static int EnemyGroup;
        public static int ZombieGroup;

        public readonly static ControlManager ControlManager = new ControlManager();
        private static ControllablePopulator _populator;

        public static Viewport Viewport;
        public static Player Player;

        public Main() {
            
            Tick += OnTick;
            KeyDown += OnKeyDown;

            Viewport = View;
            Player = Game.Player;

            WindowManager = new WindowManager();

            UpdateRelationships();
            _populator = new ControllablePopulator(ControlManager, 8, 3, 200f, 350f);

            Interval = 1;

        }

        public static void Toggle() {

            IsToggled = !IsToggled;
            if (!IsToggled) {
                ControlManager.RemoveAndDeleteAll();
            }

        }

        private static void UpdateRelationships() {

            Player.Character.RelationshipGroup = PlayerGroup;

            PlayerGroup = World.AddRelationShipGroup("PLAYER");
            EnemyGroup = World.AddRelationShipGroup("ENEMY");
            ZombieGroup = World.AddRelationShipGroup("ZOMBIE");

            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, EnemyGroup);
            World.SetRelationshipBetweenGroups(Relationship.Dislike, ZombieGroup, EnemyGroup);
            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, ZombieGroup);

        }

        private static void PopulateWorld() {

            if (IsToggled) {

                // _populator.PopulateWithPed(new ZombiePed(ControlManager.LivingPeds.ToList().Count), PedHash.Zombie01, Player.Character.Position, 25, 150, new Random(Game.GameTime));
                _populator.PopulateWithRandomZombie(new ZombiePed(ControlManager.Entities.ToList().Count), Player.Character.Position, 25, 150, new Random(Game.GameTime));
                _populator.PopulateWithAbandonedVehicle(Player.Character.Position, 50, 300, new Random(Game.GameTime));

                // Despawns all the entities that are out of its range.
                _populator.DespawnOutOfRange();

            }

        }

        public static void Log(object msg) {
            File.AppendAllText("log.txt", msg + Environment.NewLine);
        }

        private static void OnKeyDown(object sender, KeyEventArgs keyEventArgs) {

            ControlManager.KeyDown(keyEventArgs);
            WindowManager.KeyDown(sender, keyEventArgs);

            if (WindowManager.MenuList.Count == 0) {

                switch (keyEventArgs.KeyCode) {
                    case Keys.F7:
                        WindowManager.AddMenu(new PlayerMainMenu());
                        break;
                }

            }

        }

        private static void OnTick(object sender, EventArgs eventArgs) {

            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);

            ControlManager.Tick();
            WindowManager.OnTick(sender, eventArgs);

            PopulateWorld();

        }

    }

}
