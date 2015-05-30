using System.Collections.Generic;
using System.Linq;
using GTAZ.Controllable;

namespace GTAZ.Assembly {

    public abstract class EntityAssembly : ControllableEntity {

        private readonly Dictionary<string, EntityPart> _parts = new Dictionary<string, EntityPart>();
        private readonly float _weightCapacity;

        protected EntityAssembly(int uid, string groupId, float weightCapacity) : base(uid, groupId) {
            _weightCapacity = weightCapacity;
        }

        /// <summary>
        /// Adds a Part to this Assembly with the specified name.
        /// <param name="name">The name of the part to add.</param>
        /// <param name="part">The part to add.</param>
        public void AddPart(string name, EntityPart part) {
            if (!_parts.ContainsKey(name)) {
                if (!(GetTotalWeight() + part.Weight <= _weightCapacity)) {
                    // Too much weight if we add the part.
                    return;
                }
                _parts.Add(name, part);
            }
        }

        /// <summary>
        /// Returns a Part with the specified name.
        /// </summary>
        /// <param name="name">The name of the part to find.</param>
        /// <returns></returns>
        public EntityPart GetPart(string name) {
            return _parts[name];
        }

        /// <summary>
        /// Returns the total weight of this Assembly.
        /// </summary>
        /// <returns></returns>
        public float GetTotalWeight() {
            return _parts.Values.Sum(p => p.Weight);
        }

        /// <summary>
        /// Returns the amount of weight that can be stored within this Assembly.
        /// </summary>
        /// <returns></returns>
        public float GetWeightCapacity() {
            return _weightCapacity;
        }
        
        /// <summary>
        /// Initializes this Assembly.
        /// </summary>
        protected abstract void InitializeAssembly();


    }

}
