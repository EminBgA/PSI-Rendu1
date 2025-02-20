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

        public string ToString()
        {
            return "Lien entre " + noeud1 + " et " + noeud2 + ".";
        }

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
