
using GTA;

namespace GTAZ {

    public static class Achievements {

        public abstract class Achievement {

            private readonly int _pointsNeeded;
            private readonly int _zombiePointsNeeded;

            public Achievement(int points, int zpoints) {
                _pointsNeeded = points;
                _zombiePointsNeeded = zpoints;
            }

            public abstract void OnAchievementComplete();

            public bool IsComplete(int points, int zpoints) {
                return points == _pointsNeeded || zpoints == _zombiePointsNeeded;
            }

            public int GetPointsNeeded() {
                return _pointsNeeded;
            }

            public int GetZombiePointsNeeded() {
                return _zombiePointsNeeded;
            }

        }

        public class AchievementFirstTenZombieKills : Achievement {

            public AchievementFirstTenZombieKills() : base(0, 10) {}

            public void Complete(int zpoints) {
                if (IsComplete(0, zpoints))
                    OnAchievementComplete();
            }
 
            public override void OnAchievementComplete() {
                UI.Notify("Completed Achievement:\nYou have now killed 10 zombies.");
            }

        }


    }

}
