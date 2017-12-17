using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using InfirmerieDAL; // Référence la couche DAL
using InfirmerieBO;  // Référence la couche BO

namespace InfirmerieBLL
{
    public class GestionInfirmerie
    {
        private static GestionInfirmerie uneGestionInfirmerie; // Objet BLL

        // Accesseur en lecture
        public static GestionInfirmerie GetGestionInfirmerie()
        {
            if (uneGestionInfirmerie == null)
            {
                uneGestionInfirmerie = new GestionInfirmerie();
            }
            return uneGestionInfirmerie;
        }

        // Définit la chaine de connexion grâce à la méthode SetChaineConnexion de la DAL
        public static void SetChaineConnexion(ConnectionStringSettings chset)
        {
            string chaine = chset.ConnectionString;
            ConnexionBD.GetConnexionBD().SetChaineConnexion(chaine);
        }

        public List<string> recupererInfosClasses()
        {
            List<string> lesClasses = new List<string>();
            InfirmerieDAO infosClasse = new InfirmerieDAO();
            lesClasses = infosClasse.recupererClasses();
            return lesClasses;
        }

        public List<string> recupererInfosEleves(string libelle)
        {
            List<string> lesEleves = new List<string>();
            InfirmerieDAO infosEleve = new InfirmerieDAO();
            lesEleves = infosEleve.recupererEleves(libelle);
            return lesEleves;
        }

        public Eleve recupererDetailsEleves(string nom, string prenom)
        {
            Eleve lesDetails;
            InfirmerieDAO infosEleve = new InfirmerieDAO();
            lesDetails = infosEleve.recupererInfosEleves(nom, prenom);
            return lesDetails;
        }

        public List<string> recupererPrenomsEleves(string libelle)
        {
            List<string> lesEleves = new List<string>();
            InfirmerieDAO infosEleve = new InfirmerieDAO();
            lesEleves = infosEleve.recupererPrenomsEleves(libelle);
            return lesEleves;
        }

        // Méthode qui créer un nouvel élève à partir de ses attributs et qui le renvoie en l'ajoutant à la BD avec la méthode AjoutEleve de la DAL
        public static int CreerEleve(string nom, string prenom, DateTime naissance, string sante, int telEleve, int telParent, bool archive, bool tierstemps, bool visite, int classe)
        {
            Eleve el;
            el = new Eleve(nom, prenom, naissance, sante, telEleve, telParent, archive, tierstemps, visite, classe);
            return InfirmerieDAO.AjoutEleve(el);
        }

        public static int ModifierEleve(int id, string nom, string prenom, DateTime naissance, string sante, int telEleve, int telParent, bool archive, bool tierstemps, bool visite)
        {
            Eleve el;
            el = new Eleve(id, nom, prenom, naissance, sante, telEleve, telParent, archive, tierstemps, visite);
            return InfirmerieDAO.UpdateEleve(el);
        }

        public bool supprimerEleve(int id, string nom, string prenom, DateTime naissance, string sante, int telEleve, int telParent, bool archive, bool tierstemps, bool visite, int classe)
        {
            Eleve unEleve;
            unEleve = new Eleve(id, nom, prenom, naissance, sante, telEleve, telParent, archive, tierstemps, visite, classe);
            return InfirmerieDAO.supprimerEleve(unEleve);
        }
    }
}
