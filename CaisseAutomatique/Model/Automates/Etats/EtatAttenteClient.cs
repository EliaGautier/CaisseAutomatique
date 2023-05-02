using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates.Etats
{
    internal class EtatAttenteClient : Etat
    {
        public EtatAttenteClient(Caisse metier) : base(metier)
        {
        }

        public override string Message 
        { 
            get 
            {
                string ret = "Bonjour, scannez votre premier article !";
                if (NouvelArticle != null) ret = NouvelArticle.Nom;
                return ret;
            } 
        }

        public override void Action(Evenement e)
        {
            if(e == Evenement.SCAN)
            {
                Metier.EnregistreArticle();
                Metier.ScanArticle(NouvelArticle);
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat ret = new EtatAttenteClient(Metier);
            if (e == Evenement.SCAN)
            {
                ret = new EtatAttenteClient(Metier);
            }
            return ret;
        }
    }
}
