using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{    
    public class Utilisateur
    {        
        public int Id { get; set; }
        [Required(ErrorMessage = "Vous êtes obligé de saisir un nom")]
        [Display(Name ="Prénom")]
        public string Nom { get; set; }
        [Required(ErrorMessage = "Vous êtes obligé de saisir un mot de passe.")]
        [Display(Name = "Mot de passe")]
        public string MotDePasse { get; set; }        
    }
}