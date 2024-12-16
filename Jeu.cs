using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class Jeu
    {
        private List<Joueur> joueurs;
        private Plateau plateau;
        private Dictionnaire dictionnaire;
        private int durée_partie;
        private List<int> scores;
        private List<char> lettre;
        private int duree_tour;
        public Jeu()
        {
            this.dictionnaire = null;
            this.joueurs = new List<Joueur>();
            this.plateau = null;
            this.durée_partie = 2;
            this.duree_tour = 60;
            this.lettre = new List<char>();
            this.scores = new List<int>();
            string[] fichier = File.ReadAllLines("Lettres.txt");
            try
            {
                foreach (string line in fichier)
                {
                    string[] l1 = line.Split(';');
                    lettre.Add(char.Parse(l1[0]));
                    scores.Add(int.Parse(l1[1]));
                    

                }
            }
            catch (IOException f)
            {
                Console.WriteLine("Le fichier n'existe pas");
                Console.WriteLine(f.Message);
            }
        }
        public List<int> Scores
        {
            get { return scores; }
            set { scores = value; }
        }
        public void Configuration_du_jeu()
        {
            Console.WriteLine("Choisissez une langue");
            string langue = Console.ReadLine().ToUpper();
            dictionnaire = new Dictionnaire(langue);
            for(int i =1; i <= 2; i++)
            {
                Console.WriteLine("Entrez le nom du joueur " + i);
                string nom= Console.ReadLine().Trim();
                Add_Joueur(nom, 0, new List<string>());
            }
        }
        public void Add_Joueur(string nom,int score,List<string>mots_trouvés)
        {
            joueurs.Add(new Joueur(nom,score,mots_trouvés));
        }
        public void démarrer_une_partie()
        {
            Console.WriteLine("La partie Boggle commence");
            DateTime début_de_partie= DateTime.Now;
            while ((DateTime.Now - début_de_partie).TotalMinutes < durée_partie)
            {
                foreach(Joueur joueur in joueurs)
                {
                    if ((DateTime.Now - début_de_partie).TotalMinutes >= durée_partie)
                    {
                        break;
                    }
                    Console.WriteLine("\n C'est au tour de " + joueur.Nom + " de jouer");
                    try
                    {
                        Console.WriteLine("Création du plateau pour ce joueur...");
                        plateau = new Plateau(dictionnaire);
                        Console.WriteLine("Plateau créé avec succès !");
                        Console.WriteLine(plateau.toString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Erreur lors de la création du plateau "+ex.Message);
                        continue;
                    }
                    List<string> mots_du_tour = new List<string>();
                    DateTime début_du_tour = DateTime.Now;
                    while ((DateTime.Now - début_du_tour).TotalSeconds < duree_tour)
                    {
                        if ((DateTime.Now - début_du_tour).TotalSeconds >= duree_tour)
                        {
                            Console.WriteLine("Le temps est écoulé pour " + joueur.Nom + ".");
                            break;
                        }
                        Console.WriteLine("Saisissez un nouveau mot trouvé");
                        string mot = Console.ReadLine().ToUpper();
                        if ((DateTime.Now - début_du_tour).TotalSeconds >= duree_tour)
                        {
                            Console.WriteLine("Le temps est écoulé pour " + joueur.Nom + ".");
                            break;
                        }
                        if (mot.Length < 2)
                        {
                            Console.WriteLine("Le nombre minimum requis de caractères est de 2");
                            continue;

                        }
                        if (mots_du_tour.Contains(mot.ToUpper()))
                        {
                            Console.WriteLine("Le joueur "+joueur.Nom+" a déjà saisis ce mot.");
                            continue;
                        }
                        
                        if(!dictionnaire.RechDichoRecursifFr1(mot.ToUpper()) && dictionnaire.Langue=="FRANCAIS")
                        {
                            Console.WriteLine("Le mot n'est pas trouvé dans le dictionnaire");
                            continue;
                        }
                        else if(dictionnaire.Langue=="ANGLAIS"&& !dictionnaire.RechDichoRecursifEn1(mot.ToUpper()))
                        {
                            Console.WriteLine("Le mot n'est pas trouvé dans le dictionnaire");
                            continue;


                        }
                        if (!plateau.Test_Plateau(mot))
                        {
                            Console.WriteLine("Le mot est invalide sur le plateau");
                            continue;
                        }
                        mots_du_tour.Add(mot);
                        int score_du_mot=calculer_score_mot(mot);
                        joueur.Score=joueur.Score+score_du_mot;
                        joueur.Add_Mot(mot);
                        afficher_score_de_mots(joueur);
                    }
                }
            }
            Fin_de_partie();
        }
        private void afficher_score_de_mots(Joueur j)
        {
            Console.WriteLine("Le score de " + j.Nom + " est de " + j.Score + " grâce aux mots cités suivants");
            foreach(string m in j.Mots_Trouves)
            {
                Console.Write(m+" ");
            }Console.WriteLine();
        }
        private int calculer_score_mot(string mot)
        {
            int score = 0;
            foreach(char l in mot)
            {
                int i = lettre.IndexOf(l);
                if (i != -1)
                {
                    score = score + Scores[i];
                }
            }
            return score;
                
        }
        private void GenererNuagePourJoueurs()
        {
            foreach (Joueur joueur in joueurs)
            {
                Console.WriteLine($"Génération du nuage de mots pour {joueur.Nom}...");

                // Créer un dictionnaire mot -> score
                Dictionary<string, int> motsScores = joueur.Mots_Trouves
                    .ToDictionary(mot => mot, mot => calculer_score_mot(mot));

                // Générer le nuage de mots
                string cheminFichier = $"{joueur.Nom}_NuageDeMots.png";
                NuageDeMots nuage = new NuageDeMots(joueur.Mots_Trouves, motsScores);
                nuage.GenererNuage(cheminFichier);
            }
        }

        private void Fin_de_partie()
        {
            Console.WriteLine("\n La partie est terminée");

            for(int i=0; i<joueurs.Count; i++)
            {
                Joueur joueur = joueurs[i];
                Console.WriteLine(joueur.Nom+" a un score de "+joueur.Score);
            }
            Joueur Le_joueur_gagnant = calculer_le_gagnant();
            Console.WriteLine("Le gagnant de cette partie est "+Le_joueur_gagnant.Nom+" avec "+Le_joueur_gagnant.Score+" points.");
            GenererNuagePourJoueurs();

        }
        private Joueur calculer_le_gagnant()
        {
            Joueur le_joueur_gagnant = joueurs[0];
            for(int i = 1; i < joueurs.Count; i++)
            {
                if (joueurs[i].Score > le_joueur_gagnant.Score)
                {
                    le_joueur_gagnant= joueurs[i];
                }
            }
            return le_joueur_gagnant;
        }
    }
}
