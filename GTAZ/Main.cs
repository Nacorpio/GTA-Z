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

        public static int PLAYER_GROUP;
        public static int ENEMY_GROUP;

        public static List<ControllablePed> peds = new List<ControllablePed>();
        public static Player player;

        public Main() {
            
            Tick += OnTick;
            KeyDown += OnKeyDown;

            player = Game.Player;

            PLAYER_GROUP = World.AddRelationShipGroup("PLAYER");
            ENEMY_GROUP = World.AddRelationShipGroup("ENEMY");

            World.SetRelationshipBetweenGroups(Relationship.Hate, PLAYER_GROUP, ENEMY_GROUP);

            player.Character.RelationshipGroup = PLAYER_GROUP;

        }

        private static void OnKeyDown(object sender, KeyEventArgs keyEventArgs) {

            var tped = new TeamPed(peds.Count, "z" + peds.Count);
            var zped = new ZombiePed(peds.Count, "z" + peds.Count);

            Ped p = null;

            switch (keyEventArgs.KeyCode) {

                case Keys.F5:

                    p = World.CreatePed(PedHash.Swat01SMY, player.Character.Position);
                    tped.Control(p);
                    break;

                case Keys.F7:

                    p = World.CreatePed(PedHash.Zombie01, player.Character.Position.Around(25f));
                    zped.Control(p);
                    break;

            }

            peds.Add(tped);
            peds.Add(zped);

        }

        private static void OnTick(object sender, EventArgs eventArgs) {
            peds.ForEach(p => { p.OnTick(); });
        }
    }

}
