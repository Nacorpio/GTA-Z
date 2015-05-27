using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Native;
using GTAZ.Controllable;

namespace GTAZ
{

    public class Main : Script {

        public static int PlayerGroup;
        public static int EnemyGroup;

        private readonly static CPedManager PedManager = new CPedManager();
        public static Player player;

        public Main() {
            
            Tick += OnTick;
            KeyDown += OnKeyDown;

            player = Game.Player;

            PlayerGroup = World.AddRelationShipGroup("PLAYER");
            EnemyGroup = World.AddRelationShipGroup("ENEMY");

            World.SetRelationshipBetweenGroups(Relationship.Dislike, PlayerGroup, EnemyGroup);
            player.Character.RelationshipGroup = PlayerGroup;

            Interval = 1;

        }

        private static void OnKeyDown(object sender, KeyEventArgs keyEventArgs) {

            switch (keyEventArgs.KeyCode) {

                case Keys.F5:

                    PedManager.Create(new TeamPed(PedManager.Count), PedHash.Swat01SMY, player.Character.Position);
                    break;

                case Keys.F7:

                    PedManager.Create(new ZombiePed(PedManager.Count), PedHash.Zombie01, player.Character.Position.Around(5f));
                    break;

            }

        }

        private static void OnTick(object sender, EventArgs eventArgs) {

            Function.Call(Hash.SET_PARKED_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_RANDOM_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_SCENARIO_PED_DENSITY_MULTIPLIER_THIS_FRAME, 0f);
            Function.Call(Hash.SET_VEHICLE_DENSITY_MULTIPLIER_THIS_FRAME, 0f);

            PedManager.Tick();

        }
    }

}
