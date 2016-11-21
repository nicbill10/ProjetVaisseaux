using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CVaisseau
    {
        int capaciteMax;
        public CVaisseau(int capaciteMax)
        {
            this.capaciteMax = capaciteMax;
        }

        public int CAPACITEMAX
        {
            get { return capaciteMax; }
        }
    }
}
