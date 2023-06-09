﻿using CaisseAutomatique.Model.Articles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CaisseAutomatique.Model 
{
    /// <summary>
    /// Caisse automatique (couche métier)
    /// </summary>
    public class Caisse : INotifyPropertyChanged
    {
        public double Reste => PrixTotal - SommePayee;

        /// <summary>
        /// Liste des articles enregistrés
        /// </summary>
        private List<Article> articles;
        public List<Article> Articles => articles;

        /// <summary>
        /// Dernier article scanné (dans un vrai modèle, seule la référence est connue et on utiliserait une Bdd pour retrouver l'article)
        /// </summary>
        public Article DernierArticleScanne => dernierArticleScanne;
        private Article dernierArticleScanne;

        /// <summary>
        /// Quantité saisie pour les articles dénombrables
        /// </summary>
        public int QuantiteSaise => quantiteSaisie;
        private int quantiteSaisie;
        

        /// <summary>
        /// Poids des objets sur la balance
        /// </summary>
        public double PoidsBalance => poidsBalance;
        private double poidsBalance;

        /// <summary>
        /// Somme déjà payée par l'utilisateur
        /// </summary>
        public double SommePayee => sommePayee;
        private double sommePayee;

        /// <summary>
        /// Poids attendu dans la balance
        /// </summary>
        public double PoidsAttendu
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Poids;
                return res;
            }
        }

        /// <summary>
        /// Prix total
        /// </summary>
        public double PrixTotal
        {
            get
            {
                double res = 0;
                foreach (Article article in articles) res += article.Prix;
                return res;
            }
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        public Caisse()
        {
            InitAttributes();
        }

        private void InitAttributes()
        {
            this.articles = new List<Article>();
            this.dernierArticleScanne = null;
            this.poidsBalance = 0;
            this.sommePayee = 0;
        }

        /// <summary>
        /// Saisie d'une quantité pour un article dénombrable
        /// </summary>
        /// <param name="valeur">Valeur de la quantité</param>

        public void SaisieQuantite(int valeur)
        {
            this.quantiteSaisie = valeur;
        }

        /// <summary>
        /// Scan d'un article mais ne l'enregistre pas
        /// </summary>
        /// <param name="article">L'article scanné</param>
        public void ScanArticle(Article article)
        {
            this.dernierArticleScanne = article;
            quantiteSaisie++;
        }

        /// <summary>
        /// Enregistre le dernier article scanné
        /// </summary>
        public void EnregistreArticle()
        {
            for (int i = 0; i<quantiteSaisie; i++)
                Articles.Add(this.dernierArticleScanne);
            quantiteSaisie = 0;
            NotifyPropertyChanged("Articles");
        }

        /// <summary>
        /// Pattern d'observable
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Paye une somme à la caisse
        /// </summary>
        /// <param name="somme">Valeur payée</param>
        public void Payer(double somme)
        {
            sommePayee += somme;
            NotifyPropertyChanged("SommePayee");
        }

        /// <summary>
        /// Réinitialise tous les attributs de la caisse
        /// </summary>
        public void Reset()
        {
            InitAttributes();
            this.NotifyPropertyChanged("Reset");
        }

        /// <summary>
        /// Ajoute un article dans la balance
        /// </summary>
        /// <param name="article">L'article ajouté</param>
        public void PoseArticle(Article article)
        {
            poidsBalance += article.Poids;
            NotifyPropertyChanged("PoidsBalance");
        }

        /// <summary>
        /// Retire un article de la balance
        /// </summary>
        /// <param name="article">L'article retiré</param>
        public void RetireArticle(Article article)
        {
            poidsBalance -= article.Poids;
            NotifyPropertyChanged("PoidsBalance");
        }
    }
}
