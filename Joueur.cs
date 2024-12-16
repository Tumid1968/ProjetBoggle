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
        public Joueur(string n, int s, List<string> m)
        {
            this.nom = n;
            this.score = s;
            this.mots_Trouves = m;
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
        public bool Contain(string mot) {
            return Mots_Trouves.Contains(mot.ToUpper());  
        }
        public void Add_Mot(string mot)
        {
            if(!Contain(mot))
            {
                Mots_Trouves.Add(mot);
            }
        }
        public string toString()
        {
            string joueur_résultat = "le score de "+Nom+" est de "+Score+" grâce aux mots cités suivants"+string.Join(" ",Mots_Trouves);
            return joueur_résultat;
        }

    }
}
