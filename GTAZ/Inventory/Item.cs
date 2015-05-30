
using System;
using GTA;
using GTA.Math;

namespace GTAZ.Inventory {

    public delegate void ItemChangedEventHandler(object sender, EventArgs e);
    public delegate void ItemUseEventHandler(Player trigger, Ped target, object sender, EventArgs e);
    
    public abstract class Item {

        protected event ItemUseEventHandler Used;
        
        private readonly int _id;
        private readonly string _name;

        protected Item(int id, string name) {
            _id = id;
            _name = name;
        }

        public void Use(Player trigger, Ped target) {
            if (trigger == null || target == null) return;
            if (Used != null) Used(trigger, target, this, EventArgs.Empty);
        }
                    
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

    }

}
