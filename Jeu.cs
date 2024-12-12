using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    internal class Jeu
    {
        private Plateau plateau;
        private List<Joueur> khelil;
        private int tempsPassé;
        private List<(char lettre, int score)> lettre_score;
        private Random random;
        public Jeu(Random random,int tempsPassé = 6)
        {
            this.random = random;
            this.tempsPassé=tempsPassé;
            khelil = new List<Joueur>();
            plateau = new Plateau(random, new Dictionnaire("Mots_PossiblesFR.txt", "fr"), new Dé[4, 4]);
            lettre_score = donne_score_lettre("Lettres.txt");
        }
        private List<(char lettre, int score)> donne_score_lettre(string fichier)
        {
            List<(char lettre, int score)> lettres = new List<(char lettre, int score)>();
            try
            {
                string[] l = File.ReadAllLines(fichier);
                foreach(string s in l)
                {
                    string[] c = s.Split(';');
                    if(c.Length >= 2)
                    {
                        char le = c[0][0];
                        int score = int.Parse(c[1]);
                        lettres.Add((le,score));
                    }
                }
            }
            catch(FileNotFoundException f)
            {
                Console.WriteLine("Fichier introuvable"+f.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur lors du chargement du message "+e.Message);
            }
            return lettres;
        }
        public void AjouterKhelil(string prenom)
        {
            khelil.Add(new Joueur(prenom, 0, new string[] { }));
            
        }
        public void Demarrer()
        {
            Console.WriteLine("partie de Boggle commence");
            DateTime finpartie= DateTime.Now.AddMinutes(tempsPassé);
            int numKhelil = 0;
            while(DateTime.Now<finpartie)
            {
                Joueur khelilactuel = khelil[numKhelil];
                Console.WriteLine("C'est au tour de " + khelilactuel.Nom);
                Console.WriteLine(plateau.toString());
                DateTime fin_du_tour = DateTime.Now.AddMinutes(1);
                while (DateTime.Now < fin_du_tour)
                {
                    Console.WriteLine("Ecris un mot");
                    string mot = Console.ReadLine();
                    if (mot.Length < 2)
                    {
                        Console.WriteLine("Mot invalide : doit contenir au moins 2 lettres.");
                        continue;
                    }
                    if(!plateau.Test_Plateau(mot))
                    {
                        Console.WriteLine("Mot invalide : n'est pas formable à partir des dés.");
                        continue;
                    }
                    if (khelilactuel.Contain(mot))
                    {
                        Console.WriteLine("Mot déjà trouvé.");
                        continue;
                    }
                    khelilactuel.Add_Mot(mot);
                    int ScoreM = 0;
                    foreach(char lettre in mot.ToUpper())
                    {
                        foreach(var(lettre_utilisé,score) in lettre_score)
                        {
                            if(lettre==lettre_utilisé)
                            {
                                ScoreM = ScoreM + score;
                                break;

                            }
                        }
                    }
                    khelilactuel.Score = khelilactuel.Score + ScoreM;
                    Console.WriteLine("Mot validé : "+mot+" et son score est : "+ScoreM);


                }
                numKhelil=(numKhelil+1)%khelil.Count;
            }
            Console.WriteLine("Le temps est fini, vous pouvez donc voir les résultats ci-dessous");
            foreach(Joueur j in khelil)
            {
                Console.WriteLine(j.toString());
            }
        }



    }
}
