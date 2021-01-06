using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateformeConcours.Repository
{
    public interface IEtudiantRepository
    {
     
        Etudiant Get(int id);
        Etudiant GetByCne(string cne);
        Etudiant GetByNumDossier(string NumDossier);
        void Save();
        IEnumerable<Etudiant> GetListEtudiantByEtat(string Etat);
        IEnumerable<Etudiant> GetListEtudiantByFiliere(string fil);
        IEnumerable<Etudiant> GetDeletedEtudiants();
        int CountTotale();
        string Chart();
    }
}
