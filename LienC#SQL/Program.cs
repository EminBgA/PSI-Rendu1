using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;


namespace LienC_Sql
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Que voulez vous faire?");
            Console.WriteLine("1-Ajouter un utilisateur/client/cuisinier dans la BDD");
            Console.WriteLine("2-Lancer une requête");
            int s = Convert.ToInt32(Console.ReadLine());
            switch (s)
            {
                case 1:
                    Console.WriteLine("Saisissez l'id de l'utilisateur:");
                    string idU= Console.ReadLine();
                    Console.WriteLine("Saisissez le mot de passe de l'utilisateur:");
                    string mdp= Console.ReadLine();
                    Utilisateur user1 = new Utilisateur(idU, mdp);
                    InsertUtilisateurIntoDB(user1);
                    Console.WriteLine("L'utilisateur a été créé");
                    Console.WriteLine("Voulez vous que votre utilisateur soit?: ");
                    Console.WriteLine("1-Un client");
                    Console.WriteLine("2-Un cuisinier");
                    Console.WriteLine("3-Un client et un cuisinier");
                    int v = Convert.ToInt32(Console.ReadLine());
                    switch(v)
                    {
                        case 1:
                            Console.WriteLine("id du client:");
                            int idc= Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Nom du client:");
                            string nomc= Console.ReadLine();
                            Console.WriteLine("Prénom du client:");
                            string prénomc = Console.ReadLine();
                            Console.WriteLine("adresse du client:");
                            string adressec = Console.ReadLine();
                            Console.WriteLine("téléphone du client:");
                            string telc = Console.ReadLine();
                            Console.WriteLine("mail du client:");
                            string mailc = Console.ReadLine();
                            Console.WriteLine("régime alimentaire du client:");
                            string regc = Console.ReadLine();
                            // Appel de la méthode pour ajouter un utilisateur/client
                            Client client = new Client(idc, nomc, prénomc, adressec, telc, mailc, regc, idU);
                            InsertClientIntoDB(client);
                            break;

                        case 2:
                            Console.WriteLine("id du cuisinier");
 
                            int idcui = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Nom du cuisinier:");
                            string nomcui = Console.ReadLine();
                            Console.WriteLine("Prénom du cuisinier:");
                            string prénomcui = Console.ReadLine();
                            Console.WriteLine("adresse du cuisinier:");
                            string adressecui = Console.ReadLine();
                            Console.WriteLine("téléphone du cuisinier:");
                            string telcui = Console.ReadLine();
                            Console.WriteLine("mail du cuisinier:");
                            string mailcui = Console.ReadLine();
                            Console.WriteLine("spécialités du cuisinier:");
                            string specui = Console.ReadLine();
                            Cuisinier cuisinier= new Client(idcui, nomcui, prénomcui, adressecui, telcui, mailcui, regcui, idU);
                            InsertCuisinierIntoDB(cuisinier);
                            break;
                        case 3:
                            Console.WriteLine("id du client:");
                            int idc = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Nom du client:");
                            string nomc = Console.ReadLine();
                            Console.WriteLine("Prénom du client:");
                            string prénomc = Console.ReadLine();
                            Console.WriteLine("adresse du client:");
                            string adressec = Console.ReadLine();
                            Console.WriteLine("téléphone du client:");
                            string telc = Console.ReadLine();
                            Console.WriteLine("mail du client:");
                            string mailc = Console.ReadLine();
                            Console.WriteLine("régime alimentaire du client:");
                            string regc = Console.ReadLine();
                            // Appel de la méthode pour ajouter un utilisateur/client
                            Client client = new Client(idc, nomc, prénomc, adressec, telc, mailc, regc, idU);
                            InsertClientIntoDB(client);
                            break;
                    }
                    
                    break;

                case 2:
                    //// Création et configuration de l'objet connexion
                    string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                    MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                    Connexion.Open();

                    //// Récupération mot clé de l'utilisateur
                    Console.WriteLine("Veuillez saisir le nom du client recherché ");
                    string NomSaisi = Console.ReadLine();

                    //// Ajout des paramètres
                    MySqlParameter ParamNom = new MySqlParameter("@nom", MySqlDbType.VarChar);
                    ParamNom.Value = NomSaisi;

                    ////Création de l'objet Command avec la requête
                    string Requete = "Select * from Client where NomC =@nom ;";
                    MySqlCommand Command = Connexion.CreateCommand();
                    Command.CommandText = Requete;
                    Command.Parameters.Add(ParamNom);
                    //// Execution de la requête et récupération des résultats dans l'objet MySqlDataReader
                    MySqlDataReader reader = Command.ExecuteReader();



                    //// Récupération des résultats dans des variables C#
                    string[] valueString = new string[reader.FieldCount];
                    while (reader.Read())
                    {

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            valueString[i] = reader.GetValue(i).ToString();
                            Console.Write(valueString[i] + " , ");


                            Console.WriteLine();
                        }

                        Console.ReadLine();

                        reader.Close();
                        Command.Dispose();
                        Connexion.Close();
                    }
                    break;

                default:
                    Console.WriteLine("Option invalide. Veuillez choisir 1 ou 2.");
                    break;
            }
            

            
        }

        static void InsertClientIntoDB(Client item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            Console.WriteLine("Vous voulez insérez le client "+ item.idClient+" dans la BDD");
            string createClient = $@"INSERT INTO plateforme.client (idClient, NomC, PrénomC, AdresseC, TelephoneC, EmailC, regimeAlC, Id_Utilisateur)
                                VALUES ('{item.idClient}', '{item.NomC}', '{item.PrénomC}', '{item.AdresseC}', '{item.TelephoneC}', '{item.EmailC}', '{item.regimeAlC}', '{item.Id_Utilisateur}')";


            MySqlCommand command = Connexion.CreateCommand();
            command.CommandText= createClient;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur connexion: " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        static void InsertUtilisateurIntoDB(Utilisateur item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            Console.WriteLine("Vous voulez insérez l'utilisateur " + item.Id_Utilisateur + " dans la BDD");
            string createClient = $@"INSERT INTO plateforme.Utilisateur (Id_Utilisateur, Mot_de_Passe)
                                VALUES ('{item.Id_Utilisateur}', '{item.Mot_de_Passe}')";


            MySqlCommand command = Connexion.CreateCommand();
            command.CommandText = createClient;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur connexion: " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        static void InsertCuisinierIntoDB(Cuisinier item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            Console.WriteLine("Vous voulez insérez l'utilisateur " + item.IdCuisinier + " dans la BDD");
            string createClient = $@"INSERT INTO plateforme.cusinier (IdCuisinier, NomP, prenomP, AdresseP, telephoneP, EmailP, spécialités, Id_Utilisateur)
                                VALUES ('{item.IdCuisinier}', '{item.nomP}', '{item.prenomP}', '{item.adresseP}', '{item.telephoneP}', '{item.emailP}', '{item.spécialités}', '{item.Id_Utilisateur}')";

            MySqlCommand command = Connexion.CreateCommand();
            command.CommandText = createClient;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erreur connexion: " + e.ToString());
                Console.ReadLine();
                return;
            }
            command.Dispose();
        }

        static List<Client> GetListClientsFromCSV()
        {
            List<Client> Liste = new List<Client>();
            // Cette méthode(fonction) va se charger de parser le fichier csv et
            //récupérer chaque ligne du fichier csv dans un objet client
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\cyril\OneDrive\Bureau\projet PSI\PSI-Rendu1\Clients.xlsx"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        String[] Elements = field.Split(';');

                        Client UnClient = new Client();
                        UnClient.idClient = Convert.ToInt32(Elements[0]);
                        UnClient.NomC = Elements[1];
                        UnClient.PrénomC = Elements[2];
                        UnClient.AdresseC = Elements[3];
                        UnClient.TelephoneC = Elements[4];
                        UnClient.EmailC = Elements[5];
                        UnClient.regimeAlC = Elements[6];
                        UnClient.Id_Utilisateur = Elements[7];
                       

                        Liste.Add(UnClient);
                    }
                }
            }

            return Liste;
        }
    }
}
