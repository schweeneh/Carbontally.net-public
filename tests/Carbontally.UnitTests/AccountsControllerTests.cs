using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbontally.Controllers;
using System.Web.Mvc;
using System.Web;
using Assert = NUnit.Framework.Assert;
using Moq;
using Carbontally.Abstract;
using Carbontally.Models;
using System.Web.Security;

namespace Carbontally.UnitTests
{
    [TestClass]
    public class AccountsControllerTests
    {
        [TestMethod]
        public void AccountControllerAccountCreatedWhenModelIsValid()
        {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();
            
            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act.
            controller.Register(model);

            // Assert.
            mock.Verify(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()), Times.Once(), "Expected CreateUserAndAccount() to be called but it was not.");
        }

        [TestMethod]
        public void AccountControllerAccountNotCreatedWhenModelIsNotValid()
        {
            // Arrange
            var model = new RegisterViewModel();
            
            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act.
            controller.ModelState.AddModelError("key", "Model is invalid");
            controller.Register(model);
            
            // Assert.
            mock.Verify(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()), Times.Never(), "Expected CreateUserAndAccount() to NOT be called but it was.");
        }

        [TestMethod]
        public void AccountControllerAddsErrorToModelStateWhenAccountCreationFails() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();
            mock.Setup(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Throws<MembershipCreateUserException>();

            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act
            controller.Register(model);
            var target = controller.ModelState;

            // Assert
            Assert.AreEqual(false, target.IsValid);
        }

        [TestMethod]
        public void AccountControllerRedirectsToHomeIndexWhenAccountCreationSucceeds() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act
            RedirectToRouteResult result = controller.Register(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void AccountControllerLogsInUserWhenAccountCreationSucceeds() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act
            controller.Register(model);

            // Assert
            mock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Once(), "Expected Login() to be called but it was not.");
        }

        [TestMethod]
        public void AccountControllerDoesNotLogInUserWhenModelIsNotValid() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object);

            // Act.
            controller.ModelState.AddModelError("key", "Model is invalid");
            controller.Register(model);

            // Assert.
            mock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never(), "Expected Login() to NOT be called but it was.");
        }
    }
}
