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

        public override string Message => "Bonjour, scannez votre premier article !";

        public override void Action(Evenement e)
        {
            throw new NotImplementedException();
        }

        public override Etat Transition(Evenement e)
        {
            return new EtatAttenteClient(Metier);
        }
    }
}
