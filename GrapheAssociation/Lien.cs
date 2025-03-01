using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapheAssociation
{
    public class Lien
    {
        private Noeud noeud1;
        private Noeud noeud2;

        public Lien(Noeud n1, Noeud n2)
        {
            this.noeud1 = n1;
            this.noeud2 = n2;
        }


        /// <summary>
        /// Cette fonction renvoie sous forme de string les deux noeuds formant le lien.
        /// </summary>
        /// <returns></returns>
        public string ToString()
        {
            return "Lien entre " + noeud1 + " et " + noeud2 + ".";
        }

        /// <summary>
        /// Ctete fonction renvoie soit le premier ou le deuxième noeud de ce lien.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public Noeud GetNoeud (int num)
        {
            Noeud noeud = noeud2;
            if (num == 1)
            {
                noeud = noeud1;
            }
            return noeud;
        }

       

    }
}
