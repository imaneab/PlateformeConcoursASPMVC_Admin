using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class Preselection4aViewModel
    {
        public Filiere Filiere { get; set; }
        public string TypeDiplome { get; set; }
        [Range(1, 9)]
        public int Bac { get; set; }
        [Range(1, 9)]
        public int S1 { get; set; }
        [Range(1, 9)]
        public int S2 { get; set; }
        [Range(1, 9)]
        public int S3 { get; set; }
        [Range(1, 9)]
        public int S4 { get; set; }
        [Range(1, 9)]
        public int S5 { get; set; }
        [Range(1, 9)]
        public int S6 { get; set; }
        [Range(1, 20)]
        public double Seuil { get; set; }
    }
}