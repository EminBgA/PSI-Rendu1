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
        private string nom; 
        private List<Noeud> voisins;

        public Noeud(int id, string nom)
        {
            this.id = id;
            voisins = new List<Noeud>();
            this.nom = nom;
        }

        /// <summary>
        /// Cette fonction ajouter un voisin à la liste de tous les voisins de ce noeud.
        /// </summary>
        /// <param name="voisin"></param>
        public void AjouterVoisin(Noeud voisin)
        {
            if (!voisins.Contains(voisin))
            {
                voisins.Add(voisin);
            }
        }

        /// <summary>
        /// Cette fonction renvoie la liste de tous les voisins de ce noeud.
        /// </summary>
        /// <returns></returns>
        public List<Noeud> GetVoisins()
        {
            return voisins;
        }

        public string GetNom()
        {
            return nom;
        }

        /// <summary>
        /// Cette fonction renvoie tous les voisins de ce noeud sous forme de string.
        /// </summary>
        /// <returns></returns>
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


        /// <summary>
        /// Cette fonction renvoie le numéro d'identification du membre.s
        /// </summary>
        /// <returns></returns>
        public int GetID()
        {
            return id;
        }


    }
}
