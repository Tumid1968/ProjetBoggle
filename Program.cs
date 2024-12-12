using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string Fichier = "lettres.txt"; 

            try
            {
                Console.WriteLine("Création d'un dé :");
                Dé de = new Dé(Fichier);
                Console.WriteLine(de.toString());

                Random random = new Random();

                Console.WriteLine("Lancement du dé :");
                de.Lance(random);
                Console.WriteLine($"Face visible: {de.FaceVisible}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur pendant l'exécution : {e.Message}");
            }

            /*Dictionnaire dictionnaireFr = new Dictionnaire("MotsPossiblesFR.txt", "Français");

             Console.Write(dictionnaireFr.toString());

             string mot = "PENIS";
             if (dictionnaireFr.RechDichoRecursif(mot))
             {

                 Console.WriteLine("Le mot " + mot.ToUpper() + " est dans le dictionnaire francais.");
                 Console.WriteLine();
             }
             else
             {
                 Console.WriteLine("Le mot " + mot.ToUpper() + " n'est pas dans le dictionnaire francais.");
                 Console.WriteLine() ;
             }

             Dictionnaire dictionnaireEn = new Dictionnaire("MotsPossiblesEN.txt", "Anglais");

             Console.Write(dictionnaireEn.toString());

             mot = "Khelil";
             if (dictionnaireEn.RechDichoRecursif(mot))
             {
                 Console.WriteLine("Le mot "+ mot.ToUpper() + " est dans le dictionnaire anglais.");
                 Console.WriteLine();
             }
             else
             {
                 Console.WriteLine("Le mot " + mot.ToUpper() + " n'est pas dans dictionnaire anglais.");
                 Console.WriteLine();
             }
            */


            Console.WriteLine("Appuyez sur une touche pour quitter...");
            Console.ReadKey();  

        }
    }
}
