using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class Joueur
    {
        private string nom;
        private int score;
        private List<string> mots_Trouves;
        private List<string> mots_Trouves_pour_Nuage;

        public Joueur(string n, int s, List<string> m)
        {
            this.nom = n;
            this.score = s;
            this.mots_Trouves = new List<string>();
            this.mots_Trouves_pour_Nuage = new List<string>(mots_Trouves);
        }
        public string Nom
        {
            get { return nom; }
        }
        public int Score
        {
            get { return score; }
            set { score = value; }
        }
        public List<string> Mots_Trouves
        {
            get { return mots_Trouves; }
            set { mots_Trouves = value; }
        }
        public List<string> Mots_Trouves_pour_Nuage
        {
            get { return mots_Trouves_pour_Nuage; } 
        }
        public bool Contain(string mot) {
            return Mots_Trouves.Contains(mot.ToUpper());  
        }
        public void Add_Mot(string mot)
        {
            mots_Trouves_pour_Nuage.Add(mot.ToUpper()); // Ajoute toujours à la liste des originaux

            if (!Contain(mot)) // Ajoute aux mots uniques seulement si absent
            {
                mots_Trouves.Add(mot.ToUpper());
            }
        }
       
        public string toString()
        {
            string joueur_résultat = "le score de "+Nom+" est de "+Score+" grâce aux mots cités suivants"+string.Join(" ",Mots_Trouves);
            return joueur_résultat;
        }

    }
}
