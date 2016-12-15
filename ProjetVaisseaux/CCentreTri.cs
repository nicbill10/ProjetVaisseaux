using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetVaisseaux
{
    class CCentreTri
    {
        Queue<CVaisseau> fileArrivee = new Queue<CVaisseau>();
        Queue<CVaisseau> fileDepart = new Queue<CVaisseau>();
        Queue<CVaisseau> fileVaisseauxVide = new Queue<CVaisseau>(); //Création d'une file pour envoyer les vaisseaux vides
        Stack<CPapier> pilePapier = new Stack<CPapier>();
        Stack<CVerre> pileVerre = new Stack<CVerre>();
        Stack<CPlastique> pilePlastique = new Stack<CPlastique>();
        Stack<CFerraille> pileFerraille = new Stack<CFerraille>();
        Stack<CTerreContaminee> pileTerre = new Stack<CTerreContaminee>();

        int capaciteMaxPapier, capaciteMaxVerre, capaciteMaxPlastique, capaciteMaxFerraille, capaciteMaxTerre;
        double noCentre;
        string type;
        public CCentreTri(string type, double NoCentre)
        {
            this.type = type;
            this.noCentre = NoCentre;

            if(this.type == "pair")
            {
                capaciteMaxPapier = 1003;
                capaciteMaxVerre = 857;
                capaciteMaxPlastique = 3456;
                capaciteMaxFerraille = 457;
                capaciteMaxTerre = 639;
            }
            if(this.type == "impair")
            { 
                capaciteMaxPapier = 3067;
                capaciteMaxVerre = 2456;
                capaciteMaxPlastique = 561;
                capaciteMaxFerraille = 2658;
                capaciteMaxTerre = 8234;
            }
            if (this.type == "premier/impair")
            {
                capaciteMaxPapier = 0;
                capaciteMaxVerre = 0;
                capaciteMaxPlastique = 561;
                capaciteMaxFerraille = 2658;
                capaciteMaxTerre = 0;
            }
            if (this.type == "multiple/impair")
            {
                capaciteMaxPapier = 0;
                capaciteMaxVerre = 2456;
                capaciteMaxPlastique = 0;
                capaciteMaxFerraille = 0;
                capaciteMaxTerre = 8234;
            }
            if (this.type == "premier/pair")
            {
                capaciteMaxPapier = 0;
                capaciteMaxVerre = 0;
                capaciteMaxPlastique = 3456;
                capaciteMaxFerraille = 457;
                capaciteMaxTerre = 0;
            }
            if (this.type == "multiple/pair")
            {
                capaciteMaxPapier = 0;
                capaciteMaxVerre = 857;
                capaciteMaxPlastique = 0;
                capaciteMaxFerraille = 0;
                capaciteMaxTerre = 639;
            }
        }

        public string TYPE
        {
            get { return type; }
        }

        public Stack<CPapier> PILEPAPIER
        {
            get { return pilePapier; }
            set { pilePapier = value; }
        }

        public Stack<CVerre> PILEVERRE
        {
            get { return pileVerre; }
            set { pileVerre = value; }
        }

        public Stack<CPlastique> PILEPLASTIQUE
        {
            get { return pilePlastique; }
            set { pilePlastique = value; }
        }

        public Stack<CFerraille> PILEFERRAILLE
        {
            get { return pileFerraille; }
            set { pileFerraille = value; }
        }

        public Stack<CTerreContaminee> PILETERRE
        {
            get { return pileTerre; }
            set { pileTerre = value; }
        }

        public Queue<CVaisseau> FILEARRIVEE
        {
            get { return fileArrivee; }
            set { fileArrivee = value; }
        }

        public Queue<CVaisseau> FILEDEPART
        {
            get { return fileDepart; }
            set { fileDepart = value; }
        }

        public Queue<CVaisseau> FILEVAISSEAUXVIDE
        {
            get { return fileVaisseauxVide; }
            set { fileVaisseauxVide = value; }
        }

        public double NOCENTRE
        {
            get { return noCentre; }
            set { noCentre = value; }
        }

        public int CAPACITEMAXPAPIER
        {
            get { return capaciteMaxPapier; }
        }

        public int CAPACITEMAXFERAILLE
        {
            get { return capaciteMaxFerraille; }
        }

        public int CAPACITEMAXVERRE
        {
            get { return capaciteMaxVerre; }
        }

        public int CAPACITEMAXPLASTIQUE
        {
            get { return capaciteMaxPlastique; }
        }

        public int CAPACITEMAXTERRE
        {
            get { return capaciteMaxTerre; }
        }
    }
}
