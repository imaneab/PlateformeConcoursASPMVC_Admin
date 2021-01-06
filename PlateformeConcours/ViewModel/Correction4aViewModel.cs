using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class Correction4aViewModel
    {
        public int ID { get; set; }
        public string NumDossier { get; set; }
        public string Cin { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Diplome { get; set; }
        public string Filiere { get; set; }
        public int Classement { get; set; }
    }
}