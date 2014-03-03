using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Carbontally.Abstract;
using Carbontally.Concrete;
using System.Net.Mail;
using Assert = NUnit.Framework.Assert;
using Moq;

namespace Carbontally.UnitTests
{
    [TestClass]
    public class CarbontallyEmailProviderTests
    {
        private Mock<ISmtpClient> _smtpMock;
        private CarbontallyEmailProvider _email;

        [TestInitialize]
        public void Initialize() {
            // Arrange
            _smtpMock = new Mock<ISmtpClient>();

            _email = new CarbontallyEmailProvider(_smtpMock.Object);
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldSendEmail() {
            // Act
            _email.SendAccountActivationEmail("test@test.com", "1234", "http://test.com");

            // Assert
            _smtpMock.Verify(m => m.Send(It.IsAny<MailMessage>()), Times.Once(), "Expected Send() to be called but it was not.");
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldSetConfirmationUrl() {
            // Act
            _email.SendAccountActivationEmail("test@test.com", "1234", "http://www.test.com");

            // Assert
            Assert.AreEqual("http://www.test.com/Account/Confirm?token=1234", _email.ConfirmationLink);
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldSendToCorrectEmailAddress() {
            // Arrange
            var target = "test@test.com";

            // Act 
            _email.SendAccountActivationEmail(target, "", "");

            // Assert
            Assert.AreEqual(target, _email.To);
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldThrowExceptionWhenEmailIsEmptyString()
        {
            // Assert
            Assert.Throws(typeof(SendActivationEmailException), delegate { _email.SendAccountActivationEmail("", "", ""); });
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldThrowExceptionWhenEmailIsNull()
        {
            // Assert
            Assert.Throws(typeof(SendActivationEmailException), delegate { _email.SendAccountActivationEmail(null, "", ""); });
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldThrowExceptionWhenSecurityTokenIsNull()
        {
            // Assert
            Assert.Throws(typeof(SendActivationEmailException), delegate { _email.SendAccountActivationEmail("a@a.com", null, ""); });
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldThrowExceptionWhenConfirmationUrlIsNull()
        {
            // Assert
            Assert.Throws(typeof(SendActivationEmailException), delegate { _email.SendAccountActivationEmail("a@a.com", "", null); });
        }
    }
}
