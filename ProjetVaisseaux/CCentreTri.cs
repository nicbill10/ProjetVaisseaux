using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CCentreTri
    {
        Stack<CPapier> pilePapier = new Stack<CPapier>();
        Stack<CVerre> pileVerre = new Stack<CVerre>();
        Stack<CPlastique> pilePlastique = new Stack<CPlastique>();
        Stack<CFerraille> pileFerraille = new Stack<CFerraille>();
        Stack<CTerreContaminee> pileTerre = new Stack<CTerreContaminee>();
        public CCentreTri(string type)
        {

        }
    }
}
