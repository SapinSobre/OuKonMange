using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OuKonMange3;

namespace OuKonMange3.Models
{
    public class DalEnDur : IDal
    {
        private List<Resto> listeDesRestaurants;
        private List<Utilisateur> listeDesUtilisateurs;
        private List<Sondage> listeDesSondages;
        private List<Image> listeDesImages;

        public DalEnDur()
        {
            listeDesRestaurants = new List<Resto>
            {
                new Resto { Id = 1, Nom = "Resto pinambour", Telephone = "0102030405", Email = "pinambour@mail.com"},
                new Resto { Id = 2, Nom = "Resto pinière", Telephone = "0102030405", Email = "piniere@mail.com"},
                new Resto { Id = 3, Nom = "Resto toro", Telephone = "0102030405", Email = "toro@mail.com"},
            };
            listeDesUtilisateurs = new List<Utilisateur>();
            listeDesSondages = new List<Sondage>();
        }

        public List<Resto> ObtenirTousLesRestaurants()
        {
            return listeDesRestaurants;
        }

        public List<Sondage> ObtenirTousLesSondages()
        {
            return listeDesSondages;
        }

        public List<Image> ObtenirToutesLesImages()
        {
            return listeDesImages;
        }

        public void CreerRestaurant(string nom, string telephone, string email)
        {
            int id = listeDesRestaurants.Count == 0 ? 1 : listeDesRestaurants.Max(r => r.Id) + 1;
            listeDesRestaurants.Add(new Resto { Id = id, Nom = nom, Telephone = telephone, Email = email });
        }

        public void ModifierRestaurant(int id, string nom, string telephone, string email)
        {
            Resto resto = listeDesRestaurants.FirstOrDefault(r => r.Id == id);
            if (resto != null)
            {
                resto.Nom = nom;
                resto.Telephone = telephone;
                resto.Email = email;
            }
        }

        public bool RestaurantExiste(string nom)
        {
            return listeDesRestaurants.Any(resto => string.Compare(resto.Nom, nom, StringComparison.CurrentCultureIgnoreCase) == 0);
        }

        public int CreerUtilisateur(string nom, string motDePasse)
        {
            int id = listeDesUtilisateurs.Count == 0 ? 1 : listeDesUtilisateurs.Max(u => u.Id) + 1;
            listeDesUtilisateurs.Add(new Utilisateur { Id = id, Nom = nom, MotDePasse = motDePasse });
            return id;
        }

        public Utilisateur Authentifier(string nom, string motDePasse)
        {
            return listeDesUtilisateurs.FirstOrDefault(u => u.Nom == nom && u.MotDePasse == motDePasse);
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return listeDesUtilisateurs.FirstOrDefault(u => u.Id == id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            int id;
            if (int.TryParse(idStr, out id))
                return ObtenirUtilisateur(id);
            return null;
        }

        public int CreerUnSondage()
        {
            int id = listeDesSondages.Count == 0 ? 1 : listeDesSondages.Max(s => s.Id) + 1;
            listeDesSondages.Add(new Sondage { Id = id, Date = DateTime.Now, Votes = new List<Vote>() });
            return id;
        }

        public void AjouterVote(int idSondage, int idResto, int idUtilisateur)
        {
            Vote vote = new Vote
            {
                Resto = listeDesRestaurants.First(r => r.Id == idResto),
                Utilisateur = listeDesUtilisateurs.First(u => u.Id == idUtilisateur)
            };
            Sondage sondage = listeDesSondages.First(s => s.Id == idSondage);
            sondage.Votes.Add(vote);
        }

        public bool ADejaVote(int idSondage, string idStr)
        {
            Utilisateur utilisateur = ObtenirUtilisateur(idStr);
            if (utilisateur == null)
                return false;
            Sondage sondage = listeDesSondages.First(s => s.Id == idSondage);
            return sondage.Votes.Any(v => v.Utilisateur.Id == utilisateur.Id);
        }

        public List<Resultat> ObtenirLesResultats(int idSondage)
        {
            List<Resto> restaurants = ObtenirTousLesRestaurants();
            List<Resultat> resultats = new List<Resultat>();
            Sondage sondage = listeDesSondages.First(s => s.Id == idSondage);
            foreach (IGrouping<int, Vote> grouping in sondage.Votes.GroupBy(v => v.Resto.Id))
            {
                int idRestaurant = grouping.Key;
                Resto resto = restaurants.First(r => r.Id == idRestaurant);
                int nombreDeVotes = grouping.Count();
                resultats.Add(new Resultat { Nom = resto.Nom, Telephone = resto.Telephone, Score = nombreDeVotes });
            }
            return resultats;
        }

        public void Dispose()
        {
            listeDesRestaurants = new List<Resto>();
            listeDesUtilisateurs = new List<Utilisateur>();
            listeDesSondages = new List<Sondage>();
            listeDesImages = new List<Image>();
        }
    }
}