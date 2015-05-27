using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTAZ.Population {

    public abstract class ControllablePopulator {

        protected enum PopulationType {
            Vehicle,
            Prop,
            Ped,
        }

        protected ControllablePopulator() {
            
        }

    }

}
