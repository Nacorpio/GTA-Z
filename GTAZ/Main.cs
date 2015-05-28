using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;
using GTAZ.Population;
using mlgthatsme;

namespace GTAZ
{

    public class Main : Script {

        public static int PlayerGroup;
        public static int EnemyGroup;

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

            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, EnemyGroup);
            Player.Character.RelationshipGroup = PlayerGroup;

            _populator = new ControllablePopulator(ControlManager, 20);

            Interval = 1;

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
            _populator.Tick();

        }
    }

}
