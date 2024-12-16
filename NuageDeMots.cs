using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class NuageDeMots
    {
        private int largeur;
        private int hauteur;

        public NuageDeMots(int largeur, int hauteur)
        {
            this.largeur = largeur;
            this.hauteur = hauteur;
        }
        public void generer_nuage(Joueur joueur_gagnant,string chemin_du_fichier)
        {
            if(joueur_gagnant.Mots_Trouves.Count == 0)
            {
                Console.WriteLine("Aucun mot trouvé par le joueur gagnant pour générer un nuage.");
                return;
            }
            using (Bitmap bitmap = new Bitmap(largeur, hauteur))
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                Random random = new Random();
                foreach(var mot in joueur_gagnant.Mots_Trouves)
                {
                    int repetitions = 0;
                    foreach (var m in joueur_gagnant.Mots_Trouves_pour_Nuage)
                    {
                        if (m == mot)
                        {
                            repetitions++;
                        }
                    }
                    int taille_police = 10+repetitions*10;
                    Font police = new Font("Arial", taille_police);
                    Brush pinceau = new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256),random.Next(256)));
                    int x = random.Next(0, largeur - 100);
                    int y = random.Next(0, hauteur - 100);
                    g.DrawString(mot,police,pinceau,new PointF(x,y));
                }
                bitmap.Save(chemin_du_fichier,ImageFormat.Png);
                Console.WriteLine("Le nuage de mots a été généré :" + chemin_du_fichier);
            }
            

            
        }

        

        

    }
}
