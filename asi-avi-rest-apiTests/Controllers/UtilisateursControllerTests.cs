using asi_avi_rest_api.Controllers;
using asi_avi_rest_api.Models.DataManager;
using asi_avi_rest_api.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
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

        /*[TestMethod]
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

        /*[TestMethod]
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
        }*/

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            Utilisateur user = new Utilisateur
            {
                Nom = "POISSON",
                Prenom = "Pascal",
                Mobile = "1",
                Mail = "poisson@gmail.com",
                Pwd = "Toto12345678!",
                rue = "Chemin de Bellevue",
                CodePostal = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var actionResult = userController.PostUtilisateur(user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(ActionResult<Utilisateur>), "Pas un ActionResult<Utilisateur>");
            Assert.IsInstanceOfType(actionResult.Result, typeof(CreatedAtActionResult), "Pas un CreatedAtActionResult");
            var result = actionResult.Result as CreatedAtActionResult;
            Assert.IsInstanceOfType(result.Value, typeof(Utilisateur), "Pas un Utilisateur");
            user.Idutilisateur = ((Utilisateur)result.Value).Idutilisateur;
            Assert.AreEqual(user, (Utilisateur)result.Value, "Utilisateurs pas identiques");
        }

        [TestMethod]
        public void GetUtilisateurById_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                Idutilisateur = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(1).Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurById_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurById(0).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundObjectResult));
        }

        [TestMethod]
        public void GetUtilisateurByEmail_ExistingIdPassed_ReturnsRightItem_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                Idutilisateur = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByStringAsync("clilleymd@last.fm").Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("clilleymd@last.fm").Result;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsNotNull(actionResult.Value);
            Assert.AreEqual(user, actionResult.Value as Utilisateur);
        }

        [TestMethod]
        public void GetUtilisateurByEmail_UnknownIdPassed_ReturnsNotFoundResult_AvecMoq()
        {
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.GetUtilisateurByEmail("test@test.com").Result;
            // Assert
            Assert.IsInstanceOfType(actionResult.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoq()
        {
            // Arrange
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void DeleteUtilisateurTest_AvecMoqV2()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                Idutilisateur = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.DeleteUtilisateur(1).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }

        [TestMethod]
        public void PutUtilisateurTest_AvecMoq()
        {
            // Arrange
            Utilisateur user = new Utilisateur
            {
                Idutilisateur = 1,
                Nom = "Calida",
                Prenom = "Lilley",
                Mobile = "0653930778",
                Mail = "clilleymd@last.fm",
                Pwd = "Toto12345678!",
                rue = "Impasse des bergeronnettes",
                CodePostal = "74200",
                Ville = "Allinges",
                Pays = "France",
                Latitude = 46.344795F,
                Longitude = 6.4885845F
            };
            var mockRepository = new Mock<IDataRepository<Utilisateur>>();
            mockRepository.Setup(x => x.GetByIdAsync(1).Result).Returns(user);
            var userController = new UtilisateursController(mockRepository.Object);
            // Act
            var actionResult = userController.PutUtilisateurAsync(1,user).Result;
            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NoContentResult), "Pas un NoContentResult"); // Test du type de retour
        }
    }
}
