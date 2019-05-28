using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace OuKonMange3.Models
{
    public class Dal : IDal
    {
        private BddContext bdd;

        public Dal()
        {
            bdd = new BddContext();
        }

        public List<Resto> ObtenirTousLesRestaurants()
        {            
            return bdd.ListeRestos.ToList();
        }

        public List<Sondage> ObtenirTousLesSondages()
        {
            return bdd.ListeSondages.ToList();
        }

        public List<Image> ObtenirToutesLesImages()
        {
            return bdd.ListeImages.ToList();
        }

        public void CreerRestaurant(string nom, string telephone, string email)
        {
            bdd.ListeRestos.Add(new Resto { Nom = nom, Telephone = telephone, Email = email });
            bdd.SaveChanges();           
        }

        public void ModifierRestaurant(int restoId, string nom, string telephone, string email)
        {
            Resto resto = ObtenirTousLesRestaurants().FirstOrDefault(r => r.Id == restoId);
            if(resto != null)
            {
                resto.Nom = nom;
                resto.Telephone = telephone;
                resto.Email = email;
                bdd.SaveChanges();               
            }
        }

        public bool RestaurantExiste(string nom)
        {
            return ObtenirTousLesRestaurants().Any(resto => string.Compare(resto.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public int CreerUtilisateur(string nom, string motDePasse)
        {            
            /*byte[] salt = CryptageMotDePasse.GenerateSalt();
            byte[] motDePasseEncodeUtf8 = Encoding.UTF8.GetBytes(motDePasse);
            byte[] motDePasseHashe = CryptageMotDePasse.HashageSHA256(motDePasseEncodeUtf8, salt);
            string motDePasseHasheString = Convert.ToBase64String(motDePasseHashe);*/
            Utilisateur utilisateur = new Utilisateur { Nom = nom, MotDePasse = motDePasse };
            bdd.ListeUtilisateurs.Add(utilisateur);
            bdd.SaveChanges();
            return utilisateur.Id;
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return bdd.ListeUtilisateurs.FirstOrDefault(u => u.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if(int.TryParse(idStr, out id))
            {
                return ObtenirUtilisateur(id);
            }
            return null;
        }        

        private Utilisateur CreeOuRecupere(string nom, string motDePasse)
        {
            Utilisateur utilisateur = Authentifier(nom, motDePasse);
            if (utilisateur == null)
            {
                int id = CreerUtilisateur(nom, motDePasse);
                return ObtenirUtilisateur(id);
            }
            return utilisateur;
        }

        public Utilisateur Authentifier (string nom, string motDePasse)
        {
            /*byte[] salt = CryptageMotDePasse.GenerateSalt();
            byte[] motDePasseEncodeUTF8 = Encoding.UTF8.GetBytes(motDePasse);
            byte[] motDePasseHashe = CryptageMotDePasse.HashageSHA256(motDePasseEncodeUTF8, salt);
            string motDePasseHasheString = Convert.ToBase64String(motDePasseHashe);*/
            return bdd.ListeUtilisateurs.FirstOrDefault(u => u.Nom == nom && u.MotDePasse == motDePasse);
        }

        public int CreerUnSondage()
        {
            Sondage sondage = new Sondage { Date = DateTime.Now };
            bdd.ListeSondages.Add(sondage);
            bdd.SaveChanges();
            return sondage.Id;
        }

        public bool ADejaVote(int idSondage, string idUtilisateur)
        {
            int id;
            if(int.TryParse(idUtilisateur, out id))
            {
                Sondage sondage = bdd.ListeSondages.First(s => s.Id == idSondage);
                if (sondage.Votes == null)
                    return false;
                return sondage.Votes.Any(v => v.Utilisateur != null && v.Utilisateur.Id == id);
            }
            return false;          
        }       

        public void AjouterVote(int idSondage, int idResto, int idUtilisateur)
        {
            Vote vote = new Vote
            {
                Resto = bdd.ListeRestos.First(r => r.Id == idResto),
                Utilisateur = bdd.ListeUtilisateurs.First(u => u.Id == idUtilisateur)
            };
            Sondage sondage = bdd.ListeSondages.First(s => s.Id == idSondage);
            if(sondage.Votes == null)
            {
                sondage.Votes = new List<Vote>();
            }
            sondage.Votes.Add(vote);
            bdd.SaveChanges();
        }

        public List<Resultat> ObtenirLesResultats(int idSondage)
        {
            List<Resto> listeRestos = ObtenirTousLesRestaurants();
            List<Resultat> resultats = new List<Resultat>();
            Sondage sondage = bdd.ListeSondages.First(s => s.Id == idSondage);
            foreach(IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Resto.Id))
            {               
                int idResto = grouping.Key;
                Resto resto = ObtenirTousLesRestaurants().First(r => r.Id == idResto);               
                resultats.Add(new Resultat { Nom = resto.Nom, Telephone = resto.Telephone, Score = grouping.Count()});
            }
            bdd.SaveChanges();
            return resultats;
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
    }
}