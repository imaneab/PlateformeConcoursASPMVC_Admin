using PlateformeConcours.Models;
using System.Collections.Generic;

namespace PlateformeConcours.Repository
{
    public interface IFiliereRepository
    {
        IEnumerable<Filiere> Get();
        Filiere Get(int id);
        string GetFiliereTitre(int id);
        string GetFiliereTitre(int? id);
        void Add(Filiere filiere);
        void Update(Filiere filiere);
        void Remove(Filiere filiere);
        void Save();
    }
}