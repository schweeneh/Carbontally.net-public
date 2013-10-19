using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbontally.Controllers;
using Carbontally.Concrete;
using Carbontally.Models;

namespace Carbontally.IntegrationTests
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestInitialize]
        public void Initialize() {

        }

        [TestMethod]
        public void Register_CreatesAnAccount() {
            var model = new RegisterViewModel();
            
            model.Email = "test@test.com";
            model.Password = "test";
            model.ConfirmPassword = "test";

            AccountController target = new AccountController(new CarbontallySecurityProvider(), new CarbontallyEmailProvider(new CarbontallySmtpClient()));

            
        }

        [TestMethod]
        public void Confirm_ActivatesAnAccount() {

        }
    }
}
