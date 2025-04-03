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

        // Hello worldd

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
        public void AjouterNoeud(int id, string nom)
        {
            //Console.WriteLine(id);
            if (listeSommets[id] == null)
            {
                listeSommets[id] = new Noeud(id, nom);
            }
        }


        /// <summary>
        /// Cette fonction crée un lien entre deux noeuds s'il en existe pas encore un.
        /// </summary>
        /// <param name="id1"></param>
        /// <param name="id2"></param>
        public void AjouterLien(int id1, int id2, int distance, int tempsChangement, string nomMetro, int numMetro)
        {
            if (listeSommets[id1] != null && listeSommets[id2] != null)
            {
                Noeud n1 = listeSommets[id1];
                Noeud n2 = listeSommets[id2];
                if (!n1.GetVoisins().Contains(n2))//  Pour un graphe non orienté :  && !n2.GetVoisins().Contains(n1))
                {
                    n1.AjouterVoisin(n2);
                    // Pour un graphe non orienté : n2.AjouterVoisin(n1);
                    listeLiens.Add(new Lien(n1, n2, distance, tempsChangement, nomMetro, numMetro));
                }
                else
                {
                    //Console.WriteLine("hello world");
                }
            }
        }

        public string NomSommet(int idSommet)
        {
            string rep = "";
            foreach (Noeud sommet in  listeSommets)
            {
                if (sommet != null && sommet.GetID() == idSommet)
                {
                    rep = sommet.GetNom();
                }
            }
            return rep;
        }

        public int IDSommet(string nomSommet)
        {
            int rep = 0;
            foreach (Noeud sommet in listeSommets)
            {
                if (sommet != null && sommet.GetNom() == nomSommet)
                {
                    rep = sommet.GetID();
                }
            }
            return rep;
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
                if (noeud != null && noeud.GetID() != 0)
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
                    if (matriceAdjacence[i,j] != null)
                    {
                        Console.Write(matriceAdjacence[i, j] + " ");
                    }
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
        
        public int CalculerTempsTrajetMetro(Lien[] listeLiens)
        {
            int temps = 0;
            for (int i = 0; i < listeLiens.Length; i++)
            {
                temps += listeLiens[i].Distance();
                if (i >= 1 && listeLiens[i].GetNumMetro() != listeLiens[i - 1].GetNumMetro())
                {
                    temps += listeLiens[i].GetTempsChangement();
                }
            }
            return temps;
        }


        public Lien[] FloydWarshall(int sommetDepart, int sommetArrive)
        {
            int maxInt = 1000;
            int[,] distances = new int[numSommets, numSommets];
            int[,] pred = new int[numSommets, numSommets];
            Lien[,] lienPred = new Lien[numSommets, numSommets]; // Stocke les liens empruntés
            Lien[,] dernierLienUtilise = new Lien[numSommets, numSommets];

            for (int i = 0; i < numSommets; i++)
            {
                for (int j = 0; j < numSommets; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        distances[i, j] = maxInt;
                        pred[i, j] = -1;
                        lienPred[i, j] = null;
                        dernierLienUtilise[i, j] = null;
                    }
                }
            }

            foreach (Lien lien in listeLiens)
            {
                int id1 = lien.GetNoeud(1).GetID();
                int id2 = lien.GetNoeud(2).GetID();
                distances[id1, id2] = lien.Distance();
                pred[id1, id2] = id1;
                lienPred[id1, id2] = lien;
                dernierLienUtilise[id1 , id2] = lien;
            }

            for (int k = 0; k < numSommets; k++)
            {
                for (int i = 0; i < numSommets; i++)
                {
                    for (int j = 0; j < numSommets; j++)
                    {
                        int tempsAttente = 0;
                        if (dernierLienUtilise[i,k] != null && dernierLienUtilise[k,j] != null && dernierLienUtilise[i,k] != dernierLienUtilise[k,j])
                        {
                            tempsAttente += dernierLienUtilise[k, j].GetTempsChangement();
                        }
                        
                        if (distances[i, k] + distances[k, j] + tempsAttente < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j];
                            pred[i, j] = pred[k, j];
                            lienPred[i, j] = lienPred[k, j];
                            dernierLienUtilise[i, j] = dernierLienUtilise[k, j];
                        }
                    }
                }
            }

            if (pred[sommetDepart, sommetArrive] == -1)
            {
                Console.WriteLine("retourne rien");
                return new Lien[0];
            }

            var chemin = new List<Lien>();
            int sommet = sommetArrive;
            while (sommet != sommetDepart)
            {
                Lien lien = lienPred[pred[sommetDepart, sommet], sommet];
                if (lien == null) return new Lien[0];

                chemin.Add(lien);
                sommet = pred[sommetDepart, sommet];
            }

            chemin.Reverse();
            return chemin.ToArray();
        }

        public Lien[] FloydWarshall2(int sommetDepart, int sommetArrive)
        {
            int maxInt = 1000;
            int[,] distances = new int[numSommets, numSommets];
            int[,] pred = new int[numSommets, numSommets];
            Lien[,] lienPred = new Lien[numSommets, numSommets]; // Stocke les liens empruntés
            Lien[,] dernierLienUtilise = new Lien[numSommets, numSommets];

            for (int i = 0; i < numSommets; i++)
            {
                for (int j = 0; j < numSommets; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = 0;
                    }
                    else
                    {
                        distances[i, j] = maxInt;
                        pred[i, j] = -1;
                        lienPred[i, j] = null;
                        dernierLienUtilise[i, j] = null;
                    }
                }
            }

            foreach (Lien lien in listeLiens)
            {
                int id1 = lien.GetNoeud(1).GetID();
                int id2 = lien.GetNoeud(2).GetID();
                distances[id1, id2] = lien.Distance();
                pred[id1, id2] = id1;
                lienPred[id1, id2] = lien;
                dernierLienUtilise[id1, id2] = lien;
            }

            for (int k = 0; k < numSommets; k++)
            {
                for (int i = 0; i < numSommets; i++)
                {
                    for (int j = 0; j < numSommets; j++)
                    {
                        if (distances[i,k] == maxInt || distances[k, j] == maxInt)
                        {
                            continue;
                        }
                        
                        int tempsAttente = 0;
                        if (dernierLienUtilise[i, k] != null && dernierLienUtilise[k, j] != null && dernierLienUtilise[i, k].GetNumMetro() != dernierLienUtilise[k, j].GetNumMetro())
                        {
                            tempsAttente += dernierLienUtilise[k, j].GetTempsChangement();
                        }

                        if (distances[i, k] + distances[k, j] + tempsAttente < distances[i, j])
                        {
                            distances[i, j] = distances[i, k] + distances[k, j] + tempsAttente;
                            pred[i, j] = k;// pred[k, j];
                            lienPred[i, j] = lienPred[k, j];
                            dernierLienUtilise[i, j] = dernierLienUtilise[k, j];
                        }
                    }
                }
            }

            if (pred[sommetDepart, sommetArrive] == -1)
            {
                Console.WriteLine("retourne rien");
                return new Lien[0];
            }

            var chemin = new List<Lien>();
            int sommet = sommetArrive;
            while (sommet != sommetDepart)
            {
                Lien lien = lienPred[pred[sommetDepart, sommet], sommet];
                if (lien == null) return new Lien[0];

                chemin.Add(lien);
                sommet = pred[sommetDepart, sommet];
            }

            chemin.Reverse();
            return chemin.ToArray();
        }


        public Lien[] BellmanFord(int sommetDepart, int sommetArrive)
        {
            var distances = new Dictionary<int, int>();
            var pred = new Dictionary<int, int>();
            var lienPred = new Dictionary<int, Lien>(); // Stocke le lien utilisé pour atteindre chaque sommet
            var dernierLienUtilise = new Dictionary<int, Lien>();

            foreach (Noeud sommet in listeSommets)
            {
                if (sommet != null)
                {
                    distances[sommet.GetID()] = int.MaxValue;
                    pred[sommet.GetID()] = 0;
                    lienPred[sommet.GetID()] = null;
                    dernierLienUtilise[sommet.GetID()] = null;
                }
            }
            distances[sommetDepart] = 0;

            for (int i = 1; i < numSommets; i++)
            {
                foreach (Lien lien in listeLiens)
                {
                    int id1 = lien.GetNoeud(1).GetID();
                    int id2 = lien.GetNoeud(2).GetID();

                    int tempsAttente = 0;
                    if (dernierLienUtilise[id1] != null && dernierLienUtilise[id1].GetNumMetro() != lien.GetNumMetro())
                    {
                        tempsAttente += lien.GetTempsChangement();// lien.Distance();
                    }

                    if (distances[id1] != int.MaxValue && distances[id1] + lien.Distance() + tempsAttente < distances[id2])
                    { 
                        distances[id2] = distances[id1] + lien.Distance();
                        pred[id2] = id1;
                        lienPred[id2] = lien; // Stocke le lien emprunté
                        dernierLienUtilise[id2] = lien;
                    }
                }
            }

            if (lienPred[sommetArrive] == null)
            {
                Console.WriteLine("retourne rien");
                return new Lien[0]; // Pas de chemin trouvé
            }

            var chemin = new List<Lien>();
            int nsommet = sommetArrive;
            while (pred[nsommet] != 0)
            {
                chemin.Add(lienPred[nsommet]);
                nsommet = pred[nsommet];
            }

            chemin.Reverse();
            return chemin.ToArray();
        }


        public Lien[] Dijkstra(int sommetDepart, int sommetArrive)
        {
            var distances = new Dictionary<int, int>();
            var pred = new Dictionary<int, int>();
            var lienPred = new Dictionary<int, Lien>(); // Associe chaque sommet à son lien prédécesseur
            var dernierLienUtilise = new Dictionary<int, Lien>(); // Pour déterminer si il y a des changements de métro.
            var nonVisites = new List<int>();

            foreach (Noeud sommet in listeSommets)
            {
                if (sommet != null)
                {
                    distances[sommet.GetID()] = int.MaxValue;
                    pred[sommet.GetID()] = 0;
                    lienPred[sommet.GetID()] = null;
                    dernierLienUtilise[sommet.GetID()] = null;
                    nonVisites.Add(sommet.GetID());
                }
            }
            distances[sommetDepart] = 0;

            while (nonVisites.Count > 0)
            {
                int sommetDepartActuel = -1;
                int minDistance = int.MaxValue;

                // Trouver le sommet avec la plus petite distance
                foreach (int sommetID in nonVisites)
                {
                    if (distances[sommetID] < minDistance)
                    {
                        minDistance = distances[sommetID];
                        sommetDepartActuel = sommetID;
                    }
                }

                if (sommetDepartActuel == -1)
                    break; // Aucun sommet atteignable

                nonVisites.Remove(sommetDepartActuel);

                if (sommetDepartActuel == sommetArrive)
                    break;

                // Trouver le sommet actuel
                Noeud sommetActuel = null;
                foreach (var sommet in listeSommets)
                {
                    if (sommet != null && sommet.GetID() == sommetDepartActuel)
                    {
                        sommetActuel = sommet;
                        break;
                    }
                }
                if (sommetActuel == null) continue;

                // Explorer les voisins
                foreach (Noeud voisin in sommetActuel.GetVoisins())
                {
                    int sommetID = voisin.GetID();
                    Lien lien = null;

                    // Trouver le lien correspondant
                    foreach (var l in listeLiens)
                    {
                        if (l.GetNoeud(1).GetID() == sommetDepartActuel && l.GetNoeud(2).GetID() == sommetID)
                        {
                            lien = l;
                            break;
                        }
                    }
                    if (lien == null) continue;

                    int tempsAttente = 0;
                    
                    if (dernierLienUtilise[sommetActuel.GetID()] != null && dernierLienUtilise[sommetActuel.GetID()].GetNumMetro() != lien.GetNumMetro())
                    {
                        tempsAttente += lien.GetTempsChangement();// lien.Distance();
                    }
                    int nouvelleDistance = distances[sommetDepartActuel] + lien.Distance() + tempsAttente;
                    if (nouvelleDistance < distances[sommetID])
                    {
                        distances[sommetID] = nouvelleDistance;
                        pred[sommetID] = sommetDepartActuel;
                        lienPred[sommetID] = lien; // Stocker le lien emprunté
                        dernierLienUtilise[sommetID] = lien;
                    }
                }
            }

            if (lienPred[sommetArrive] == null)
                return new Lien[0]; // Pas de chemin trouvé

            var chemin = new List<Lien>();
            int nSommet = sommetArrive;
            while (pred[nSommet] != 0)
            {
                chemin.Add(lienPred[nSommet]);
                nSommet = pred[nSommet];
            }
            chemin.Reverse();
            return chemin.ToArray();
        }







        /// <summary>
        /// Cette fonction crée et dessine le graphe.
        /// </summary>
        /// <param name="filename"></param>
        public void DessinerGraphe(string filename, Dictionary<string, Dictionary<string, object>> listeGares)
        {
            // Taille de l'image
            int width = 5000;
            int height = 5000;
            
            // Calcul de la position de chaque sommet sur le graphe
            positions = CalculerPosition3(width, height, listeGares);

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
                    if (listeSommets[i] != null)
                    {
                        for (int j = 0; j < listeSommets[i].GetVoisins().Count; j++)   // Ajout de chaque arrête.
                        {
                            if (listeSommets[i].GetID() < listeSommets[i].GetVoisins()[j].GetID())  // Vérification qu'une arête n'est pas déjà dessiné
                            {
                                canvas.DrawLine(positions[listeSommets[i].GetID()], positions[listeSommets[i].GetVoisins()[j].GetID()], paintEdge);
                            }
                        }
                    }
                    

                }// Fin

                // Début : dessin des sommets
                foreach (var node in positions)
                {
                    SKPoint pos = node.Value;
                    canvas.DrawCircle(pos, 20, paintNode);  // Dessin d'un cercle
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
        public Dictionary<int, SKPoint> CalculerPosition2(int width, int height, Dictionary<string, Dictionary<string, object>> listeGares)
        {
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();
            int n = numSommets + 1;
            double angleStep = 2 * Math.PI / Math.Max(n, 1);  // Calcul de l'angle entre chaque sommet dépendant du nombre de sommets dans le graphe
            int centerX = width / 2, centerY = height / 2, radius = 700; // Calcul du centre et du rayon du cercle

            
            // Calcul des coord de chaque sommet
            for (int i = 1; i < listeSommets.Length; i++)
            {
                if (listeSommets[i] != null)
                {
                    int sommet = listeSommets[i].GetID();
                    float x = centerX + (float)(radius * Math.Cos(i * angleStep)); // Calcul de position X
                    float y = centerY + (float)(radius * Math.Sin(i * angleStep)); // Calcul de poistion Y
                    positions[sommet] = new SKPoint(x, y);
                }
                
            } 
            return positions;  // Retourne la position de chaque sommet
        }
        public Dictionary<int, SKPoint> CalculerPosition3(int width, int height, Dictionary<string, Dictionary<string, object>> listeGares)
        {
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

            float minLat = float.MaxValue;
            float maxLat = float.MinValue;
            float minLon = float.MaxValue;
            float maxLon = float.MinValue;

            foreach (string nomGare in listeGares.Keys)
            {
                double lat2 = (double)listeGares[nomGare]["lat"];
                float lat = (float)lat2;
                double lon2 = (double)listeGares[nomGare]["lon"];
                float lon = (float)lon2;
                if (lat < minLat) minLat = lat;
                if (lat > maxLat) maxLat = lat;
                if (lon < minLon) minLon = lon;
                if (lon > maxLon) maxLon = lon;
            }

            float scaleX = (width - 100) / (maxLon - minLon);
            float scaleY = (height - 100) / (maxLat - minLat);
            float scale = Math.Min(scaleX, scaleY);

            float offsetX = (width - (maxLon - minLon) * scaleX) / 2;
            float offsetY = (height - (maxLat - minLat) * scaleY) / 2;

            foreach(string nomGare in listeGares.Keys)
            {
                int id = IDSommet(nomGare);
                double x2 = ((double)listeGares[nomGare]["lon"] - minLon) * scaleX + 0;
                float x = (float )x2;
                double y2 = height - ((double)listeGares[nomGare]["lat"] - minLat) * scaleY + 0;
                float y = (float )y2;
                positions[id] = new SKPoint(x, y);
            }

            return positions;

        }
        public Dictionary<int, SKPoint> CalculerPosition(int width, int height, Dictionary<string, Dictionary<string, object>> listeGares)
        {
            Dictionary<int, SKPoint> positions = new Dictionary<int, SKPoint>();

            // Récupérer les valeurs min/max de latitude et longitude
            double minLat = double.MaxValue, maxLat = double.MinValue;
            double minLon = double.MaxValue, maxLon = double.MinValue;

            foreach (string nomGare in listeGares.Keys)
            {
                if (nomGare != null)
                {
                    double lat = (double)listeGares[nomGare]["lat"];
                    double lon = (double)listeGares[nomGare]["lon"];
                    minLat = Math.Min(minLat, lat);
                    maxLat = Math.Max(maxLat, lat);
                    minLon = Math.Min(minLon, lon);
                    maxLon = Math.Max(maxLon, lon);
                }
            }

            // Calcul du ratio pour conserver le bon rapport d’aspect
            double scaleX = (width - 100) / (maxLon - minLon);
            double scaleY = (height - 100) / (maxLat - minLat);
            double scale = Math.Min(scaleX, scaleY); // Prendre le plus petit pour éviter une distorsion

            // Centrage des sommets
            double offsetX = (width - (maxLon - minLon) * scale) / 2;
            double offsetY = (height - (maxLat - minLat) * scale) / 2;

            foreach (string nomGare in listeGares.Keys)
            {
                if (nomGare != null)
                {
                    int id = IDSommet(nomGare);
                    double x = ((double)listeGares[nomGare]["lon"] - minLon) * scale + offsetX;
                    double y = height - (((double)listeGares[nomGare]["lat"] - minLat) * scale + offsetY); // Inversion de l’axe Y
                    positions[id] = new SKPoint((float)x, (float)y);
                }
            }

            return positions;
        }




    }
}
