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
        private Noeud[] listeNoeuds;
        private List<Lien> listeLiens;
        private int[,] matriceAdjacence;
        private int numNoeuds;


        private Dictionary<int, List<int>> ListeAdjacence;
        private Dictionary<int, SKPoint> positions;

        public Graphe(int numNoeuds)
        {
            listeNoeuds = new Noeud[numNoeuds];
            listeLiens = new List<Lien>();
            this.numNoeuds = numNoeuds;
        }

        /// <summary>
        /// Cette fonction créer un nouveau noeud et lui assigne le numéro d'identification passé en argument.
        /// </summary>
        /// <param name="id"></param>
        public void AjouterNoeud(int id)
        {
            if (listeNoeuds[id] == null)
            {
                listeNoeuds[id] = new Noeud(id);
            }
        }

        /// <summary>
        /// Cette fonction crée un lien entre deux noeuds s'il en existe pas encore un.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        public void AjouterLien(int id1, int id2)
        {
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

        /// <summary>
        /// Cette fonction construit la matrice adjacente à partir de la liste de tous les liens.
        /// </summary>
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

        /// <summary>
        /// Cette fonction affiche la liste adjacente.
        /// </summary>
        public void AfficherListeAdjacence()
        {
            foreach (var noeud in listeNoeuds)
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
            for (int i = 1; i < listeNoeuds.Length; i++)
            {
                for (int j = 1; j < listeNoeuds.Length; j++)
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
            bool[] visites = new bool[numNoeuds];
            List<int> liste = new List<int>();

            liste.Add(noeudInitial);
            visites[noeudInitial] = true;

            Console.WriteLine("Parcours en Largeur:");

            while (liste.Count > 0)
            {
                int noeudCourant = liste[0];
                liste.RemoveAt(0);
                Console.Write(noeudCourant + " ");
                
                for (int i = 0; i < listeNoeuds[noeudCourant].GetVoisins().Count; i++)
                {
                    Noeud voisin = listeNoeuds[noeudCourant].GetVoisins()[i];
                    if (!visites[voisin.GetID()])
                    {
                        liste.Add(voisin.GetID());
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
            bool[] visites = new bool[numNoeuds];
            List<int> liste = new List<int>();

            liste.Add(noeudInitial);
            //visites[noeudInitial] = true;

            Console.WriteLine("Parcours en Profondeur:");

            while (liste.Count > 0)
            {
                int noeudCourant = liste[liste.Count - 1];
                if (!visites[noeudCourant])
                {
                    liste.RemoveAt(liste.Count - 1);
                    visites[noeudCourant] = true;
                    Console.Write(noeudCourant + " ");

                    for (int i = listeNoeuds[noeudCourant].GetVoisins().Count - 1; i >= 0; i--)
                    {
                        Noeud voisin = listeNoeuds[noeudCourant].GetVoisins()[i];
                        if (!visites[voisin.GetID()])
                        {
                            liste.Add(voisin.GetID());
                        }
                    }
                }
                else
                {
                    liste.RemoveAt(liste.Count - 1);
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
            bool[] visites = new bool[numNoeuds];
            bool rep = true;
            visites = ParcoursProfondeur(1);

            for (int i = 1; i < numNoeuds; i++)
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
            bool[] visites = new bool[numNoeuds];
            bool rep = false;
            for (int i = 1; i < numNoeuds; i++)
            {
                if (!visites[i])
                {
                    if (DetecterCycle(i, -1, visites))
                    {
                        rep = true; ;
                    }
                }
            }

            return rep;
        }

        /// <summary>
        /// Cette fonction vérifie si il y a un cycle à partir d'un noeud.
        /// </summary>
        /// <param name="sommet"></param>
        /// <param name="parent"></param>
        /// <param name="visites"></param>
        /// <returns></returns>
        public bool DetecterCycle(int noeud, int parent, bool[] visites)
        {
            visites[noeud] = true;

            foreach (Noeud voisin in listeNoeuds[noeud].GetVoisins())
            {
                int idVoisin = voisin.GetID();

                if (!visites[idVoisin])
                {
                    if (DetecterCycle(idVoisin, noeud, visites))
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

        /// <summary>
        /// Cette fonction crée et dessine le graphe.
        /// </summary>
        /// <param name="filename"></param>
        public void DessinerGraphe(string filename)
        {
            int width = 2000;
            int height = 2000;
            positions = CalculerPosition(width, height);

            using (var bitmap = new SKBitmap(width, height))
            using (var canvas = new SKCanvas(bitmap))
            using (var paintEdge = new SKPaint { Color = SKColors.Black, StrokeWidth = 3 })
            using (var paintNode = new SKPaint { Color = SKColors.Blue, IsAntialias = true })
            using (var paintText = new SKPaint { Color = SKColors.Red, TextSize = 40, IsAntialias = true })
            {
                canvas.Clear(SKColors.White);

                for (int i = 1; i < listeNoeuds.Length; i++)
                {
                    for (int j = 0; j < listeNoeuds[i].GetVoisins().Count; j++)
                    {
                        if (listeNoeuds[i].GetID() < listeNoeuds[i].GetVoisins()[j].GetID())
                        {
                            canvas.DrawLine(positions[listeNoeuds[i].GetID()], positions[listeNoeuds[i].GetVoisins()[j].GetID()], paintEdge);
                        }
                    }

                }

                foreach (var node in positions)
                {
                    SKPoint pos = node.Value;
                    canvas.DrawCircle(pos, 40, paintNode);
                    canvas.DrawText(node.Key.ToString(), pos.X - 12, pos.Y + 7, paintText);
                }

                using (var image = SKImage.FromBitmap(bitmap))
                using (var data = image.Encode(SKEncodedImageFormat.Png, 100))
                using (var stream = File.OpenWrite(filename))
                {
                    data.SaveTo(stream);
                }

            }
        }

        /// <summary>
        /// Cette fonction calcule la position de chaque noeud sur le canvas.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Dictionary<int, SKPoint> CalculerPosition(int width, int height)
        {
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();
            int n = numNoeuds + 1;
            double angleStep = 2 * Math.PI / Math.Max(n, 1);
            int centerX = width / 2, centerY = height / 2, radius = 700;

            //int i = 0;

            for (int i = 1; i < listeNoeuds.Length; i++)
            {
                int sommet = listeNoeuds[i].GetID();
                float x = centerX + (float)(radius * Math.Cos(i * angleStep));
                float y = centerY + (float)(radius * Math.Sin(i * angleStep));
                positions[sommet] = new SKPoint(x, y);
            } 
            return positions;
        }

        


    }
}
