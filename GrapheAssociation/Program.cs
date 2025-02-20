using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace GrapheAssociation
{
    internal class Program
    {

        

        static void Main(string[] args)
        {
            

            string chemin = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string filePath = Path.Combine(chemin, @"soc-karate.mtx");
            List<int[]> listeAssociations = LireFichierMTX(filePath);
            int numNoeuds = GetNombreNoeuds(filePath) + 1;

            Graphe graphe = new Graphe(numNoeuds);

            for (int i = 0; i < numNoeuds; i++)
            {
                graphe.AjouterNoeud(i);
            }
            for (int i = 0; i < listeAssociations.Count; i++)
            {
                graphe.AjouterLien(listeAssociations[i][0], listeAssociations[i][1]);
            }
            graphe.AfficherListeAdjacence();
            Console.WriteLine("\n\n\n");
            graphe.ConstruireMatriceAdjacence();
            graphe.AfficherMatriceAdjacence();

        }

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
                        // Ignorer les commentaires (% en début de ligne)
                        if (ligne.StartsWith("%")) continue;

                        string[] parties = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        // Lire la première ligne qui contient les dimensions et passer aux données
                        if (!debutDonnees)
                        {
                            debutDonnees = true;
                            
                        }
                        else
                        {
                            // Convertir en int et stocker dans le tableau
                            int[] triplet = new int[2]
                            {
                            int.Parse(parties[0]),
                            int.Parse(parties[1]),
                            //int.Parse(parties[2])
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
                        // Ignorer les commentaires (% en début de ligne)
                        if (ligne.StartsWith("%")) continue;

                        string[] parties = ligne.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                        // Lire la première ligne qui contient les dimensions et passer aux données
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
