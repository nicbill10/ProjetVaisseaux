using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CDeroulement
    {
        CVaisseau vaisseauActif;
        Queue<CVaisseau> fileVaisseaux = new Queue<CVaisseau>();
        List<CCentreTri> listeCentresTri = new List<CCentreTri>();
        Random random = new Random();
        int nbrVaisseaux, nbrCentresTri, noCentreTri;
        public CDeroulement()
        {
            choisirNbrVaisseaux();
            CreerListeVaisseaux();
            CreerCentresTri();
            DechargerVaisseau();
        }

        public void choisirNbrVaisseaux() //Demande à l'utilisateur le nombre de vaisseaux et de centres de tri il va y avoir
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

        public void CreerListeVaisseaux() //Crée la liste de vaisseaux aléatoirement
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
            foreach (CVaisseau vaisseau in fileVaisseaux) //Le random n'est pas optimisé
            {
                cpt++;
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.PAPIER = new CPapier(random.Next(1, (vaisseau.CAPACITEMAX - 3))));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.VERRE = new CVerre(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - 2)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.PLASTIQUE = new CPlastique(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - 1)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.FERRAILLE = new CFerraille(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE)));
                vaisseau.PILEMATIEREVAISSEAU.Push(vaisseau.TERRE = new CTerreContaminee(vaisseau.CAPACITEMAX - vaisseau.PAPIER.QUANTITE - vaisseau.VERRE.QUANTITE - vaisseau.PLASTIQUE.QUANTITE - vaisseau.FERRAILLE.QUANTITE));
            }
            Console.ReadKey();
        }

        public void CreerCentresTri() //création des centres de tri avec gestion des nombres premiers et des multiple de 5
        {
            for (double i = 1; i <= nbrCentresTri; i++)
            {
                if ((i % 2) == 0)
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
            Console.ReadKey();
        }

        public void DechargerVaisseau() //décharge les vaisseaux à l'aide du drone de transport
        {
            bool stayinWhile = true;

            Console.WriteLine("Centre Tri #     Papier     Verre     Plastique     Ferraille     Terre");
            Console.WriteLine("-----------------------------------------------------------------------");

            for (noCentreTri = 0; noCentreTri <= listeCentresTri.Count - 1; noCentreTri++)
            {
                 while (listeCentresTri[noCentreTri].FILEARRIVEE.Count > 0) 
                {
                    vaisseauActif = listeCentresTri[noCentreTri].FILEARRIVEE.Dequeue();
                    stayinWhile = true;
                    while (vaisseauActif.PILEMATIEREVAISSEAU.Count > 0 && stayinWhile == true)
                    {
                        CMatières droneTransport = vaisseauActif.PILEMATIEREVAISSEAU.Pop();
                        switch (droneTransport.TYPE)
                        {
                            case "papier":

                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXPAPIER) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXPAPIER != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXPAPIER - listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILEPAPIER.Push(new CPapier(listeCentresTri[noCentreTri].CAPACITEMAXPAPIER - listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("PAPIER");
                                        else
                                            stayinWhile = false;
                                    }
                                    else //Si la capacité est de 0
                                    {
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        stayinWhile = false;
                                    }
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE) == listeCentresTri[noCentreTri].CAPACITEMAXPAPIER) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est égale à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEPAPIER.Push(droneTransport as CPapier);
                                    if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                        ViderPileCentreTri("PAPIER");
                                    else
                                        stayinWhile = false;
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE) < listeCentresTri[noCentreTri].CAPACITEMAXPAPIER) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEPAPIER.Push(droneTransport as CPapier);
                                }
                                break;

                            case "verre":

                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXVERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXVERRE != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXVERRE - listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILEVERRE.Push(new CVerre(listeCentresTri[noCentreTri].CAPACITEMAXVERRE - listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("VERRE");
                                        else
                                            stayinWhile = false;
                                    }
                                    else //Si la capacité est de 0
                                    {
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        stayinWhile = false;
                                    }
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE) == listeCentresTri[noCentreTri].CAPACITEMAXVERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEVERRE.Push(droneTransport as CVerre);
                                    if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                        ViderPileCentreTri("VERRE");
                                    else
                                        stayinWhile = false;
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE) < listeCentresTri[noCentreTri].CAPACITEMAXVERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEVERRE.Push(droneTransport as CVerre);
                                }

                                break;

                            case "plastique":

                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE - listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILEPLASTIQUE.Push(new CPlastique(listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE - listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("PLASTIQUE");
                                        else
                                            stayinWhile = false;
                                    }
                                    else //Si la capacité est de 0
                                    {
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        stayinWhile = false;
                                    }
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE) == listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE)  //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri arrive pile sur la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEPLASTIQUE.Push(droneTransport as CPlastique);
                                    if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                        ViderPileCentreTri("PLASTIQUE");
                                    else
                                        stayinWhile = false;
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE) < listeCentresTri[noCentreTri].CAPACITEMAXPLASTIQUE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEPLASTIQUE.Push(droneTransport as CPlastique);
                                }

                                break;
                            case "ferraille":

                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE - listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILEFERRAILLE.Push(new CFerraille(listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE - listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("FERRAILLE");
                                        else
                                            stayinWhile = false;
                                    }
                                    else //Si la capacité est de 0
                                    {
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        stayinWhile = false;
                                    }
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE) == listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEFERRAILLE.Push(droneTransport as CFerraille);
                                    if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                        ViderPileCentreTri("FERRAILLE");
                                    else
                                        stayinWhile = false;
                                } 
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE) < listeCentresTri[noCentreTri].CAPACITEMAXFERAILLE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILEFERRAILLE.Push(droneTransport as CFerraille);
                                }

                                break;
                            case "terre":


                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXTERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXTERRE != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXTERRE - listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILETERRE.Push(new CTerreContaminee(listeCentresTri[noCentreTri].CAPACITEMAXTERRE - listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("FERRAILLE");
                                        else
                                            stayinWhile = false;
                                    }
                                    else //Si la capacité est de 0
                                    {
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        stayinWhile = false;
                                    }
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE) == listeCentresTri[noCentreTri].CAPACITEMAXTERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILETERRE.Push(droneTransport as CTerreContaminee);
                                    if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                        ViderPileCentreTri("FERRAILLE");
                                    else
                                        stayinWhile = false;
                                }
                                else if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE) < listeCentresTri[noCentreTri].CAPACITEMAXTERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri est inférieur à la capacité maximal
                                {
                                    listeCentresTri[noCentreTri].PILETERRE.Push(droneTransport as CTerreContaminee);
                                }
                                break;
                        }



                    }
                    listeCentresTri[noCentreTri].FILEDEPART.Enqueue(vaisseauActif);
                }
                EnvoyerPileDepart();
                Console.Write(noCentreTri + 1);
                Console.CursorLeft = 10;
                Console.Write(listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE));
                Console.CursorLeft = 20;
                Console.Write(listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 30;
                Console.Write(listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 40;
                Console.Write(listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 50;
                Console.WriteLine(listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE));
            }
        }

        public void ViderPileCentreTri(string matiere) //vide les piles qui sont pleines
        {
            int espaceRestante;
            switch (matiere)
            {
                case "PAPIER":
                    while (listeCentresTri[noCentreTri].FILEDEPART.Count != 0 && listeCentresTri[noCentreTri].PILEPAPIER.Count != 0)
                    {
                        espaceRestante = listeCentresTri[noCentreTri].FILEDEPART.Peek().CAPACITEMAX - listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE);
                        if (espaceRestante <= 0)
                        {
                            if (listeCentresTri[noCentreTri].PILEPAPIER.Peek().QUANTITE <= espaceRestante)
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(listeCentresTri[noCentreTri].PILEPAPIER.Pop());
                            }
                            else
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(new CPapier(espaceRestante));
                                listeCentresTri[noCentreTri].PILEPAPIER.Peek().QUANTITE -= espaceRestante;
                                if (noCentreTri != nbrCentresTri - 1)
                                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                                else
                                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                            }
                        }
                        else
                        {
                            if (noCentreTri != nbrCentresTri - 1)
                                listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                            else
                                listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                        }   
                    }
                    break;

                case "FERRAILLE":
                    while (listeCentresTri[noCentreTri].FILEDEPART.Count != 0 && listeCentresTri[noCentreTri].PILEFERRAILLE.Count != 0)
                    {
                        espaceRestante = listeCentresTri[noCentreTri].FILEDEPART.Peek().CAPACITEMAX - listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE);
                        if (espaceRestante <= 0)
                        {
                            if (listeCentresTri[noCentreTri].PILEFERRAILLE.Peek().QUANTITE <= espaceRestante)
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(listeCentresTri[noCentreTri].PILEFERRAILLE.Pop());
                            }
                            else
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(new CFerraille(espaceRestante));
                                listeCentresTri[noCentreTri].PILEFERRAILLE.Peek().QUANTITE -= espaceRestante;
                                if (noCentreTri != nbrCentresTri - 1)
                                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                                else
                                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                            }
                        }
                        else
                        {
                            if (noCentreTri != nbrCentresTri - 1)
                                listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                            else
                                listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                        }
                    }
                    break;

                case "TERRE":
                    while (listeCentresTri[noCentreTri].FILEDEPART.Count != 0 && listeCentresTri[noCentreTri].PILETERRE.Count != 0)
                    {
                        espaceRestante = listeCentresTri[noCentreTri].FILEDEPART.Peek().CAPACITEMAX - listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE);
                        if (espaceRestante <= 0)
                        {
                            if (listeCentresTri[noCentreTri].PILETERRE.Peek().QUANTITE <= espaceRestante)
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(listeCentresTri[noCentreTri].PILETERRE.Pop());
                            }
                            else
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(new CTerreContaminee(espaceRestante));
                                listeCentresTri[noCentreTri].PILETERRE.Peek().QUANTITE -= espaceRestante;
                                if (noCentreTri != nbrCentresTri - 1)
                                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                                else
                                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                            }
                        }
                        else
                        {
                            if (noCentreTri != nbrCentresTri - 1)
                                listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                            else
                                listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                        }
                    }
                    break;

                case "VERRE":
                    while (listeCentresTri[noCentreTri].FILEDEPART.Count != 0 && listeCentresTri[noCentreTri].PILEVERRE.Count != 0)
                    {
                        espaceRestante = listeCentresTri[noCentreTri].FILEDEPART.Peek().CAPACITEMAX - listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE);
                        if (espaceRestante != 0)
                        {
                            if (listeCentresTri[noCentreTri].PILEVERRE.Peek().QUANTITE <= espaceRestante)
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(listeCentresTri[noCentreTri].PILEVERRE.Pop());
                            }
                            else
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(new CVerre(espaceRestante));
                                listeCentresTri[noCentreTri].PILEVERRE.Peek().QUANTITE -= espaceRestante;
                                if (noCentreTri != nbrCentresTri - 1)
                                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                                else
                                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                            }
                        }
                        else
                        {
                            listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                        }
                    }
                    break;

                case "PLASTIQUE":
                    while (listeCentresTri[noCentreTri].FILEDEPART.Count != 0 && listeCentresTri[noCentreTri].PILEPLASTIQUE.Count != 0)
                    {
                        espaceRestante = listeCentresTri[noCentreTri].FILEDEPART.Peek().CAPACITEMAX - listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE);
                        if (espaceRestante <= 0)
                        {
                            if (listeCentresTri[noCentreTri].PILEPLASTIQUE.Peek().QUANTITE <= espaceRestante)
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(listeCentresTri[noCentreTri].PILEPLASTIQUE.Pop());
                            }
                            else
                            {
                                listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Push(new CPlastique(espaceRestante));
                                listeCentresTri[noCentreTri].PILEPLASTIQUE.Peek().QUANTITE -= espaceRestante;
                                if (noCentreTri != nbrCentresTri - 1)
                                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                                else
                                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                            }
                        }
                        else
                        {
                            if (noCentreTri != nbrCentresTri - 1)
                                listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                            else
                                listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
                        }
                    }
                    break;
            }
        }


        private bool Premier(int nbre) //détermine les nombres qui sont premiers
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

        private void EnvoyerPileDepart() //déplace les vaisseaux des files de départ vers les files d'arrivée des centres de tri suivants
        {
            while (listeCentresTri[noCentreTri].FILEDEPART.Count > 0)
            {
                    if (noCentreTri != nbrCentresTri - 1)
                        listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                    else
                        listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
            }

        }
    }
}
