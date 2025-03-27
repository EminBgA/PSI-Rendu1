using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;


namespace LienC_Sql
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Bienvenue sur Liv'in Paris!");
            Console.WriteLine("1-Nouveau? Inscrivez-vous! (Ajouter un utilisateur/client/cuisinier dans la BDD) ");
            Console.WriteLine("2-Connectez-vous.   (Afficher/modifier les informations d'un client/cuisinier, supprimer un client/cuisinier) ");
            Console.WriteLine("3-Vous êtes admin? Lançer des requêtes sur la BDD de la plateforme. (Lancer des requêtes sur l'ensemble de la BDD) ");
            int s = Convert.ToInt32(Console.ReadLine());
            switch (s)
            {
                case 1:
                    RegistrationToPlatform();
                    break;

                case 2:
                    ConnectionToPlatform();
                    break;

                case 3:

                    break;

                default:
                    Console.WriteLine("Option invalide. Veuillez choisir 1,2 ou 3.");
                    break;
            }
            


        }

        //Principale Méthode
        static void RegistrationToPlatform()
        {
            Console.WriteLine("Créez un id d'utilisateur de la plateforme:");
            string idU = Console.ReadLine();
            Console.WriteLine("Créez un mot de passe d'utilisateur de la plateforme:");
            string mdp = Console.ReadLine();
            Utilisateur user1 = new Utilisateur(idU, mdp);
            InsertUtilisateurIntoDB(user1);
            Console.WriteLine("Bienvenue, vous êtes un nouvel utilisateur de la plateforme!");

            Console.WriteLine("Quel est votre nom?");
            string nom = Console.ReadLine();
            Console.WriteLine("Votre prénom?");
            string prénom = Console.ReadLine();
            Console.WriteLine("Votre adresse?");
            string adresse = Console.ReadLine();
            Console.WriteLine("Votre numéro de téléphone");
            long tel = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("Votre email?");   // le cuisinier et le client partage le même mail
            string mail = Console.ReadLine();

            Console.WriteLine("Bien, maintenant vous voulez vous inscrire en tant que? ");
            Console.WriteLine("1-Un client");
            Console.WriteLine("2-Un cuisinier");
            Console.WriteLine("3-Un client et un cuisinier");
            int v = Convert.ToInt32(Console.ReadLine());
            switch (v)
            {

                case 1:
                    string idC=null;
                    Console.WriteLine("Précisez votre régime alimentaire");
                    string reg = Console.ReadLine();
                    Client client = new Client(idC, nom, prénom, adresse, tel, mail, reg, idU);
                    InsertClientIntoDB(client);
                    Console.WriteLine("Bienvenue en tant que nouveau client de Liv'in Paris!");
                    break;

                case 2:
                    string idcui = null;
                    Console.WriteLine("quels sont vos spécialités?");
                    string specui = Console.ReadLine();
                    Cuisinier cuisinier = new Cuisinier(idcui, nom, prénom, adresse, tel, mail, specui, idU);
                    InsertCuisinierIntoDB(cuisinier);
                    Console.WriteLine("Bienvenue en tant que nouveau cuisinier de Liv'in Paris!");
                    break;

                case 3:
                    string idC1 = null;
                    Console.WriteLine("régime alimentaire du client:");
                    string regV = Console.ReadLine();
                    Client client1 = new Client(idC1, nom, prénom, adresse, tel, mail, regV, idU);
                    InsertClientIntoDB(client1);
                    Console.WriteLine("Client créé");
                    string idcuis = null;
                    Console.WriteLine("quels sont vos spécialités en tant que cuisinier?");
                    string specuis = Console.ReadLine();
                    Cuisinier cuisinier1 = new Cuisinier(idcuis, nom, prénom, adresse, tel, mail, specuis, idU);
                    InsertCuisinierIntoDB(cuisinier1);
                    Console.WriteLine("Bienvenue en tant que nouveau client et cuisinier de Liv'in Paris!");
                    break;
                default:
                    Console.WriteLine("Option invalide. Veuillez choisir 1,2 ou 3.");
                    break;
            }
        }

        static void ConnectionToPlatform()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            bool flag = true;
            while (flag)
            {
                //// Récupération mot clé de l'utilisateur
                Console.WriteLine("Veuillez saisir votre id utilisateur");
                string IdSaisi = Console.ReadLine();
                Console.WriteLine("Saisissez votre mot de passe");
                string mdpSaisi = Console.ReadLine();

                //// Ajout des paramètres
                MySqlParameter ParamId = new MySqlParameter("@id", MySqlDbType.VarChar);
                ParamId.Value = IdSaisi;
                MySqlParameter ParamMdp = new MySqlParameter("@mdp", MySqlDbType.VarChar);
                ParamMdp.Value = mdpSaisi;

                ////Création de l'objet Command avec la requête
                string Requete = "Select * from Utilisateur where Id_Utilisateur = @id AND Mot_de_Passe= @mdp ;";
                MySqlCommand Command = Connexion.CreateCommand();
                Command.CommandText = Requete;
                Command.Parameters.Add(ParamId);
                Command.Parameters.Add(ParamMdp);
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
                    }
                }
                if (valueString.Length != 2)
                {
                    Console.WriteLine("Le mot de passe est erroné, réessayez");
                }
                else
                {
                    flag = false;
                    Console.WriteLine("Bienvenue " + IdSaisi + " !");
                    reader.Close();
                    Command.Dispose();
                    Console.WriteLine("Que voulez-vous faire?");
                    Console.WriteLine("1-Consultez vos informations de clients");
                    Console.WriteLine("2-Consultez vos informations de cuisiniers");
                    Console.WriteLine("3-Modifiez vos informations de clients");
                    Console.WriteLine("4-Modifiez vos informations de cuisiniers");
                    int t = Convert.ToInt32(Console.ReadLine());
                    switch (t)
                    {
                        case 1:
                            string Requete1 = "Select * from client where Id_Utilisateur=@id";
                            MySqlCommand Command1 = Connexion.CreateCommand();
                            Command1.CommandText = Requete1;
                            Command1.Parameters.Add(ParamId);
                            MySqlDataReader reader1 = Command1.ExecuteReader();
                            string[] valueString1 = new string[reader1.FieldCount];
                            Console.WriteLine("Vos informations de clients sont: ");
                            Console.WriteLine();
                            while (reader1.Read())
                            {
                                for (int i = 0; i < reader1.FieldCount; i++)
                                {
                                    valueString1[i] = reader1.GetValue(i).ToString();
                                    Console.Write(valueString1[i] + " , ");
                                }
                            }
                            if (valueString1.Length == 0)
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que client");
                            }
                            reader1.Close();
                            Command1.Dispose();
                            break;

                        case 2:
                            string Requete2 = "Select * from cuisinier where Id_Utilisateur=@id";
                            MySqlCommand Command2 = Connexion.CreateCommand();
                            Command2.CommandText = Requete2;
                            Command2.Parameters.Add(ParamId);
                            MySqlDataReader reader2 = Command2.ExecuteReader();
                            string[] valueString2 = new string[reader2.FieldCount];
                            Console.WriteLine("Vos informations de cuisiniers sont: ");
                            Console.WriteLine();
                            while (reader2.Read())
                            {
                                for (int i = 0; i < reader2.FieldCount; i++)
                                {
                                    valueString2[i] = reader2.GetValue(i).ToString();
                                    Console.Write(valueString2[i] + " , ");
                                }
                            }
                            if (valueString2.Length == 0)
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que cuisinier");
                            }
                            reader2.Close();
                            Command2.Dispose();
                            break;
                        case 3:
                            break;
                        case 4:
                            break;
                        default:
                            Console.WriteLine("Option invalide. Veuillez choisir 1,2,3 ou 4");
                            break;


                    }

                }


            }
            Connexion.Close();

        }

        //Méthode intermédiaire
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

        static void InsertClientIntoDB(Client item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            //Création de l'objet Command avec la requête
            string Requete = "select MAX(IdClient) AS IdClient from client;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            // Execution de la requête et récupération des résultats dans l'objet MySqlDataReader
            MySqlDataReader reader = Command.ExecuteReader();
            string IdClient = "";
            // Récupération des résultats dans des variables C#
            while (reader.Read())
            {
                IdClient = reader["IdClient"].ToString();
            }
            reader.Close();
            // traitement du CodeC récupéré pour générer un nouveau C672
            int PartieNumerique = Convert.ToInt32(IdClient.Substring(2));
            PartieNumerique++;
            string NouveauIdClient = "CL" + PartieNumerique.ToString();

            Console.WriteLine("Vous voulez insérez le client " + NouveauIdClient + " dans la BDD");
            string createClient = $@"INSERT INTO plateforme.client (idClient, NomC, PrénomC, AdresseC, TelephoneC, EmailC, regimeAlC, Id_Utilisateur)
                                VALUES ('{NouveauIdClient}', '{item.NomC}', '{item.PrénomC}', '{item.AdresseC}', '{item.TelephoneC}', '{item.EmailC}', '{item.regimeAlC}', '{item.Id_Utilisateur}')";
            item.idClient = NouveauIdClient; //on ajoute aussi l'id du client sur C#

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
            //Création de l'objet Command avec la requête
            string Requete = "select MAX(IdCuisinier) AS IdCuisinier from cuisinier;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            // Execution de la requête et récupération des résultats dans l'objet MySqlDataReader
            MySqlDataReader reader = Command.ExecuteReader();
            string IdCuisinier = "";
            // Récupération des résultats dans des variables C#
            while (reader.Read())
            {
                IdCuisinier = reader["IdCuisinier"].ToString();
            }
            reader.Close();
          
            int PartieNumerique = Convert.ToInt32(IdCuisinier.Substring(2));
            PartieNumerique++;
            string NouveauIdCuisinier = "CU" + PartieNumerique.ToString();

            Console.WriteLine("Vous voulez insérez l'utilisateur " + NouveauIdCuisinier+ " dans la BDD");
            string createClient = $@"INSERT INTO plateforme.cuisinier (IdCuisinier, NomP, prenomP, AdresseP, telephoneP, EmailP, spécialités, Id_Utilisateur)
                                VALUES ('{NouveauIdCuisinier}', '{item.nomP}', '{item.prenomP}', '{item.adresseP}', '{item.telephoneP}', '{item.emailP}', '{item.spécialités}', '{item.Id_Utilisateur}')";
            item.IdCuisinier = NouveauIdCuisinier;  //on modifie l'id du cuisinier également sur C#

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
                        UnClient.idClient = Elements[0];
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
