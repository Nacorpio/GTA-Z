using Microsoft.SqlServer.Server;

namespace GTAZ.Assembly {

    public abstract class EntityPart {

        /// <summary>
        /// Constructs a Part with the specified weight.
        /// </summary>
        /// <param name="name">The name of the Part.</param>
        /// <param name="weight">The weight of the Part.</param>
        public EntityPart(string name, float weight) {
            Weight = weight;
            Name = name;
        }

        /// <summary>
        /// Constructs a Part that weighs nothing.
        /// </summary>
        /// <param name="name">The name of the Part.</param>
        public EntityPart(string name) {
            Name = name;
            Weight = 0;
        }

        /// <summary>
        /// Returns the name of this Part.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the weight of this Part.
        /// </summary>
        public float Weight { get; private set; }

    }

}
