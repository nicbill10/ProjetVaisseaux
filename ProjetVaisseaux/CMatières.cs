using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CMatières
    {
        string type;
        int quantite;
        public CMatières(string type, int quantite)
        {
            this.type = type;
            this.quantite = quantite;
        }

        public int QUANTITE
        {
            get { return quantite; }
            set { quantite = value; }
        }

        public string TYPE
        {
            get { return type; }
            set { type = value; }
        }
    }
}
