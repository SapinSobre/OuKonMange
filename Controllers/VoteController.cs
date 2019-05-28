using OuKonMange3.Filters;
using OuKonMange3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OuKonMange3.Controllers
{
    [Authorize]
    public class VoteController : Controller
    {
        private IDal dal;

        public VoteController() : this(new Dal())
        {

        }

        public VoteController(IDal dalIoc)
        {
            dal = dalIoc;
        }
        
        [HttpGet]
        public ActionResult Index(int id)
        {            
            RestoVoteViewModel viewModel = new RestoVoteViewModel
            {
                ListeRestosCheckes = dal.ObtenirTousLesRestaurants().Select(r => new RestoCheckBoxViewModel { Id = r.Id, NomEtTelephone = string.Format("{0} ({1})", r.Nom, r.Telephone) }).ToList()
            };            
            if (dal.ADejaVote(id, HttpContext.User.Identity.Name))
            {                
                return RedirectToAction("AfficheResultat", "Vote", new { id } );
            }            
            return View(viewModel);
        }

        [HttpPost]
        [ActionName("Index")]
        public ActionResult IndexPost(RestoVoteViewModel viewModel, int id)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            Utilisateur utilisateur = dal.ObtenirUtilisateur(HttpContext.User.Identity.Name);
            if (utilisateur == null)
                return new HttpUnauthorizedResult();

            foreach (RestoCheckBoxViewModel resto in viewModel.ListeRestosCheckes.Where(r => r.EstSelectionne))
            {
                dal.AjouterVote(id, resto.Id, utilisateur.Id);
            }

            return RedirectToAction("AfficheResultat", new { id = id });
        }

        public ActionResult AfficheResultat(int id)
        {
            if (!dal.ADejaVote(id, HttpContext.User.Identity.Name))
            {
                return RedirectToAction("Index", new { id = id });
            }
            ViewBag.Id = id;
            return View();
        }
       
        [AjaxFilter]
        public ActionResult AfficheTableau(int id)
        {
            List<Resultat> resultats = dal.ObtenirLesResultats(id);
            return PartialView(resultats.OrderByDescending(r => r.Score).ToList());
        }
    }
}