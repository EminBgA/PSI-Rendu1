using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;
using SkiaSharp.Views.Desktop;

namespace GrapheAssociation
{
    public class Graphe
    {

        

        // Ajouter des nœuds
        //private Dictionary<int, Noeud> Noeuds { get; private set; }
        private Noeud[] listeSommets;
        private List<Lien> listeLiens;
        private int[,] matriceAdjacence;
        private int numSommets;


        private Dictionary<int, List<int>> ListeAdjacence;
        private Dictionary<int, SKPoint> positions;

        public Graphe(int numNoeuds)
        {
            listeSommets = new Noeud[numNoeuds];
            listeLiens = new List<Lien>();
            this.numSommets = numNoeuds;
        }

        /// <summary>
        /// Cette fonction créer un nouveau noeud et lui assigne le numéro d'identification passé en argument.
        /// </summary>
        /// <param name="id"></param>
        public void AjouterNoeud(int id)
        {
            if (listeSommets[id] == null)
            {
                listeSommets[id] = new Noeud(id);
            }
        }

        /// <summary>
        /// Cette fonction crée un lien entre deux noeuds s'il en existe pas encore un.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        public void AjouterLien(int id1, int id2)
        {
            if (listeSommets[id1] != null && listeSommets[id2] != null)
            {
                Noeud n1 = listeSommets[id1];
                Noeud n2 = listeSommets[id2];
                if (!n1.GetVoisins().Contains(n2) && !n2.GetVoisins().Contains(n1))
                {
                    n1.AjouterVoisin(n2);
                    n2.AjouterVoisin(n1);
                    listeLiens.Add(new Lien(n1, n2));
                }
            }
        }

        /// <summary>
        /// Cette fonction construit la matrice adjacente à partir de la liste de tous les liens.
        /// </summary>
        public void ConstruireMatriceAdjacence()
        {
            int taille = listeSommets.Length;
            matriceAdjacence = new int[taille, taille];

            foreach (var lien in listeLiens)
            {
                int i = lien.GetNoeud(1).GetID();
                int j = lien.GetNoeud(2).GetID();
                matriceAdjacence[i, j] = 1;
                matriceAdjacence[j, i] = 1; // Graphe non orienté
            }
        }

        /// <summary>
        /// Cette fonction affiche la liste adjacente.
        /// </summary>
        public void AfficherListeAdjacence()
        {
            foreach (var noeud in listeSommets)
            {
                if (noeud.GetID() != 0)
                {
                   Console.WriteLine(noeud.GetID() + " : " + noeud.VoisinsToString());
                }
               
            }
        }

        /// <summary>
        /// Cette fonction affiche la matrice adjacente. 
        /// </summary>
        public void AfficherMatriceAdjacence()
        {
            for (int i = 1; i < listeSommets.Length; i++)
            {
                for (int j = 1; j < listeSommets.Length; j++)
                {
                    Console.Write(matriceAdjacence[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Cette fonction fait le parcours en largeur du graphe.
        /// </summary>
        /// <param name="noeudInitial"></param>
        /// <returns></returns>
        public bool[] ParcoursLargeur(int noeudInitial)
        {
            bool[] visites = new bool[numSommets];
            List<int> pile = new List<int>();

            pile.Add(noeudInitial);
            visites[noeudInitial] = true;

            Console.WriteLine("Parcours en Largeur:");

            while (pile.Count > 0)
            {
                int noeudCourant = pile[0];
                pile.RemoveAt(0);
                Console.Write(noeudCourant + " ");
                
                for (int i = 0; i < listeSommets[noeudCourant].GetVoisins().Count; i++)
                {
                    Noeud voisin = listeSommets[noeudCourant].GetVoisins()[i];
                    if (!visites[voisin.GetID()])
                    {
                        pile.Add(voisin.GetID());
                        visites[voisin.GetID()] = true;
                    }
                }
            }
            Console.WriteLine();
            return visites;
        }
        
        /// <summary>
        /// Cette fonction fait le parcours en profondeur du graphe.
        /// </summary>
        /// <param name="noeudInitial"></param>
        /// <returns></returns>
        public bool[] ParcoursProfondeur(int noeudInitial)
        {
            bool[] visites = new bool[numSommets];
            List<int> pile = new List<int>();

            pile.Add(noeudInitial);

            Console.WriteLine("Parcours en Profondeur:");

            while (pile.Count > 0)
            {
                int noeudCourant = pile[pile.Count - 1];
                if (!visites[noeudCourant])
                {
                    pile.RemoveAt(pile.Count - 1);
                    visites[noeudCourant] = true;
                    Console.Write(noeudCourant + " ");

                    for (int i = listeSommets[noeudCourant].GetVoisins().Count - 1; i >= 0; i--)
                    {
                        Noeud voisin = listeSommets[noeudCourant].GetVoisins()[i];
                        if (!visites[voisin.GetID()])
                        {
                            pile.Add(voisin.GetID());
                        }
                    }
                }
                else
                {
                    pile.RemoveAt(pile.Count - 1);
                }   
            }
            Console.WriteLine();
            return visites;
        }



        /// <summary>
        /// Cette fonction vérifie si le graphe est connexe.
        /// </summary>
        /// <returns></returns>
        public bool EstConnexe()
        {
            bool[] visites = new bool[numSommets];
            bool rep = true;
            visites = ParcoursProfondeur(1);

            for (int i = 1; i < numSommets; i++)
            {
                if (!visites[i])
                {
                    rep = false;
                }
                    
            }

            return rep;
        }

        /// <summary>
        /// Cette fonction vérifie si le graphe contient des cycles.
        /// </summary>
        /// <returns></returns>
        public bool ContientCycle()
        {
            bool rep = false;
            int sommetInitial = 1;
            bool[] visites = new bool[numSommets];
            List<int> pile = new List<int>();

            pile.Add(sommetInitial);


            while (pile.Count > 0)
            {
                int sommetCourant = pile[pile.Count - 1];
                if (!visites[sommetCourant])
                {
                    pile.RemoveAt(pile.Count - 1);
                    visites[sommetCourant] = true;   

                    for (int i = listeSommets[sommetCourant].GetVoisins().Count - 1; i >= 0; i--)
                    {
                        Noeud voisin = listeSommets[sommetCourant].GetVoisins()[i];
                        if (!visites[voisin.GetID()])
                        {
                            pile.Add(voisin.GetID());
                        }
                        else
                        {
                            rep = true;
                        }
                    }
                }
                else
                {
                    pile.RemoveAt(pile.Count - 1);
                }
            }
            return rep;
        }

        /// <summary>
        /// Cette fonction crée et dessine le graphe.
        /// </summary>
        /// <param name="filename"></param>
        public void DessinerGraphe(string filename)
        {
            // Taille de l'image
            int width = 2000;
            int height = 2000;
            
            // Calcul de la position de chaque sommet sur le graphe
            positions = CalculerPosition(width, height);

            // Création du canvas pour le graphe
            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            using (var paintEdge = new SKPaint { Color = SKColors.Black, StrokeWidth = 3 }) // Apparence des arêtes.
            using (var paintNode = new SKPaint { Color = SKColors.Blue, IsAntialias = true }) // Apparence des sommets.
            using (var paintText = new SKPaint { Color = SKColors.Red, TextSize = 40, IsAntialias = true }) // Apparence du texte.
            {
                canvas.Clear(SKColors.White); // Remplissage du fond du canvas en blanc.

                
                // Début : dessin des arrêtes
                for (int i = 1; i < listeSommets.Length; i++)  // On accède à chaque sommet.
                {
                    for (int j = 0; j < listeSommets[i].GetVoisins().Count; j++)   // Ajout de chaque arrête.
                    {
                        if (listeSommets[i].GetID() < listeSommets[i].GetVoisins()[j].GetID())  // Vérification qu'une arête n'est pas déjà dessiné
                        {
                            canvas.DrawLine(positions[listeSommets[i].GetID()], positions[listeSommets[i].GetVoisins()[j].GetID()], paintEdge);
                        }
                    }

                }// Fin

                // Début : dessin des sommets
                foreach (var node in positions)
                {
                    SKPoint pos = node.Value;
                    canvas.DrawCircle(pos, 40, paintNode);  // Dessin d'un cercle
                    canvas.DrawText(node.Key.ToString(), pos.X - 12, pos.Y + 7, paintText); // Ajout de l'ID du sommet
                }// Fin

                //Sauvegarde de l'image en format png à l'endroit souhaité.
                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite(filename))
                {
                    data.SaveTo(stream);
                }

            }
        }

        /// <summary>
        /// Cette fonction calcule la position de chaque sommet sur le canvas de sorte que les sommets forment un cercle.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Dictionary<int, SKPoint> CalculerPosition(int width, int height)
        {
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();
            int n = numSommets + 1;
            double angleStep = 2 * Math.PI / Math.Max(n, 1);  // Calcul de l'angle entre chaque sommet dépendant du nombre de sommets dans le graphe
            int centerX = width / 2, centerY = height / 2, radius = 700; // Calcul du centre et du rayon du cercle

            
            // Calcul des coord de chaque sommet
            for (int i = 1; i < listeSommets.Length; i++)
            {
                int sommet = listeSommets[i].GetID();
                float x = centerX + (float)(radius * Math.Cos(i * angleStep)); // Calcul de position X
                float y = centerY + (float)(radius * Math.Sin(i * angleStep)); // Calcul de poistion Y
                positions[sommet] = new SKPoint(x, y);
            } 
            return positions;  // Retourne la position de chaque sommet
        }

        


    }
}
