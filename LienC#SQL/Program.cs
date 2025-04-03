using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic.FileIO;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Tls.Crypto.Impl.BC;


namespace LienC_Sql
{
    internal class Program
    {

        static void Main(string[] args)
        {
<<<<<<< Updated upstream
            bool flag = true;
=======
>>>>>>> Stashed changes
            Console.WriteLine("Bienvenue sur Liv'in Paris!");
            Console.WriteLine("1-Nouveau? Inscrivez-vous! (Ajouter un utilisateur/client/cuisinier dans la BDD) ");
            Console.WriteLine("2-Connectez-vous.   (Afficher/modifier les informations d'un client/cuisinier, consulter les informations d'un client/cuisinier en particulier, supprimer un client/cuisinier) ");
            Console.WriteLine("3-Accès admin.(Vous avez besoin du mot de passe admin)   (Lancer des requêtes sur l'ensemble de la BDD, mettre à jour la BDD après une modification des fichiers csv) ");
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
                    bool flag1 = true;
                    while (flag1)
                    {
                        Console.WriteLine("Entrez le mot de passe d'admin:");
                        string u = Console.ReadLine();
                        if (u != "azerty")
                        {
                            Console.WriteLine("Mot de passe erroné, veuillez réessayer.");
                        }
                        else
                        {
                            flag1 = false;
                        }
                    }
                    flag1 = true;
                    while (flag1)
                    {
                        Console.WriteLine("Bienvenue admin, que voulez-vous faire?");
                        Console.WriteLine("1-Liste des clients par ordre alphabétique");
                        Console.WriteLine("2-Liste des clients par rue(cette méthode ne fonctionne pas)");
                        Console.WriteLine("3-Liste des clients par achat cumulés");
                        Console.WriteLine("4-Liste des cuisiniers par ordre alphabétique");
<<<<<<< Updated upstream
                        Console.WriteLine("5-Moyenne des prix des commandes");
<<<<<<< HEAD
                        Console.WriteLine("6-Connaitre les nationalités des plats réservés par chaque client");
                        Console.WriteLine("7-Quittez la page");
=======
                        Console.WriteLine("5-Quittez la page");
>>>>>>> Stashed changes
=======
                        Console.WriteLine("6-Connaitre l'historique des commandes et plat d'un client triés par nationalité du plat");
                        Console.WriteLine("7-Mettre à jour la BDD après modification des fichiers CSV");
                        Console.WriteLine("8-Quittez la page");
>>>>>>> 01be7191adb8d52e8a958be5a0ffe9f221fe983f
                        int v = Convert.ToInt32(Console.ReadLine());
                        switch (v)
                        {
                            case 1:
                                ClientAlphabeticOrder();
                                break;
                            case 2:
                                ClientStreetOrder();
                                break;
                            case 3:
                                ClientCumulativeAchatOrder();
                                break;
                            case 4:
                                CookerAlphabeticOrder();
                                break;
                            case 5:
<<<<<<< Updated upstream
                                MoyennePrixCommande();
                                break;
                            case 6:
                                CommandeNationalitePlat();
                                break;
                            case 7:
<<<<<<< HEAD
=======
>>>>>>> Stashed changes
=======
                                List<Utilisateur> UtilisateuraAjouté = GetListUtilisateurFromCSV();
                                List<Client> ClientsaAjouté = GetListClientsFromCSV();
                                List<Cuisinier> CuisiniersaAjouté = GetListCuisinierFromCSV();
                                for (int i = 0; i < UtilisateuraAjouté.Count; i++)
                                {
                                    if (ExistUtilisateur(UtilisateuraAjouté[i].Id_Utilisateur) == false)
                                    {
                                        InsertUtilisateurIntoDB(UtilisateuraAjouté[i]);
                                    }
                                }
                                for (int i = 0; i < ClientsaAjouté.Count; i++)
                                {
                                    if (ExistClient(ClientsaAjouté[i].idClient) == false)
                                    {
                                        InsertClientIntoDBv2(ClientsaAjouté[i]);
                                    }
                                }
                                for (int i = 0; i < CuisiniersaAjouté.Count; i++)
                                {
                                    if (ExistCuisinier(CuisiniersaAjouté[i].IdCuisinier) == false)
                                    {
                                        InsertCuisinierIntoDBv2(CuisiniersaAjouté[i]);
                                    }
                                }
                                Console.WriteLine("Mise à jour des données effectuées");
                                break;
                            case 8:
>>>>>>> 01be7191adb8d52e8a958be5a0ffe9f221fe983f
                                Console.WriteLine("A bientôt!");
                                flag1 = false;
                                break;
                            default:
                                Console.WriteLine("Option invalide.");
                                break;
                        }
                        Console.WriteLine();
                    }

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


            Console.WriteLine("Bien, maintenant vous voulez vous inscrire en tant que? ");
            Console.WriteLine("1-Un client");
            Console.WriteLine("2-Un cuisinier");
            Console.WriteLine("3-Un client et un cuisinier");
            int v = Convert.ToInt32(Console.ReadLine());
            switch (v)
            {

                case 1:
                    Console.WriteLine("Votre latitude de client? Attention écriture avec virgule");
                    double latitudeC = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre longitude de client?  Attention écriture avec virgule");
                    double longitudeC = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre numéro de téléphone de client");
                    long tel = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("Votre email de client?");
                    string mail = Console.ReadLine();
                    string idC = null;
                    Console.WriteLine("Précisez votre régime alimentaire");
                    string reg = Console.ReadLine();
                    Client client = new Client(idC, nom, prénom, latitudeC, longitudeC, tel, mail, reg, idU);
                    InsertClientIntoDB(client);
                    Console.WriteLine("Bienvenue en tant que nouveau client de Liv'in Paris!");
                    break;

                case 2:
                    Console.WriteLine("Votre latitude de cuisinier? Attention: Ecriture avec une virgule au lieu d'un point");
                    double latitudeP = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre longitude de cuisinier? Attention: Ecriture avec une virgule au lieu d'un point");
                    double longitudeP = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre numéro de téléphone de cuisinier? Attention: Ecriture avec une virgule au lieu d'un point");
                    long telP = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("Votre email de cuisinier?");
                    string mailP = Console.ReadLine();
                    string idcui = null;
                    Console.WriteLine("quels sont vos spécialités?");
                    string specui = Console.ReadLine();
                    Cuisinier cuisinier = new Cuisinier(idcui, nom, prénom, latitudeP, longitudeP, telP, mailP, specui, idU);
                    InsertCuisinierIntoDB(cuisinier);
                    Console.WriteLine("Bienvenue en tant que nouveau cuisinier de Liv'in Paris!");
                    break;

                case 3:
                    Console.WriteLine("Votre latitude de client? Attention: Ecriture avec une virgule au lieu d'un point");
                    double latitudeC1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre longitude de client Attention: Ecriture avec une virgule au lieu d'un point?");
                    double longitudeC1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre numéro de téléphone de client");
                    long tel1 = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("Votre email de client?");
                    string mail1 = Console.ReadLine();
                    string idC1 = null;
                    Console.WriteLine("Précisez votre régime alimentaire");
                    string reg1 = Console.ReadLine();
                    Client client1 = new Client(idC1, nom, prénom, latitudeC1, longitudeC1, tel1, mail1, reg1, idU);
                    InsertClientIntoDB(client1);
                    Console.WriteLine("Bienvenue en tant que nouveau client de Liv'in Paris!");
                    Console.WriteLine("Votre latitude de cuisinier? Attention: Ecriture avec une virgule au lieu d'un point");
                    double latitudeP1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre longitude de cuisinier? Attention: Ecriture avec une virgule au lieu d'un point");
                    double longitudeP1 = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Votre numéro de téléphone de cuisinier");
                    long telP1 = Convert.ToInt64(Console.ReadLine());
                    Console.WriteLine("Votre email de cuisinier?");
                    string mailP1 = Console.ReadLine();
                    string idcui1 = null;
                    Console.WriteLine("quels sont vos spécialités?");
                    string specui1 = Console.ReadLine();
                    Cuisinier cuisinier1 = new Cuisinier(idcui1, nom, prénom, latitudeP1, longitudeP1, telP1, mailP1, specui1, idU);
                    InsertCuisinierIntoDB(cuisinier1);
                    Console.WriteLine("Bienvenue en tant que nouveau cuisinier de Liv'in Paris!");
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
            if (valueString[0] == null)
            {
                Console.WriteLine("Le mot de passe est erroné, réessayez");
            }
            else
            {
                Console.WriteLine("Bienvenue " + IdSaisi + " !");
                reader.Close();
                Command.Dispose();
                bool flag1 = true;
                while (flag1)
                {
                    Console.WriteLine();
                    Console.WriteLine("Que voulez-vous faire?");
                    Console.WriteLine("1-Consultez vos informations de clients");
                    Console.WriteLine("2-Consultez vos informations de cuisiniers");
                    Console.WriteLine("3-Modifiez vos informations de clients");
                    Console.WriteLine("4-Modifiez vos informations de cuisiniers");
                    Console.WriteLine("5-Supprimez votre compte client");
                    Console.WriteLine("6-Supprimez votre compte cuisinier");
                    Console.WriteLine("7-Supprimez votre compte utilisateur");
                    Console.WriteLine("8-Si vous êtes cuisinier, consulter la liste des clients que vous avez livré");
                    Console.WriteLine("9-Si vous êtes cuisinier et que vous avez des commandes à livrés, regarder le trajet optimal que vous devez suivre");
                    Console.WriteLine("10-Quitter la plateforme");
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
                            string Requete3 = "Select IdClient from client where Id_Utilisateur=@id";
                            MySqlCommand Command3 = Connexion.CreateCommand();
                            Command3.CommandText = Requete3;
                            Command3.Parameters.Add(ParamId);
                            MySqlDataReader reader3 = Command3.ExecuteReader();
                            string[] valueString3 = new string[reader3.FieldCount];
                            Console.WriteLine(reader3.FieldCount);
                            while (reader3.Read())
                            {
                                for (int i = 0; i < reader3.FieldCount; i++)
                                {
                                    valueString3[i] = reader3.GetValue(i).ToString();
                                    Console.WriteLine(valueString3[i]);
                                }
                            }
                            if (valueString3[0] != null)
                            {
                                Client client = new Client(valueString3[0]);
                                reader3.Close();
                                Command3.Dispose();
                                UpdateClient(client);       // Lancement de la méthode intermédiaire si l'utilisateur est bien un client
                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que client");
                                reader3.Close();
                                Command3.Dispose();
                            }
                            break;
                        case 4:
                            string Requete4 = "Select IdCuisinier from cuisinier where Id_Utilisateur=@id";
                            MySqlCommand Command4 = Connexion.CreateCommand();
                            Command4.CommandText = Requete4;
                            Command4.Parameters.Add(ParamId);
                            MySqlDataReader reader4 = Command4.ExecuteReader();
                            string[] valueString4 = new string[reader4.FieldCount];
                            while (reader4.Read())
                            {
                                for (int i = 0; i < reader4.FieldCount; i++)
                                {
                                    valueString4[i] = reader4.GetValue(i).ToString();
                                }

                            }
                            if (valueString4[0] != null)
                            {
                                Cuisinier cuisinier = new Cuisinier(valueString4[0]);
                                reader4.Close();
                                Command4.Dispose();
                                UpdateCoocker(cuisinier);  // Lancement de la méthode intermédiaire si l'utilisateur est bien un cuisinier

                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que cuisinier");
                                reader4.Close();
                                Command4.Dispose();
                            }

                            break;
                        case 5:
                            string Requete6 = "Select IdClient from client where Id_Utilisateur=@id";
                            MySqlCommand Command6 = Connexion.CreateCommand();
                            Command6.CommandText = Requete6;
                            Command6.Parameters.Add(ParamId);
                            MySqlDataReader reader6 = Command6.ExecuteReader();
                            string[] valueString6 = new string[reader6.FieldCount];
                            while (reader6.Read())
                            {
                                for (int i = 0; i < reader6.FieldCount; i++)
                                {
                                    valueString6[i] = reader6.GetValue(i).ToString();
                                }

                            }
                            if (valueString6[0] != null)
                            {
                                Client client = new Client(valueString6[0]);
                                reader6.Close();
                                Command6.Dispose();
                                DeleteClient(client);  // Lancement de la méthode intermédiaire si l'utilisateur est bien un cuisinier

                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que cuisinier");
                                reader6.Close();
                                Command6.Dispose();
                            }
                            break;
                        case 6:
                            string Requete5 = "Select IdCuisinier from cuisinier where Id_Utilisateur=@id";
                            MySqlCommand Command5 = Connexion.CreateCommand();
                            Command5.CommandText = Requete5;
                            Command5.Parameters.Add(ParamId);
                            MySqlDataReader reader5 = Command5.ExecuteReader();
                            string[] valueString5 = new string[reader5.FieldCount];
                            while (reader5.Read())
                            {
                                for (int i = 0; i < reader5.FieldCount; i++)
                                {
                                    valueString5[i] = reader5.GetValue(i).ToString();
                                }

                            }
                            if (valueString5[0] != null)
                            {
                                Cuisinier cuisinier = new Cuisinier(valueString5[0]);
                                reader5.Close();
                                Command5.Dispose();
                                DeleteCuisinier(cuisinier);  // Lancement de la méthode intermédiaire si l'utilisateur est bien un cuisinier

                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant que cuisinier");
                                reader5.Close();
                                Command5.Dispose();
                            }
                            break;
                        case 7:
                            string Requete7 = "Select Id_Utilisateur from utilisateur where Id_Utilisateur=@id";
                            MySqlCommand Command7 = Connexion.CreateCommand();
                            Command7.CommandText = Requete7;
                            Command7.Parameters.Add(ParamId);
                            MySqlDataReader reader7 = Command7.ExecuteReader();
                            string[] valueString7 = new string[reader7.FieldCount];
                            while (reader7.Read())
                            {
                                for (int i = 0; i < reader7.FieldCount; i++)
                                {
                                    valueString7[i] = reader7.GetValue(i).ToString();
                                }

                            }
                            if (valueString7[0] != null)
                            {
                                Utilisateur utilisateur = new Utilisateur(valueString7[0]);
                                reader7.Close();
                                Command7.Dispose();
                                DeleteUser(utilisateur);

                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas inscrit en tant qu'utilisateur");
                                reader7.Close();
                                Command7.Dispose();
                            }
                            break;
                        case 8:
                            string Requete8 = "Select IdCuisinier from cuisinier where Id_Utilisateur=@id";
                            MySqlCommand Command8 = Connexion.CreateCommand();
                            Command8.CommandText = Requete8;
                            Command8.Parameters.Add(ParamId);
                            MySqlDataReader reader8 = Command8.ExecuteReader();
                            string[] valueString8 = new string[reader8.FieldCount];
                            Console.WriteLine(reader8.FieldCount);
                            while (reader8.Read())
                            {
                                for (int i = 0; i < reader8.FieldCount; i++)
                                {
                                    valueString8[i] = reader8.GetValue(i).ToString();
                                }
                            }
                            if (valueString8[0] != null)
                            {
                                Cuisinier cu = new Cuisinier(valueString8[0]);
                                reader8.Close();
                                Command8.Dispose();
                                ListeClientLivreParCuisinier(cu);      // Lancement de la méthode intermédiaire si l'utilisateur est bien un client
                            }
                            else
                            {
                                Console.WriteLine("Vous n'êtes pas cuisinier, vous ne pouvez pas faire cette action");
                                reader8.Close();
                                Command8.Dispose();
                            }

                            break;
                        case 9:
                            string Requete9 = "Select IdCuisinier from cuisinier where Id_Utilisateur=@id";
                            MySqlCommand Command9 = Connexion.CreateCommand();
                            Command9.CommandText = Requete9;
                            Command9.Parameters.Add(ParamId);
                            MySqlDataReader reader9 = Command9.ExecuteReader();
                            string[] valueString9 = new string[reader9.FieldCount];
                            Console.WriteLine(reader9.FieldCount);
                            while (reader9.Read())
                            {
                                for (int i = 0; i < reader9.FieldCount; i++)
                                {
                                    valueString9[i] = reader9.GetValue(i).ToString();
                                }
                            }
                            if (valueString9[0] != null)
                            {
                                Cuisinier cu = new Cuisinier(valueString9[0]);
                                reader9.Close();
                                Command9.Dispose();
                                Console.WriteLine("Voici les commandes que vous avez à livrés avec le trajet optimal à suivre");
                                TrajetaSuivrepourCuisinier(cu);
                            }
                            break;
                        case 10:
                            flag1 = false;
                            Console.WriteLine("A bientôt");
                            break;
                        default:
                            Console.WriteLine("Option invalide.");
                            break;



                    }
                }

            }



            Connexion.Close();

        }

        //Méthode intermédiaire   (--> il faudra faire en sorte que ces méthodes soient des méthodes de classe)

        //Méthode menu 1
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
<<<<<<< Updated upstream
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
            int PartieNumerique = Convert.ToInt32(IdClient.Substring(2));
            PartieNumerique++;
            string NouveauIdClient = "CL" + PartieNumerique.ToString();

            Console.WriteLine("Vous voulez insérez le client " + NouveauIdClient + " dans la BDD");

            item.idClient = NouveauIdClient; //on ajoute aussi l'id du client sur C#

            Console.WriteLine(NouveauIdClient);
            string createClient = @"INSERT INTO plateforme.client (idClient, NomC, PrénomC, LatitudeC, LongitudeC, TelephoneC, EmailC, regimeAlC, Id_Utilisateur)
                               VALUES (@idClient, @NomC, @PrénomC, @LatitudeC, @LongitudeC, @TelephoneC, @EmailC, @regimeAlC, @Id_Utilisateur)";

            using (MySqlCommand command = new MySqlCommand(createClient, Connexion))
            {
                command.Parameters.AddWithValue("@idClient", NouveauIdClient);
                command.Parameters.AddWithValue("@NomC", item.NomC);
                command.Parameters.AddWithValue("@PrénomC", item.PrénomC);
                command.Parameters.AddWithValue("@LatitudeC", item.latitudeC);
                command.Parameters.AddWithValue("@LongitudeC", item.longitudeC);
                command.Parameters.AddWithValue("@TelephoneC", item.TelephoneC);
                command.Parameters.AddWithValue("@EmailC", item.EmailC);
                command.Parameters.AddWithValue("@regimeAlC", item.regimeAlC);
                command.Parameters.AddWithValue("@Id_Utilisateur", item.Id_Utilisateur);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur connexion: " + e.ToString());
                    Console.ReadLine();
                }
            }
        }
        static void InsertCuisinierIntoDB(Cuisinier item)
=======
>>>>>>> Stashed changes
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            //Création de l'objet Command avec la requête
<<<<<<< Updated upstream
            string Requete = "select MAX(IdCuisinier) AS IdCuisinier from cuisinier;";
=======
            string Requete = "select MAX(IdClient) AS IdClient from client;";
>>>>>>> Stashed changes
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            // Execution de la requête et récupération des résultats dans l'objet MySqlDataReader
            MySqlDataReader reader = Command.ExecuteReader();
<<<<<<< Updated upstream
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
=======
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
>>>>>>> Stashed changes

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

            Console.WriteLine("Vous voulez insérez le cuisinier" + NouveauIdCuisinier + " dans la BDD");
            string createClient = $@"INSERT IGNORE INTO plateforme.cuisinier (IdCuisinier, NomP, prenomP, LatitudeP, LongitudeP telephoneP, EmailP, spécialités, Id_Utilisateur)
                                VALUES ('{NouveauIdCuisinier}', '{item.nomP}', '{item.prenomP}', '{Convert.ToString(item.latitudeP)}','{Convert.ToString(item.longitudeP)}' '{item.telephoneP}', '{item.emailP}', '{item.spécialités}', '{item.Id_Utilisateur}')";
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

        static void InsertClientIntoDBv2(Client item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            Console.WriteLine("Vous voulez insérez le client " + item.idClient + " dans la BDD");

            string createClient = @"INSERT INTO plateforme.client (idClient, NomC, PrénomC, LatitudeC, LongitudeC, TelephoneC, EmailC, regimeAlC, Id_Utilisateur)
                               VALUES (@idClient, @NomC, @PrénomC, @LatitudeC, @LongitudeC, @TelephoneC, @EmailC, @regimeAlC, @Id_Utilisateur)";

            using (MySqlCommand command = new MySqlCommand(createClient, Connexion))
            {
                command.Parameters.AddWithValue("@idClient", item.idClient);
                command.Parameters.AddWithValue("@NomC", item.NomC);
                command.Parameters.AddWithValue("@PrénomC", item.PrénomC);
                command.Parameters.AddWithValue("@LatitudeC", item.latitudeC);
                command.Parameters.AddWithValue("@LongitudeC", item.longitudeC);
                command.Parameters.AddWithValue("@TelephoneC", item.TelephoneC);
                command.Parameters.AddWithValue("@EmailC", item.EmailC);
                command.Parameters.AddWithValue("@regimeAlC", item.regimeAlC);
                command.Parameters.AddWithValue("@Id_Utilisateur", item.Id_Utilisateur);

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (MySqlException e)
                {
                    Console.WriteLine("Erreur connexion: " + e.ToString());
                    Console.ReadLine();
                }
                //MySqlCommand command = Connexion.CreateCommand();
                //command.CommandText = createClient;
                //try
                //{
                //   command.ExecuteNonQuery();
                //}
                //catch (MySqlException e)
                //{
                //    Console.WriteLine("Erreur connexion: " + e.ToString());
                //    Console.ReadLine();
                //   return;
                // }
                command.Dispose();
            }
        }
        static void InsertCuisinierIntoDBv2(Cuisinier item)
            {
                string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                Connexion.Open();
                Console.WriteLine("Vous voulez insérez le cuisinier" + item.IdCuisinier + " dans la BDD");
                string createClient = $@"INSERT IGNORE INTO plateforme.cuisinier (IdCuisinier, NomP, prenomP, LatitudeP, LongitudeP, telephoneP, EmailP, spécialités, Id_Utilisateur)
                                VALUES ('{item.IdCuisinier}', '{item.nomP}', '{item.prenomP}', '{Convert.ToString(item.latitudeP)}', '{Convert.ToString(item.longitudeP)}', '{item.telephoneP}', '{item.emailP}', '{item.spécialités}', '{item.Id_Utilisateur}')";

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
            //Méthode menu 2
        static void UpdateClient(Client client)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = client.idClient;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Quelle donnée voulez-vous modifier?");
                Console.WriteLine("1-Nom");
                Console.WriteLine("2-Prénom");
                Console.WriteLine("3-Adresse");
                Console.WriteLine("4-Téléphone");
                Console.WriteLine("5-Email");
                Console.WriteLine("6-Régime");
                Console.WriteLine("7-Revenir au menu précédent");
                int s = Convert.ToInt32(Console.ReadLine());
                switch (s)
                {
                    case 1:
                        Console.WriteLine("Entrez le nouveau nom:");
                        string newnomC = Console.ReadLine();
                        MySqlParameter ParamnewnomC = new MySqlParameter("@newnomC", MySqlDbType.VarChar);
                        ParamnewnomC.Value = newnomC;
                        string requete1 = "UPDATE client SET nomC = @newnomC WHERE IdClient = @idC";
                        MySqlCommand Command = Connexion.CreateCommand();
                        Command.CommandText = requete1;
                        Command.Parameters.Add(ParamIdC);
                        Command.Parameters.Add(ParamnewnomC);
                        int rowsAffected1 = Command.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Entrez le nouveau prénom:");
                        string newnameC = Console.ReadLine();
                        MySqlParameter ParamnewnameC = new MySqlParameter("@newnameC", MySqlDbType.VarChar);
                        ParamnewnameC.Value = newnameC;
                        string requete2 = "UPDATE client SET PrénomC = @newnnameC WHERE IdClient = @idC";
                        MySqlCommand Command2 = Connexion.CreateCommand();
                        Command2.CommandText = requete2;
                        Command2.Parameters.Add(ParamIdC);
                        Command2.Parameters.Add(ParamnewnameC);
                        int rowsAffected2 = Command2.ExecuteNonQuery();
                        break;
                    case 3:
                        Console.WriteLine("Entrez la nouvelle adresse :");
                        string newadresseC = Console.ReadLine();
                        MySqlParameter ParamnewadresseC = new MySqlParameter("@newadresseC", MySqlDbType.VarChar);
                        ParamnewadresseC.Value = newadresseC;
                        string requete3 = "UPDATE client SET AdresseC = @newnadresseC WHERE IdClient = @idC";
                        MySqlCommand Command3 = Connexion.CreateCommand();
                        Command3.CommandText = requete3;
                        Command3.Parameters.Add(ParamIdC);
                        Command3.Parameters.Add(ParamnewadresseC);
                        int rowsAffected3 = Command3.ExecuteNonQuery();
                        break;
                    case 4:
                        Console.WriteLine("Entrez la nouveau numéro de téléphone :");
                        long newtelC = Convert.ToInt64(Console.ReadLine());
                        MySqlParameter ParamnewtelC = new MySqlParameter("@newtelC", MySqlDbType.VarChar);
                        ParamnewtelC.Value = newtelC;
                        string requete4 = "UPDATE client SET TelephoneC = @newtelC WHERE IdClient = @idC";
                        MySqlCommand Command4 = Connexion.CreateCommand();
                        Command4.CommandText = requete4;
                        Command4.Parameters.Add(ParamIdC);
                        Command4.Parameters.Add(ParamnewtelC);
                        int rowsAffected4 = Command4.ExecuteNonQuery();
                        break;

                    case 5:
                        Console.WriteLine("Entrez le nouvel email :");
                        string newEmailC = Console.ReadLine();
                        MySqlParameter ParamNewEmailC = new MySqlParameter("@newEmailC", MySqlDbType.VarChar);
                        ParamNewEmailC.Value = newEmailC;

                        string requete5 = "UPDATE client SET EmailC = @newEmailC WHERE IdClient = @idC";
                        MySqlCommand Command5 = Connexion.CreateCommand();
                        Command5.CommandText = requete5;
                        Command5.Parameters.Add(ParamIdC);
                        Command5.Parameters.Add(ParamNewEmailC);
                        int rowsAffected5 = Command5.ExecuteNonQuery();
                        break;
                    case 6:
                        Console.WriteLine("Entrez le nouveau régime alimentaire :");
                        string newRegimeAlC = Console.ReadLine();
                        MySqlParameter ParamNewRegimeAlC = new MySqlParameter("@newRegimeAlC", MySqlDbType.VarChar);
                        ParamNewRegimeAlC.Value = newRegimeAlC;

                        string requete6 = "UPDATE client SET regimeAlC = @newRegimeAlC WHERE IdClient = @idC";
                        MySqlCommand Command6 = Connexion.CreateCommand();
                        Command6.CommandText = requete6;
                        Command6.Parameters.Add(ParamIdC);
                        Command6.Parameters.Add(ParamNewRegimeAlC);
                        int rowsAffected6 = Command6.ExecuteNonQuery();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
            Console.WriteLine("Si il y a eu modification, voici vos nouvelles informations:");
            SpecificClient(idC);
        }
        static void UpdateCoocker(Cuisinier cuisinier)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = cuisinier.IdCuisinier;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Quelle donnée voulez-vous modifier?");
                Console.WriteLine("1-Nom");
                Console.WriteLine("2-Prénom");
                Console.WriteLine("3-Adresse");
                Console.WriteLine("4-Téléphone");
                Console.WriteLine("5-Email");
                Console.WriteLine("6-Spécialités");
                Console.WriteLine("7-Revenir au menu précédent");
                int s = Convert.ToInt32(Console.ReadLine());
                switch (s)
                {
                    case 1:
                        Console.WriteLine("Entrez le nouveau nom:");
                        string newnomC = Console.ReadLine();
                        MySqlParameter ParamnewnomC = new MySqlParameter("@newnomC", MySqlDbType.VarChar);
                        ParamnewnomC.Value = newnomC;
                        string requete1 = "UPDATE cuisinier SET nomP = @newnomC WHERE IDCuisinier = @idC";
                        MySqlCommand Command = Connexion.CreateCommand();
                        Command.CommandText = requete1;
                        Command.Parameters.Add(ParamIdC);
                        Command.Parameters.Add(ParamnewnomC);
                        int rowsAffected1 = Command.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Entrez le nouveau prénom:");
                        string newnameC = Console.ReadLine();
                        MySqlParameter ParamnewnameC = new MySqlParameter("@newnameC", MySqlDbType.VarChar);
                        ParamnewnameC.Value = newnameC;
                        string requete2 = "UPDATE cuisinier SET prenomP = @newnnameC WHERE IDCuisinier = @idC";
                        MySqlCommand Command2 = Connexion.CreateCommand();
                        Command2.CommandText = requete2;
                        Command2.Parameters.Add(ParamIdC);
                        Command2.Parameters.Add(ParamnewnameC);
                        int rowsAffected2 = Command2.ExecuteNonQuery();
                        break;
                    case 3:
                        Console.WriteLine("Entrez la nouvelle adresse :");
                        string newadresseC = Console.ReadLine();
                        MySqlParameter ParamnewadresseC = new MySqlParameter("@newadresseC", MySqlDbType.VarChar);
                        ParamnewadresseC.Value = newadresseC;
                        string requete3 = "UPDATE cuisinier SET adresseP = @newnadresseC WHERE IDCuisinier = @idC";
                        MySqlCommand Command3 = Connexion.CreateCommand();
                        Command3.CommandText = requete3;
                        Command3.Parameters.Add(ParamIdC);
                        Command3.Parameters.Add(ParamnewadresseC);
                        int rowsAffected3 = Command3.ExecuteNonQuery();
                        break;
                    case 4:
                        Console.WriteLine("Entrez la nouveau numéro de téléphone :");
                        long newtelC = Convert.ToInt64(Console.ReadLine());
                        MySqlParameter ParamnewtelC = new MySqlParameter("@newtelC", MySqlDbType.VarChar);
                        ParamnewtelC.Value = newtelC;
                        string requete4 = "UPDATE cuisinier SET telephoneP = @newtelC WHERE IDCuisinier = @idC";
                        MySqlCommand Command4 = Connexion.CreateCommand();
                        Command4.CommandText = requete4;
                        Command4.Parameters.Add(ParamIdC);
                        Command4.Parameters.Add(ParamnewtelC);
                        int rowsAffected4 = Command4.ExecuteNonQuery();
                        break;

                    case 5:
                        Console.WriteLine("Entrez le nouvel email :");
                        string newEmailC = Console.ReadLine();
                        MySqlParameter ParamNewEmailC = new MySqlParameter("@newEmailC", MySqlDbType.VarChar);
                        ParamNewEmailC.Value = newEmailC;

                        string requete5 = "UPDATE cuisinier SET emailP = @newEmailC WHERE IDCuisinier = @idC";
                        MySqlCommand Command5 = Connexion.CreateCommand();
                        Command5.CommandText = requete5;
                        Command5.Parameters.Add(ParamIdC);
                        Command5.Parameters.Add(ParamNewEmailC);
                        int rowsAffected5 = Command5.ExecuteNonQuery();
                        break;
                    case 6:
                        Console.WriteLine("Entrez la nouvelle spécialité :");
                        string newspecialite = Console.ReadLine();
                        MySqlParameter ParamNewspecialite = new MySqlParameter("@newspecialite", MySqlDbType.VarChar);
                        ParamNewspecialite.Value = newspecialite;

                        string requete6 = "UPDATE cuisinier SET spécialités = @newspecialite WHERE IDCuisinier = @idC";
                        MySqlCommand Command6 = Connexion.CreateCommand();
                        Command6.CommandText = requete6;
                        Command6.Parameters.Add(ParamIdC);
                        Command6.Parameters.Add(ParamNewspecialite);
                        int rowsAffected6 = Command6.ExecuteNonQuery();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
            Console.WriteLine("Si il y a eu modification, voici vos nouvelles informations:");
            SpecificCooker(idC);
        }


        static void DeleteClient(Client client)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = client.idClient;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete = "DELETE FROM client WHERE IdClient = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Client" + client.idClient + " supprimé");
        }
        static void DeleteCuisinier(Cuisinier cuisinier)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = cuisinier.IdCuisinier;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete = "DELETE FROM cuisinier WHERE IDCuisinier = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Cuisinier" + cuisinier.IdCuisinier + " supprimé");
        }
        static void DeleteUser(Utilisateur utilisateur)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = utilisateur.Id_Utilisateur;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete2 = "DELETE FROM cuisinier WHERE Id_Utilisateur = @idC";
            MySqlCommand Command2 = Connexion.CreateCommand();
            Command2.CommandText = requete2;
            Command2.Parameters.Add(ParamIdC);
            int rowsAffected2 = Command2.ExecuteNonQuery();
            Console.WriteLine(" Votre compte cuisinier est supprimé");
            string requete3 = "DELETE FROM client WHERE Id_Utilisateur = @idC";
            MySqlCommand Command3 = Connexion.CreateCommand();
            Command3.CommandText = requete3;
            Command3.Parameters.Add(ParamIdC);
            int rowsAffected3 = Command3.ExecuteNonQuery();
            Console.WriteLine(" Votre compte client est supprimé");
            string requete = "DELETE FROM utilisateur WHERE Id_Utilisateur = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Utilisateur" + utilisateur.Id_Utilisateur + " supprimé");
        }

        static void ListeClientLivreParCuisinier(Cuisinier item)
        {
            string idC = item.IdCuisinier;
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            MySqlParameter ParamId = new MySqlParameter("@id", MySqlDbType.VarChar);
            ParamId.Value = idC;
            string Requete = "SELECT DISTINCT c.idClient FROM Client c JOIN Commande cmd ON c.idClient = cmd.idClient JOIN Livrer l ON cmd.idCommande = l.IdCommande JOIN Cuisinier cu ON l.IDCuisinier = cu.IDCuisinier WHERE cu.IDCuisinier = @id AND l.Statut = 'livré';";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.Parameters.Add(ParamId);
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients que le cuisinier a livré");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }

        static void TrajetaSuivrepourCuisinier(Cuisinier item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            // il faut calculer le chemin à suivre ici
            string cheminaSuivre=" ";
            MySqlParameter ParamcheminaSuivre = new MySqlParameter("@chemin", MySqlDbType.VarChar);
            ParamcheminaSuivre.Value = cheminaSuivre;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = item.IdCuisinier;
            string requetePrel = "UPDATE commande SET CheminOpt = @chemin WHERE IDCuisinier = @idC";
            MySqlCommand CommandPrel = Connexion.CreateCommand();
            CommandPrel.CommandText = requetePrel;
            CommandPrel.Parameters.Add(ParamcheminaSuivre);
            int rowsAffected1 = CommandPrel.ExecuteNonQuery();
     
            string Requete = "SELECT IdCommande, CheminOpt FROM livrer WHERE Statut = 'non livré' AND IdCuisinier = @idC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            Command.Parameters.Add(ParamIdC);
            MySqlDataReader reader = Command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }
        // Méthode menu 3
        static void ClientAlphabeticOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Client order by NomC ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par ordre alphabétique:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }

        static bool ExistUtilisateur(string idU)
        {
            bool flag = true;
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idU;

            string Requete = "Select Id_Utilisateur from Utilisateur WHERE Id_Utilisateur=@idC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            Command.Parameters.Add(ParamIdC);
            MySqlDataReader reader = Command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            if (valueString[0] == null)
            {
                flag = false;
            }
            reader.Close();
            Command.Dispose();
            return flag;
        }

        static bool ExistClient(string idC)
        {
            bool flag = true;
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;

            string Requete = "Select IdClient from client WHERE IdClient=@idC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            Command.Parameters.Add(ParamIdC);
            MySqlDataReader reader = Command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            if (valueString[0] == null)
            {
                flag = false;
            }
            reader.Close();
            Command.Dispose();
            return flag;
        }
        static bool ExistCuisinier(string idC)
        {
            bool flag = true;
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;

            string Requete = "Select IdCuisinier from cuisinier WHERE IdCuisinier=@idC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            Command.Parameters.Add(ParamIdC);
            MySqlDataReader reader = Command.ExecuteReader();
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            if (valueString[0] == null)
            {
                flag = false;
            }
            reader.Close();
            Command.Dispose();
            return flag;
        }

        static void SpecificClient(string idC)
        {
            {
                string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                Connexion.Open();

                MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
                ParamIdC.Value = idC;

                string Requete = "Select * from Client WHERE IDClient=@idC;";
                MySqlCommand Command = Connexion.CreateCommand();
                Command.CommandText = Requete;
                Command.Parameters.Add(ParamIdC);
                MySqlDataReader reader = Command.ExecuteReader();
                Console.WriteLine("Voici vos nouvelles informations de client:");
                string[] valueString = new string[reader.FieldCount];
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        valueString[i] = reader.GetValue(i).ToString();
                        Console.Write(valueString[i] + " , ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                Command.Dispose();
            }
        }
        static void SpecificCooker(string idC)
        {
            {
                string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                Connexion.Open();

                MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
                ParamIdC.Value = idC;

                string Requete = "Select * from cuisinier WHERE IDCuisinier=@idC;";
                MySqlCommand Command = Connexion.CreateCommand();
                Command.CommandText = Requete;
                Command.Parameters.Add(ParamIdC);
                MySqlDataReader reader = Command.ExecuteReader();
                Console.WriteLine("Voici vos nouvelles informations de cuisinier:");
                string[] valueString = new string[reader.FieldCount];
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        valueString[i] = reader.GetValue(i).ToString();
                        Console.Write(valueString[i] + " , ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                Command.Dispose();
            }
        }
        static void CookerAlphabeticOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Cuisinier order by nomP ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des cuisiniers ordonnées par ordre alphabétique:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }
        static void ClientStreetOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Client order by AdresseC ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par adresse:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();

        }
        static void ClientCumulativeAchatOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string Requete = "SELECT c.IdClient, NomC, PrénomC, SUM(p.prix) AS AchatCumulés FROM Client c JOIN Commande cmd ON c.IdClient = cmd.IdClient JOIN constituer_de_ cd ON cmd.IdCommande = cd.IdCommande JOIN Plat p ON cd.Id_Plat = p.Id_Plat GROUP BY c.IdClient ORDER BY AchatCumulés ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par ordre croissant des achats cumulés:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();

        }

<<<<<<< HEAD

        //Méthode menu 2
        static void UpdateClient(Client client)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = client.idClient;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Quelle donnée voulez-vous modifier?");
                Console.WriteLine("1-Nom");
                Console.WriteLine("2-Prénom");
                Console.WriteLine("3-Adresse");
                Console.WriteLine("4-Téléphone");
                Console.WriteLine("5-Email");
                Console.WriteLine("6-Régime");
                Console.WriteLine("7-Revenir au menu précédent");
                int s = Convert.ToInt32(Console.ReadLine());
                switch (s)
                {
                    case 1:
                        Console.WriteLine("Entrez le nouveau nom:");
                        string newnomC = Console.ReadLine();
                        MySqlParameter ParamnewnomC = new MySqlParameter("@newnomC", MySqlDbType.VarChar);
                        ParamnewnomC.Value = newnomC;
                        string requete1 = "UPDATE client SET nomC = @newnomC WHERE IdClient = @idC";
                        MySqlCommand Command = Connexion.CreateCommand();
                        Command.CommandText = requete1;
                        Command.Parameters.Add(ParamIdC);
                        Command.Parameters.Add(ParamnewnomC);
                        int rowsAffected1 = Command.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Entrez le nouveau prénom:");
                        string newnameC = Console.ReadLine();
                        MySqlParameter ParamnewnameC = new MySqlParameter("@newnameC", MySqlDbType.VarChar);
                        ParamnewnameC.Value = newnameC;
                        string requete2 = "UPDATE client SET PrénomC = @newnnameC WHERE IdClient = @idC";
                        MySqlCommand Command2 = Connexion.CreateCommand();
                        Command2.CommandText = requete2;
                        Command2.Parameters.Add(ParamIdC);
                        Command2.Parameters.Add(ParamnewnameC);
                        int rowsAffected2 = Command2.ExecuteNonQuery();
                        break;
                    case 3:
                        Console.WriteLine("Entrez la nouvelle adresse :");
                        string newadresseC = Console.ReadLine();
                        MySqlParameter ParamnewadresseC = new MySqlParameter("@newadresseC", MySqlDbType.VarChar);
                        ParamnewadresseC.Value = newadresseC;
                        string requete3 = "UPDATE client SET AdresseC = @newnadresseC WHERE IdClient = @idC";
                        MySqlCommand Command3 = Connexion.CreateCommand();
                        Command3.CommandText = requete3;
                        Command3.Parameters.Add(ParamIdC);
                        Command3.Parameters.Add(ParamnewadresseC);
                        int rowsAffected3 = Command3.ExecuteNonQuery();
                        break;
                    case 4:
                        Console.WriteLine("Entrez la nouveau numéro de téléphone :");
                        long newtelC = Convert.ToInt64(Console.ReadLine());
                        MySqlParameter ParamnewtelC = new MySqlParameter("@newtelC", MySqlDbType.VarChar);
                        ParamnewtelC.Value = newtelC;
                        string requete4 = "UPDATE client SET TelephoneC = @newtelC WHERE IdClient = @idC";
                        MySqlCommand Command4 = Connexion.CreateCommand();
                        Command4.CommandText = requete4;
                        Command4.Parameters.Add(ParamIdC);
                        Command4.Parameters.Add(ParamnewtelC);
                        int rowsAffected4 = Command4.ExecuteNonQuery();
                        break;

                    case 5:
                        Console.WriteLine("Entrez le nouvel email :");
                        string newEmailC = Console.ReadLine();
                        MySqlParameter ParamNewEmailC = new MySqlParameter("@newEmailC", MySqlDbType.VarChar);
                        ParamNewEmailC.Value = newEmailC;

                        string requete5 = "UPDATE client SET EmailC = @newEmailC WHERE IdClient = @idC";
                        MySqlCommand Command5 = Connexion.CreateCommand();
                        Command5.CommandText = requete5;
                        Command5.Parameters.Add(ParamIdC);
                        Command5.Parameters.Add(ParamNewEmailC);
                        int rowsAffected5 = Command5.ExecuteNonQuery();
                        break;
                    case 6:
                        Console.WriteLine("Entrez le nouveau régime alimentaire :");
                        string newRegimeAlC = Console.ReadLine();
                        MySqlParameter ParamNewRegimeAlC = new MySqlParameter("@newRegimeAlC", MySqlDbType.VarChar);
                        ParamNewRegimeAlC.Value = newRegimeAlC;

                        string requete6 = "UPDATE client SET regimeAlC = @newRegimeAlC WHERE IdClient = @idC";
                        MySqlCommand Command6 = Connexion.CreateCommand();
                        Command6.CommandText = requete6;
                        Command6.Parameters.Add(ParamIdC);
                        Command6.Parameters.Add(ParamNewRegimeAlC);
                        int rowsAffected6 = Command6.ExecuteNonQuery();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
            Console.WriteLine("Si il y a eu modification, voici vos nouvelles informations:");
            SpecificClient(idC);
        }
        static void UpdateCoocker(Cuisinier cuisinier)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = cuisinier.IdCuisinier;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            bool flag = true;
            while (flag)
            {
                Console.WriteLine("Quelle donnée voulez-vous modifier?");
                Console.WriteLine("1-Nom");
                Console.WriteLine("2-Prénom");
                Console.WriteLine("3-Adresse");
                Console.WriteLine("4-Téléphone");
                Console.WriteLine("5-Email");
                Console.WriteLine("6-Spécialités");
                Console.WriteLine("7-Revenir au menu précédent");
                int s = Convert.ToInt32(Console.ReadLine());
                switch (s)
                {
                    case 1:
                        Console.WriteLine("Entrez le nouveau nom:");
                        string newnomC = Console.ReadLine();
                        MySqlParameter ParamnewnomC = new MySqlParameter("@newnomC", MySqlDbType.VarChar);
                        ParamnewnomC.Value = newnomC;
                        string requete1 = "UPDATE cuisinier SET nomP = @newnomC WHERE IDCuisinier = @idC";
                        MySqlCommand Command = Connexion.CreateCommand();
                        Command.CommandText = requete1;
                        Command.Parameters.Add(ParamIdC);
                        Command.Parameters.Add(ParamnewnomC);
                        int rowsAffected1 = Command.ExecuteNonQuery();
                        break;
                    case 2:
                        Console.WriteLine("Entrez le nouveau prénom:");
                        string newnameC = Console.ReadLine();
                        MySqlParameter ParamnewnameC = new MySqlParameter("@newnameC", MySqlDbType.VarChar);
                        ParamnewnameC.Value = newnameC;
                        string requete2 = "UPDATE cuisinier SET prenomP = @newnnameC WHERE IDCuisinier = @idC";
                        MySqlCommand Command2 = Connexion.CreateCommand();
                        Command2.CommandText = requete2;
                        Command2.Parameters.Add(ParamIdC);
                        Command2.Parameters.Add(ParamnewnameC);
                        int rowsAffected2 = Command2.ExecuteNonQuery();
                        break;
                    case 3:
                        Console.WriteLine("Entrez la nouvelle adresse :");
                        string newadresseC = Console.ReadLine();
                        MySqlParameter ParamnewadresseC = new MySqlParameter("@newadresseC", MySqlDbType.VarChar);
                        ParamnewadresseC.Value = newadresseC;
                        string requete3 = "UPDATE cuisinier SET adresseP = @newnadresseC WHERE IDCuisinier = @idC";
                        MySqlCommand Command3 = Connexion.CreateCommand();
                        Command3.CommandText = requete3;
                        Command3.Parameters.Add(ParamIdC);
                        Command3.Parameters.Add(ParamnewadresseC);
                        int rowsAffected3 = Command3.ExecuteNonQuery();
                        break;
                    case 4:
                        Console.WriteLine("Entrez la nouveau numéro de téléphone :");
                        long newtelC = Convert.ToInt64(Console.ReadLine());
                        MySqlParameter ParamnewtelC = new MySqlParameter("@newtelC", MySqlDbType.VarChar);
                        ParamnewtelC.Value = newtelC;
                        string requete4 = "UPDATE cuisinier SET telephoneP = @newtelC WHERE IDCuisinier = @idC";
                        MySqlCommand Command4 = Connexion.CreateCommand();
                        Command4.CommandText = requete4;
                        Command4.Parameters.Add(ParamIdC);
                        Command4.Parameters.Add(ParamnewtelC);
                        int rowsAffected4 = Command4.ExecuteNonQuery();
                        break;

                    case 5:
                        Console.WriteLine("Entrez le nouvel email :");
                        string newEmailC = Console.ReadLine();
                        MySqlParameter ParamNewEmailC = new MySqlParameter("@newEmailC", MySqlDbType.VarChar);
                        ParamNewEmailC.Value = newEmailC;

                        string requete5 = "UPDATE cuisinier SET emailP = @newEmailC WHERE IDCuisinier = @idC";
                        MySqlCommand Command5 = Connexion.CreateCommand();
                        Command5.CommandText = requete5;
                        Command5.Parameters.Add(ParamIdC);
                        Command5.Parameters.Add(ParamNewEmailC);
                        int rowsAffected5 = Command5.ExecuteNonQuery();
                        break;
                    case 6:
                        Console.WriteLine("Entrez la nouvelle spécialité :");
                        string newspecialite = Console.ReadLine();
                        MySqlParameter ParamNewspecialite = new MySqlParameter("@newspecialite", MySqlDbType.VarChar);
                        ParamNewspecialite.Value =newspecialite;

                        string requete6 = "UPDATE cuisinier SET spécialités = @newspecialite WHERE IDCuisinier = @idC";
                        MySqlCommand Command6 = Connexion.CreateCommand();
                        Command6.CommandText = requete6;
                        Command6.Parameters.Add(ParamIdC);
                        Command6.Parameters.Add(ParamNewspecialite);
                        int rowsAffected6 = Command6.ExecuteNonQuery();
                        break;
                    case 7:
                        flag = false;
                        break;
                    default:
                        Console.WriteLine("Option invalide");
                        break;
                }
            }
            Console.WriteLine("Si il y a eu modification, voici vos nouvelles informations:");
            SpecificCooker(idC);
        }


        static void DeleteClient(Client client) 
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = client.idClient;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete = "DELETE FROM client WHERE IdClient = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Client" + client.idClient + " supprimé");
        }
        static void DeleteCuisinier(Cuisinier cuisinier) 
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = cuisinier.IdCuisinier;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete = "DELETE FROM cuisinier WHERE IDCuisinier = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Cuisinier" + cuisinier.IdCuisinier + " supprimé");
        }
        static void DeleteUser(Utilisateur utilisateur) 
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string idC = utilisateur.Id_Utilisateur;
            MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
            ParamIdC.Value = idC;
            string requete2 = "DELETE FROM cuisinier WHERE Id_Utilisateur = @idC";
            MySqlCommand Command2 = Connexion.CreateCommand();
            Command2.CommandText = requete2;
            Command2.Parameters.Add(ParamIdC);
            int rowsAffected2 = Command2.ExecuteNonQuery();
            Console.WriteLine(" Votre compte cuisinier est supprimé");
            string requete3 = "DELETE FROM client WHERE Id_Utilisateur = @idC";
            MySqlCommand Command3 = Connexion.CreateCommand();
            Command3.CommandText = requete3;
            Command3.Parameters.Add(ParamIdC);
            int rowsAffected3 = Command3.ExecuteNonQuery();
            Console.WriteLine(" Votre compte client est supprimé");
            string requete = "DELETE FROM utilisateur WHERE Id_Utilisateur = @idC";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = requete;
            Command.Parameters.Add(ParamIdC);
            int rowsAffected = Command.ExecuteNonQuery();
            Console.WriteLine("Utilisateur" + utilisateur.Id_Utilisateur + " supprimé");
        }

        // Méthode menu 3
        static void ClientAlphabeticOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Client order by NomC ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par ordre alphabétique:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }
        static void SpecificClient(string idC)
        {
            {
                string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                Connexion.Open();

                MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
                ParamIdC.Value = idC;

                string Requete = "Select * from Client WHERE IDClient=@idC;";
                MySqlCommand Command = Connexion.CreateCommand();
                Command.CommandText = Requete;
                Command.Parameters.Add(ParamIdC);
                MySqlDataReader reader = Command.ExecuteReader();
                Console.WriteLine("Voici vos nouvelles informations de client:");
                string[] valueString = new string[reader.FieldCount];
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        valueString[i] = reader.GetValue(i).ToString();
                        Console.Write(valueString[i] + " , ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                Command.Dispose();
            }
        }
        static void SpecificCooker(string idC)
        {
            {
                string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
                MySqlConnection Connexion = new MySqlConnection(ConnexionString);
                Connexion.Open();

                MySqlParameter ParamIdC = new MySqlParameter("@idC", MySqlDbType.VarChar);
                ParamIdC.Value = idC;

                string Requete = "Select * from cuisinier WHERE IDCuisinier=@idC;";
                MySqlCommand Command = Connexion.CreateCommand();
                Command.CommandText = Requete;
                Command.Parameters.Add(ParamIdC);
                MySqlDataReader reader = Command.ExecuteReader();
                Console.WriteLine("Voici vos nouvelles informations de cuisinier:");
                string[] valueString = new string[reader.FieldCount];
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        valueString[i] = reader.GetValue(i).ToString();
                        Console.Write(valueString[i] + " , ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                Command.Dispose();
            }
        }
        static void CookerAlphabeticOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Cuisinier order by nomP ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des cuisiniers ordonnées par ordre alphabétique:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }
        static void ClientStreetOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "Select * from Client order by AdresseC ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par adresse:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();

        }
        static void ClientCumulativeAchatOrder()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string Requete = "SELECT c.IdClient, NomC, PrénomC, SUM(p.prix) AS AchatCumulés FROM Client c JOIN Commande cmd ON c.IdClient = cmd.IdClient JOIN constituer_de_ cd ON cmd.IdCommande = cd.IdCommande JOIN Plat p ON cd.Id_Plat = p.Id_Plat GROUP BY c.IdClient ORDER BY AchatCumulés ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la liste des clients ordonnées par ordre croissant des achats cumulés:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();

        }

        static void MoyennePrixCommande()
=======
        static void CommandeNationalitePlat()
>>>>>>> 01be7191adb8d52e8a958be5a0ffe9f221fe983f
        {
            Console.WriteLine("Saisissez l'id d'un client:");
            string idC = Console.ReadLine();
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            MySqlParameter ParamId = new MySqlParameter("@id", MySqlDbType.VarChar);
            ParamId.Value = idC;
            string Requete = "SELECT Idcommande, NomP, Nationalité FROM constituer_de_ cd JOIN Plat p ON cd.Id_Plat=p.Id_Plat WHERE Idcommande IN ( SELECT idcommande FROM commande WHERE idCLient=@id) ";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.Parameters.Add(ParamId);
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici l'historique des commandes du client triés par nationalité des plats");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();
        }

        static void MoyennePrixCommande()
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=plateforme;UID=root;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
            string Requete = "SELECT AVG(prix) as MoyennePrixCommande FROM Commande com JOIN constituer_de_ cd ON com.IdCommande=cd.IdCommande JOIN Plat p ON cd.Id_Plat = p.Id_Plat ORDER BY MoyennePrixCommande ASC;";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;
            MySqlDataReader reader = Command.ExecuteReader();
            Console.WriteLine("Voici la moyenne des prix des commandes:");
            string[] valueString = new string[reader.FieldCount];
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    valueString[i] = reader.GetValue(i).ToString();
                    Console.Write(valueString[i] + " , ");
                }
                Console.WriteLine();
            }
            reader.Close();
            Command.Dispose();

        }

        //méthode lien .csv/BDD
        static List<Client> GetListClientsFromCSV()
        {
            List<Client> ListeClient = new List<Client>();
            // Cette méthode(fonction) va se charger de parser le fichier csv et
            //récupérer chaque ligne du fichier csv dans un objet client
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\cyril\OneDrive\Bureau\projet PSI\PSI-Rendu1\Clients.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    try
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
                            UnClient.latitudeC = double.Parse(Elements[3], CultureInfo.InvariantCulture);
                            UnClient.longitudeC = double.Parse(Elements[4], CultureInfo.InvariantCulture);
                            UnClient.TelephoneC = long.Parse(Elements[5]);
                            UnClient.EmailC = Elements[6];
                            UnClient.regimeAlC = Elements[7];
                            UnClient.Id_Utilisateur = Elements[8];

                            ListeClient.Add(UnClient);

                        }
                    }
                    catch (MalformedLineException ex)
                    {
                        Console.WriteLine($"Error parsing line: {ex.Message}");
                    }
                }
            }
            //for (int i=0;i<ListeClient.Count;i++)
            //{
            // Console.WriteLine(ListeClient[i].latitudeC);
            //}

            return ListeClient;
        }

        static List<Cuisinier> GetListCuisinierFromCSV()
        {
            List<Cuisinier> ListeCuisinier = new List<Cuisinier>();
            // Cette méthode(fonction) va se charger de parser le fichier csv et
            //récupérer chaque ligne du fichier csv dans un objet client
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\cyril\OneDrive\Bureau\projet PSI\PSI-Rendu1\Cuisiniers.csv"))
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
                        Cuisinier UnCuisinier = new Cuisinier();
                        UnCuisinier.IdCuisinier = Elements[0];
                        UnCuisinier.nomP = Elements[1];
                        UnCuisinier.prenomP = Elements[2];
                        UnCuisinier.latitudeP = double.Parse(Elements[3], CultureInfo.InvariantCulture);
                        UnCuisinier.longitudeP = double.Parse(Elements[4], CultureInfo.InvariantCulture);
                        UnCuisinier.telephoneP = long.Parse(Elements[5]);
                        UnCuisinier.emailP = Elements[6];
                        UnCuisinier.spécialités = Elements[7];
                        UnCuisinier.Id_Utilisateur = Elements[8];
                        ListeCuisinier.Add(UnCuisinier);

                    }

                }
                //Console.WriteLine(ListeCuisinier.Count);
                //for (int i=0;i<ListeCuisinier.Count;i++)
                //{
                //   Console.WriteLine(ListeCuisinier[i].nomP);
                //    Console.WriteLine(ListeCuisinier[i].latitudeP);


                //} debug
            }
            return ListeCuisinier;
        }

        static List<Utilisateur> GetListUtilisateurFromCSV()
        {
            List<Utilisateur> ListeUtilisateur = new List<Utilisateur>();
            // Cette méthode(fonction) va se charger de parser le fichier csv et
            // récupérer chaque ligne du fichier csv dans un objet utilisateur
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\cyril\OneDrive\Bureau\projet PSI\PSI-Rendu1\Utilisateurs.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    // Processing row
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        String[] Elements = field.Split(';');

                        Utilisateur UnUtilisateur = new Utilisateur();
                        UnUtilisateur.Id_Utilisateur = Elements[0];
                        UnUtilisateur.Mot_de_Passe = Elements[1];

                        ListeUtilisateur.Add(UnUtilisateur);
                    }
                }
            }

            return ListeUtilisateur;
        }

        }
    
}
