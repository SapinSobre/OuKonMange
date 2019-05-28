using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{
    public class RechercheViewModel
    {
        [Key]
        public string Recherche { get; set; }
        public List<Resto> ListeRestos { get; set; }
    }
}