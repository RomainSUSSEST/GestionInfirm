using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InfirmerieBO
{
    public class Utilisateur
    {
        // Déclaration des variables
        private int idUtilisateur;
        private string loginUtilisateur;
        private string mdpUtilisateur;

        public int IdUtilisateur
        {
            get { return idUtilisateur; }
            set { idUtilisateur = value; }
        }

        public string LoginUtilisateur
        {
            get { return loginUtilisateur; }
            set { loginUtilisateur = value; }
        }
        
        public string MdpUtilisateur
        {
            get { return mdpUtilisateur; }
            set { mdpUtilisateur = value; }
        }

        public Utilisateur(string nomUtilisateur)
        {
            this.mdpUtilisateur = nomUtilisateur;
        }

        public Utilisateur(string loginUtilisateur, string nomUtilisateur)
        {
            this.loginUtilisateur = loginUtilisateur;
            this.mdpUtilisateur = nomUtilisateur;
        }

        public string NomEtId
        {
            get { return mdpUtilisateur + " " + idUtilisateur; }
        }
    }
}
