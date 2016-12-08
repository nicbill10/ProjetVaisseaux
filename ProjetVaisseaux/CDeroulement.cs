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
        int nbrVaisseaux, nbrCentresTri;
        public CDeroulement()
        {
            choisirNbrVaisseaux();
            CreerListeVaisseaux();
            CreerCentresTri();
            DechargerVaisseau();
        }

        public void choisirNbrVaisseaux()
        {
            Console.WriteLine("Entrez le nombre de vaisseaux que vous souhaitez retrouver dans la simulation (100, 200, 300, 400 ou 500): ");
            nbrVaisseaux = Convert.ToInt32(Console.ReadLine());
            if ((nbrVaisseaux != 100) && (nbrVaisseaux != 200) && (nbrVaisseaux != 300) && (nbrVaisseaux != 400) && (nbrVaisseaux != 500))
            {
                Console.WriteLine("Le nombre de vaisseaux voulus doit etre de 100, 200, 300, 400 ou 500. Réessayez.");
                Console.ReadKey();
                Console.Clear();
                choisirNbrVaisseaux();
            }

            Console.Clear();

            Console.WriteLine("Entrez le nombre de centres de tri que vous souhaitez retrouver dans la simulation (10, 20, 30, 40 ou 50): ");
            nbrCentresTri = Convert.ToInt32(Console.ReadLine());
            if ((nbrCentresTri != 10) && (nbrCentresTri != 20) && (nbrCentresTri != 30) && (nbrCentresTri != 40) && (nbrCentresTri != 50))
            {
                Console.WriteLine("Le nombre de centres de tri voulus doit etre de 10, 20, 30, 40 ou 50. Réessayez.");
                Console.ReadKey();
                Console.Clear();
                choisirNbrVaisseaux();
            }
            Console.Clear();
        }

        public void CreerListeVaisseaux()
        {
            for (int i = 1; i <= nbrVaisseaux; i++)
            {
                int rndTypeVaisseau = random.Next(1, 3);
                if (rndTypeVaisseau == 1)
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
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.PAPIER = new CPapier(random.Next(1, (vaisseau.CAPACITEMAX - 3))));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.VERRE = new CVerre(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - 2)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.PLASTIQUE = new CPlastique(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - 1)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.FERRAILLE = new CFerraille(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.TERRE = new CTerreContaminee(vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE - vaisseau.FERRAILLE.QUANTITE));

                Console.WriteLine(cpt + " " + vaisseau.CAPACITEMAX + " " + vaisseau.PAPIER.QUANTITE + " " + vaisseau.VERRE.QUANTITE + " " + vaisseau.PLASTIQUE.QUANTITE + " " + vaisseau.FERRAILLE.QUANTITE + " " + vaisseau.TERRE.QUANTITE);
            }
            Console.ReadKey();
        }

        public void CreerCentresTri()
        {
            for (double i = 1; i <= nbrCentresTri; i++)
            {
                if ((i % 2) == 0 )
                {
                    if (Premier(Convert.ToInt32(i)))
                        listeCentresTri.Add(new CCentreTri("premier/pair", i));

                    else if (i % 5 == 0)
                        listeCentresTri.Add(new CCentreTri("multiple/pair", i));

                    else { listeCentresTri.Add(new CCentreTri("pair", i)); }
                }
                else
                {
                    if (Premier(Convert.ToInt32(i)))
                        listeCentresTri.Add(new CCentreTri("premier/impair", i));

                    else if (i % 5 == 0)
                        listeCentresTri.Add(new CCentreTri("multiple/impair", i));

                    else { listeCentresTri.Add(new CCentreTri("impair", i)); }
                }

             
                
            }
            listeCentresTri[0].FILEARRIVEE = fileVaisseaux;
            foreach (CCentreTri centre in listeCentresTri)
            {
                Console.WriteLine(centre.NOCENTRE + " " + centre.TYPE);
            }
            Console.ReadKey();
        }

        public void DechargerVaisseau()
        {
            CVaisseau vaisseauActif = listeCentresTri[0].FILEARRIVEE.Dequeue();
            CMatières droneTransport = vaisseauActif.PILEMATIEREVAISSEAU.Pop();
            switch (droneTransport.TYPE)
            {
                case "papier":

                    if (droneTransport.QUANTITE + listeCentresTri[0].PILEPAPIER.Sum(i => i.QUANTITE) > listeCentresTri[0].CAPACITEMAXPAPIER)
                    {
                        if (listeCentresTri[0].CAPACITEMAXPAPIER != 0)
                        {
                            droneTransport.QUANTITE -= listeCentresTri[0].CAPACITEMAXPAPIER - listeCentresTri[0].PILEPAPIER.Sum(i => i.QUANTITE);
                            listeCentresTri[0].PILEPAPIER.Push(new CPapier(listeCentresTri[0].CAPACITEMAXPAPIER - listeCentresTri[0].PILEPAPIER.Sum(i => i.QUANTITE)));
                            ViderPileCentreTri();
                        }
                        else
                        {
                            vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                            listeCentresTri[0].FILEDEPART.Enqueue(vaisseauActif);
                        }
                    }
                    else if(droneTransport.QUANTITE + listeCentresTri[0].PILEPAPIER.Sum(i => i.QUANTITE) == listeCentresTri[0].CAPACITEMAXPAPIER)
                    {
                        listeCentresTri[0].PILEPAPIER.Push(droneTransport as CPapier);
                        ViderPileCentreTri();
                    }
                    else if (droneTransport.QUANTITE + listeCentresTri[0].PILEPAPIER.Sum(i => i.QUANTITE) < listeCentresTri[0].CAPACITEMAXPAPIER)
                    {
                        listeCentresTri[0].PILEPAPIER.Push(droneTransport as CPapier);
                    }


                    break;
                case "verre":
                    break;
                case "plastique":
                    break;
                case "ferraille":
                    break;
                case "terre":
                    break;
            }
        }

        public void ViderPileCentreTri()
        {

        }


        private bool Premier(int nbre)
        {
            if (nbre == 1 || nbre == 2)
                return true;
            else if (nbre % 2 == 0 || nbre == 5)
                return false;
            double racine = Math.Sqrt(nbre);

            if (racine == Math.Floor(racine))
                return false;

            for (int i = 3; i < racine; i += 2)
            {
                if (nbre % i == 0)
                    return false;
            }
            return true;
        }
    }
}
