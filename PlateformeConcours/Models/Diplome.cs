using PlateformeConcours.Models.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlateformeConcours.Models
{
    public class Diplome : ISoftDelete
    {
        //public int ID { get; set; }
        [Key, ForeignKey("Etudiant")]
        public int EtudiantDiplomeId { get; set; }

        public string Titre { get; set; }

        public string Etablissement { get; set; }
        public string Ville { get; set; }

        public int Annee1 { get; set; }

        public int Annee2 { get; set; }

        public int Annee3 { get; set; }

        public int AnneeBac { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        public virtual Etudiant Etudiant { get; set; }

        public string fichier { get; set; }
        //public string ScanDiplome { get; set; }
        [Timestamp]
        public byte[] CreatedAt;
        [Timestamp]
        public byte[] UpdatedAt;
    }
}