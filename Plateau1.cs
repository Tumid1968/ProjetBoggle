using Boggle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet_algo
{
    internal class Plateau1
    {
        private Dé[,] des;
        private Dictionnaire dictionnaire;
        private Random random;

        public Plateau1(Random random, Dictionnaire dictionnaire, Dé[,] des)
        {

            this.dictionnaire = dictionnaire;
            this.des = new Dé[4, 4];
            this.random = new Random();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.des[i, j] = new Dé(random);
                    this.des[i, j].Lance(random);
                }
            }
        }

        public string toString()
        {
            string result = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    result += des[i, j].FaceVisible + " ";
                }
                result += "\n";
            }
            return result;
        }

        public bool Test_Plateau(string mot)
        {
            if (string.IsNullOrEmpty(mot)) return false;
            if (!dictionnaire.RechDichoRecursif(mot)) return false;

            bool[,] visited = new bool[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (des[i, j].FaceVisible == mot[0] &&
                        TestRecursif(mot, 0, i, j, visited))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool TestRecursif(string mot, int index, int x, int y, bool[,] visited)
        {
            if (index == mot.Length) return true;

            if (x < 0 || x >= 4 || y < 0 || y >= 4 || visited[x, y]) return false;

            if (des[x, y].FaceVisible != mot[index]) return false;

            visited[x, y] = true;

            int[] dx = { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = { -1, 0, 1, -1, 1, -1, 0, 1 };

            for (int i = 0; i < 8; i++)
            {
                if (TestRecursif(mot, index + 1, x + dx[i], y + dy[i], visited))
                {
                    return true;
                }
            }

            visited[x, y] = false;
            return false;
        }


    }
}
