using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace OuKonMange3.Models
{
    [Table("Restos")]
    public class Resto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom du restaurant est requis.")]
        //[Remote("VerifNomResto", "Restaurant", ErrorMessage = "Ce nom de restaurant existe déjà.")]
        public string Nom { get; set; }
        [RegularExpression(@"^0[0-9]{9}$", ErrorMessage = "Le numéro de téléphone n'est pas correct.")]
        [Display(Name = "Téléphone")]
        [AuMoinsUnDesDeux(Parametre1 = "Telephone", Parametre2 = "Email", ErrorMessage = "Vous devez au moins saisir un moyen de contacter le restaurant.")]
        public string Telephone { get; set; }
        [AuMoinsUnDesDeux(Parametre1 = "Telephone", Parametre2 = "Email", ErrorMessage = "Vous devez au moins saisir un moyen de contacter le restaurant.")]
        public string Email { get; set; }
    } 
}