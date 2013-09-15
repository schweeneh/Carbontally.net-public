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
        public void SendAccountActivationEmail_ShouldSendsEmail() {
            // Act
            _email.SendAccountActivationEmail("test@test.com", "1234", "http://test.com");

            // Assert
            _smtpMock.Verify(m => m.Send(It.IsAny<MailMessage>()), Times.Once(), "Expected Send() to be called but it was not.");
        }

        [TestMethod]
        public void SendAccountActivationEmail_ShouldSetConfirmationUrl() {
            // Act
            _email.SendAccountActivationEmail("test@test.com", "1234", "http://test.com");

            // Assert
            Assert.AreEqual("http://test.com?token=1234", _email.ConfirmationLink);
        }
    }
}
