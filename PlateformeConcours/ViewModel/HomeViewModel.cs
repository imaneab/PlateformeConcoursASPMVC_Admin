using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class HomeViewModel
    {

        //Statistique 3eme
        public int NbrTotal3 { get; set; }
        public int NbrInfo3 { get; set; }
        public int NbrIndus3 { get; set; }
        public int NbrProcede3 { get; set; }
        public int NbrTelecom3 { get; set; }
        public int NbrTotalSupp3 { get; set; }
        public IEnumerable<Etudiant3a> Preselectionne3Info { get; set; }
        public IEnumerable<Etudiant3a> Preselectionne3Indus { get; set; }
        public IEnumerable<Etudiant3a> Preselectionne3Procede { get; set; }
        public IEnumerable<Etudiant3a> Preselectionne3Telecom { get; set; }
        //Statistique 4eme
        public int NbrTotal4 { get; set; }
        public int NbrInfo4 { get; set; }
        public int NbrIndus4 { get; set; }
        public int NbrProcede4 { get; set; }
        public int NbrTelecom4 { get; set; }
        public int NbrTotalSupp4 { get; set; }
        public IEnumerable<Etudiant4a> Preselectionne4Info { get; set; }
        public IEnumerable<Etudiant4a> Preselectionne4Indus { get; set; }
        public IEnumerable<Etudiant4a> Preselectionne4Procede { get; set; }
        public IEnumerable<Etudiant4a> Preselectionne4Telecom { get; set; }
        //Statistique generale
        public int NbrTotal { get; set; }
        //Statistique Charts
        public string Datapoints { get; set; }

        
    }
}