
namespace GTAZ.Inventory {

    public abstract class Item {

        private readonly int _id;
        private readonly string _name;

        protected Item(int id, string name) {
            _id = id;
            _name = name;
        }

        //

        /// <summary>
        /// Returns the unique identifier of this Item.
        /// </summary>
        public int Id {
            get { return _id; }
        }

        /// <summary>
        /// Returns the unique name of this Item.
        /// </summary>
        public string Name {
            get { return _name; }
        }

        //

        public abstract void OnItemUse(params object[] args);

        public abstract void OnItemDrop(params object[] args);

        public abstract void OnItemPickup(params object[] args);

        //

    }

}
