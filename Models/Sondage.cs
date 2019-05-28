﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OuKonMange3.Models
{
    public class Sondage
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public virtual List<Vote> Votes { get; set; }
    }
}