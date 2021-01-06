using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class ListsResultat3a
    {
        public int? FiliereId { get; set; }
        public string Titre { get; set; }
        public List<Etudiant3a> ListAdmis { get; set; }
        public List<Etudiant3a> ListAttente { get; set; }
        public ListsResultat3a()
        {
            ListAdmis = new List<Etudiant3a>();
            ListAttente = new List<Etudiant3a>();
        }
    }
}
