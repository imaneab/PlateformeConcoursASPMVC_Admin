namespace PlateformeConcours.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Caches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 60),
                        Value = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Diplomes",
                c => new
                    {
                        EtudiantDiplomeId = c.Int(nullable: false),
                        Titre = c.String(),
                        Etablissement = c.String(),
                        Ville = c.String(),
                        Annee1 = c.Int(nullable: false),
                        Annee2 = c.Int(nullable: false),
                        Annee3 = c.Int(nullable: false),
                        AnneeBac = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        fichier = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Diplome_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EtudiantDiplomeId)
                .ForeignKey("dbo.Etudiants", t => t.EtudiantDiplomeId)
                .Index(t => t.EtudiantDiplomeId);
            
            CreateTable(
                "dbo.Etudiants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Nom = c.String(maxLength: 30),
                        Prenom = c.String(maxLength: 30),
                        Cne = c.String(nullable: false, maxLength: 10),
                        Cin = c.String(nullable: false, maxLength: 8),
                        picture = c.String(),
                        Password = c.String(),
                        Email = c.String(nullable: false, maxLength: 64),
                        Civilite = c.String(),
                        NumDossier = c.String(),
                        Etat = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        DateNaiss = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Nationnalite = c.String(maxLength: 20),
                        Tel = c.String(),
                        Etablissement = c.String(),
                        ID_Filiere = c.Int(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Etudiant_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Filieres", t => t.ID_Filiere)
                .Index(t => t.ID_Filiere);
            
            CreateTable(
                "dbo.Etudiant3a",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EtudiantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Etudiants", t => t.EtudiantId, cascadeDelete: true)
                .Index(t => t.EtudiantId);
            
            CreateTable(
                "dbo.Etudiant4a",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EtudiantId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Etudiants", t => t.EtudiantId, cascadeDelete: true)
                .Index(t => t.EtudiantId);
            
            CreateTable(
                "dbo.Filieres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Titre = c.String(nullable: false, maxLength: 100),
                        Desc = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Length = c.Int(nullable: false),
                        Content = c.Binary(),
                        Titre_concours = c.String(),
                        Annee = c.Int(nullable: false),
                        Id_fil = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Filieres", t => t.Id_fil)
                .Index(t => t.Id_fil);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        EtudiantNoteId = c.Int(nullable: false),
                        NoteBac = c.Double(nullable: false),
                        S1 = c.Double(nullable: false),
                        S2 = c.Double(nullable: false),
                        S3 = c.Double(nullable: false),
                        S4 = c.Double(nullable: false),
                        S5 = c.Double(),
                        S6 = c.Double(),
                        Redoublant1 = c.Boolean(nullable: false),
                        Redoublant2 = c.Boolean(nullable: false),
                        Redoublant3 = c.Boolean(nullable: false),
                        NoteMath = c.Double(),
                        Specialite = c.Double(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Note_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EtudiantNoteId)
                .ForeignKey("dbo.Etudiants", t => t.EtudiantNoteId)
                .Index(t => t.EtudiantNoteId);
            
            CreateTable(
                "dbo.Parametres",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DelaiSignup = c.Int(nullable: false),
                        Titre = c.String(),
                        PlaceVacante = c.Int(nullable: false),
                        Resultats = c.Boolean(nullable: false),
                        ListePrincipaleB3 = c.Boolean(nullable: false),
                        ListeAttenteB3 = c.Boolean(nullable: false),
                        ListPrincipaleB4 = c.Boolean(nullable: false),
                        ListAttenteB3 = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        EtudiantUploadId = c.Int(nullable: false),
                        FichierPdf = c.String(),
                        Photo = c.String(),
                        FileYear = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Upload_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                })
                .PrimaryKey(t => t.EtudiantUploadId)
                .ForeignKey("dbo.Etudiants", t => t.EtudiantUploadId)
                .Index(t => t.EtudiantUploadId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Uploads", "EtudiantUploadId", "dbo.Etudiants");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Diplomes", "EtudiantDiplomeId", "dbo.Etudiants");
            DropForeignKey("dbo.Notes", "EtudiantNoteId", "dbo.Etudiants");
            DropForeignKey("dbo.Etudiants", "ID_Filiere", "dbo.Filieres");
            DropForeignKey("dbo.Files", "Id_fil", "dbo.Filieres");
            DropForeignKey("dbo.Etudiant4a", "EtudiantId", "dbo.Etudiants");
            DropForeignKey("dbo.Etudiant3a", "EtudiantId", "dbo.Etudiants");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Uploads", new[] { "EtudiantUploadId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Notes", new[] { "EtudiantNoteId" });
            DropIndex("dbo.Files", new[] { "Id_fil" });
            DropIndex("dbo.Etudiant4a", new[] { "EtudiantId" });
            DropIndex("dbo.Etudiant3a", new[] { "EtudiantId" });
            DropIndex("dbo.Etudiants", new[] { "ID_Filiere" });
            DropIndex("dbo.Diplomes", new[] { "EtudiantDiplomeId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Uploads",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Upload_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parametres");
            DropTable("dbo.Notes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Note_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Files");
            DropTable("dbo.Filieres");
            DropTable("dbo.Etudiant4a");
            DropTable("dbo.Etudiant3a");
            DropTable("dbo.Etudiants",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Etudiant_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Diplomes",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "DynamicFilter_Diplome_IsDeleted", "EntityFramework.DynamicFilters.DynamicFilterDefinition" },
                });
            DropTable("dbo.Caches");
        }
    }
}
