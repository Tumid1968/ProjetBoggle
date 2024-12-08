using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boggle
{
    internal class Dé
    {
        private List<char> faces;
        private char faceVisible;
        public Dé(string fichier)
        {
            faces = new List<char>();
            Random r = new Random();
            try
            {
                string[] ligne = File.ReadAllLines(fichier); // Ca ca lit le fichier et ca donne quoi comme info?
                foreach (string l in ligne)
                {
                    string[] compo = l.Split(';');
                    if (compo.Length > 0 && compo[0].Length>0)
                    {
                        faces.Add(compo[0][0]); 
                    }
                }
                List<char> fA = new List<char>();
                while (fA.Count < 6)
                {
                    int i = r.Next(faces.Count); 
                    char lettrePrise = faces[i];

                    if (!fA.Contains(lettrePrise))
                    {
                        fA.Add(lettrePrise);
                    }
                }

                faces = fA;
                if (faces.Count != 6)
                {
                    Console.WriteLine("Le fichier doit contenir exactement 6 lettres");
                }

            }
            catch (FileNotFoundException f) 
            {
                Console.WriteLine("Le fichier n'existe pas " + f.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public char FaceVisible
        {
            get { return faceVisible; }

        }

        public void Lance (Random r)
        {
            if(faces.Count == 6)
            {
                int LettreAleat = r.Next(0, faces.Count);
                faceVisible = faces[LettreAleat];
                
            }
            else
            {
                Console.WriteLine("erreur");
            }

        }
        public string toString()
        {
            string résultat = " Le Dé avec faces : " + string.Join(", ", faces) + " et la face visible est " + faceVisible;
            return résultat;
        }
    }
}
