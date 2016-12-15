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

            int Qte, matiereRandom;

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
            foreach (CVaisseau vaisseau in fileVaisseaux) //Nouveau random qui choisi quelle matière sera ajouté, et ensuite il insère une quantité de cette matière aléatoirement, sauf s'il reste 20 m³ d'espace ou moins.
            {
                Qte = 0;
                while (Qte < vaisseau.CAPACITEMAX)
                {
                    
                    matiereRandom = random.Next(5);
                    switch(matiereRandom)
                    {
                        case 0:
                            if ((vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)) <= 20)
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CPapier(vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)));
                            else
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CPapier(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE))));
                            break;
                        case 1:
                            if ((vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)) <= 20)
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CPlastique(vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)));
                            else
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CPlastique(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE))));
                            break;
                        case 2:
                            if ((vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)) <= 20)
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CVerre(vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)));
                            else
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CVerre(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE))));
                            break;
                        case 3:
                            if ((vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)) <= 20)
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CTerreContaminee(vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)));
                            else
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CTerreContaminee(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE))));
                            break;
                        case 4:
                            if((vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)) <= 20)
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CFerraille(vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE)));
                            else
                                vaisseau.PILEMATIEREVAISSEAU.Push(new CFerraille(random.Next(1, vaisseau.CAPACITEMAX - vaisseau.PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE))));
                            break;
                    }
                    Qte += vaisseau.PILEMATIEREVAISSEAU.Peek().QUANTITE;
                }
            }
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
        }

        public void DechargerVaisseau() //décharge les vaisseaux à l'aide du drone de transport
        {
            bool stayinWhile = true;

            Console.WriteLine("╔══════════╤════════╤═══════╤═══════════╤═══════════╤═══════╤═══════════════════════════╗");
            Console.WriteLine("║ Centre # │ Papier │ Verre │ Plastique │ Ferraille │ Terre │ Vaisseaux vides restants  ║");

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
                            case "terre": //J'avais oublié de mettre TERRE à la place de ferraille lors de l'appel de la méthode ViderCentreTri


                                if (droneTransport.QUANTITE + listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE) > listeCentresTri[noCentreTri].CAPACITEMAXTERRE) //Si la quantité de matières dans le drone + la quantité déjà dans la pile du centre de tri dépasse la capacité maximal
                                {
                                    if (listeCentresTri[noCentreTri].CAPACITEMAXTERRE != 0) //Si la capacité n'est pas de zéro
                                    {
                                        droneTransport.QUANTITE -= listeCentresTri[noCentreTri].CAPACITEMAXTERRE - listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE);
                                        listeCentresTri[noCentreTri].PILETERRE.Push(new CTerreContaminee(listeCentresTri[noCentreTri].CAPACITEMAXTERRE - listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE)));
                                        vaisseauActif.PILEMATIEREVAISSEAU.Push(droneTransport);
                                        if (listeCentresTri[noCentreTri].FILEDEPART.Count != 0)
                                            ViderPileCentreTri("TERRE");
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
                                        ViderPileCentreTri("TERRE");
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
                Console.WriteLine("╟──────────┼────────┼───────┼───────────┼───────────┼───────┼───────────────────────────╢");
                Console.Write("║          │        │       │           │           │       │                           ║");
                Console.CursorLeft = 2;
                Console.Write(noCentreTri + 1);
                Console.CursorLeft = 13;
                Console.Write(listeCentresTri[noCentreTri].PILEPAPIER.Sum(i => i.QUANTITE));
                Console.CursorLeft = 22;
                Console.Write(listeCentresTri[noCentreTri].PILEVERRE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 31;                                                                                                //Modification de l'interface: Affichage dans un tableau plus lisible
                Console.Write(listeCentresTri[noCentreTri].PILEPLASTIQUE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 43;
                Console.Write(listeCentresTri[noCentreTri].PILEFERRAILLE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 54;
                Console.Write(listeCentresTri[noCentreTri].PILETERRE.Sum(i => i.QUANTITE));
                Console.CursorLeft = 73;
                Console.WriteLine(listeCentresTri[noCentreTri].FILEVAISSEAUXVIDE.Count);
            }
            Console.WriteLine("╚══════════╧════════╧═══════╧═══════════╧═══════════╧═══════╧═══════════════════════════╝");
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
                                EnvoyerVaisseau();
                            }
                        }
                        else
                        {
                            EnvoyerVaisseau();
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
                                EnvoyerVaisseau();
                            }
                        }
                        else
                        {
                            EnvoyerVaisseau();
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
                                EnvoyerVaisseau();
                            }
                        }
                        else
                        {
                            EnvoyerVaisseau();
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
                                EnvoyerVaisseau();
                            }
                        }
                        else
                        {
                            EnvoyerVaisseau();
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
                                EnvoyerVaisseau();
                            }
                        }
                        else
                        {
                            EnvoyerVaisseau();
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

        private void EnvoyerPileDepart() //déplace les vaisseaux des files de départ vers les files d'arrivée des centres de tri suivants ou vers le néant si c'eSt le dernier centre de tri
        {
            while (listeCentresTri[noCentreTri].FILEDEPART.Count > 0)
            {
                EnvoyerVaisseau();
            }
        }

        private void EnvoyerVaisseau() //Méthode qui sert à décider le sort du vaisseaux lorsqu'on doit l'envoyer ailleurs
        {
            if(listeCentresTri[noCentreTri].FILEDEPART.Peek().PILEMATIEREVAISSEAU.Sum(i => i.QUANTITE) > 0)
            {
                if (noCentreTri != nbrCentresTri - 1)
                    listeCentresTri[noCentreTri + 1].FILEARRIVEE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
                else
                    listeCentresTri[noCentreTri].FILEDEPART.Dequeue();
            }
            else
            {
                listeCentresTri[noCentreTri].FILEVAISSEAUXVIDE.Enqueue(listeCentresTri[noCentreTri].FILEDEPART.Dequeue());
            }
           
        }
    }
}
