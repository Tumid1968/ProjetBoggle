using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    internal class Dictionnaire
    {
        private List<string> mots;
        private string langue;
        public Dictionnaire(string fichier, string langue)
        {
            this.langue = langue;
            mots = new List<string>();
            try
            {
                string[] lignes = File.ReadAllLines(fichier);
                foreach (string c in lignes)
                {
                    string[] motsL = c.Split(' ');
                    foreach(string s in motsL)
                    {
                        mots.Add(s.ToLower());
                    }
                }
                mots.Sort();
            }
        

            catch(FileNotFoundException f)
            {
                Console.WriteLine("Fichier Introuvable"+f.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur" + e.Message);
            }
            
            
        }
        public string toString()
        {
            string result = "Dictionnaire en " + langue + ":\n";
            result = result + "Nombre de mots : " + mots.Count + "\n";
            return result;
        }
        public bool RechDichoRecursif(string mot, int d, int f)
        {
            mot = mot.ToLower();
            if (d > f)
            {
                return false;
            }
            int mid = (d + f) / 2;
            
            int compar = string.Compare(mots[mid], mot);
            if(compar == 0)
            {
                return true;
            }
            else if(compar < 0) {
                return RechDichoRecursif(mot, mid + 1, f);

            }
            else
            {
                return RechDichoRecursif(mot, d, mid - 1);
            }
            
        }
        public bool RechDichoRecursif(string mot)
        {
            mot = mot.ToLower();
            return RechDichoRecursif(mot, 0, mots.Count - 1);
        }
    }
}
