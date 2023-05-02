using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace CaisseAutomatique.Model.Automates.Etats
{
    internal class EtatFin : Etat
    {
        private static Timer timer;

        public EtatFin(Caisse metier, Automate automate) : base(metier, automate)
        {
            timer = null;
            if (timer == null)
            {
                timer = new Timer(2000);
                timer.Elapsed += TimerElapsed;
                timer.AutoReset = false;
                timer.Start();
            }
        }

        public override string Message => "Au revoir !";

        public override void Action(Evenement e)
        {
            if (e == Evenement.RESET)
            {
                Metier.Reset();
            }
        }

        public override Etat Transition(Evenement e)
        {
            Etat ret;
            switch (e)
            {
                case Evenement.RESET:
                    ret = new EtatAttenteClient(Metier, Automate);
                    break;
                default:
                    ret = this;
                    break;
            }
            return ret;
        }

        private void TimerElapsed(object? sender, ElapsedEventArgs e)
        {
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                this.Automate.Activer(Evenement.RESET);
            });
            timer.Dispose();
            timer = null;
        }
    }
}
