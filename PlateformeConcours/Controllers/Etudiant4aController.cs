using PlateformeConcours.Models;
using PlateformeConcours.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PlateformeConcours.ViewModel;
using System.Net;

namespace PlateformeConcours.Controllers
{
    public class Etudiant4aController : Controller
    {
        private IEtudiant4aRepository repository;
        private IFiliereRepository FilRepo;

        public int etudiant { get; private set; }

        public Etudiant4aController(IEtudiant4aRepository repository, IFiliereRepository FilRepo)
        {
            this.repository = repository;
            this.FilRepo = FilRepo;
        }

        //Preselection 4ieme annee
        // GET: Etudiant
        [Authorize]
        public ActionResult Preselection4A()
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
        public ActionResult Liste(int x, Preselection4aViewModel model)
        {
            List<Etudiant> list = new List<Etudiant>();
            double notefinal = 0;

            foreach (var item in repository.GetPreselection(x, model.TypeDiplome))
            {
               //notefinal = ((item.Etudiant.Note.NoteBac * model.Bac) + (item.Etudiant.Note.S1 * model.S1) + (item.Etudiant.Note.S2 * model.S2) + (item.Etudiant.Note.S3 * model.S3) + (item.Etudiant.Note.S4 * model.S4)) / (model.Bac + model.S1 + model.S2 + model.S3 + model.S4);
                notefinal = ((item.Etudiant.Note.NoteBac * model.Bac) + (item.Etudiant.Note.S1 * model.S1) + (item.Etudiant.Note.S2 * model.S2) + (item.Etudiant.Note.S3 * model.S3) + (item.Etudiant.Note.S4 * model.S4) + (Conv(item.Etudiant.Note.S5) * model.S5)+ (Conv(item.Etudiant.Note.S6) * model.S6)) / (model.Bac + model.S1 + model.S2 + model.S3 + model.S4 + model.S5 + model.S6);
                if (notefinal >= model.Seuil)
                    list.Add(item.Etudiant);
                item.Etudiant.Etat = "preselectionne";
                repository.Save();
                repository.SendEmailPreselectionAsync(x, item.EtudiantId);

            }
            ViewBag.type = model.TypeDiplome;
            return View(list);

        }

        public double Conv(double? n)
        {
            return n.GetValueOrDefault();
        }

        //Recherche3A
        [Authorize]
        public ActionResult GetAllCondidatData()
        {
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
            Etudiant4a etudiant = repository.Get(id);

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

            Etudiant4a etudiant = repository.Get(id);
            etudiant.Etudiant.IsDeleted = true;
            etudiant.Etudiant.Note.IsDeleted = true;
            etudiant.Etudiant.Diplome.IsDeleted = true;
            repository.Save();
            return RedirectToAction("GetAllCondidatData");
        }

        //RechercheCorbeil
        [Authorize]
        public ActionResult Recherche4ACorbeil()
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
            Etudiant4a etudiant = repository.GetSup(id);

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

            Etudiant4a etudiant = repository.GetSup(id);
            etudiant.Etudiant.IsDeleted = false;
            etudiant.Etudiant.Note.IsDeleted = false;
            etudiant.Etudiant.Diplome.IsDeleted = false;
            repository.Save();
            return RedirectToAction("Recherche4ACorbeil");
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
            Etudiant4a etudiant = repository.GetSup2(id);

            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }



        // POST: Etudiants/Delete/5
        [Authorize]
        [HttpPost, ActionName("DeleteC")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCConfirmed(int id)
        {
            Etudiant4a etudiant = repository.GetSup2(id);

            repository.Remove(etudiant);
            // repository.Save();
            return RedirectToAction("Recherche4ACorbeil");
        }
        //****************************** delib *******************************//
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
        public ActionResult Correction4a(int? id)
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
            List<Correction4aViewModel> Models = new List<Correction4aViewModel>();
            List<Etudiant4a> list = repository.GetListPreselectionneByFiliere(id).ToList();
            foreach (var item in list)
            {
                Models.Add(new Correction4aViewModel
                {
                    ID = item.ID,
                    NumDossier = item.Etudiant.NumDossier,
                    Cin = item.Etudiant.Cin,
                    Nom = item.Etudiant.Nom,
                    Prenom = item.Etudiant.Prenom,
                    Diplome = item.Etudiant.Diplome.Titre,
                    Filiere = item.Etudiant.Filiere.Titre,
                    Classement = Convert.ToInt16(item.Etudiant.Note.Specialite),
                });

            }
            return View(Models);
        }

        [HttpPost]

        public ActionResult Correction4a(List<Correction4aViewModel> model)
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
                            etudiant.Etudiant.Note.Specialite = item.Classement;
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

        public ActionResult Deliberation()
        {
            var ListFiliere = FilRepo.Get();
            ViewBag.liste = new SelectList(ListFiliere, "ID", "Titre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ListDeliberation(int id, Deliberation4ViewModel model)
        {
            try
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
            catch
            {
                return RedirectToAction("Deliberation");
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
            try
            {
                List<Etudiant4a> list = repository.GetListAfterDelibByFiliere(id).ToList();
                List<ListDeliberation4ViewModel> res = new List<ListDeliberation4ViewModel>();
                foreach (var item in list)
                {
                    res.Add(new ListDeliberation4ViewModel(id, item));
                }
                return View(res);
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult ResultatsConcours(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListsResultat4a Resultats = new ListsResultat4a
            {
                ListAdmis = repository.GetListAdmisByFiliere(id).ToList(),
                ListAttente = repository.GetListAttenteByFiliere(id).ToList(),
                Titre = FilRepo.GetFiliereTitre(id)
            };
            return View(Resultats);
        }

        //***************************** Statistique **********************//
        [Authorize]
        public ActionResult Statistique()
        {
            //DUT
            
            int NombreDUT = repository.ListeDUT();
            ViewData["NombreDUT"] = NombreDUT;
            // DEUG
           
            int NombreDEUG = repository.ListeDEUG();
            ViewData["NombreDEUG"] = NombreDEUG;

            // DEUST 
            
            int NombreDEUST = repository.ListeDEUST();
            ViewData["NombreDEUST"] = NombreDEUST;

            //LF
            
            int NombreLF = repository.ListeLF();
            ViewData["NombreLF"] = NombreLF;

            //Lp
           int NombreLP = repository.ListeLP();
            ViewData["NombreLP"] = NombreLP;

            // Stat tab
            // Preinscrit 
            //lP
         
            int NombreGINFLP = repository.ListeGINFLP();
            ViewData["NombreGINFLP"] = NombreGINFLP;

           
            int NombreGINDLP = repository.ListeGINDLP();
            ViewData["NombreGINDLP"] = NombreGINDLP;

           
            int NombreGPMCLP = repository.ListeGPMCLP();
            ViewData["NombreGPMCLP"] = NombreGPMCLP;

            
            int NombreGTRLP = repository.ListeGTRLP();
            ViewData["NombreGTRLP"] = NombreGTRLP;

            //lF
            
            int NombreGINFLF = repository.ListeGINFLF();
            ViewData["NombreGINFLF"] = NombreGINFLF;

           
            int NombreGINDLF = repository.ListeGINDLF();
            ViewData["NombreGINDLF"] = NombreGINDLF;

            
            int NombreGPMCLF = repository.ListeGPMCLF();
            ViewData["NombreGPMCLF"] = NombreGPMCLF;

           
            int NombreGTRLF = repository.ListeGTRLF();
            ViewData["NombreGTRLF"] = NombreGTRLF;


            //lST

            int NombreGINFLST = repository.ListeGINFLST();
            ViewData["NombreGINFLST"] = NombreGINFLST;


            int NombreGINDLST = repository.ListeGINDLST();
            ViewData["NombreGINDLST"] = NombreGINDLST;


            int NombreGPMCLST = repository.ListeGPMCLST();
            ViewData["NombreGPMCLST"] = NombreGPMCLST;


            int NombreGTRLST = repository.ListeGTRLST();
            ViewData["NombreGTRLST"] = NombreGTRLST;

            
            int TotalPreinsLP = NombreGINFLP + NombreGINDLP + NombreGPMCLP + NombreGTRLP;
            ViewData["TotalPreinsLP"] = TotalPreinsLP;

            int TotalPreinsLF = NombreGINFLF + NombreGINDLF + NombreGPMCLF + NombreGTRLF;
            ViewData["TotalPreinsLF"] = TotalPreinsLF;

            int TotalPreinsLST = NombreGINFLST + NombreGINDLST + NombreGPMCLST + NombreGTRLST;
            ViewData["TotalPreinsLST"] = TotalPreinsLST;

            int TotalPreinscrit = TotalPreinsLP + TotalPreinsLF + TotalPreinsLST;
            ViewData["TotalPreinscrit"] = TotalPreinscrit;

            int TotalGINF = NombreGINFLF + NombreGINFLP + NombreGINFLST;
            ViewData["TotalGINF"] = TotalGINF;

            int TotalGTR = NombreGTRLF + NombreGTRLP + NombreGTRLST;
            ViewData["TotalGTR"] = TotalGTR;

            int TotalGIND = NombreGINDLF + NombreGINDLP + NombreGINDLST;
            ViewData["TotalGIND"] = TotalGIND;

            int TotalGPMC = NombreGPMCLF + NombreGPMCLP + NombreGPMCLST;
            ViewData["TotalGPMC"] = TotalGPMC;


            // Convoque

            //lP

            int NombreGINFLPC = repository.ListeGINFLPC();
            ViewData["NombreGINFLPC"] = NombreGINFLPC;


            int NombreGINDLPC = repository.ListeGINDLPC();
            ViewData["NombreGINDLPC"] = NombreGINDLPC;


            int NombreGPMCLPC = repository.ListeGPMCLPC();
            ViewData["NombreGPMCLPC"] = NombreGPMCLPC;


            int NombreGTRLPC = repository.ListeGTRLPC();
            ViewData["NombreGTRLPC"] = NombreGTRLPC;

            //lF

            int NombreGINFLFC = repository.ListeGINFLFC();
            ViewData["NombreGINFLFC"] = NombreGINFLFC;


            int NombreGINDLFC = repository.ListeGINDLFC();
            ViewData["NombreGINDLFC"] = NombreGINDLFC;


            int NombreGPMCLFC = repository.ListeGPMCLFC();
            ViewData["NombreGPMCLFC"] = NombreGPMCLFC;


            int NombreGTRLFC = repository.ListeGTRLFC();
            ViewData["NombreGTRLFC"] = NombreGTRLFC;

            //lST

            int NombreGINFLSTC = repository.ListeGINFLSTC();
            ViewData["NombreGINFLSTC"] = NombreGINFLSTC;


            int NombreGINDLSTC = repository.ListeGINDLSTC();
            ViewData["NombreGINDLSTC"] = NombreGINDLSTC;


            int NombreGPMCLSTC = repository.ListeGPMCLSTC();
            ViewData["NombreGPMCLSTC"] = NombreGPMCLSTC;


            int NombreGTRLSTC = repository.ListeGTRLSTC();
            ViewData["NombreGTRLSTC"] = NombreGTRLSTC;

            int TotalConvoqueLP = NombreGINFLPC + NombreGINDLPC + NombreGPMCLPC + NombreGTRLPC;
            ViewData["TotalConvoqueLP"] = TotalConvoqueLP;

            int TotalConvoqueLF = NombreGINFLFC + NombreGINDLFC + NombreGPMCLFC + NombreGTRLFC;
            ViewData["TotalConvoqueLF"] = TotalConvoqueLF;

            int TotalConvoqueLST = NombreGINFLSTC + NombreGINDLSTC + NombreGPMCLSTC + NombreGTRLSTC;
            ViewData["TotalConvoqueLST"] = TotalConvoqueLST;

            int TotalConvoque = TotalConvoqueLF + TotalConvoqueLP + TotalConvoqueLST;
            ViewData["TotalConvoque"] = TotalConvoque;

            int TotalGINFC = NombreGINFLFC + NombreGINFLPC + NombreGINFLSTC;
            ViewData["TotalGINFC"] = TotalGINFC;

            int TotalGPMCC = NombreGPMCLFC + NombreGPMCLPC + NombreGPMCLSTC;
            ViewData["TotalGPMCC"] = TotalGPMCC;

            int TotalGTRC = NombreGTRLFC + NombreGTRLPC + NombreGTRLSTC;
            ViewData["TotalGTRC"] = TotalGTRC;

            int TotalGINDC = NombreGINDLFC + NombreGINDLPC + NombreGINDLSTC;
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
            Etudiant4a etudiant = repository.GetAllCandidatsData2(id);

            if (etudiant == null)
            {
                return HttpNotFound();
            }
            return View(etudiant);
        }

        // POST: Etudiants/Delete/5
        [Authorize]
        [HttpPost, ActionName("NumDossier")]
        [ValidateAntiForgeryToken]
        public ActionResult NumDossierConfirmed(int id)
        {

            Etudiant4a etudiant = repository.GetAllCandidatsData2(id);

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