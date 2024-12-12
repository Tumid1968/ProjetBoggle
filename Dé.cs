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
        private List<(char let, int NbreDeFois)> lettreNombre;
        private char faceVisible;
        public Dé(string fichier)
        {
            faces = new List<char>();
            lettreNombre = new List<(char let, int NbreDeFois)>();
            Random r = new Random();
            try
            {
                string[] ligne = File.ReadAllLines(fichier); // Ca ca lit le fichier et ca donne quoi comme info?
                foreach (string l in ligne)
                {
                    string[] compo = l.Split(';');
                    if (compo.Length==3)
                    {
                        char le = compo[0][0];
                        int NbreDeFois = int.Parse(compo[2]);
                        lettreNombre.Add((le, NbreDeFois));
                    }
                }
                List<char> fA = new List<char>();
                while (fA.Count < 6 && lettreNombre.Count>0)
                {
                    int i = r.Next(lettreNombre.Count); 
                    var (lettrePr, Restants) = lettreNombre[i];


                    if (Restants > 0)
                    {
                        fA.Add(lettrePr);
                        lettreNombre[i] = (lettrePr, Restants - 1);
                        if (lettreNombre[i].NbreDeFois ==0)
                        {
                            lettreNombre.RemoveAt(i);
                        }
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
        public List<char> Faces
        {
            get { return faces; }
            set { faces = value; }
        }
        public List<(char let, int NbreDeFois)> LettreNombre
        {
            get { return lettreNombre; }
            set { lettreNombre = value; }   
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
