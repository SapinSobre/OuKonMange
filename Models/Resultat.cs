using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{    
    public class Resultat
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        [Display(Name = "Téléphone")]
        public string Telephone { get; set; }
        public int Score { get; set; }
    }
}