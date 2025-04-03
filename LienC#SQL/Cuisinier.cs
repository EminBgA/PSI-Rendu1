using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LienC_Sql
{
    internal class Cuisinier
    {
        internal string IdCuisinier;
        internal string nomP;
        internal string prenomP;
        internal double latitudeP;
        internal double longitudeP;
        internal long telephoneP;
        internal string emailP;
        internal string spécialités;
        internal string Id_Utilisateur;

        public Cuisinier(string IdCuisinier, string nomP, string prenomP, double latitudeP, double longitudeP, long telephoneP, string emailP, string spécialités, string Id_Utilisateur)
        {
            this.IdCuisinier = IdCuisinier;
            this.nomP = nomP;
            this.prenomP = prenomP;
            this.latitudeP = latitudeP;
            this.longitudeP = latitudeP;
            this.telephoneP = telephoneP;
            this.emailP = emailP;
            this.spécialités = spécialités;
            this.Id_Utilisateur = Id_Utilisateur;
        }
        public Cuisinier (string IdCuisinier)
        {
            this.IdCuisinier = IdCuisinier;
        }
        public Cuisinier() { }

    }
}
