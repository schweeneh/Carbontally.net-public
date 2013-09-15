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
    public class AccountControllerTests
    {
        [TestMethod]
        public void AccountControllerAccountCreatedWhenModelIsValid()
        {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();
            
            
            // setup controller.
            var controller = new AccountController(mock.Object, emailMock.Object);

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

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object, emailMock.Object);

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

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object, emailMock.Object);

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

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object, emailMock.Object);

            // Act
            RedirectToRouteResult result = controller.Register(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Home", result.RouteValues["controller"]);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void AccountControllerDoesNotLogInUserWhenModelIsNotValid() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            Mock<ISecurityProvider> mock = new Mock<ISecurityProvider>();

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();

            // setup controller.
            var controller = new AccountController(mock.Object, emailMock.Object);

            // Act.
            controller.ModelState.AddModelError("key", "Model is invalid");
            controller.Register(model);

            // Assert.
            mock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never(), "Expected Login() to NOT be called but it was.");
        }

        [TestMethod]
        public void Register_SendsEmailWhenAccountCreationSucceeds() {
            // Arrange
            var model = new RegisterViewModel();

            // setup mock security provider.
            var securityToken = "1234";
            Mock<ISecurityProvider> securityMock = new Mock<ISecurityProvider>();
            securityMock.Setup(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Returns(securityToken);

            // setup mock email provider.
            Mock<IEmailProvider> emailMock = new Mock<IEmailProvider>();

            // setup controller.
            var controller = new AccountController(securityMock.Object, emailMock.Object);

            // Setup model.
            model.Email = "test@test.com";

            var confirmationUrl = "http://www.carbontally.org";

            // Act
            controller.Register(model);

            // Assert
            emailMock.Verify(m => m.SendAccountActivationEmail(model.Email, securityToken, confirmationUrl), Times.Once(),"Expected SendAccountActivationEmail() to be called but it was not.");
        }
    }
}
