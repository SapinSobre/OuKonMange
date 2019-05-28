using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{
    public class BddContext : DbContext
    {
        public DbSet<Resto> ListeRestos { get; set; }
        public DbSet<Sondage> ListeSondages { get; set; }
        public DbSet<Utilisateur> ListeUtilisateurs { get; set; }
        public DbSet<Image> ListeImages { get; set; }

        public DbSet<RechercheViewModel> RechercheViewModels { get; set; }
    }
}