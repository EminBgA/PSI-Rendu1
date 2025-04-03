using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.FileIO;


namespace GrapheAssociation
{
    internal class Program
    {

        

        static void Main(string[] args)
        {
            

            string chemin = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(chemin, @"soc-karate.mtx");
            List<Dictionary<string, object>> listeStations = new List<Dictionary<string, object>>();

            listeStations = LireFichierCSV(filePath);
            List<int> listeSommets = GetSommets(listeStations);
            int numSommets = listeSommets.Count;

            //List<int[]> listeAssociations = LireFichierMTX(filePath);
            //int numNoeuds = GetNombreNoeuds(filePath) + 1;

            Graphe graphe = new Graphe(numSommets);
            /*
            for (int i = 0; i < numNoeuds; i++)
            {
                graphe.AjouterNoeud(i);
            }

            for (int i = 0; i < listeAssociations.Count; i++)
            {
                graphe.AjouterLien(listeAssociations[i][0], listeAssociations[i][1]);
            }*/

            foreach (int idSommet in listeSommets)
            {
                graphe.AjouterNoeud(idSommet);
            }

            for (int i = 0; i < listeStations.Count; i++)
            {
                if ((int)listeStations[i]["idP"] != 0)
                {
                    graphe.AjouterLien((int)listeStations[i]["idP"], (int)listeStations[i]["id"], (int)listeStations[i]["tempsStation"]);
                }
            }

            graphe.AfficherListeAdjacence();
            Console.WriteLine("\n\n\n");+
            graphe.ConstruireMatriceAdjacence();
            graphe.AfficherMatriceAdjacence();

            graphe.ParcoursLargeur(1);
            graphe.ParcoursProfondeur(1);
            Console.WriteLine("Le graphe est connexe : " + graphe.EstConnexe());
            Console.WriteLine("Le graphe contient des cycles : " + graphe.ContientCycle());


            int[] listeDij = graphe.Dijkstra(1, 17);
            foreach (int i in listeDij)
            {
                Console.Write(i + ", ");
            }
            Console.WriteLine();

            filePath = Path.Combine(chemin, @"graphe.png");
            graphe.DessinerGraphe(filePath);
            FileStream file = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
        }

        static List<Dictionary<string, object>> LireFichierCSV(string filePath)
        {
            List<Dictionary<string, object>> listeStations = new List<Dictionary<string, object>>();
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        String[] elements = field.Split(';');

                        if (elements[0] != "")
                        {
                            int idStation = Convert.ToInt32(elements[0]);
                            while (listeStations.Count <= idStation)
                            {
                                listeStations.Add(new Dictionary<string, object>());
                                listeStations[(listeStations.Count) - 1]["id"] = 0;
                            }

                            listeStations[idStation]["id"] = Convert.ToInt32(elements[0]);
                            listeStations[idStation]["nom"] = elements[1];
                            listeStations[idStation]["idP"] = Convert.ToInt32(elements[2]);
                            listeStations[idStation]["idS"] = Convert.ToInt32(elements[3]);
                            listeStations[idStation]["tempsStations"] = Convert.ToInt32(elements[4]);
                            listeStations[idStation]["tempsCorrespondance"] = Convert.ToInt32(elements[5]);
                            listeStations[idStation]["numMetro"] = Convert.ToInt32(elements[6]);
                        }

                    }
                }
            }
            for (int i = 0; i < listeStations.Count; i++)
            {
                if ((int)listeStations[i]["id"] == 0)
                {
                    listeStations.RemoveAt(i);
                    i--;
                }
            }


            return listeStations;
        }

        static List<int> GetSommets(List<Dictionary<string, object>> listeStations)
        {
            List<int> allStationID = new List<int>();
            for (int i = 0; i < listeStations.Count; i++)
            {
                if ((int)listeStations[i]["id"] != 0 && allStationID.Contains((int)listeStations[i]["id"]) == false)
                {
                    allStationID.Add((int)listeStations[i]["id"]);
                }
            }
            return allStationID;
        }


        /// <summary>
        /// Cette fonction transforme le fichier MTX fourni en tableau de tableau de int.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static List<int[]> LireFichierMTX(string filePath)
        {
            List<int[]> valeurs = new List<int[]>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string ligne;
                    bool debutDonnees = false;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        if (ligne.StartsWith("%")) continue;

                        string[] parties = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if (!debutDonnees)
                        {
                            debutDonnees = true;
                            
                        }
                        else
                        {
                            int[] triplet = new int[2]
                            {
                            int.Parse(parties[0]),
                            int.Parse(parties[1]),
                            };

                            valeurs.Add(triplet);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.ToString()}");
            }

            return valeurs;
        }

        /// <summary>
        /// Cette fonction renvoie le nombre de noeuds inscrit sur le fichier MTX.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        static int GetNombreNoeuds(string filePath)
        {
            List<int[]> valeurs = new List<int[]>();
            int num = 0;
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string ligne;
                    bool debutDonnees = false;
                    while ((ligne = sr.ReadLine()) != null)
                    {
                        if (ligne.StartsWith("%")) continue;

                        string[] parties = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        if (!debutDonnees)
                        {
                            debutDonnees = true;
                            num = int.Parse(parties[0]);
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex.ToString()}");
            }

            return num;
        }
    }
}
