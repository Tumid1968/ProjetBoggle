using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Projet_Boggle
{
    internal class Dé
    {
        private static List<string> lettresTotal;
        private static List<int> occurences;
        private static List<int> occurences_non_modifiées;
        private string face_visible;
        private static int compteur_création_dés;
        private List<string> dé_obtenu;

        public Dé()
        {
            
            this.dé_obtenu = new List<string>();


            string[] fichier = File.ReadAllLines("Lettres.txt");
            if(lettresTotal == null|| occurences==null|| occurences_non_modifiées==null ) {
                lettresTotal = new List<string>();
                occurences_non_modifiées = new List<int>();
                occurences = new List<int>();
                try
                {
                    foreach (string line in fichier)
                    {
                        string[] l1 = line.Split(';');
                        lettresTotal.Add(l1[0]);
                        occurences.Add(int.Parse(l1[2]));
                        occurences_non_modifiées.Add(int.Parse(l1[2]));

                    }
                }
                catch (IOException f)
                {
                    Console.WriteLine("Le fichier n'existe pas");
                    Console.WriteLine(f.Message);
                }
            }
            if(compteur_création_dés>=16)
            {
                reinitialiser_occurences();
            }
            
            
            
            
            Random r = new Random();
            
            while(dé_obtenu.Count<6)
            {
                int Aleatoire2= r.Next(0, lettresTotal.Count);
                
                if (occurences[Aleatoire2]>0)
                {
                    dé_obtenu.Add(lettresTotal[Aleatoire2]);
                    occurences[Aleatoire2] -= 1;
                }
                
            }
            compteur_création_dés = compteur_création_dés + 1;
            Lance(r);
            




        }

        
        private static void reinitialiser_occurences()
        {
            for(int i=0; i<occurences.Count; i++)
            {
                occurences[i] = occurences_non_modifiées[i];
            }
            compteur_création_dés = 0;
        }
        
        public List<int> Occurences
        {
            get { return occurences; }
        }
        public string Face_visible
        {
            get { return face_visible; }
            set { face_visible = value; }
        }
        public List<string> Dé_obtenu
        {
            get { return dé_obtenu; }
        }
        public List<string> LettresTotal
        {
            get { return lettresTotal; }
        }

        public void Lance(Random r)
        {           
            int Aleatoire3 = r.Next(0,dé_obtenu.Count);
            face_visible = dé_obtenu[Aleatoire3];           
        }
        public string toString()
        {
            string dé_résultat = "Le Dé obtenu est " + string.Join(" ", dé_obtenu) + " et la face visible de ce Dé est " + face_visible;
            return dé_résultat;
        }
    }
}
