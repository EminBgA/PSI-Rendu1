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

            graphe.ParcoursLargeur(1);
            graphe.ParcoursProfondeur(1);
            Console.WriteLine("Le graphe est connexe : " + graphe.EstConnexe());
            Console.WriteLine("Le graphe contient des cycles : " + graphe.ContientCycle());

            filePath = Path.Combine(chemin, @"graphe.png");
            graphe.DessinerGraphe(filePath);
            FileStream file = File.Open(filePath, FileMode.Open, FileAccess.Write, FileShare.None);
            Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
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
