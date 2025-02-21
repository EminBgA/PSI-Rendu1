using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
        private int numNoeuds;

        public Graphe(int numNoeuds)
        {
            listeNoeuds = new Noeud[numNoeuds];
            listeLiens = new List<Lien>();
            this.numNoeuds = numNoeuds;
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

        //public Dictionary<int, List<int>> ListeAdjacence = new Dictionary<int, List<int>>();
        
        
        // Ajoute une connexion entre deux sommets
        

        // Parcours en Largeur (BFS)
        public bool[] ParcoursLargeur(int sommetInitial)
        {
            // Création d'une file pour gérer l'ordre de visite
            bool[] visites = new bool[numNoeuds];
            // Un ensemble pour suivre les sommets visités
            List<int> file = new List<int>();

            // Ajouter le sommet initial à la file
            file.Add(sommetInitial);
            visites[sommetInitial] = true;

            Console.WriteLine("Parcours en Largeur (BFS) :");

            while (file.Count > 0)
            {
                int sommetCourant = file[0];
                file.RemoveAt(0);
                Console.Write(sommetCourant + " ");

                // Ajouter les voisins non visités du sommet courant dans la file
                
                for (int i = 0; i < listeNoeuds[sommetCourant].GetVoisins().Count; i++)
                {
                    Noeud voisin = listeNoeuds[sommetCourant].GetVoisins()[i];
                    if (!visites[voisin.GetID()])
                    {
                        file.Add(voisin.GetID());
                        visites[voisin.GetID()] = true;
                    }
                }
            }
            Console.WriteLine();
            return visites;
        }

        public bool[] ParcoursProfondeur(int sommetInitial)
        {
            bool[] visites = new bool[numNoeuds + 1];

            // Liste pour gérer les sommets à visiter (similaire à une pile)
            List<int> file = new List<int>();

            // Ajouter le sommet initial à la pile
            file.Add(sommetInitial);
            visites[sommetInitial] = true;

            Console.WriteLine("Parcours en Profondeur (DFS) :");

            // Tant que la pile n'est pas vide
            while (file.Count > 0)
            {
                // Retirer le sommet du haut de la pile
                int sommetCourant = file[file.Count - 1];
                file.RemoveAt(file.Count - 1);

                Console.Write(sommetCourant + " ");

                // Explorer les voisins du sommet actuel    
                /*foreach (var voisin in ListeAdjacence[sommetCourant])
                {
                    if (!visites[voisin])
                    {
                        // Ajouter le voisin non visité à la pile
                        pile.Add(voisin);
                        visites[voisin] = true;
                    }
                }*/
                for (int i = 0; i < listeNoeuds[sommetCourant].GetVoisins().Count; i++)
                {
                    Noeud voisin = listeNoeuds[sommetCourant].GetVoisins()[i];
                    if (!visites[voisin.GetID()])
                    {
                        file.Add(voisin.GetID());
                        visites[voisin.GetID()] = true;
                    }
                }
            }

            Console.WriteLine();
            return visites;
        }

        public bool EstConnexe()
        {
            // Tableau pour suivre les sommets visités
            bool[] visites = new bool[numNoeuds];

            // Lancer le parcours en largeur depuis le sommet 0
            visites = ParcoursProfondeur(1);

            // Vérifier si tous les sommets ont été visités
            for (int i = 1; i < numNoeuds; i++)
            {
                if (!visites[i])
                    return false;
            }

            return true;
        }

        public bool ContientCycle()
        {
            bool[] visites = new bool[numNoeuds];

            // Vérifie chaque composante connexe du graphe
            for (int i = 1; i < numNoeuds; i++)
            {
                if (!visites[i])
                {
                    if (DetecterCycleDFS(i, -1, visites))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool DetecterCycleDFS(int sommet, int parent, bool[] visites)
        {
            visites[sommet] = true;

            foreach (Noeud voisin in listeNoeuds[sommet].GetVoisins())
            {
                int idVoisin = voisin.GetID();

                if (!visites[idVoisin])
                {
                    if (DetecterCycleDFS(idVoisin, sommet, visites))
                    {
                        return true;
                    }
                }
                else if (idVoisin != parent)
                {
                    return true; // Cycle détecté
                }
            }

            return false;
        }

    }
}
