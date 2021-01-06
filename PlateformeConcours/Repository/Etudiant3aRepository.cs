using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Validation;
using EntityFramework.DynamicFilters;
using Unity;
using PlateformeConcours.ViewModel;
using System.Diagnostics;
using System.Net.Http;


namespace PlateformeConcours.Repository
{
    public class Etudiant3aRepository : IEtudiant3aRepository
    {
        [Dependency]
        public ApplicationDbContext db { get; set; }


        //test

        public Etudiant3a GetAllCandidatsData2(int? id)
        {
            
            return db.Etudiants3a.Where(e => e.Etudiant.ID == id && e.Etudiant.IsDeleted == false && e.Etudiant.Etat == "preselectionne").FirstOrDefault();
        }

        public Etudiant3a GetAllCandidatsData2(int id)
        {
            
            return db.Etudiants3a.Where(e => e.Etudiant.ID == id && e.Etudiant.IsDeleted == false && e.Etudiant.Etat == "preselectionne").FirstOrDefault();
        }


        public IEnumerable<Etudiant3a> GetListPreselectionneByFiliere(int FiliereID)
        {
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.ID_Filiere == FiliereID
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId
            && e.Etudiant.Etat == "preselectionne"
            && !string.IsNullOrEmpty(e.Etudiant.NumDossier)
            ).ToList();
        }
        public IEnumerable<Etudiant3a> GetListPreselectionneByFiliere(int? FiliereID)
        {
            if (FiliereID == null)
            {
                return null;
            }
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.ID_Filiere == FiliereID
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId
            && e.Etudiant.Etat == "preselectionne"
            && !string.IsNullOrEmpty(e.Etudiant.NumDossier)).ToList();
        }
        public void Add(Etudiant3a etudiant3A)
        {
            db.Etudiants3a.Add(etudiant3A);
            db.SaveChanges();
        }

        public IEnumerable<Etudiant3a> GetListEtudiantByFiliere(int FiliereID)
        {
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID && e.Etudiant.ID_Filiere == FiliereID).ToList();
        }
        public IEnumerable<Etudiant3a> GetListEtudiantByFiliere(int? FiliereID)
        {
            if (FiliereID == null)
            {
                return null;
            }
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID && e.Etudiant.ID_Filiere == FiliereID).ToList();
        }

        public IEnumerable<Etudiant3a> Get()
        {
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).ToList();
        }
        public Etudiant3a Get(int id)
        {
            
            return db.Etudiants3a.Include(e => e.Etudiant).Where(e => e.ID == id && e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).FirstOrDefault();
        }
        public Etudiant3a Get(int? id)
        {
            
            return db.Etudiants3a.Include(e => e.Etudiant).Where(e => e.ID == id && e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).FirstOrDefault();
        }
        public Etudiant3a GetSup(int id)
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Include(e => e.Etudiant).Where(e => e.ID == id && e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).FirstOrDefault();
        }
        public Etudiant3a GetSup(int? id)
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Include(e => e.Etudiant).Where(e => e.ID == id && e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).FirstOrDefault();
        }
        //****************** supprimer definitivement **************************/
        public Etudiant3a GetSup2(int id)
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Find(id);
        }
        public Etudiant3a GetSup2(int? id)
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Find(id);
        }

        public void Remove(Etudiant3a etudiant)
        {

            db.SetFilterScopedParameterValue("IsDeleted", true);
            var dip = db.Diplomes.Where(e => e.EtudiantDiplomeId == etudiant.EtudiantId).FirstOrDefault();
            db.Diplomes.Remove(dip);
            var note = db.Notes.Where(e => e.EtudiantNoteId == etudiant.EtudiantId).FirstOrDefault();
            db.Notes.Remove(note);
            var et = db.Etudiants.Where(e => e.ID == etudiant.EtudiantId).FirstOrDefault();
            db.Etudiants.Remove(et);
            db.SaveChanges();
        }

       

        //*******************************************************/
        public Etudiant3a GetByCne(string cne)
        {
            return db.Etudiants3a.Where(e => e.Etudiant.Cne == cne).FirstOrDefault();
        }
        public Etudiant3a GetByNumDossier(string NumDossier)
        {
            return db.Etudiants3a.Where(e => e.Etudiant.NumDossier == NumDossier).FirstOrDefault();
        }
        public void Delete(int id)
        {
            Etudiant obj = db.Etudiants.Find(id);
            obj.IsDeleted = true;
            db.Entry(obj).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove2(Etudiant3a etudiant)
        {

            db.SetFilterScopedParameterValue("IsDeleted", true);
            var student = db.Etudiants.Find(2);
            db.Etudiants.Remove(student);
            db.SaveChanges();


        }

      

        public void Save()
        {
            db.SaveChanges();
        }

        public void Update(Etudiant3a etudiant)
        {
            db.Entry(etudiant).State = EntityState.Modified;
            db.SaveChanges();
        }

        public IEnumerable<Etudiant3a> GetListEtudiantByEtat(string Etat)
        {
            return db.Etudiants3a.Where(e => e.Etudiant.Etat == Etat).ToList();
        }



        public IEnumerable<Etudiant3a> GetDeletedEtudiants()
        {
            return db.Etudiants3a.Where(e => e.Etudiant.IsDeleted == true).ToList();
        }

        // nombre des inscrits en 3ieme année

        public int CountTotale()
        {
            int nbrTotale3 = db.Etudiants3a.Where(s => s.EtudiantId == s.Etudiant.ID).Count();
            return nbrTotale3;
        }

        //

        public IEnumerable<Etudiant3a> GetPreselection(int Filiere, string Diplome)
        {
            return db.Etudiants3a.Where(p => p.Etudiant.IsDeleted == false && p.Etudiant.ID_Filiere == Filiere && p.Etudiant.Diplome.Titre == Diplome).ToList();
        }
        // nombre des inscrits en 3ieme année informatique

        public int CountInfo()
        {
            int nbrInfo3 = db.Etudiants3a.Where(s => s.Etudiant.ID_Filiere == 1).Count();
            return nbrInfo3;
        }

        // nombre des inscrits en 3ieme année industriel

        public int CountIndus()
        {
            int nbrIndus3 = db.Etudiants3a.Where(s => s.Etudiant.ID_Filiere == 2).Count();
            return nbrIndus3;
        }

        // nombre des inscrits en 3ieme année Procedes

        public int CountProcede()
        {
            int nbrProcede3 = db.Etudiants3a.Where(s => s.Etudiant.ID_Filiere == 3).Count();
            return nbrProcede3;
        }

        // nombre des inscrits en 3ieme année Telecom

        public int CountTelecom()
        {
            int nbrTelecom3 = db.Etudiants3a.Where(s => s.Etudiant.ID_Filiere == 4).Count();
            return nbrTelecom3;
        }

        // nombre des 3ieme annee supprimés 

        public int CountTotaleSupprime()
        {
            //db.SetFilterScopedParameterValue("IsDeleted", true);
            int nbrTotale3Supprime = db.Etudiants3a.Where(s => s.Etudiant.IsDeleted == true).Count();
            return nbrTotale3Supprime;
            
        }
        //Rechereche pour N°Dossier
        public IEnumerable<Etudiant3a> GetAllCandidatsDataNumDossier()
        {
            
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.Etat == "preselectionne" 
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId)
            .OrderBy(e => e.Etudiant.Cne)
            .ToList();
        }
        //Recherche
        public IEnumerable<Etudiant3a> GetAllCandidatsData()
        {
            
       
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
             && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).ToList();
        }

        //Recherche Corbeil
        public IEnumerable<Etudiant3a> GetAllCandidatsDataSupp()
        {
            
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
             && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
             && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId).ToList();
        }

        public Etudiant3a GetDeleted(int? id)
        {
            db.SetFilterScopedParameterValue("IsDeleted", true);
            return db.Etudiants3a.Include(e => e.Etudiant).Where(e => e.ID == id && e.EtudiantId == e.Etudiant.ID).FirstOrDefault();
        }
        //*************************Deliberation***********************************//

        public IEnumerable<Etudiant3a> GetListAfterDelibByFiliere(int? FiliereID)
        {
            if (FiliereID == null)
            {
                return null;
            }
            return db.Etudiants3a
            .Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.ID_Filiere == FiliereID
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId
            && e.Etudiant.Etat != "inscrit"
            && !string.IsNullOrEmpty(e.Etudiant.NumDossier)
            )
            .ToList();
        }
      

        public void GetDeliberationListAttente(int Filiere, DeliberationViewModel model)
        {
            int nbrAdmis = model.NbrPlaces - model.ListeAtt;
            List<Etudiant3a> AllEtudiants = GetListPreselectionneByFiliere(Filiere)
                .OrderByDescending(e => (e.Etudiant.Note.NoteMath * model.CoefMath + e.Etudiant.Note.Specialite * model.CoefSpec) / model.CoefSpec + model.CoefMath)
                .ThenByDescending(e => e.Etudiant.Note.NoteBac)
                .ThenByDescending(e => e.Etudiant.Note.NoteMath).ToList();


            if (AllEtudiants.Count >= model.NbrPlaces)
            {

                AllEtudiants = AllEtudiants.Take(model.NbrPlaces).ToList();

                List<Etudiant3a> ListAdmis = AllEtudiants.Take(nbrAdmis).ToList();
                List<Etudiant3a> ListAttentes = AllEtudiants.GetRange(nbrAdmis, model.ListeAtt);
                foreach (var item in ListAdmis)
                {
                    item.Etudiant.Etat = "admis";
                }
                foreach (var item in ListAttentes)
                {
                    item.Etudiant.Etat = "attente";
                }
            }



        }
        public async System.Threading.Tasks.Task DeliberationMethodAsync(int Filiere, DeliberationViewModel model)
        {
            int nbrAdmis = model.NbrPlaces - model.ListeAtt;
            List<Etudiant3a> AllEtudiants = GetListPreselectionneByFiliere(Filiere)
                .OrderByDescending(e => (e.Etudiant.Note.NoteMath * model.CoefMath + e.Etudiant.Note.Specialite * model.CoefSpec) / model.CoefSpec + model.CoefMath)
                .ThenByDescending(e => e.Etudiant.Note.NoteBac)
                .ThenByDescending(e => e.Etudiant.Note.NoteMath).ToList();
            Debug.WriteLine(AllEtudiants.Count());

            if (AllEtudiants.Count >= model.NbrPlaces)
            {
                AllEtudiants = AllEtudiants.Take(model.NbrPlaces).ToList();
                List<Etudiant3a> ListAdmis = AllEtudiants.Take(nbrAdmis).ToList();
                List<Etudiant3a> ListAttentes = AllEtudiants.GetRange(nbrAdmis, model.ListeAtt);
                foreach (var item in ListAdmis)
                {
                    item.Etudiant.Etat = "admis";
                    Debug.WriteLine("Liste :" + item.Etudiant.NomComplet);
                    db.SaveChanges();
                    //await SendEmailAsync(item.EtudiantId, item.Etudiant.ID_Filiere);
                }
                foreach (var item in ListAttentes)
                {
                    Debug.WriteLine("Liste en Attente");
                    item.Etudiant.Etat = "attente";
                    db.SaveChanges();
                }
            }


        }

        public async System.Threading.Tasks.Task<bool> SendEmailAsync(int id, int? FiliereID)
        {
            var client = new HttpClient();
            string nomFiliere = db.Filieres.Find(FiliereID).Titre;
            string url = "http://20.41.74.71:3000/api/email/accepted/" + id + "?json={filiere:\"" + nomFiliere + "\"}";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("token", "vDdzzWaXf8866Gbsdzfz788221Afd");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }
        
        public async System.Threading.Tasks.Task<bool> SendEmailPreselectionAsync(int id, int? FiliereID)
        {
            var client = new HttpClient();
            string nomFiliere = db.Filieres.Find(FiliereID).Titre;
            string url = "http://20.41.74.71:3000/api/email/preselection/" + id + "?json={filiere:\"" + nomFiliere + "\"}";
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("token", "vDdzzWaXf8866Gbsdzfz788221Afd");
            HttpResponseMessage response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;

        }

        public IEnumerable<Etudiant3a> GetListAdmisByFiliere(int? FiliereID)
        {
            if (FiliereID == null)
            {
                return null;
            }
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.ID_Filiere == FiliereID
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId
            && e.Etudiant.Etat == "admis"
            && !string.IsNullOrEmpty(e.Etudiant.NumDossier)
            ).OrderByDescending(e => (e.Etudiant.Note.NoteMath + e.Etudiant.Note.Specialite) / 2)
            .ToList();
        }
        public IEnumerable<Etudiant3a> GetListAttenteByFiliere(int? FiliereID)
        {
            if (FiliereID == null)
            {
                return null;
            }
            return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
            && e.Etudiant.ID_Filiere == FiliereID
            && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
            && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId
            && e.Etudiant.Etat == "attente"
            && !string.IsNullOrEmpty(e.Etudiant.NumDossier)
            ).OrderByDescending(e => (e.Etudiant.Note.NoteMath + e.Etudiant.Note.Specialite) / 2)
            .ToList();
        }

        //************************ Statistique ********************************//

        //DUT
        public int ListeDUT()
        {
            var ListeDUT = (from e in db.Etudiants3a
                            from D in db.Diplomes
                            where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT"
                            select e);
            return ListeDUT.Count();
           /* return db.Etudiants3a.Where(e => e.EtudiantId == e.Etudiant.ID
           && e.EtudiantId == e.Etudiant.Note.EtudiantNoteId
           && e.EtudiantId == e.Etudiant.Diplome.EtudiantDiplomeId ).Count();*/
        }
        //DEUG
        public int ListeDEUG()
        {
            var ListeDEUG = (from e in db.Etudiants3a
                             from D in db.Diplomes
                             where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG"
                             select e);
           return ListeDEUG.Count();
        }
        //DEUST
        public int ListeDEUST()
        {
            var ListeDEUST = (from e in db.Etudiants3a
                              from D in db.Diplomes
                              where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST"
                              select e);
            return ListeDEUST.Count();
        }
        //LF
        public int ListeLF()
        {
            var ListeLF = (from e in db.Etudiants3a
                           from D in db.Diplomes
                           where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LF"
                           select e);
            return ListeLF.Count();
        }
        //LP
        public int ListeLP()
        {
            var ListeLP = (from e in db.Etudiants3a
                           from D in db.Diplomes
                           where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LP"
                           select e);
            return ListeLP.Count();
        }

        //**************table statistique********************//
        public int ListeGINFDUT()
        {
            var ListeGINFDUT = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 1
                                select e);
            return ListeGINFDUT.Count();
        }

        public int ListeGINDDUT()
        {
            var ListeGINDDUT = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 2
                                select e);
            return ListeGINDDUT.Count();

        }

        public int ListeGPMCDUT()
        {
            var ListeGPMCDUT = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 3
                                select e);
            return ListeGPMCDUT.Count();

        }

        public int ListeGTRDUT()
        {
            var ListeGTRDUT = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 4
                               select e);
            return ListeGTRDUT.Count();

        }

        public int ListeGINFDEUG()
        {
            var ListeGINFDEUG = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 1
                                 select e);
            return ListeGINFDEUG.Count();

        }

        public int ListeGINDDEUG()
        {
            var ListeGINDDEUG = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 2
                                 select e);
            return ListeGINDDEUG.Count();

        }

        public int ListeGPMCDEUG()
        {
            var ListeGPMCDEUG = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 3
                                 select e);
            return ListeGPMCDEUG.Count();

        }

        public int ListeGTRDEUG()
        {
            var ListeGTRDEUG = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 4
                                select e);
            return ListeGTRDEUG.Count();

        }

        public int ListeGINFDEUST()
        {
            var ListeGINFDEUST = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 1
                                  select e);
            return ListeGINFDEUST.Count();

        }

        public int ListeGINDDEUST()
        {
            var ListeGINDDEUST = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 2
                                  select e);
            return ListeGINDDEUST.Count();

        }

        public int ListeGPMCDEUST()
        {
            var ListeGPMCDEUST = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 3
                                  select e);
            return ListeGPMCDEUST.Count();

        }

        public int ListeGTRDEUST()
        {
            var ListeGTRDEUST = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 4
                                 select e);
            return ListeGTRDEUST.Count();

        }

        public int ListeGINFLP()
        {
            var ListeGINFLP = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 1
                               select e);
            return ListeGINFLP.Count();

        }

        public int ListeGINDLP()
        {
            var ListeGINDLP = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 2
                               select e);
            return ListeGINDLP.Count();

        }

        public int ListeGPMCLP()
        {
            var ListeGPMCLP = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 3
                               select e);
            return ListeGPMCLP.Count();

        }

        public int ListeGTRLP()
        {
            var ListeGTRLP = (from e in db.Etudiants3a
                              from D in db.Diplomes
                              where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 4
                              select e);
            return ListeGTRLP.Count();

        }

        public int ListeGINFLF()
        {
            var ListeGINFLF = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 1
                               select e);
            return ListeGINFLF.Count();

        }

        public int ListeGINDLF()
        {
            var ListeGINDLF = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 2
                               select e);
            return ListeGINDLF.Count();

        }

        public int ListeGPMCLF()
        {
            var ListeGPMCLF = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 3
                               select e);
            return ListeGPMCLF.Count();
        }

        public int ListeGTRLF()
        {
            var ListeGTRLF = (from e in db.Etudiants3a
                              from D in db.Diplomes
                              where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 4
                              select e);
           return ListeGTRLF.Count();
        }
     //*********tab stat preselectionne
        public int ListeGINFDUTC()
        {
            var ListeGINFDUTC = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 1
                                 && e.Etudiant.Etat == "preselectionne"
                                 select e);
            return ListeGINFDUTC.Count();
        }

        public int ListeGINDDUTC()
        {
            var ListeGINDDUTC = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 2
                                 && e.Etudiant.Etat == "preselectionne"
                                 select e);
            return ListeGINDDUTC.Count();
        }

        public int ListeGPMCDUTC()
        {
            var ListeGPMCDUTC = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 3
                                 && e.Etudiant.Etat == "preselectionne"
                                 select e);
            return ListeGPMCDUTC.Count();
        }

        public int ListeGTRDUTC()
        {
            var ListeGTRDUTC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DUT" && e.Etudiant.ID_Filiere == 4
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGTRDUTC.Count();
        }

        public int ListeGINFDEUGC()
        {
            var ListeGINFDEUGC = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 1
                                  && e.Etudiant.Etat == "preselectionne"
                                  select e);
            return ListeGINFDEUGC.Count();
        }

        public int ListeGINDDEUGC()
        {
            var ListeGINDDEUGC = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 2
                                  && e.Etudiant.Etat == "preselectionne"
                                  select e);
            return ListeGINDDEUGC.Count();
        }

        public int ListeGPMCDEUGC()
        {
            var ListeGPMCDEUGC = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 3
                                  && e.Etudiant.Etat == "preselectionne"
                                  select e);
            return ListeGPMCDEUGC.Count();
        }

        public int ListeGTRDEUGC()
        {
            var ListeGTRDEUGC = (from e in db.Etudiants3a
                                 from D in db.Diplomes
                                 where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUG" && e.Etudiant.ID_Filiere == 4
                                 && e.Etudiant.Etat == "preselectionne"
                                 select e);
            return ListeGTRDEUGC.Count();
        }

        public int ListeGINFDEUSTC()
        {
            var ListeGINFDEUSTC = (from e in db.Etudiants3a
                                   from D in db.Diplomes
                                   where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 1
                                   && e.Etudiant.Etat == "preselectionne"
                                   select e);
            return ListeGINFDEUSTC.Count();
        }

        public int ListeGINDDEUSTC()
        {
            var ListeGINDDEUSTC = (from e in db.Etudiants3a
                                   from D in db.Diplomes
                                   where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 2
                                   && e.Etudiant.Etat == "preselectionne"
                                   select e);
            return ListeGINDDEUSTC.Count();
        }

        public int ListeGPMCDEUSTC()
        {
            var ListeGPMCDEUSTC = (from e in db.Etudiants3a
                                   from D in db.Diplomes
                                   where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 3
                                   && e.Etudiant.Etat == "preselectionne"
                                   select e);
            return ListeGPMCDEUSTC.Count();
        }

        public int ListeGTRDEUSTC()
        {
            var ListeGTRDEUSTC = (from e in db.Etudiants3a
                                  from D in db.Diplomes
                                  where e.EtudiantId == D.EtudiantDiplomeId && D.Titre == "DEUST" && e.Etudiant.ID_Filiere == 4
                                  && e.Etudiant.Etat == "preselectionne"
                                  select e);
            return ListeGTRDEUSTC.Count();
        }

        public int ListeGINFLPC()
        {
            var ListeGINFLPC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 1
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGINFLPC.Count();
        }

        public int ListeGINDLPC()
        {
            var ListeGINDLPC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 2
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGINDLPC.Count();
        }

        public int ListeGPMCLPC()
        {
            var ListeGPMCLPC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 3
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGPMCLPC.Count();
        }

        public int ListeGTRLPC()
        {
            var ListeGTRLPC = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.ID == D.EtudiantDiplomeId && D.Titre == "LP" && e.Etudiant.ID_Filiere == 4
                               && e.Etudiant.Etat == "preselectionne"
                               select e);
            return ListeGTRLPC.Count();
        }

        public int ListeGINFLFC()
        {
            var ListeGINFLFC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 1
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGINFLFC.Count();
        }

        public int ListeGINDLFC()
        {
            var ListeGINDLFC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 2
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGINDLFC.Count();
        }

        public int ListeGPMCLFC()
        {
            var ListeGPMCLFC = (from e in db.Etudiants3a
                                from D in db.Diplomes
                                where e.ID == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 3
                                && e.Etudiant.Etat == "preselectionne"
                                select e);
            return ListeGPMCLFC.Count();
        }

        public int ListeGTRLFC()
        {
            var ListeGTRLFC = (from e in db.Etudiants3a
                               from D in db.Diplomes
                               where e.ID == D.EtudiantDiplomeId && D.Titre == "LF" && e.Etudiant.ID_Filiere == 4
                               && e.Etudiant.Etat == "preselectionne"
                               select e);
            return ListeGTRLFC.Count();
        }
    }
}
