using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using OuKonMange3.Models;

namespace OuKonMange3.Models
{
    public class RestoVoteViewModel : IValidatableObject
    {
        public List<RestoCheckBoxViewModel> ListeRestosCheckes { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(!ListeRestosCheckes.Any(r => r.EstSelectionne))
                yield return new ValidationResult ("Vous devez au moins choisir un restaurant où vous avez envie d'aller manger.", new[] { "ListeRestosCheckes" });
        }
    }
}