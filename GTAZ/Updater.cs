using System;
using System.Collections.Generic;
using GTA;

namespace GTAV_purge_mod {

    public abstract class Updater {

        private bool _active = true;

        private int _activeTick;
        private int _inactiveTick;

        private readonly int _startTick;
        private int _tick;

        private Dictionary<int, Action> _actionQueue = new Dictionary<int, Action>(); 

        protected Updater(int startTick = 0) {
            _tick = startTick;
            _startTick = startTick;
        }

        public void OnTick() {

            OnUpdate(_tick);

            if (_actionQueue.ContainsKey(_tick)) {
                _actionQueue[_tick].DynamicInvoke();
            }

            if (_tick == 0 || _tick == _startTick) {
                
                OnFirstUpdate();

            }

            if (_active) {

                _inactiveTick = 0;

                OnActiveUpdate(_activeTick, _tick);

                if (_activeTick == 0) {
                    OnFirstActiveUpdate(_tick);
                }

                _activeTick++;

            } 
            
            if (!_active) {

                _activeTick = 0;

                OnInactiveUpdate(_inactiveTick, _tick);

                if (_inactiveTick == 0) {
                    OnFirstInactiveUpdate(_tick);
                }

                _inactiveTick++;

            }

            _tick++;

        }

        /// <summary>
        /// Fired everytime a tick has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected abstract void OnUpdate(int tick);

        /// <summary>
        /// Fired on the absolute first update.
        /// </summary>
        protected void OnFirstUpdate() {}

        /// <summary>
        /// Fired everytime a tick has been made while active.
        /// </summary>
        /// <param name="activeTick">The current tick on this activity state.</param>
        /// <param name="tick">The total amount of ticks.</param>
        protected abstract void OnActiveUpdate(int activeTick, int tick);

        /// <summary>
        /// Fired everytime a tick has been made while inactive.
        /// </summary>
        /// <param name="inactiveTick">The current tick on this activity state.</param>
        /// <param name="tick">The total amount of ticks.</param>
        protected void OnInactiveUpdate(int inactiveTick, int tick) {}

        /// <summary>
        /// Fired the first time an active update has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected void OnFirstActiveUpdate(int tick) {}

        /// <summary>
        /// Fired the first time an inactive update has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected void OnFirstInactiveUpdate(int tick) {}

        public Dictionary<int, Action> ActionQueue { get { return _actionQueue; } set { _actionQueue = value; }} 

        /// <summary>
        /// Returns the tick of which the ticking started.
        /// This isn't always 0, as the StartTick can be set.
        /// </summary>
        public int StartTick { get { return _startTick; }}

        /// <summary>
        /// Returns how many times this Updating instance has ticked.
        /// </summary>
        public int Tick { get { return _tick; } }

        /// <summary>
        /// If active, returns how many times it has been ticked during its session.
        /// </summary>
        public int ActiveTicks { get { return _activeTick; }}

        /// <summary>
        /// If inactive, returns how many times it has been ticked during its session.
        /// </summary>
        public int InactiveTicks { get { return _inactiveTick; }}

        /// <summary>
        /// Returns how many seconds this updater has been alive.
        /// </summary>
        public int SecondsAlive { get { return _tick/1000; }}

        /// <summary>
        /// Returns whether this Updating instance is active.
        /// </summary>
        public bool IsActive {
            get { return _active; }
            protected set { _active = value; }
        }

    }

}
