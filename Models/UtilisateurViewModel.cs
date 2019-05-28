using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{
    public class UtilisateurViewModel
    {        
        public Utilisateur Utilisateur { get; set; }
        public bool Authentifie { get; set; }
    }
}