using OuKonMange3.Models;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace OuKonMange3.Controllers
{    
    public class HomeController : Controller
    {
        private IDal dal;

        public HomeController() : this(new Dal())
        {
        }

        public HomeController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        
        public ActionResult Index()
        {            
            UtilisateurViewModel viewmodel = new UtilisateurViewModel();
            if (HttpContext.User.Identity.Name != null)
            {
                viewmodel.Utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
                viewmodel.Authentifie = true;
            }
            
            return View(viewmodel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost()
        {
            int idSondage;
            if (dal.ObtenirTousLesSondages().Count == 0)
            {
                idSondage = dal.CreerUnSondage();
            }
            idSondage = 1;
            return RedirectToAction("Index", "Vote", new { id = idSondage });
        }

        [ChildActionOnly]
        public ActionResult AfficheListeRestaurant()
        {
            List<Resto> listeDesRestos = dal.ObtenirTousLesRestaurants();
            return PartialView(listeDesRestos);
        }   
        
        [ChildActionOnly]
        public ActionResult AfficheRestosImages()
        {
            RestosImagesViewModel restosImages = new RestosImagesViewModel {
                ListeRestos = dal.ObtenirTousLesRestaurants(),
                ListeImages = dal.ObtenirToutesLesImages()
            };
            return PartialView(restosImages);
        }
    }
}