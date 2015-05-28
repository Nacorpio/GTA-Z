using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Peds;
using GTAZ.Population;
using mlgthatsme;

namespace GTAZ
{

    public class Main : Script {

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

            PlayerGroup = World.AddRelationShipGroup("PLAYER");
            EnemyGroup = World.AddRelationShipGroup("ENEMY");
            ZombieGroup = World.AddRelationShipGroup("ZOMBIE");

            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, EnemyGroup);
            World.SetRelationshipBetweenGroups(Relationship.Dislike, ZombieGroup, EnemyGroup);
            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, ZombieGroup);

            Player.Character.RelationshipGroup = PlayerGroup;

            _populator = new ControllablePopulator(ControlManager, 8, 3, 200f, 350f);

            Interval = 1;

        }

        public static void Log(object msg) {
            File.AppendAllText("log.txt", msg + Environment.NewLine);
        }

        private static void OnKeyDown(object sender, KeyEventArgs keyEventArgs) {

            ControlManager.KeyDown(keyEventArgs);

            switch (keyEventArgs.KeyCode) {

                case Keys.F5:
                    break;

            }

        }

        private static void OnTick(object sender, EventArgs eventArgs) {

            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);

            ControlManager.Tick();

            // _populator.PopulateWithPed(new ZombiePed(ControlManager.LivingPeds.ToList().Count), PedHash.Zombie01, Player.Character.Position, 25, 150, new Random(Game.GameTime));
            _populator.PopulateWithRandomZombie(new ZombiePed(ControlManager.LivingPeds.ToList().Count), Player.Character.Position, 25, 150, new Random(Game.GameTime));
            _populator.DespawnPeds();

            _populator.PopulateWithAbandonedVehicle(Player.Character.Position, 50, 300, new Random(Game.GameTime));
            _populator.DespawnVehicles();

        }
    }

}
