using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates.Etats
{
    internal class EtatFin : Etat
    {
        public EtatFin(Caisse metier) : base(metier)
        {
        }

        public override string Message => "Au revoir !";

        public override void Action(Evenement e)
        {
        }

        public override Etat Transition(Evenement e)
        {
            return new EtatFin(Metier);
        }
    }
}
