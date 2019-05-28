using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{
    public class InitOuKonMange3 : DropCreateDatabaseAlways<BddContext>
    {
        protected override void Seed(BddContext context)
        {
            context.ListeRestos.Add(new Resto { Nom = "Chez Bougnèm", Telephone = "0125698740", Email = "bobo@mail.com" });
            context.ListeRestos.Add(new Resto { Nom = "La frite hirsute", Telephone = "0125748950", Email = "frite@mai.com"});
            context.ListeRestos.Add(new Resto { Nom = "Au chicon farci", Telephone = "0147852360", Email = "chiconfarci@mail.be" });
            context.ListeRestos.Add(new Resto { Nom = "L'olive mauve", Telephone = "0568897458", Email = "olive@mail.com" });
            context.ListeUtilisateurs.Add(new Utilisateur { Id = 1, Nom = "Sapin", MotDePasse = "Sapin"});
            context.ListeUtilisateurs.Add(new Utilisateur { Id = 2, Nom = "Hassna", MotDePasse = "Hassna"});
            context.ListeUtilisateurs.Add(new Utilisateur { Id = 3, Nom = "Nico", MotDePasse = "Nico" });
            context.ListeUtilisateurs.Add(new Utilisateur { Id = 4, Nom = "Mouton", MotDePasse = "Mouton" });
            context.ListeImages.Add(new Image { Id = 1, Url = "chezBougnem", RestoId = 1});
            context.ListeImages.Add(new Image { Id = 2, Url = "laFriteHirsute", RestoId = 2 });
            context.ListeImages.Add(new Image { Id = 3, Url = "auChiconFarci", RestoId = 3 });
            context.ListeImages.Add(new Image { Id = 4, Url = "lOliveMauve", RestoId = 4 });
            base.Seed(context);
        }
    }
}