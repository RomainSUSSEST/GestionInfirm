using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InfirmerieBO; // Référence à la couche BO
using System.Data.SqlClient;

namespace InfirmerieDAL
{
    public class InfirmerieDAO
    {
        private static InfirmerieDAO uneInfirmerieDAO;

        // Accesseur en lecture, renvoi une instance
        public static InfirmerieDAO GetuneInfirmerieDAO()
        {
            if (uneInfirmerieDAO == null)
            {
                uneInfirmerieDAO = new InfirmerieDAO();
            }
            return uneInfirmerieDAO;
        }

        public List<string> recupererClasses() // Requête qui permet de récupérer les classes de la BDD
        {
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            // Création d'une liste vide d'objets Medicaments
            List<string> lesClasses = new List<string>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT Id_classe, Libelle_classe FROM CLASSE";

            SqlDataReader monReader = cmd.ExecuteReader();

            while (monReader.Read())
            {
                // Ajout de chaque libellé dans la liste Box
                lesClasses.Add(monReader["Libelle_classe"].ToString());
            }
            maConnexion.Close();
            return lesClasses;
        }

        public List<string> recupererEleves(string libelle) // Requête qui permet de récupérer les élèves selon la classe choisie
        {
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            // Création d'une liste vide d'objets Eleves
            List<string> lesEleves = new List<string>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT Id_eleve, Nom_eleve, Prenom_eleve FROM ELEVE,CLASSE WHERE ELEVE.Id_classe = CLASSE.Id_classe AND Libelle_classe ='" + libelle + "'";

            SqlDataReader monReader = cmd.ExecuteReader();

            while (monReader.Read())
            {
                string unEleve = monReader["Nom_eleve"].ToString();
                lesEleves.Add(unEleve);
            }
            maConnexion.Close();
            return lesEleves;
        }

        public List<string> recupererPrenomsEleves(string libelle) // Requête qui permet de récupérer les élèves selon le nom choisi
        {
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            // Création d'une liste vide d'objets Eleves
            List<string> lesPrenoms = new List<string>();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT Prenom_eleve FROM ELEVE WHERE Nom_eleve ='" + libelle + "'";

            SqlDataReader monReader = cmd.ExecuteReader();

            while (monReader.Read())
            {
                string unPrenom = monReader["Prenom_eleve"].ToString();
                lesPrenoms.Add(unPrenom);
            }
            maConnexion.Close();
            return lesPrenoms;
        }

        public int recupererIdEleve(int id)
        {
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            // Création d'une liste vide d'objets Eleves
            int idEleve = 0;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT * FROM ELEVE WHERE Id_eleve = " + id;

            SqlDataReader monReader = cmd.ExecuteReader();

            while (monReader.Read())
            {
                idEleve = Convert.ToInt32(monReader["Id_eleve"].ToString());
            }
            maConnexion.Close();
            return idEleve;
        }
        
        public Eleve recupererInfosEleves(string nomEleve, string prenomEleve) // Requête qui permet de récupérer les élèves selon la classe choisie
        {
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "SELECT * FROM ELEVE WHERE Nom_eleve = '"+ nomEleve +"' AND Prenom_eleve ='"+ prenomEleve+"'";

            SqlDataReader monReader = cmd.ExecuteReader();

            int id = 0;
            string nom = "";
            string prenom = "";
            DateTime date = new DateTime();
            string sante = "";
            int telEleve = 0;
            int telParent = 0;
            bool archive = false;
            bool tiersTemps = false;
            bool visite = false;
            int idClasse = 0;

            while (monReader.Read())
            {
                id = Convert.ToInt32(monReader["Id_eleve"].ToString());
                nom = monReader["Nom_eleve"].ToString();
                prenom = monReader["Prenom_eleve"].ToString();
                date = Convert.ToDateTime(monReader["DateNaissance_eleve"]);
                sante = monReader["Sante_eleve"].ToString();
                telEleve = Convert.ToInt32(monReader["Tel_eleve"].ToString());
                telParent = Convert.ToInt32(monReader["TelParents_eleve"].ToString());
                archive = Convert.ToBoolean(monReader["Archive_eleve"]);
                tiersTemps = Convert.ToBoolean(monReader["PossedeTiersTempsExam"]);
                visite = Convert.ToBoolean(monReader["PossedeVisite"]);
                idClasse = Convert.ToInt32(monReader["Id_classe"].ToString());     
            }

            Eleve unEleve = new Eleve(id, nom, prenom, date, sante, telEleve, telParent, archive, tiersTemps, visite, idClasse);
            maConnexion.Close();
            return unEleve;
        }

        public static int AjoutEleve(Eleve unEleve)
        {
            int nbEnr;

            // Connexion BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "INSERT INTO ELEVE values('" + unEleve.Nom + "','" + unEleve.Prenom + "', '" + unEleve.Naissance + "', '" + unEleve.Sante + "', '" + unEleve.TelEleve + "', '" + unEleve.TelParent + "', '" + false + "', '" + unEleve.Tierstemps + "', '" + false + "');";

            nbEnr = cmd.ExecuteNonQuery();

            // Fermeture connexion
            maConnexion.Close();
            return nbEnr;
        }

        public static int UpdateEleve(Eleve unEleve)
        {
            int nbEnr;
            int archive;
            int visite;
            int tiertemps;

            if (unEleve.Archive == true)
            {
                archive = 1;
            } else
            {
                archive = 0;
            }
            if (unEleve.Visite == true)
            {
                visite = 1;
            }
            else
            {
                visite = 0;
            }
            if (unEleve.Tierstemps == true)
            {
                tiertemps = 1;
            }
            else
            {
                tiertemps = 0;
            }
            // Connexion à la BD
            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();



            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;   
            //cmd.CommandText = "UPDATE ELEVE SET nom_eleve, prenom_eleve, Datenaissance_eleve, sante_eleve, tel_Eleve, telParents_eleve, archive_eleve, PossedeTiersTempsExam, PossedeVisite = '" + unEleve.Nom + unEleve.Prenom + unEleve.Naissance + unEleve.Sante + unEleve.TelEleve + unEleve.TelParent + unEleve.Archive + unEleve.Tierstemps + unEleve.Visite + "' WHERE Id_eleve = " + unEleve.Id;
            cmd.CommandText = "UPDATE ELEVE SET nom_eleve = '" + unEleve.Nom + "', prenom_eleve = '" + unEleve.Prenom + "', Datenaissance_eleve = '" + unEleve.Naissance + "', sante_eleve ='" + unEleve.Sante + "', tel_Eleve =" + unEleve.TelEleve + ", telParents_eleve =" + unEleve.TelParent + ", archive_eleve=" + archive + ", PossedeTiersTempsExam =" + tiertemps + ", PossedeVisite=" + visite + " WHERE Id_eleve =" + unEleve.Id;
            nbEnr = cmd.ExecuteNonQuery();

            // Fermeture de la connexion maConnexion.Close(); 
            maConnexion.Close();
            return nbEnr;
        }

        public static bool supprimerEleve(Eleve unEleve) // Méthode qui supprime l'élève de la BDD
        {
            int result;

            SqlConnection maConnexion = ConnexionBD.GetConnexionBD().GetSqlConnexion();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = maConnexion;
            cmd.CommandText = "DELETE FROM Eleve WHERE Id_eleve =" + unEleve.Id;
            result = cmd.ExecuteNonQuery();
            

            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
