using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class DeliberationViewModel
    {
        public Filiere Filiere { get; set; }
        [DefaultValue(1)]
        public int CoefMath { get; set; }
        [DefaultValue(1)]
        public int CoefSpec { get; set; }
        public int NbrPlaces { get; set; }
        public int ListeAtt { get; set; }
        public string Classement1 { get; set; }
        public string Classement2 { get; set; }
        public string Classement3 { get; set; }

    }
}