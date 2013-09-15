using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carbontally.Abstract;
using System.Net.Mail;
using log4net;
using Ninject;
using Carbontally.Infrastructure;

namespace Carbontally.Concrete
{
    public class CarbontallyEmailProvider : IEmailProvider
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CarbontallyEmailProvider));

        private readonly ISmtpClient _smtpClient;

        public string To { get; private set; }

        public string From { get; private set; }

        public string Subject { get; private set; }

        public string Body { get; private set; }

        public string ConfirmationLink { get; private set; }

        public CarbontallyEmailProvider(ISmtpClient smtpClient) {
            _smtpClient = smtpClient;
        }

        public void SendAccountActivationEmail(string emailAddress, string securityToken, string confirmationUrl) {

            To = emailAddress.ThrowIfNull("emailAddress");
            From = "noreply@carbontally.com";
            Subject = "Carbontally.com account confirmation";
            Body = string.Format("Click the link below to confirm your Carbontally.com account:\n\n{0}", CreateConfirmationLink(securityToken, confirmationUrl));

            MailAddress addressTo = new MailAddress(To);
            MailAddress addressFrom = new MailAddress(From);
            MailMessage message = new MailMessage(addressFrom, addressTo);
            message.Subject = Subject;
            message.Body = Body;

            try {
                _smtpClient.Send(message);
            }
            catch (Exception ex) {
                log.Error(string.Format("Error sending email to {0}: {1}", To.ToString(), ex.Message));
                throw;
            }
        }

        private string CreateConfirmationLink(string securityToken, string confirmationUrl) {
            ConfirmationLink = string.Format("{0}?token={1}", confirmationUrl.ThrowIfNull("confirmationUrl"), securityToken.ThrowIfNull("securityToken"));

            return ConfirmationLink;
        }
    }
}