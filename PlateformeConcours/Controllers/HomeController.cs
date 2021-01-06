using PlateformeConcours.Models;
using PlateformeConcours.Repository;
using PlateformeConcours.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PlateformeConcours.Controllers
{
    public class HomeController : Controller
    {
        private IEtudiantRepository repository;
        private IEtudiant3aRepository repository3;
        private IEtudiant4aRepository repository4;
        private IFiliereRepository FilRepo;

        public HomeController(IEtudiantRepository repository, IEtudiant3aRepository repository3, IEtudiant4aRepository repository4, IFiliereRepository FilRepo)
        {
            this.repository = repository;
            this.repository3 = repository3;
            this.repository4 = repository4;
            this.FilRepo = FilRepo;
        }



        [Authorize]
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel()
            {
                Preselectionne3Info = repository3.GetListPreselectionneByFiliere(1),
                Preselectionne3Indus = repository3.GetListPreselectionneByFiliere(2),
                Preselectionne3Procede = repository3.GetListPreselectionneByFiliere(3),
                Preselectionne3Telecom = repository3.GetListPreselectionneByFiliere(4),

                Preselectionne4Info = repository4.GetListPreselectionneByFiliere(1),
                Preselectionne4Indus = repository4.GetListPreselectionneByFiliere(2),
                Preselectionne4Procede = repository4.GetListPreselectionneByFiliere(3),
                Preselectionne4Telecom = repository4.GetListPreselectionneByFiliere(4),
                // nombre des inscrits en 3ieme année
                NbrTotal3 = repository3.CountTotale(),
                // nombre des inscrits en 3ieme année informatique
                NbrInfo3 = repository3.CountInfo(),
                // nombre des inscrits en 3ieme année industriel
                NbrIndus3 = repository3.CountIndus(),
                // nombre des inscrits en 3ieme année Procedes
                NbrProcede3 = repository3.CountProcede(),
                // nombre des inscrits en 3ieme année Telecom
                NbrTelecom3 = repository3.CountTelecom(),
                // nombre des 3ieme annee supprimés
                NbrTotalSupp3 = repository3.CountTotaleSupprime(),

                

                // nombre des inscrits en 4ieme année
                NbrTotal4 = repository4.CountTotale(),
                // nombre des inscrits en 3ieme année informatique
                NbrInfo4 = repository4.CountInfo(),
                // nombre des inscrits en 3ieme année industriel
                NbrIndus4 = repository4.CountIndus(),
                // nombre des inscrits en 3ieme année Procedes
                NbrProcede4 = repository4.CountProcede(),
                // nombre des inscrits en 3ieme année Telecom
                NbrTelecom4 = repository4.CountTelecom(),
                // nombre des 3ieme annee supprimés
                NbrTotalSupp4 = repository4.CountTotaleSupprime(),

                

                //nombre totale des utilisateurs
                NbrTotal = repository.CountTotale(),

                //Chart
                Datapoints = repository.Chart(),

        };

            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        //[Authorize]
        //public ActionResult Dashboard()
        //{
        //    context = new ApplicationDbContext();
        //    var result = context.Etudiants3a.ToList();
            
        //    return View(result);
        //}
        [Authorize]
        public ActionResult Statistiques()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Account", "Register");
        }
        
    }
}