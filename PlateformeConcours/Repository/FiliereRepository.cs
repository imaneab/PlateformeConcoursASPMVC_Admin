using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Unity;

namespace PlateformeConcours.Repository
{
    public class FiliereRepository : IFiliereRepository
    {
        [Dependency]
        public ApplicationDbContext db { get; set; }
        public void Add(Filiere filiere)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Filiere> Get()
        {
            return db.Filieres.ToList();
        }

        public Filiere Get(int id)
        {
            throw new NotImplementedException();
        }

        public string GetFiliereTitre(int id)
        {
            return db.Filieres.Where(e => e.ID == id).Select(e => e.Titre).FirstOrDefault();
        }
        public string GetFiliereTitre(int? id)
        {
            return db.Filieres.Where(e => e.ID == id).Select(e => e.Titre).FirstOrDefault();
        }

        public void Remove(Filiere filiere)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Filiere filiere)
        {
            throw new NotImplementedException();
        }
    }
}