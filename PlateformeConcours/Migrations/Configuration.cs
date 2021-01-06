namespace PlateformeConcours.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using PlateformeConcours.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PlateformeConcours.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PlateformeConcours.Models.ApplicationDbContext context)
        {
            //Creation d'admin
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var user = new ApplicationUser { UserName = "admin@admin.com", Email = "admin@admin.com" };
            manager.Create(user, "123456");

            //Ajout des filiers
            var filieres = new List<Filiere>();
            filieres.Add(new Filiere() { ID = 1, Titre = "Genie informatique" });
            filieres.Add(new Filiere() { ID = 2, Titre = "Genie industriel" });
            filieres.Add(new Filiere() { ID = 3, Titre = "Genie Procedes" });
            filieres.Add(new Filiere() { ID = 4, Titre = "Genie Telecom" });
            context.Filieres.AddRange(filieres);

            //Ajout des etudiant
            var etudiants = new List<Etudiant>();
            etudiants.Add(new Etudiant() {ID = 1, Nom = "Abouakil", Prenom = "imane", Cne = "4227", Cin = "KOl33", Email = "imaneabououakil@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 6083", Etablissement = "ENSA", ID_Filiere = 1, Password = "abc", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 2, Nom = "Jnaini", Prenom = "zineb", Cne = "4567", Cin = "KLC54", Email = "jnainizineb@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 6983", Etablissement = "ENSA", ID_Filiere = 1, Password = "ali", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 3, Nom = "Roudani", Prenom = "med", Cne = "40167", Cin = "KL1743", Email = "roudani.muhamed@gmail.com", Civilite = "Mr", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 6999", Etablissement = "ENSA", ID_Filiere = 1, Password = "asp", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 4, Nom = "Hakik", Prenom = "Ahlam", Cne = "9567", Cin = "KI643", Email = "melimi@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 0399", Etablissement = "ENSA", ID_Filiere = 2, Password = "afl", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 5, Nom = "Mira", Prenom = "chaimaa", Cne = "02167", Cin = "PL243", Email = "mirachima@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 1234", Etablissement = "ENSA", ID_Filiere = 3, Password = "anp", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 6, Nom = "Chekkouri", Prenom = "ikram", Cne = "1234", Cin = "ML243", Email = "ikramChek@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 1748", Etablissement = "ENSA", ID_Filiere = 3, Password = "axz", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 7, Nom = "baba", Prenom = "chaimaa", Cne = "12367", Cin = "PL200", Email = "babachima@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 6664", Etablissement = "ENSA", ID_Filiere = 4, Password = "ppp", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 8, Nom = "gramti", Prenom = "ahmed", Cne = "9292", Cin = "MK243", Email = "gramti123@gmail.com", Civilite = "Mr", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 12094", Etablissement = "ENSA", ID_Filiere = 2, Password = "anm", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 9, Nom = "jnaini", Prenom = "meryem", Cne = "99987", Cin = "KO243", Email = "meryJn@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 9094", Etablissement = "ENSA", ID_Filiere = 2, Password = "lnp", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 10, Nom = "ait hamou", Prenom = "mouad", Cne = "2347", Cin = "OL243", Email = "aitMouad@gmail.com", Civilite = "Mr", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 7874", Etablissement = "ENSA", ID_Filiere = 4, Password = "auj", IsDeleted = false, Etat = "inscrit" });
            etudiants.Add(new Etudiant() { ID = 11, Nom = "Bennis", Prenom = "aya", Cne = "13167", Cin = "PLN243", Email = "bennisaya@gmail.com", Civilite = "Mme", DateNaiss = new DateTime(1998,01, 01), Nationnalite = "Marocaine", Tel = "+212 0034", Etablissement = "ENSA", ID_Filiere = 3, Password = "aply", IsDeleted = false, Etat = "inscrit" });
            context.Etudiants.AddRange(etudiants);

            
            //Diplomes
            var diplomes = new List<Diplome>();
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 1, Etablissement = "ESTE", Titre = "DUT", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 2, Etablissement = "ESTE", Titre = "DUT", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 3, Etablissement = "ESTE", Titre = "DEUG", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 4, Etablissement = "ESTE", Titre = "DEUG", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 5, Etablissement = "ESTE", Titre = "DEUST", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 6, Etablissement = "ESTE", Titre = "LP", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 7, Etablissement = "ESTE", Titre = "LP", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 8, Etablissement = "ESTE", Titre = "LF", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 9, Etablissement = "ESTE", Titre = "LF", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 10, Etablissement = "ESTE", Titre = "LP", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            diplomes.Add(new Diplome() { EtudiantDiplomeId = 11, Etablissement = "ESTE", Titre = "LP", Annee3 = 2019, Annee2 = 2018, Annee1 = 2017, AnneeBac = 2015 });
            
            context.Diplomes.AddRange(diplomes);

            //Note
            var notes = new List<Note>();
            notes.Add(new Note() { EtudiantNoteId = 1, NoteBac = 15, S1 = 16, S2 = 17, S3 = 16, S4 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 2, NoteBac = 13, S1 = 13, S2 = 14, S3 = 12, S4 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 3, NoteBac = 15, S1 = 17, S2 = 14, S3 = 15, S4 = 16, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 4, NoteBac = 13, S1 = 12, S2 = 15, S3 = 16, S4 = 14, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 5, NoteBac = 17, S1 = 14, S2 = 17, S3 = 14, S4 = 16, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 6, NoteBac = 14, S1 = 14, S2 = 12, S3 = 13, S4 = 16, S5 = 14 , S6 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 7, NoteBac = 13, S1 = 14, S2 = 15, S3 = 14, S4 = 14, S5 = 12 , S6 = 16, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 8, NoteBac = 13, S1 = 14, S2 = 14, S3 = 13, S4 = 14, S5 = 13 , S6 = 13, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 9, NoteBac = 14, S1 = 15, S2 = 15, S3 = 13, S4 = 14, S5 = 15 , S6 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 10, NoteBac = 14, S1 = 14, S2 = 14, S3 = 13, S4 = 15, S5 = 13 , S6 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });
            notes.Add(new Note() { EtudiantNoteId = 11, NoteBac = 14, S1 = 14, S2 = 14, S3 = 13, S4 = 15, S5 = 15 , S6 = 15, Redoublant1 = false, Redoublant2 = false, Redoublant3 = false });


            context.Notes.AddRange(notes);

            base.Seed(context);
            //Ajout d etudiant 3
            var etudiant3 = new List<Etudiant3a>();
            etudiant3.Add(new Etudiant3a() { EtudiantId = 1 });
            etudiant3.Add(new Etudiant3a() { EtudiantId = 2 });
            etudiant3.Add(new Etudiant3a() { EtudiantId = 3 });
            etudiant3.Add(new Etudiant3a() { EtudiantId = 4 });
            etudiant3.Add(new Etudiant3a() { EtudiantId = 5 });
            context.Etudiants3a.AddRange(etudiant3);

            //Ajout d etudiant 4
            var etudiant4 = new List<Etudiant4a>();
            etudiant4.Add(new Etudiant4a() { EtudiantId = 6 });
            etudiant4.Add(new Etudiant4a() { EtudiantId = 7 });
            etudiant4.Add(new Etudiant4a() { EtudiantId = 8 });
            etudiant4.Add(new Etudiant4a() { EtudiantId = 9 });
            etudiant4.Add(new Etudiant4a() { EtudiantId = 10 });
            etudiant4.Add(new Etudiant4a() { EtudiantId = 11 });
            context.Etudiants4a.AddRange(etudiant4);


        }
    }
}
