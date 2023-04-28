using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates
{
    internal abstract class Etat
    {
        private Caisse metier;

        protected Caisse Metier { get => metier; }

        /// <summary>
        /// Message associé à l'état
        /// </summary>
        public abstract string Message { get; }

        public Etat(Caisse metier)
        {
            this.metier = metier;
        }

        /// <summary>
        /// Initie une transition vers un nouvel état
        /// </summary>
        /// <param name="e">Événement provoquant la transition</param>
        /// <returns>Le nouvel État</returns>
        public abstract Etat Transition(Evenement e);

        /// <summary>
        /// Effectue l'action associée à un événement
        /// </summary>
        /// <param name="e">L'événement déclenchant l'action</param>
        public abstract void Action(Evenement e);
    }
}
