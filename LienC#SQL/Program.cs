using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
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
            Console.WriteLine("Bienvenue sur Liv'in Paris!");
            Console.WriteLine("1-Nouveau? Inscrivez-vous!");
            Console.WriteLine("2-Lancer une requête");
            int s = Convert.ToInt32(Console.ReadLine());
            switch (s)
            {
                case 1:
                    InsertUtilisateurInBD();

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
        static void InsertUtilisateurInBD()
        {
            Console.WriteLine("Créez id d'utilisateur de la plateforme:");
            string idU = Console.ReadLine();
            Console.WriteLine("Créez un mot de passe d'utilisateur de la plateforme:");
            string mdp = Console.ReadLine();
            Utilisateur user1 = new Utilisateur(idU, mdp);
            InsertUtilisateurIntoDB(user1);
            Console.WriteLine("Bienvenue, vous êtes un nouvel utilisateur de la plateforme");

            Console.WriteLine("Quel est votre nom?");
            string nom = Console.ReadLine();
            Console.WriteLine("Votre prénom?");
            string prénom = Console.ReadLine();
            Console.WriteLine("Votre adresse?");
            string adresse = Console.ReadLine();
            Console.WriteLine("Votre numéro de téléphone");
            long tel = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Votre mail?");   // le cuisinier et le client partage le même mail
            string mail = Console.ReadLine();

            Console.WriteLine("Bien maintenant, vous voulez vous inscrire en tant que? ");
            Console.WriteLine("1-Un client");
            Console.WriteLine("2-Un cuisinier");
            Console.WriteLine("3-Un client et un cuisinier");
            int v = Convert.ToInt32(Console.ReadLine());
            switch (v)
            {

                case 1:
                    Console.WriteLine("Votre id de client:");    // à créer automatiquement
                    int idC = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Précisez votre régime alimentaire");
                    string reg = Console.ReadLine();
                    Client client = new Client(idC, nom, prénom, adresse, tel, mail, reg, idU);
                    InsertClientIntoDB(client);
                    Console.WriteLine("Bienvenue en tant que nouveau client de Liv'in Paris!");
                    break;

                case 2:
                    Console.WriteLine("id du cuisinier:");    // à créer automatiquement aussi
                    int idcui = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("quels sont vos spécialités?");
                    string specui = Console.ReadLine();
                    Cuisinier cuisinier = new Cuisinier(idcui, nom, prénom, adresse, tel, mail, specui, idU);
                    InsertCuisinierIntoDB(cuisinier);
                    Console.WriteLine("Bienvenue en tant que nouveau cuisinier de Liv'in Paris!");
                    break;

                case 3:
                    Console.WriteLine("id du client:");    // à créer automatiquement aussi
                    int idV = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("régime alimentaire du client:");
                    string regV = Console.ReadLine();
                    Client client1 = new Client(idV, nom, prénom, adresse, tel, mail, regV, idU);
                    InsertClientIntoDB(client1);
                    Console.WriteLine("Client créé");
                    Console.WriteLine("id du cuisinier");
                    int idCuis = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("quels sont vos spécialités?");
                    string specuis = Console.ReadLine();
                    Cuisinier cuisinier1 = new Cuisinier(idCuis, nom, prénom, adresse, tel, mail, specuis, idU);
                    InsertCuisinierIntoDB(cuisinier1);
                    Console.WriteLine("Bienvenue en tant que nouveau client et cuisinier de Liv'in Paris!");
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
            string createClient = $@"INSERT INTO plateforme.cuisinier (IdCuisinier, NomP, prenomP, AdresseP, telephoneP, EmailP, spécialités, Id_Utilisateur)
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
                        UnClient.TelephoneC = Convert.ToInt64(Elements[4]);
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
