using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CDeroulement
    {
        Queue<CVaisseau> fileVaisseaux = new Queue<CVaisseau>();
        List<CCentreTri> listeCentresTri = new List<CCentreTri>();
        Random random = new Random();
        public CDeroulement()
        {
            CreerListeVaisseaux();
            CreerCentresTri();
        }

        public void CreerListeVaisseaux()
        {
            for(int i = 1; i <= 100; i++)
            {
                int rndTypeVaisseau = random.Next(1, 3);
                if(rndTypeVaisseau == 1)
                {
                    fileVaisseaux.Enqueue(new CVaisseauLeger());
                }
                else
                {
                    fileVaisseaux.Enqueue(new CVaisseauCargo());
                }
            }

            int cpt = 0;
            foreach (CVaisseau vaisseau in fileVaisseaux)
            {
                cpt++;
                vaisseau.PAPIER = new CPapier(random.Next(1, (vaisseau.CAPACITEMAX - 3)));
                vaisseau.VERRE = new CVerre(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - 2));
                vaisseau.PLASTIQUE = new CPlastique(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - 1));
                vaisseau.FERRAILLE = new CFerraille(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE));
                vaisseau.TERRE = new CTerreContaminee(vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE - vaisseau.FERRAILLE.QUANTITE);
                int sommeMateriaux = vaisseau.PAPIER.QUANTITE + vaisseau.VERRE.QUANTITE + vaisseau.PLASTIQUE.QUANTITE + vaisseau.FERRAILLE.QUANTITE + vaisseau.TERRE.QUANTITE;

                Console.WriteLine(cpt + " " + vaisseau.CAPACITEMAX + " " + vaisseau.PAPIER.QUANTITE + " " + vaisseau.VERRE.QUANTITE + " " + vaisseau.PLASTIQUE.QUANTITE + " " + vaisseau.FERRAILLE.QUANTITE + " " + vaisseau.TERRE.QUANTITE + " " + sommeMateriaux);
            }          
            Console.ReadKey();
        }

        public void CreerCentresTri()
        {
            for(int i = 1; i <= 5; i++)
            {
                listeCentresTri.Add(new CCentreTri("impair"));
                listeCentresTri.Add(new CCentreTri("pair"));
            }
            foreach(CCentreTri centreTri in listeCentresTri)
            {
                Console.WriteLine(centreTri.TYPE);
            }
            Console.ReadKey();

            listeCentresTri[0].FILEARRIVEE = fileVaisseaux;
        }

        public void DechargerVaisseau()
        {
            int CentreTri = 0;

            if()
            listeCentresTri[CentreTri].PILEPAPIER.Push(listeCentresTri[CentreTri].FILEARRIVEE.Peek().PAPIER);
        }

        
    }
}
