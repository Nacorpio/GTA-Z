using System;
using System.Collections.Generic;

namespace GTAZ
{
    public abstract class Updater
    {
        protected Updater(int startTick = 0)
        {
            Tick = startTick;
            StartTick = startTick;
        }

        public void OnTick()
        {
            OnUpdate(Tick);

            if (ActionQueue.ContainsKey(Tick))
                ActionQueue[Tick].DynamicInvoke();

            if (Tick == 0 || Tick == StartTick)
                OnFirstUpdate();

            if (IsActive)
            {
                InactiveTicks = 0;

                OnActiveUpdate(ActiveTicks, Tick);

                if (ActiveTicks == 0)
                    OnFirstActiveUpdate(Tick);

                ActiveTicks++;
            } 
            
            if (!IsActive)
            {
                ActiveTicks = 0;

                OnInactiveUpdate(InactiveTicks, Tick);

                if (InactiveTicks == 0)
                    OnFirstInactiveUpdate(Tick);

                InactiveTicks++;
            }

            Tick++;
        }

        /// <summary>
        /// Fired every time a tick has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected abstract void OnUpdate(int tick);

        /// <summary>
        /// Fired on the absolute first update.
        /// </summary>
        protected void OnFirstUpdate()
        { }

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
        protected void OnInactiveUpdate(int inactiveTick, int tick)
        { }

        /// <summary>
        /// Fired the first time an active update has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected void OnFirstActiveUpdate(int tick)
        { }

        /// <summary>
        /// Fired the first time an inactive update has been made.
        /// </summary>
        /// <param name="tick">The total amount of ticks.</param>
        protected void OnFirstInactiveUpdate(int tick)
        { }

        public Dictionary<int, Action> ActionQueue { get; set; } = new Dictionary<int, Action>();

        /// <summary>
        /// Returns the tick of which the ticking started.
        /// This isn't always 0, as the StartTick can be set.
        /// </summary>
        public int StartTick { get; }

        /// <summary>
        /// Returns how many times this Updating instance has ticked.
        /// </summary>
        public int Tick { get; private set; }

        /// <summary>
        /// If active, returns how many times it has been ticked during its session.
        /// </summary>
        public int ActiveTicks { get; private set; }

        /// <summary>
        /// If inactive, returns how many times it has been ticked during its session.
        /// </summary>
        public int InactiveTicks { get; private set; }

        /// <summary>
        /// Returns how many seconds this updater has been alive.
        /// </summary>
        public int SecondsAlive => Tick/1000;

        /// <summary>
        /// Returns whether this Updating instance is active.
        /// </summary>
        public bool IsActive { get; protected set; } = true;
    }
}
