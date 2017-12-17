using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfirmerieBO; // Référence à la couche BO
using System.Data.SqlClient;

namespace InfirmerieDAL
{
    public class UtilisateurDAO
    {
        private static UtilisateurDAO unUtilisateurDAO;

        // Accesseur en lecture, renvoi une instance
        public static UtilisateurDAO GetUnUtilisateurDAO()
        {
            if (unUtilisateurDAO == null)
            {
                unUtilisateurDAO = new UtilisateurDAO();
            }
            return unUtilisateurDAO;
        }

        // Cette méthode retourne un objets Utilisateur contenus dans la table UTILISATEUR avec son NOM et son ID
        public static int GetUtilisateurs(Utilisateur unUtilisateur)
        {
            int resultRequete;

            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            // Création d'une liste vide d'objets Utilisateurs
            List<Utilisateur> lesUtilisateurs = new List<Utilisateur>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT * FROM UTILISATEUR WHERE Login_utilisateur = '" + unUtilisateur.LoginUtilisateur + "' AND Password_utilisateur = '" + unUtilisateur.MdpUtilisateur+"'";

            SqlDataReader monReader = cmd.ExecuteReader();

            if (monReader.HasRows)
            {
                resultRequete = 1;
            }
            else
            {
                resultRequete = 0;
            }
            maConnexion.Close();
            return resultRequete;
        }
    }
}
