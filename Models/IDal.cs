using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OuKonMange3.Models
{
    public interface IDal : IDisposable
    {
        List<Resto> ObtenirTousLesRestaurants();
        List<Sondage> ObtenirTousLesSondages();
        List<Image> ObtenirToutesLesImages();
        void CreerRestaurant(string nom, string telephone, string email);
        void ModifierRestaurant(int restoId, string nom, string telephone, string email);
        bool RestaurantExiste(string nom);
        int CreerUtilisateur(string nom, string motDePasse);
        Utilisateur ObtenirUtilisateur(int id);
        Utilisateur ObtenirUtilisateur(string idStr);
        int CreerUnSondage();
        Utilisateur Authentifier(string nom, string motDePasse);
        bool ADejaVote(int idSondage, string idUtilisateur);
        void AjouterVote(int idSondage, int idResto, int idUtilisateur);
        List<Resultat> ObtenirLesResultats(int idSondage);
    }
}