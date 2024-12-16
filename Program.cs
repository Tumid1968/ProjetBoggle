using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            
            Jeu jeu = new Jeu();
            jeu.Configuration_du_jeu();
            jeu.démarrer_une_partie();

            Joueur gagnant = jeu.calculer_le_gagnant();

            NuageDeMots nuage = new NuageDeMots(800, 600);
            nuage.generer_nuage(gagnant, "nuage_gagnant.png");
            Console.WriteLine("Fin de la partie. Le nuage de mots a été généré pour le joueur gagnant.");
            /*Jeu jeu = new Jeu();

               Console.WriteLine("Configuration du jeu");
               jeu.Configuration_du_jeu();

               Console.WriteLine("Lancement de la partie !");
               jeu.démarrer_une_partie();



               Console.WriteLine("Fin du programme.");*/
            Console.ReadKey();
            
        }
    }
}
