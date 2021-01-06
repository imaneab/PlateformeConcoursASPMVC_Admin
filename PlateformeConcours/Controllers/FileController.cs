using PlateformeConcours.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlateformeConcours.Controllers
{
    public class FileController : Controller
    {
        //Context2 db = new Context2();
        ApplicationDbContext db = new ApplicationDbContext();
        //1er view :: contient table des filiere avec (button liste des epreuves -> vers une table des pdf)+(button vers form d'insertion des pdf)    
        public ActionResult Index()
        {

            //Context2 db = new Context2();
            List<Filiere> filieres = new List<Filiere>();
            return View(db.Filieres.ToList());
        }


        public ActionResult delete(int Id, int Id_fil)
        {
            Models.File file = db.files.Find(Id);
            db.files.Remove(file);
            db.SaveChanges();
           // return RedirectToAction("ListFilesParFiliere", Id_fil);
            return RedirectToAction("ListFilesParFiliere", new { ID = Id_fil });

        }
        //return RedirectToAction("URL" , new { id = yourID
   


        // la liste des fichires d'une filiere
        public ActionResult ListFilesParFiliere(int? ID)
        {

            // List<Models.File> listfiles = db.Filieres.Where(f => f.ID== ID).Single().files.ToList();
            List<Models.File> listfiles = db.files.Where(f => f.Id_fil == ID).ToList();

            ViewData["ID"] = ID;
            return View(listfiles);
        }


        public ActionResult AddFile()
        {
            ViewBag.filiere = new SelectList(db.Filieres, "ID", "Titre");
            return View();
        }


        //upload file
        public ActionResult Get(int ID)
        {

            ApplicationDbContext db = new ApplicationDbContext();
            //Context2 db = new Context2();
            Models.File file = db.files.Find(ID);

            //If file exists....
            ViewBag.Id_fil = new SelectList(db.Filieres, "Id_filiere", "Nom_filiere");
            MemoryStream ms = new MemoryStream(file.Content, 0, 0, true, true);
            Response.ContentType = file.Type;
            Response.AddHeader("content-disposition", "attachment;filename=" + file.Name);
            Response.Buffer = true;
            Response.Clear();
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.End();

            ViewBag.filieres = new SelectList(db.Filieres, "ID", "Nom_fil");


            return new FileStreamResult(Response.OutputStream, file.Type);


        }


        //add file in database

        [HttpPost]
        public ActionResult Add(HttpPostedFileBase uploadFile)
        {
            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                //Context2 db = new Context2();
                Models.File file = new Models.File();

                byte[] tempFile = new byte[uploadFile.ContentLength];
                string nameFile = uploadFile.FileName;
                int lengthFile = uploadFile.ContentLength;
                string typeFile = uploadFile.ContentType;
                int Annee = uploadFile.ContentLength;


                uploadFile.InputStream.Read(tempFile, 0, uploadFile.ContentLength);
                file.Content = tempFile;
                file.Name = nameFile;
                file.Length = lengthFile;
                file.Type = typeFile;
                file.Annee = Annee;
                // file.id_fil = Convert.ToInt32(Request.Form["id_fil"]);

                file.Id_fil = Convert.ToInt32(Request.Form["ID"]);// bach anbdliha!
                file.Titre_concours = Request.Form["Titre_concours"];
                file.Annee = Convert.ToInt32(Request.Form["Annee"]);

                db.files.Add(file);
                db.SaveChanges();

            }

            return RedirectToAction("Index");

        }


    }
}