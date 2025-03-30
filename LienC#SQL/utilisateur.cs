using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LienC_Sql
{
    internal class Utilisateur
    {
        internal string Id_Utilisateur;
        internal string Mot_de_Passe;


        public Utilisateur(string Id_Utilisateur, string Mot_de_Passe)
        {
            this.Id_Utilisateur = Id_Utilisateur;
            this.Mot_de_Passe = Mot_de_Passe;
        }

        public Utilisateur(string Id_Utilisateur)
        {
            this.Id_Utilisateur = Id_Utilisateur;
        }

    }
}
