using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlateformeConcours.Repository
{
    public interface IGenericRepository<T, in TID> :IDisposable where T:class
    {
        IEnumerable<T> Get();
        T Get(TID id);
        Etudiant Get(int id);
        Etudiant GetByCne(string cne);
        Etudiant GetByNumDossier(string NumDossier);
        void Add(T model);
        void Update(T model);
        void Remove(T model);
        void Save();
        IEnumerable<Etudiant> GetListEtudiantByEtat(string Etat);
        IEnumerable<Etudiant> GetListEtudiantByFiliere(string fil);
        IEnumerable<Etudiant> GetDeletedEtudiants();
        int CountTotale();
        string Chart();

    }
    
}