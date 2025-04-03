using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Text;
using System.Threading.Tasks;

namespace LienC_Sql
{
    internal class Client
    {
        

        internal string idClient { get; set; }
        internal string NomC {  get; set; }
        internal string PrénomC { get; set; }
        internal double latitudeC { get; set; }
        internal double longitudeC { get; set; }
        internal long TelephoneC { get; set; }
        internal string EmailC { get; set; }
        internal string regimeAlC { get; set; }
        internal string Id_Utilisateur { get; set; }

        public Client(string idClient) { this.idClient = idClient; }
        public Client(string idClient, string NomC, string PrénomC, double latitude, double longitude, long TelephoneC, string EmailC, string regimeAlC, string Id_Utilisateur)
        {
            this.idClient = idClient;
            this.NomC = NomC;
            this.PrénomC = PrénomC;  
            this.latitudeC = latitude;
            this.longitudeC = longitude;
            this.TelephoneC = TelephoneC;
            this.EmailC = EmailC;
            this.regimeAlC = regimeAlC;
            this.Id_Utilisateur = Id_Utilisateur;
        }
        public Client() { }
        
    }
}
