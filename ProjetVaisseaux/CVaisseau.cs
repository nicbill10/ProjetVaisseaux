using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CVaisseau //J'ai supprimé les objets de matière qui ne servaient à rien suite au changement de random
    {
        int capaciteMax;
        Stack<CMatières> PileMatieresVaisseaux = new Stack<CMatières>();


        public CVaisseau(int capaciteMax)
        {
            this.capaciteMax = capaciteMax;
        }

        public int CAPACITEMAX
        {
            get { return capaciteMax; }
        }

        public Stack<CMatières> PILEMATIEREVAISSEAU
        {
            get { return PileMatieresVaisseaux; }
            set { PileMatieresVaisseaux = value; }
        }
    }       
}
