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
        Random random = new Random();
        public CDeroulement()
        {
            
        }

        public void CréerListeVaisseaux()
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
            foreach(CVaisseau vaisseau in fileVaisseaux)
            {
                cpt++;
                int quantitePapier = random.Next(1, (vaisseau.CAPACITEMAX - 3));
                int quantiteVerre = random.Next(1, vaisseau.CAPACITEMAX - quantitePapier - 2);
                int quantitePlastique = random.Next(1, vaisseau.CAPACITEMAX - quantitePapier - quantiteVerre - 1);
                int quantiteFerraille = random.Next(1, vaisseau.CAPACITEMAX - quantitePapier - quantiteVerre - quantitePlastique);
                int quantiteTerre = vaisseau.CAPACITEMAX - quantitePapier - quantiteVerre - quantitePlastique - quantiteFerraille;

                int sommeMateriaux = quantitePapier + quantiteVerre + quantitePlastique + quantiteFerraille + quantiteTerre;

                Console.WriteLine(cpt + " " + vaisseau.CAPACITEMAX + " " + quantitePapier + " " + quantiteVerre + " " + quantitePlastique + " " + quantiteFerraille + " " + quantiteTerre + " " + sommeMateriaux);
            }
                        
            Console.ReadKey();
        }
    }
}
