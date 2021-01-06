using EntityFramework.DynamicFilters;
using PlateformeConcours.Models;
using PlateformeConcours.Repository;
using PlateformeConcours.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PlateformeConcours.Controllers
{
    public class Etudiant3aController : Controller
    {
        private IEtudiant3aRepository repository;
        private IFiliereRepository FilRepo;

        public int etudiant { get; private set; }

        public Etudiant3aController(IEtudiant3aRepository repository, IFiliereRepository FilRepo)
        {
            this.repository = repository;
            this.FilRepo = FilRepo;
        }
        //Preselection 3ieme annee
        // GET: Etudiant
        [Authorize]
        public ActionResult Preselection3A()
        {
            var ListFiliere = FilRepo.Get();
            ViewBag.ListFil = new SelectList(ListFiliere, "ID", "Titre");
            return View();
            //return Content(ListFiliere.ToList()[0].Titre);

        }

        //Post
        //La liste des étudiants préselectionné
        [Authorize]
        [HttpPost]
        public ActionResult Liste(int x,Preselection3aViewModel model)
        {
            List<Etudiant> list = new List<Etudiant>();
            double notefinal = 0;
            
                foreach (var item in repository.GetPreselection(x,model.TypeDiplome))
                {
                    notefinal = ((item.Etudiant.Note.NoteBac * model.Bac) + (item.Etudiant.Note.S1 * model.S1) + (item.Etudiant.Note.S2 * model.S2) + (item.Etudiant.Note.S3 * model.S3) + (item.Etudiant.Note.S4 * model.S4)) / (model.Bac + model.S1 + model.S2 + model.S3 + model.S4);
                    if (notefinal >= model.Seuil)
                    {
                        list.Add(item.Etudiant);
                        item.Etudiant.Etat = "preselectionne";
                        repository.Save();
                        repository.SendEmailPreselectionAsync(item.EtudiantId, x);
                    }
                        

            }
            ViewBag.type = model.TypeDiplome;
            return View(list);

        }





        //Recherche3A
        [Authorize]
        public ActionResult GetAllCondidatData()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            return View(repository.GetAllCandidatsData());
        }

        // GET: Etudiants/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant3a etudiant = repository.Get(id) ;
       
            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                Etudiant3a etudiant = repository.Get(id);
                etudiant.Etudiant.IsDeleted = true;
                etudiant.Etudiant.Note.IsDeleted = true;
                etudiant.Etudiant.Diplome.IsDeleted = true;
                repository.Save();
                TempData["Message"] = "success";
                return RedirectToAction("GetAllCondidatData");
            }
            else
            {
                TempData["Message"] = "error";
                return RedirectToAction("GetAllCondidatData");
            }
        }


        //RechercheCorbeil
        [Authorize]
        public ActionResult Recherche3ACorbeil()
        {
            return View(repository.GetAllCandidatsDataSupp());
        }

        //Rendre un candidat non supprimé
        // GET: Etudiants/Delete/5
        [Authorize]
        public ActionResult Retourner(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant3a etudiant = repository.GetSup(id);

            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [Authorize]
        [HttpPost, ActionName("Retourner")]
        [ValidateAntiForgeryToken]
        public ActionResult RetournerConfirmed(int id)
        {

            Etudiant3a etudiant = repository.GetSup(id);
            etudiant.Etudiant.IsDeleted = false;
            etudiant.Etudiant.Note.IsDeleted = false;
            etudiant.Etudiant.Diplome.IsDeleted = false;
            repository.Save();
            return RedirectToAction("Recherche3ACorbeil");
        }

        //Supprimerun candidat corbeille
        // GET: Etudiants/Delete/5
        [Authorize]
        public ActionResult DeleteC(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant3a etudiant = repository.GetSup2(id);

            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        /*  public ActionResult Test()
          {
              repository.Remove(new Etudiant3a());
              return Content("deleted");
          }*/

        // POST: Etudiants/Delete/5
        [Authorize]
        [HttpPost, ActionName("DeleteC")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCConfirmed(int id)
        {
            Etudiant3a etudiant = repository.GetSup2(id);
            
            repository.Remove(etudiant);
           // repository.Save();
            return RedirectToAction("Recherche3ACorbeil");
        }


        //*************************** correction ***************************//

        // Affiche une liste des  filiere pour les corrections
        public ActionResult ChoixFiliereCorrection()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"].ToString();
            }
            var ListFiliere = FilRepo.Get();
            return View(new SelectList(ListFiliere, "ID", "Titre"));
        }
        [HttpGet]
        public ActionResult Correction3a(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else if (FilRepo.GetFiliereTitre(id) == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Filiere = FilRepo.GetFiliereTitre(id);
            List<CorrectionViewModel> Models = new List<CorrectionViewModel>();
            List<Etudiant3a> list = repository.GetListPreselectionneByFiliere(id).ToList();
            foreach (var item in list)
            {
                Models.Add(new CorrectionViewModel
                {
                    ID = item.ID,
                    NumDossier = item.Etudiant.NumDossier,
                    Cin = item.Etudiant.Cin,
                    Nom = item.Etudiant.Nom,
                    Prenom = item.Etudiant.Prenom,
                    Diplome = item.Etudiant.Diplome.Titre,
                    Filiere = item.Etudiant.Filiere.Titre,
                    NoteMath = item.Etudiant.Note.NoteMath,
                    NoteSpecialite = item.Etudiant.Note.Specialite
                });

            }
            return View(Models);
        }
        [HttpPost]

        public ActionResult Correction3a(List<CorrectionViewModel> model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    foreach (var item in model)
                    {
                        var etudiant = repository.Get(item.ID);
                        if (etudiant != null)
                        {
                            etudiant.Etudiant.Note.NoteMath = item.NoteMath;
                            etudiant.Etudiant.Note.Specialite = item.NoteSpecialite;
                            repository.Update(etudiant);
                        }
                    }
                    TempData["Message"] = "success";
                    return RedirectToAction("ChoixFiliereCorrection");
                }
                else
                {
                    TempData["Message"] = "error";
                    return RedirectToAction("ChoixFiliereCorrection");
                }
            }
            catch
            {
                TempData["Message"] = "error";
                return RedirectToAction("ChoixFiliereCorrection");
            }
        }

        //**************************Delibiration***************************//
        // cette action affiche la phase de deliberation pr 3eme annee
        public ActionResult Deliberation()
        {
            //ViewBag.Message = TempData["Message"].ToString();
            var ListFiliere = FilRepo.Get();
            ViewBag.liste = new SelectList(ListFiliere, "ID", "Titre");

            var classement = new List<SelectListItem>
            {
                new SelectListItem{ Text="Note Globale", Value = "Note Globale"},
                new SelectListItem{ Text="Note Mathemetique", Value = "Note Mathemetique" },
                new SelectListItem{ Text="Note du Bac", Value = "Note du Bac" },
            };
            ViewBag.Classement = classement;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListDeliberation(int id, DeliberationViewModel model)
        {
            if (ModelState.IsValid)
            {
                repository.DeliberationMethodAsync(id, model);
                TempData["Message"] = "success";
                return RedirectToAction("ListAfterDeliberation", new { id = id });
            }
            else
            {
                TempData["Message"] = "Error";
                //return View(model);
                return RedirectToAction("Deliberation", model);
            }
        }

        public ActionResult ChoixListeDeliberation()
        {
            var ListFiliere = FilRepo.Get();
            return View(new SelectList(ListFiliere, "ID", "Titre"));
        }
        [HttpPost]
        public ActionResult DeliberationActions(int? id, string submit)
        {
            switch (submit)
            {
                case "Listes":
                    return RedirectToAction("ListAfterDeliberation", new { id = id });
                case "Resultats":
                    return RedirectToAction("ResultatsConcours", new { id = id });
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        [HttpGet]
        public ActionResult ListAfterDeliberation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Etudiant3a> list = repository.GetListAfterDelibByFiliere(id).ToList();
            List<ListDeliberation3ViewModel> res = new List<ListDeliberation3ViewModel>();
            foreach (var item in list)
            {
                res.Add(new ListDeliberation3ViewModel(id, item));
            }
            return View(res);
        }

        public ActionResult ResultatsConcours(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListsResultat3a Resultats = new ListsResultat3a();
            Resultats.ListAdmis = repository.GetListAdmisByFiliere(id).ToList();
            Resultats.ListAttente = repository.GetListAttenteByFiliere(id).ToList();
            Resultats.Titre = FilRepo.GetFiliereTitre(id);
            return View(Resultats);
        }





        //*****************************************************************//
        //**************************Statistique *********************//
        [Authorize]
        public ActionResult Statistique()
        {
            ViewData["NombreDUT"] = repository.ListeDUT();
            ViewData["NombreDEUG"] = repository.ListeDEUST();
            ViewData["NombreDEUST"] = repository.ListeDEUST();
            ViewData["NombreLF"] = repository.ListeLF();
            ViewData["NombreLP"] = repository.ListeLP();

            //tab Stats
            //DUT
            
            int NombreGINFDUT = repository.ListeGINFDUT();
            ViewData["NombreGINFDUT"] = NombreGINFDUT;   
            int NombreGINDDUT = repository.ListeGINDDUT();
            ViewData["NombreGINDDUT"] = NombreGINDDUT;          
            int NombreGPMCDUT = repository.ListeGPMCDUT();
            ViewData["NombreGPMCDUT"] = NombreGPMCDUT;     
            int NombreGTRDUT = repository.ListeGTRDUT();
            ViewData["NombreGTRDUT"] = NombreGTRDUT;

            int TotalPreinsDUT = NombreGINFDUT + NombreGINDDUT + NombreGPMCDUT + NombreGTRDUT;
            ViewData["TotalPreinsDUT"] = TotalPreinsDUT;

            //DEUG
           
            int NombreGINFDEUG = repository.ListeGINFDEUG();
            ViewData["NombreGINFDEUG"] = NombreGINFDEUG;
            int NombreGINDDEUG = repository.ListeGINDDEUG();
            ViewData["NombreGINDDEUG"] = NombreGINDDEUG;
            int NombreGPMCDEUG = repository.ListeGPMCDEUG();
            ViewData["NombreGPMCDEUG"] = NombreGPMCDEUG;  
            int NombreGTRDEUG = repository.ListeGTRDEUG();
            ViewData["NombreGTRDEUG"] = NombreGTRDEUG;

            int TotalPreinsDEUG = NombreGINFDEUG + NombreGINDDEUG + NombreGPMCDEUG + NombreGTRDEUG;
            ViewData["TotalPreinsDEUG"] = TotalPreinsDEUG;

            //DEUST
            
            int NombreGINFDEUST = repository.ListeGINFDEUST();
            ViewData["NombreGINFDEUST"] = NombreGINFDEUST;
            int NombreGINDDEUST = repository.ListeGINDDEUST();
            ViewData["NombreGINDDEUST"] = NombreGINDDEUST;
            int NombreGPMCDEUST = repository.ListeGPMCDEUST();
            ViewData["NombreGPMCDEUST"] = NombreGPMCDEUST;
            int NombreGTRDEUST = repository.ListeGTRDEUST();
            ViewData["NombreGTRDEUST"] = NombreGTRDEUST;

            int TotalPreinsDEUST = NombreGINFDEUST + NombreGINDDEUST + NombreGPMCDEUST + NombreGTRDEUST;
            ViewData["TotalPreinsDEUST"] = TotalPreinsDEUST;

            //lP
            
            int NombreGINFLP = repository.ListeGINFLP();
            ViewData["NombreGINFLP"] = NombreGINFLP;
            int NombreGINDLP = repository.ListeGINDLP();
            ViewData["NombreGINDLP"] = NombreGINDLP;
            int NombreGPMCLP = repository.ListeGPMCLP();
            ViewData["NombreGPMCLP"] = NombreGPMCLP;
            int NombreGTRLP = repository.ListeGTRLP();
            ViewData["NombreGTRLP"] = NombreGTRLP;

            int TotalPreinsLP = NombreGINFLP + NombreGINDLP + NombreGPMCLP + NombreGTRLP;
            ViewData["TotalPreinsLP"] = TotalPreinsLP;
            //lF
           
            int NombreGINFLF = repository.ListeGINFLF();
            ViewData["NombreGINFLF"] = NombreGINFLF; 
            int NombreGINDLF = repository.ListeGINDLF();
            ViewData["NombreGINDLF"] = NombreGINDLF;
            int NombreGPMCLF = repository.ListeGPMCLF();
            ViewData["NombreGPMCLF"] = NombreGPMCLF;  
            int NombreGTRLF = repository.ListeGTRLF();
            ViewData["NombreGTRLF"] = NombreGTRLF;

            int TotalPreinsLF = NombreGINFLF + NombreGINDLF + NombreGPMCLF + NombreGTRLF;
            ViewData["TotalPreinsLF"] = TotalPreinsLF;
            //totale prienscrit 

            int TotalPreinscrit = TotalPreinsDEUST + TotalPreinsDEUG + TotalPreinsDUT + TotalPreinsLF + TotalPreinsLP;
            ViewData["TotalPreinscrit"] = TotalPreinscrit;

            int TotalGINF = NombreGINFLF + NombreGINFLP + NombreGINFDUT + NombreGINFDEUG + NombreGINFDEUST;
            ViewData["TotalGINF"] = TotalGINF;

            int TotalGPMC = NombreGPMCLF + NombreGPMCLP + NombreGPMCDUT + NombreGPMCDEUG + NombreGPMCDEUST;
            ViewData["TotalGPMC"] = TotalGPMC;

            int TotalGTR = NombreGTRLF + NombreGTRLP + NombreGTRDUT + NombreGTRDEUG + NombreGTRDEUST;
            ViewData["TotalGTR"] = TotalGTR;

            int TotalGIND = NombreGINDLF + NombreGINDLP + NombreGINDDUT + NombreGINDDEUG + NombreGINDDEUST;
            ViewData["TotalGIND"] = TotalGIND;


            //**********************tab Stats Convoque

            //DUT
           
            int NombreGINFDUTC = repository.ListeGINFDUTC();
            ViewData["NombreGINFDUTC"] = NombreGINFDUTC;
            int NombreGINDDUTC = repository.ListeGINDDUTC();
            ViewData["NombreGINDDUTC"] = NombreGINDDUTC;
            int NombreGPMCDUTC = repository.ListeGPMCDUTC();
            ViewData["NombreGPMCDUTC"] = NombreGPMCDUTC;
            int NombreGTRDUTC = repository.ListeGTRDUTC();
            ViewData["NombreGTRDUTC"] = NombreGTRDUTC;

            int TotalConvoqueDUT = NombreGINFDUTC + NombreGINDDUTC + NombreGPMCDUTC + NombreGTRDUTC;

            ViewData["TotalConvoqueDUT"] = TotalConvoqueDUT;

            //DEUG
            
            int NombreGINFDEUGC = repository.ListeGINFDEUGC();
            ViewData["NombreGINFDEUGC"] = NombreGINFDEUGC;  
            int NombreGINDDEUGC = repository.ListeGINDDEUGC();
            ViewData["NombreGINDDEUGC"] = NombreGINDDEUGC;  
            int NombreGPMCDEUGC = repository.ListeGPMCDEUGC();
            ViewData["NombreGPMCDEUGC"] = NombreGPMCDEUGC;  
            int NombreGTRDEUGC = repository.ListeGTRDEUGC();
            ViewData["NombreGTRDEUGC"] = NombreGTRDEUGC;

            int TotalConvoqueDEUG = NombreGINFDEUGC + NombreGINDDEUGC + NombreGPMCDEUGC + NombreGTRDEUGC;
            ViewData["TotalConvoqueDEUG"] = TotalConvoqueDEUG;
            //DEUST
            
            int NombreGINFDEUSTC = repository.ListeGINFDEUSTC();
            ViewData["NombreGINFDEUSTC"] = NombreGINFDEUSTC;
            int NombreGINDDEUSTC = repository.ListeGINDDEUSTC();
            ViewData["NombreGINDDEUSTC"] = NombreGINDDEUSTC; 
            int NombreGPMCDEUSTC = repository.ListeGPMCDEUSTC();
            ViewData["NombreGPMCDEUSTC"] = NombreGPMCDEUSTC;
            int NombreGTRDEUSTC = repository.ListeGTRDEUSTC();
            ViewData["NombreGTRDEUSTC"] = NombreGTRDEUSTC;

            int TotalConvoqueDEUST = NombreGINFDEUSTC + NombreGINDDEUSTC + NombreGPMCDEUSTC + NombreGTRDEUSTC;
            ViewData["TotalConvoqueDEUST"] = TotalConvoqueDEUST;

            //lP
            
            int NombreGINFLPC = repository.ListeGINFLPC();
            ViewData["NombreGINFLPC"] = NombreGINFLPC;
            int NombreGINDLPC = repository.ListeGINDLPC();
            ViewData["NombreGINDLPC"] = NombreGINDLPC;
            int NombreGPMCLPC = repository.ListeGPMCLPC();
            ViewData["NombreGPMCLPC"] = NombreGPMCLPC;
            int NombreGTRLPC = repository.ListeGTRLPC();
            ViewData["NombreGTRLPC"] = NombreGTRLPC;

            int TotalConvoqueLP = NombreGINFLPC + NombreGINDLPC + NombreGPMCLPC + NombreGTRLPC;
            ViewData["TotalConvoqueLP"] = TotalConvoqueLP;
            //lF
            
            int NombreGINFLFC = repository.ListeGINFLFC();
            ViewData["NombreGINFLFC"] = NombreGINFLFC;
            int NombreGINDLFC = repository.ListeGINDLFC();
            ViewData["NombreGINDLFC"] = NombreGINDLFC;
            int NombreGPMCLFC = repository.ListeGPMCLFC();
            ViewData["NombreGPMCLFC"] = NombreGPMCLFC;
            int NombreGTRLFC = repository.ListeGTRLFC();
            ViewData["NombreGTRLFC"] = NombreGTRLFC;

            int TotalConvoqueLF = NombreGINFLFC + NombreGINDLFC + NombreGPMCLFC + NombreGTRLFC;
            ViewData["TotalConvoqueLF"] = TotalConvoqueLF;
            //totale prienscrit 

            int TotalConvoque = TotalConvoqueDEUST + TotalConvoqueDEUG + TotalConvoqueDUT + TotalConvoqueLF + TotalConvoqueLP;
            ViewData["TotalConvoque"] = TotalConvoque;

            int TotalGINFC = NombreGINFLFC + NombreGINFLPC + NombreGINFDUTC + NombreGINFDEUGC + NombreGINFDEUSTC;
            ViewData["TotalGINFC"] = TotalGINFC;

            int TotalGPMCC = NombreGPMCLFC + NombreGPMCLPC + NombreGPMCDUTC + NombreGPMCDEUGC + NombreGPMCDEUSTC;
            ViewData["TotalGPMCC"] = TotalGPMCC;

            int TotalGTRC = NombreGTRLFC + NombreGTRLPC + NombreGTRDUTC + NombreGTRDEUGC + NombreGTRDEUSTC;
            ViewData["TotalGTRC"] = TotalGTRC;

            int TotalGINDC = NombreGINDLFC + NombreGINDLPC + NombreGINDDUTC + NombreGINDDEUGC + NombreGINDDEUSTC;
            ViewData["TotalGINDC"] = TotalGINDC;


            return View();
        }

        //**************************Affectation Num de dossier *********************//

        // GET: AffectationNumD
        [Authorize]
        public ActionResult AffecterNumDossier()
        {
            return View(repository.GetAllCandidatsDataNumDossier());
        }
        [Authorize]
        [HttpGet]
        public ActionResult NumDossier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Etudiant3a etudiant = repository.GetAllCandidatsData2(id);

            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        [Authorize]
        [HttpPost, ActionName("NumDossier")]
        [ValidateAntiForgeryToken]
        public ActionResult NumDossierConfirmed(int id)
        {

            Etudiant3a etudiant = repository.GetAllCandidatsData2(id);

            if (etudiant.Etudiant.ID_Filiere == 1)
            {
                etudiant.Etudiant.NumDossier = "G-INF" + etudiant.Etudiant.ID;
            }
            if (etudiant.Etudiant.ID_Filiere == 2)
            {
                etudiant.Etudiant.NumDossier = "G-TR" + etudiant.Etudiant.ID;
            }
            if (etudiant.Etudiant.ID_Filiere == 3)
            {
                etudiant.Etudiant.NumDossier = "G-PMC" + etudiant.Etudiant.ID;
            }
            if (etudiant.Etudiant.ID_Filiere == 4)
            {
                etudiant.Etudiant.NumDossier = "G-INDUS" + etudiant.Etudiant.ID;

            }
            repository.Save();
            return RedirectToAction("AffecterNumDossier");
        }

    }
}