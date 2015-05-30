using GTA;

namespace GTAZ {

    public static class PlayerStats {

        private static int _zombiePoints = 0;
        private static int _points = 0;

        public static void AddPoint() {
            _points++;
            OnPointsAdded();
        }

        public static void AddPoints(int points) {
            for (var i = 0; i < points; i++) AddPoint();
        }

        public static void AddZombiePoint() {
            _zombiePoints++;
            OnZombiePointsAdded();
        }

        public static void AddZombiePoints(int points) {
            for (var i = 0; i < points; i++) AddZombiePoint();
        }

        public static int GetPoints() {
            return _points;
        }

        public static int GetZombiePoints() {
            return _zombiePoints;
        }

        private static void OnPointsAdded() {
            
        }

        private static void OnZombiePointsAdded() {
           
        }

    }

}
