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
        CPapier papier;
        CVerre verre;
        CPlastique plastique;
        CFerraille ferraille;
        CTerreContaminee terre;


        public CVaisseau(int capaciteMax)
        {
            this.capaciteMax = capaciteMax;
        }

        public int CAPACITEMAX
        {
            get { return capaciteMax; }
        }

        public CPapier PAPIER
        {
            get { return papier; }
            set { papier = value; }
        }

        public CVerre VERRE
        {
            get { return verre; }
            set { verre = value; }
        }
    
        public CPlastique PLASTIQUE
        {
            get { return plastique; }
            set { plastique = value; }
        }

        public CFerraille FERRAILLE
        {
            get { return ferraille; }
            set { ferraille = value; }
        }

        public CTerreContaminee TERRE
        {
            get { return terre; }
            set { terre = value; }
        }
    }       
}
