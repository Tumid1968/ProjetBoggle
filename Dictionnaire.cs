using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class Dictionnaire
    {
        private List<string> dictionnaireFr;
        private List<string> dictionnaireEn;
        private string langue;
        public Dictionnaire (string l)
        {
            this.langue = l;
            this.dictionnaireFr = new List<string> ();
            this.dictionnaireEn = new List<string>();
            StreamReader sReader = null;
            if (langue == "FRANCAIS")
            {
                try
                {
                    sReader = new StreamReader("Mots_PossiblesFR.txt");
                    string line;
                    while ((line = sReader.ReadLine()) != null)
                    {
                        string[] l1 = line.Split(' ');
                        foreach (string l2 in l1)
                        {
                            dictionnaireFr.Add(l2.Trim().ToUpper());
                        }
                    }

                    dictionnaireFr = tri_fusion(dictionnaireFr);
                }
                catch (IOException f)
                {
                    Console.WriteLine("Le fichier n'existe pas");
                    Console.WriteLine(f.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sReader != null)
                    {
                        sReader.Close();
                    }
                }
            }
            else if(langue =="ANGLAIS")
            {
                try
                {
                    sReader = new StreamReader("Mots_PossiblesEn.txt");
                    string k;
                    while ((k = sReader.ReadLine()) != null)
                    {
                        string[] l1 = k.Split(' ');
                        foreach (string l2 in l1)
                        {
                            dictionnaireEn.Add(l2.Trim().ToUpper());
                        }
                    }

                    dictionnaireEn = tri_fusion(dictionnaireEn);
                }
                catch (IOException f)
                {
                    Console.WriteLine("Le fichier n'existe pas");
                    Console.WriteLine(f.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    if (sReader != null)
                    {
                        sReader.Close();
                    }
                }
            }
            
        }
        public string Langue
        {
            get { return langue; }
            set { langue = value; }
        }

        public List<string> tri_fusion(List<string> l) {
            if (l.Count <= 1)
            {
                return l;
            }
            int mid = l.Count / 2;
            List<string> partie_gauche= new List<string>(); 
            List<string> partie_droite= new List<string>();
            for(int i =0; i < l.Count; i++)
            {
                if (i < mid)
                {
                    partie_gauche.Add(l[i]);
                }
                else
                {
                    partie_droite.Add(l[i]);
                }
            }
            partie_gauche=tri_fusion(partie_gauche);
            partie_droite=tri_fusion(partie_droite);
            return fusion(partie_gauche, partie_droite);

        
        }
        private static List<string> fusion(List<string> partie_gauche,List<string> partie_droite)
        {
            List<string> tri_fini=new List<string>();
            int i_gauche = 0;
            int i_droite = 0;
            while(i_droite<partie_droite.Count && i_gauche < partie_gauche.Count)
            {
                if (partie_gauche[i_gauche].CompareTo(partie_droite[i_droite])<=0)
                {
                    tri_fini.Add(partie_gauche[i_gauche]);
                    i_gauche++;
                }
                else
                {
                    tri_fini.Add(partie_droite[i_droite]);
                    i_droite++;
                }
            }
            while (i_gauche < partie_gauche.Count)
            {
                tri_fini.Add(partie_gauche[i_gauche]); 
                i_gauche++;
            }
            while (i_droite < partie_droite.Count)
            {
                tri_fini.Add(partie_droite[i_droite]);
                i_droite++; 
            }
            return tri_fini;
        }
        public bool RechDichoRecursifFr(string mot, int d, int f)
        {
            mot = mot.ToUpper();
            if (d > f)
            {
                return false;
            }
            int mid = (d + f) / 2;
            

            int compar = string.Compare(dictionnaireFr[mid], mot.ToUpper());
            if (compar == 0)
            {
                return true;
            }
            else if (compar < 0)
            {
                return RechDichoRecursifFr(mot, mid + 1, f);

            }
            else
            {
                return RechDichoRecursifFr(mot, d, mid - 1);
            }

        }
        public bool RechDichoRecursifFr1(string mot)
        {
            mot = mot.ToUpper();
            return RechDichoRecursifFr(mot.ToUpper(), 0, dictionnaireFr.Count - 1);
        }
        public bool RechDichoRecursifEn(string mot, int d, int f)
        {
            mot = mot.ToUpper();
            if (d > f)
            {
                return false;
            }
            int mid = (d + f) / 2;


            int compar = string.Compare(dictionnaireEn[mid], mot.ToUpper());
            if (compar == 0)
            {
                return true;
            }
            else if (compar < 0)
            {
                return RechDichoRecursifEn(mot, mid + 1, f);

            }
            else
            {
                return RechDichoRecursifEn(mot, d, mid - 1);
            }

        }
        public bool RechDichoRecursifEn1(string mot)
        {
            mot = mot.ToUpper();
            return RechDichoRecursifEn(mot.ToUpper(), 0, dictionnaireEn.Count - 1);
        }
        public List<string> Tri_par_quickSort(List<string> liste)
        {
            if (liste.Count <= 1)
                return liste; // Rien à trier

            string pivot = liste[liste.Count / 2];

            List<string> moins = new List<string>();
            List<string> egaux = new List<string>();
            List<string> plus = new List<string>();

            foreach (string element in liste)
            {
                if (string.Compare(element, pivot) < 0)
                {
                    moins.Add(element);
                }
                else if (string.Compare(element, pivot) == 0)
                {
                    egaux.Add(element);
                }
                else
                {
                    plus.Add(element);
                }
            }

            List<string> resultat = new List<string>();
            resultat.AddRange(Tri_par_quickSort(moins));
            resultat.AddRange(egaux);
            resultat.AddRange(Tri_par_quickSort(plus));

            return resultat;
        }
        public List<string> TriParInsertion(string langue)
        {
            List<string> resultat = new List<string>();

            if (langue.ToUpper() == "FRANCAIS")
            {
                resultat = new List<string>(dictionnaireFr); 
            }
            else if (langue.ToUpper() == "ANGLAIS")
            {
                resultat = new List<string>(dictionnaireEn); 
            }
            else
            {
                Console.WriteLine("Langue non reconnue. Choisissez 'FRANCAIS' ou 'ANGLAIS'.");
                return resultat; 
            }

            for (int i = 1; i < resultat.Count; i++)
            {
                string current = resultat[i];
                int j = i - 1;

                while (j >= 0 && string.Compare(resultat[j], current) > 0)
                {
                    resultat[j + 1] = resultat[j];
                    j--;
                }

                resultat[j + 1] = current;
            }

            return resultat;
        }




        public string toString()
        {
            string dictionnaire_résultat;
            if(langue == "FRANCAIS")
            {
                dictionnaire_résultat = "Langue: " + Langue + " et le dictionnaire contient " + dictionnaireFr.Count;
            }
            else
            {
                dictionnaire_résultat = "Langue: " + Langue + " et le dictionnaire contient " + dictionnaireEn.Count;

            }
            return dictionnaire_résultat;
        }





    }
}
