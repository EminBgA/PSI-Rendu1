using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string ConnexionString = "SERVER=localhost;PORT=3306;DATABASE=loueur;UID=superbozo;PASSWORD=123";
            MySqlConnection Connexion = new MySqlConnection(ConnexionString);
            Connexion.Open();

            string Requete = "SELECT NOM, PRENOM FROM CLIENT";
            MySqlCommand Command = Connexion.CreateCommand();
            Command.CommandText = Requete;

            MySqlDataReader Reader = Command.ExecuteReader();   
        }
    }
}
