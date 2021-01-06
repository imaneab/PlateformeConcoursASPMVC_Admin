using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class ListDeliberation4ViewModel
    {
        public int? FiliereID { get; set; }
        public Etudiant4a Etudiant { get; set; }
        public int Classement { get; set; }
        public ListDeliberation4ViewModel()
        {
            Etudiant = new Etudiant4a();
            Classement = 0;
        }
        public ListDeliberation4ViewModel(int? id, Etudiant4a etudiant)
        {
            FiliereID = id;
            Etudiant = etudiant;
            Classement = Convert.ToInt32(etudiant.Etudiant.Note.Specialite);
        }
    }
}