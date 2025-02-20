using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapheAssociation
{
    public class Graphe
    {

        

        // Ajouter des nœuds
        //private Dictionary<int, Noeud> Noeuds { get; private set; }
        private Noeud[] listeNoeuds;
        private List<Lien> listeLiens;
        private int[,] matriceAdjacence;

        public Graphe(int numNoeuds)
        {
            listeNoeuds = new Noeud[numNoeuds];
            listeLiens = new List<Lien>();
        }

        public void AjouterNoeud(int id)
        {
            /*if (!Noeuds.ContainsKey(id))
            {
                Noeuds[id] = new Noeud(id);
            }*/
            if (listeNoeuds[id] == null)
            {
                listeNoeuds[id] = new Noeud(id);
            }
        }

        public void AjouterLien(int id1, int id2)
        {
            /*if (Noeuds.ContainsKey(id1) && Noeuds.ContainsKey(id2))
            {
                Noeud n1 = Noeuds[id1];
                Noeud n2 = Noeuds[id2];

                if (!n1.Voisins.Contains(n2))
                {
                    n1.AjouterVoisin(n2);
                    Liens.Add(new Lien(n1, n2));
                }
            }*/
            if (listeNoeuds[id1] != null && listeNoeuds[id2] != null)
            {
                Noeud n1 = listeNoeuds[id1];
                Noeud n2 = listeNoeuds[id2];
                if (!n1.GetVoisins().Contains(n2) && !n2.GetVoisins().Contains(n1))
                {
                    n1.AjouterVoisin(n2);
                    n2.AjouterVoisin(n1);
                    listeLiens.Add(new Lien(n1, n2));
                }
            }
        }

        public void ConstruireMatriceAdjacence()
        {
            int taille = listeNoeuds.Length;
            matriceAdjacence = new int[taille, taille];

            foreach (var lien in listeLiens)
            {
                int i = lien.GetNoeud(1).GetID();
                int j = lien.GetNoeud(2).GetID();
                matriceAdjacence[i, j] = 1;
                matriceAdjacence[j, i] = 1; // Graphe non orienté
            }
        }

        public void AfficherListeAdjacence()
        {
            foreach (var noeud in listeNoeuds)
            {
                /*Console.Write($"Noeud {noeud.Id} -> ");
                foreach (var voisin in noeud.Voisins)
                {
                    Console.Write($"{voisin.Id} ");
                }
                Console.WriteLine();*/
                if (noeud.GetID() != 0)
                {
                   Console.WriteLine(noeud.GetID() + " : " + noeud.VoisinsToString());
                }
               
            }
        }

        public void AfficherMatriceAdjacence()
        {
            for (int i = 1; i < listeNoeuds.Length; i++)
            {
                for (int j = 1; j < listeNoeuds.Length; j++)
                {
                    Console.Write(matriceAdjacence[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
