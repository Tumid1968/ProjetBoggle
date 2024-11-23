using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    internal class Joueur
    {
        private string nom;
        private long score;
        private List<string> mots;
        public Joueur(string n,long s,string[] m)
        {
            this.nom = n;
            this.score = s;
            this.mots = new List<string>();
        }
        public string Nom
        {
            get { return nom; }
        }
        public long Score
        {
            get { return score; }
            set { score = value; }
        }
        
        public bool Contain(string mot )
        {
            return mots.Contains( mot );
        }
        public void Add_Mot(string mot)
        {
            if (!Contain(mot))
            {
                mots.Add( mot);
            }
            
        }
        public string toString()
        {
            string res = "Le nom est "+Nom+" et le score est "+Score+" et les mots trouvés sont "+string.Join(", ", mots);
            return res;
        }


    }
}
