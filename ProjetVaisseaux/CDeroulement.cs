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
            Console.ReadKey();
        }
    }
}
