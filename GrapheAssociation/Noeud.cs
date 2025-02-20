using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapheAssociation
{
    public class Noeud
    {
        private int id;
        private List<Noeud> voisins;

        public Noeud(int id)
        {
            this.id = id;
            voisins = new List<Noeud>();
        }

        public void AjouterVoisin(Noeud voisin)
        {
            if (!voisins.Contains(voisin))
            {
                voisins.Add(voisin);
                //oisin.voisins.Add(this); // Relation réciproque car le graphe est non orienté
            }
        }


        public List<Noeud> GetVoisins()
        {
            return voisins;
        }

        public string VoisinsToString()
        {
            string txt = "{";
            if (voisins.Count > 0)
            {
                txt += voisins[0].GetID();
                for (int i = 1; i < voisins.Count; i++)
                {
                    txt = txt + ", " + voisins[i].GetID();
                }
            }
            txt += "}";
            return txt;
        }

        public int GetID()
        {
            return id;
        }


    }
}
