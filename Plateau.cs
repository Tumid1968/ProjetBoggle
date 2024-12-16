using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Boggle
{
    internal class Plateau
    {
        private Dé[,] ensemble_dés;
        private Dictionnaire dictionnaire; 
        public Plateau(Dictionnaire dict)
        {
            Console.WriteLine("Création d'un nouveau plateau");
            dictionnaire = dict;
            ensemble_dés = new Dé[4, 4];
            Random r = new Random();
            for(int i =0; i<4; i++)
            {
                for(int j =0; j<4; j++)
                {
                    ensemble_dés[i, j] = new Dé();
                    ensemble_dés[i, j].Lance(r);
                }
            }

        }
        public bool Test_Plateau(string mot)
        {
            if(dictionnaire.Langue == "FRANCAIS")
            {
                if (!dictionnaire.RechDichoRecursifFr1(mot.ToUpper()))
                {
                    return false;
                }
            }
            else 
            {
                if (!dictionnaire.RechDichoRecursifEn1(mot.ToUpper()))
                {
                    return false;
                }
            }
            bool[,] case_visitée = new bool[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (ensemble_dés[i, j].Face_visible[0] == mot[0] &&
                        TestRecursif(mot.ToUpper(), 0, i, j, case_visitée))
                    {
                        return true;
                    }
                    case_visitée = new bool[4, 4];

                }
            }
            return false;
        }
        private bool TestRecursif(string mot, int index, int x, int y, bool[,] case_visitée)
        {
            if (index == mot.Length) return true;

            if (x < 0 || x >= 4 || y < 0 || y >= 4 || case_visitée[x, y]) { 
                return false;
            
            }
                ;

            if (ensemble_dés[x, y].Face_visible[0] != mot[index]) {
                return false;
            };

            case_visitée[x, y] = true;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (TestRecursif(mot, index + 1, x + dx[i], y + dy[i], case_visitée))
                {
                    return true;
                }
            }

            case_visitée[x, y] = false;
            return false;
        }
        public string toString()
        {
            string plateau_résultat = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    plateau_résultat += ensemble_dés[i, j].Face_visible + " ";
                }
                plateau_résultat += "\n";
            }
            return plateau_résultat;
        }
    }
}
