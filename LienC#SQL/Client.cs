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
        

        internal int idCLient { get; set; }
        internal string NomC {  get; set; }
        internal string PrénomC { get; set; }
        internal string AdresseC { get; set; }
        internal string TelephoneC { get; set; }
        internal string EmailC { get; set; }
        internal string regimeAlC { get; set; }
        internal string Id_Utilisateur { get; set; }

        public Client() { }
    }
}
