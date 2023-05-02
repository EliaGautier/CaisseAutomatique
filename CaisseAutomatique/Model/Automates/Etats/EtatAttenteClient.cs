using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates.Etats
{
    internal class EtatAttenteClient : Etat
    {
        public EtatAttenteClient(Caisse metier, Automate automate) : base(metier, automate)
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
            switch (e)
            {
                case Evenement.SCAN:
                    Metier.EnregistreArticle();
                    Metier.ScanArticle(NouvelArticle);
                    break;
                case Evenement.PAYER:
                    Metier.EnregistreArticle();
                    if (Metier.Articles.Count > 0)
                        Metier.Payer(Metier.Reste);
                    break;
                case Evenement.AJOUTER_BALANCE:
                    Metier.PoseArticle(NouvelArticle);
                    break;
                case Evenement.RETIRER_BALANCE:
                    Metier.RetireArticle(NouvelArticle);
                    break;
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat ret;
            switch (e)
            {
                case Evenement.SCAN:
                    ret = CreateEtatAttenteClient();
                    break;
                case Evenement.PAYER:
                    if (Metier.Articles.Count > 0)
                        ret = new EtatFin(Metier, Automate);
                    else ret = CreateEtatAttenteClient();
                    break;
                default:
                    ret = CreateEtatAttenteClient();
                    break;
            }
            return ret;
        }

        private EtatAttenteClient CreateEtatAttenteClient()
        {
            return new EtatAttenteClient(Metier, Automate);
        }
    }
}
