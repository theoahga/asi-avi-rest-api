using asi_avi_rest_api.Controllers;
using asi_avi_rest_api.Models.DataManager;
using asi_avi_rest_api.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using NuGet.Protocol;
using System.Collections.Generic;
using System.Xml;
using TP2Console.Models.EntityFramework;

namespace asi_avi_rest_apiTests.Controllers
{
    [TestClass]
    public class UtilisateursControllerTests
    {
        private IDataRepository<Utilisateur> dataRepository;
        private readonly NotationDbContext _context;
        private UtilisateursController controller;
       public UtilisateursControllerTests() 
        {
            var builder = new DbContextOptionsBuilder<NotationDbContext>()
                .UseNpgsql("Server=localhost;port=5432;Database=NotationDB;uid=postgres;password=postgres;");

            _context = new NotationDbContext(builder.Options);
            dataRepository = new UtilisateurManager(_context);
            controller = new UtilisateursController(dataRepository);
        }

        [TestMethod]
        public async Task GetAllUtilisateurs_CompareWithDB()
        {
            var result = await controller.GetUtilisateurs();
            var userInDB = _context.Utilisateurs.ToList();
            

            Assert.IsNotNull(result);
            Assert.AreEqual(userInDB.Count, result.Value.Count());
        }

        [TestMethod]
        public async Task GetByID_SuccessGetUserByID()
        {
            var result = await controller.GetUtilisateurById(1);
            var userInDB = _context.Utilisateurs.Where(c=>c.Idutilisateur == 1).FirstOrDefault();

            Assert.AreEqual(userInDB.Mail, result.Value.Mail);
        }

        [TestMethod]
        public async Task GetByID_FailureGetUserByID()
        {
            var result = await controller.GetUtilisateurById(100000);

            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public async Task GetByEmail_SuccessGetUserByEmail()
        {
            var result = await controller.GetUtilisateurByEmail("clilleymd@last.fm");
            var userInDB = _context.Utilisateurs.Where(c => c.Mail == "clilleymd@last.fm").FirstOrDefault();

            Assert.AreEqual(userInDB.Idutilisateur, result.Value.Idutilisateur);
        }

        [TestMethod]
        public async Task GetByEmail_FailureGetUserByEmail()
        {
            var result = await controller.GetUtilisateurByEmail("test@test.com");

            Assert.AreEqual(result.Value, null);
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            Utilisateur userAtester = new Utilisateur()
            {
                Idutilisateur = chiffre,
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = controller.PostUtilisateur(userAtester).Result;
            // Assert
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault();
            userAtester.Idutilisateur = userRecupere.Idutilisateur;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }
    }
}
