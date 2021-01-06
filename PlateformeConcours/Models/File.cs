using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlateformeConcours.Models
{
    public class File
    {
        public string Name { get; set; }  //oracle
        public string Type { get; set; } //application/pdf
        public int Length { get; set; }  //1452
        public byte[] Content { get; set; }  //0x02833456789
        public string Titre_concours { get; set; } //Name
        public int Annee { get; set; }


        [Key]
        public int Id { get; set; }



        [ForeignKey("Filiere")]

        public int? Id_fil { get; set; }
        public virtual Filiere Filiere { get; set; }

    }
}