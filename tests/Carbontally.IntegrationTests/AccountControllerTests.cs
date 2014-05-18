using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbontally.Controllers;
using Carbontally.Concrete;
using Carbontally.Models;
using Carbontally.Infrastructure;
using System.Data.Entity;
using Carbontally.Domain.Persistence;

namespace Carbontally.IntegrationTests
{
    [TestClass]
    public class AccountControllerTests
    {
        private CarbontallySecurityProvider security;

        [TestInitialize]
        public void Initialize() {
            Initializer.InitializeSimpleMemberShip();
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CarbontallyContext>());
            security = new CarbontallySecurityProvider();
        }

        [TestMethod]
        public void Register_CreatesAnAccount() {
            // Arrange
            var model = new RegisterViewModel();
            
            model.Email = "test@test.com";
            model.Password = "test";
            model.ConfirmPassword = "test";

            AccountController target = new AccountController(new CarbontallySecurityProvider(), new CarbontallyEmailProvider(new CarbontallySmtpClient()));

            //Act
            target.Register(model);
        }
    }
}
