using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class Deliberation4ViewModel
    {
        public Filiere Filiere { get; set; }
        public int NbrPlaces { get; set; }
        public int ListeAtt { get; set; }
        public Deliberation4ViewModel()
        {
            Filiere = new Filiere();
            NbrPlaces = 1;
            ListeAtt = 1;
        }

    }
}