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
                    valueString[i] = reader.GetValue(i).ToString() ;
                    Console.Write(valueString[i] + " , ");

                }
                Console.WriteLine();
            }


            Console.ReadLine();

            reader.Close();
            Command.Dispose();
            Connexion.Close();
        }

        static void InsertClientIntoDB(Client item)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=loueur;UID=root;PASSWORD=root";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();
        }

        static List<Client> GetListClientsFromCSV()
        {
            List<Client> Liste = new List<Client>();
            // Cette méthode(fonction) va se charger de parser le fichier csv et
            //récupérer chaque ligne du fichier csv dans un objet client
            using (TextFieldParser parser = new TextFieldParser(@"C:\Users\moura\Downloads\Clients.csv"))
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
                        UnClient.idCLient = Convert.ToInt32(Elements[0]);
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
