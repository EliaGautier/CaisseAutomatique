using CaisseAutomatique.Model.Articles;
using CaisseAutomatique.Model.Automates.Etats;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model.Automates
{
    internal class Automate : INotifyPropertyChanged
    {
        private Caisse metier;
        private Etat etatCourant;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Message => etatCourant.Message;

        public Article NouvelArticle { get => etatCourant.NouvelArticle; set => etatCourant.NouvelArticle = value; }

        public Automate(Caisse metier)
        {
            this.metier = metier;
            etatCourant = new EtatAttenteClient(metier);

        }

        public void Activer(Evenement e)
        {
            etatCourant.Action(e);
            etatCourant.Transition(e);
            NotifyPropertyChanged("Message");
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
