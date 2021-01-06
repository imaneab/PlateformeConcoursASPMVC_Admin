using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.ViewModel
{
    public class ListDeliberation3ViewModel
    {
        public int? FiliereID { get; set; }
        public Etudiant3a Etudiant { get; set; }
        public double? NoteGlobal { get; set; }
        public ListDeliberation3ViewModel()
        {
            Etudiant = new Etudiant3a();
            NoteGlobal = 0;
        }
        public ListDeliberation3ViewModel(int? id, Etudiant3a etudiant)
        {
            FiliereID = id;
            Etudiant = etudiant;
            NoteGlobal = (etudiant.Etudiant.Note.NoteMath + etudiant.Etudiant.Note.Specialite) / 2;
        }
    }
}