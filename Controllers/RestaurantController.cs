using OuKonMange3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace OuKonMange3.Controllers
{
    public class RestaurantController : Controller
    {
        private IDal dal;

        public RestaurantController() : this(new Dal())
        {

        }

        public RestaurantController(IDal dalIoc)
        {
            dal = dalIoc;
        }

        public ActionResult Index()
        {            
            List<Resto> listeRestos = dal.ObtenirTousLesRestaurants();
            return View(listeRestos);           
        }

        [HttpGet]
        public ActionResult CreerRestaurant()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerRestaurant(Resto resto)
        {          
            if(dal.RestaurantExiste(resto.Nom))
            {
                ModelState.AddModelError("Nom", "Ce restaurant existe déjà.");
                return View(resto);
            }            
            if(!ModelState.IsValid)            
                return View(resto);            
            dal.CreerRestaurant(resto.Nom, resto.Telephone, resto.Email);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ModifierRestaurant(int? id)
        {
            if (id.HasValue)
            {                
                Resto restaurant = dal.ObtenirTousLesRestaurants().FirstOrDefault(r => r.Id == id.Value);
                if (restaurant == null)
                    return View("ErreurResto");
                return View(restaurant);                
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        public ActionResult ModifierRestaurant(Resto resto)
        {
            if (!ModelState.IsValid)
                return View(resto);           
            dal.ModifierRestaurant(resto.Id, resto.Nom, resto.Telephone, resto.Email);
            return RedirectToAction("Index");            
        }
       
        public ActionResult Recherche(RechercheViewModel viewmodel)
        {
            return View(viewmodel);
        }             
       
        public ActionResult ResultatRecherche(RechercheViewModel viewmodel)
        {
            if (!string.IsNullOrWhiteSpace(viewmodel.Recherche))
            {
                viewmodel.ListeRestos = dal.ObtenirTousLesRestaurants().Where(r => r.Nom.IndexOf(viewmodel.Recherche, StringComparison.CurrentCultureIgnoreCase) >= 0).ToList();
                Thread.Sleep(1500);
            }
            else
            {
                viewmodel.ListeRestos = new List<Resto>();
            }            
            return PartialView(viewmodel);
        }

        /*[AllowAnonymous]
        public JsonResult VerifNomResto(string Nom)
        {
            bool result = !dal.RestaurantExiste(Nom);
            return Json(result, JsonRequestBehavior.AllowGet);
        }*/
               
        public ActionResult VerifRestaurantExiste(Resto resto)
        {
            if (!string.IsNullOrWhiteSpace(resto.Nom) && dal.RestaurantExiste(resto.Nom))
            {
                ViewBag.nomRestoExiste = resto.Nom;
                ViewBag.result = !dal.RestaurantExiste(resto.Nom);                           
            }
            return PartialView();
        }
    }
}