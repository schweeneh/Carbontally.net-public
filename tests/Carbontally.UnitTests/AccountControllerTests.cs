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
        private RegisterViewModel model;
        private Mock<ISecurityProvider> securityMock;
        private Mock<IEmailProvider> emailMock;
        private AccountController controller;

        [TestInitialize]
        public void Initialize() {
            // Arrange
            model = new RegisterViewModel();
            securityMock = new Mock<ISecurityProvider>();
            emailMock = new Mock<IEmailProvider>();

            // setup controller.
            controller = new AccountController(securityMock.Object, emailMock.Object);
        }

        [TestMethod]
        public void Register_ShouldCreateAccountWhenModelIsValid()
        {
            // Act.
            controller.Register(model);

            // Assert.
            securityMock.Verify(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()), Times.Once(), "Expected CreateUserAndAccount() to be called but it was not.");
        }

        [TestMethod]
        public void Register_ShouldNotCreateAccountWhenModelIsNotValid()
        {
            // Act.
            controller.ModelState.AddModelError("key", "Model is invalid");
            controller.Register(model);
            
            // Assert.
            securityMock.Verify(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>()), Times.Never(), "Expected CreateUserAndAccount() to NOT be called but it was.");
        }

        [TestMethod]
        public void Register_ShouldAddErrorToModelStateWhenSendEmailThrows_SendActivationEmailException() {
            emailMock.Setup(m => m.SendAccountActivationEmail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Throws<SendActivationEmailException>();

            // Act
            controller.Register(model);
            var target = controller.ModelState;

            // Assert
            Assert.AreEqual(false, target.IsValid);
        }
        
        [TestMethod]
        public void Register_ShouldAddErrorToModelStateWhenAccountCreationFails()
        {
            securityMock.Setup(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Throws<MembershipCreateUserException>();

            // Act
            controller.Register(model);
            var target = controller.ModelState;

            // Assert
            Assert.AreEqual(false, target.IsValid);
        }

        [TestMethod]
        public void Register_ShouldRedirectToAccountCreatedWhenAccountCreationSucceeds() {
            // Act
            RedirectToRouteResult result = controller.Register(model) as RedirectToRouteResult;

            // Assert
            Assert.AreEqual("Account", result.RouteValues["controller"]);
            Assert.AreEqual("Created", result.RouteValues["action"]);
        }
        
        [TestMethod]
        public void Register_ShouldNotLogInUserWhenModelIsNotValid() {
            // Act.
            controller.ModelState.AddModelError("key", "Model is invalid");
            controller.Register(model);

            // Assert.
            securityMock.Verify(m => m.Login(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()), Times.Never(), "Expected Login() to NOT be called but it was.");
        }

        [TestMethod]
        public void Register_SendsEmailWhenAccountCreationSucceeds() {
            // setup mock security provider.
            var securityToken = "1234";
            
            securityMock.Setup(m => m.CreateUserAndAccount(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<object>(), It.IsAny<bool>())).Returns(securityToken);
            
            // Setup model.
            model.Email = "test@test.com";

            var confirmationUrl = "http://www.carbontally.org";

            // Act
            controller.Register(model);

            // Assert
            emailMock.Verify(m => m.SendAccountActivationEmail(model.Email, securityToken, confirmationUrl), Times.Once(),"Expected SendAccountActivationEmail() to be called but it was not.");
        }

        [TestMethod]
        public void Created_ShouldReturnView() {
            // Act
            var createdView = controller.Created();

            // Assert
            Assert.IsInstanceOf(typeof(ViewResult), createdView);
        }

        [TestMethod]
        public void Verify_ShouldConfirmAccount() {
            // Arrange
            var accountConfirmationToken = "1234";

            // Act
            controller.Confirm(accountConfirmationToken);

            // Assert 
            securityMock.Verify(m => m.ConfirmAccount(accountConfirmationToken), Times.Once(), "Expected ConfirmAccount() to be called but it was not.");
        }

        [TestMethod]
        public void Verify_ShouldReturnTrue_WhenAccountConfirmationSucceeds() {
            // Arrange 
            securityMock.Setup(m => m.ConfirmAccount(It.IsAny<string>())).Returns(true);

            // Act
            ViewResult target = controller.Confirm("");

            // Assert
            Assert.AreEqual(true, target.Model);
        }

        [TestMethod]
        public void Verify_ShouldReturnFalse_WhenAccountConfirmationFails() {
            // Arrange 
            securityMock.Setup(m => m.ConfirmAccount(It.IsAny<string>())).Returns(false);

            // Act
            ViewResult target = controller.Confirm("");

            // Assert
            Assert.AreEqual(false, target.Model);
        }

        [TestMethod]
        public void Verify_ShouldReturnFalse_WhenAccountConfirmationThrowsException() {
            // Arrange 
            securityMock.Setup(m => m.ConfirmAccount(It.IsAny<string>())).Throws<Exception>();

            // Act
            ViewResult target = controller.Confirm("");

            // Assert
            Assert.AreEqual(false, target.Model);
        }
    }
}
