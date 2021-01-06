using EntityFramework.DynamicFilters;
using Newtonsoft.Json;
using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PlateformeConcours.Repository
{
    public class EtudiantRepository : IEtudiantRepository
    {
        private ApplicationDbContext db;
        public EtudiantRepository(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(Etudiant etudiant)
        {
            db.Etudiants.Add(etudiant);
        }

        public IEnumerable<Etudiant> Get()
        {
            return db.Etudiants.ToList();
        }

        public Etudiant Get(int id)
        {
            return db.Etudiants.Find(id);
        }
        public Etudiant GetByCne(string cne)
        {
            return db.Etudiants.Where(e => e.Cne == cne).FirstOrDefault();
        }
        public Etudiant GetByNumDossier(string NumDossier)
        {
            return db.Etudiants.Where(e => e.NumDossier == NumDossier).FirstOrDefault();
        }

        public void Remove(Etudiant etudiant)
        {
            var obj = db.Etudiants.Find(etudiant.ID);
            db.Etudiants.Remove(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Etudiant etudiant)
        {
            db.Entry(etudiant).State = EntityState.Modified;
        }

        public IEnumerable<Etudiant> GetListEtudiantByEtat(string Etat)
        {
            return db.Etudiants.Where(e => e.Etat == Etat).ToList();
        }

        public IEnumerable<Etudiant> GetListEtudiantByFiliere(string fil)
        {
            return db.Etudiants.Where(e => e.Filiere.Titre.Equals(fil)).ToList();
        }
        public IEnumerable<Etudiant> GetDeletedEtudiants()
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants.Where(e => e.IsDeleted == true).ToList();
        }

        //Nombre totale des utilisateurs
         public int CountTotale()
        {
            int nbrTotal = db.Etudiants.Count();
            return nbrTotal;
        }

        //Chart (Nombre des étudiants dans chaque filière)

            public string Chart()
        {
            List<DataPoint> dataPoints = new List<DataPoint>();
            int info = db.Etudiants.Where(s => s.ID_Filiere == 1).Count();
            int indus = db.Etudiants.Where(s => s.ID_Filiere == 2).Count();
            int proc = db.Etudiants.Where(s => s.ID_Filiere == 3).Count();
            int tel = db.Etudiants.Where(s => s.ID_Filiere == 4).Count();
            dataPoints.Add(new DataPoint("Informatique", info));
            dataPoints.Add(new DataPoint("Industriel", indus));
            dataPoints.Add(new DataPoint("Procedes", proc));
            dataPoints.Add(new DataPoint("Telecom", tel));
            string DataPoints = JsonConvert.SerializeObject(dataPoints);
            return DataPoints;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~EtudiantRepository()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}