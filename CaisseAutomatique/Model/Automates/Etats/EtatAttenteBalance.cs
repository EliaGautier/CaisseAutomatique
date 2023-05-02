using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates.Etats
{
    internal class EtatAttenteBalance : Etat
    {
        public EtatAttenteBalance(Caisse metier, Automate automate) : base(metier, automate)
        {
        }

        public override string Message => String.Format("Veuillez poser votre article dans la balance");

        public override void Action(Evenement e)
        {
            switch (e) {
                case Evenement.AJOUTER_BALANCE:
                    ActionAjoutBalance();
                    break;
                case Evenement.RETIRER_BALANCE:
                    Metier.RetireArticle(NouvelArticle);
                    break;
            }
        }

        private void ActionAjoutBalance()
        {
            Metier.PoseArticle(NouvelArticle);
            if (Metier.PoidsBalance == Metier.PoidsAttendu + NouvelArticle.Poids)
            {
                Metier.EnregistreArticle();
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat ret;
            switch (e)
            {
                case Evenement.AJOUTER_BALANCE:
                    if (Metier.PoidsBalance == Metier.PoidsAttendu)
                    {
                        ret = new EtatAttenteClient(Metier, Automate);
                        ret.NouvelArticle = NouvelArticle;
                    }
                    else ret = this;
                    break;
                default:
                    ret = this;
                    break;
            }
            return ret;
        }
    }
}
