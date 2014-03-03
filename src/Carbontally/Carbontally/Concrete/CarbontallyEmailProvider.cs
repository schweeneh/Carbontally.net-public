using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carbontally.Abstract;
using System.Net.Mail;
using log4net;
using Ninject;

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

        /// <summary>
        /// Sends the account activation email with a clickable confirmation link.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="securityToken"></param>
        /// <param name="confirmationUrl"></param>
        /// <exception cref="SendActivationEmailException"></exception>
        public void SendAccountActivationEmail(string emailAddress, string securityToken, string confirmationUrl) {
            try
            {
                To = emailAddress.ThrowIfNull<string>("To");
                From = "noreply@carbontally.com";
                Subject = "Carbontally.com account confirmation";
                Body = string.Format("Click the link below to confirm your Carbontally.com account:\n\n{0}", CreateConfirmationLink(securityToken, confirmationUrl));

                MailAddress addressTo = new MailAddress(To);
                MailAddress addressFrom = new MailAddress(From);
                MailMessage message = new MailMessage(addressFrom, addressTo);
                message.Subject = Subject;
                message.Body = Body;
                _smtpClient.Send(message);
            }
            catch (SmtpException ex) {
                log.Error("Error while attempting to send account activation email.", ex);
                throw new SendActivationEmailException(string.Format("Error sending email to {0}", To == null ? "[No Email]" : To.ToString()), ex);
            }
            catch (ArgumentException ex)
            {
                log.Error("Error while attempting to send account activation email.", ex);
                throw new SendActivationEmailException(string.Format("Error sending email to {0}", To == null ? "[No Email]" : To.ToString()), ex);
            }
            catch (FormatException ex)
            {
                log.Error("Error while attempting to send account activation email.", ex);
                throw new SendActivationEmailException(string.Format("Error sending email to {0}", To == null ? "[No Email]" : To.ToString()), ex);
            }
            catch (Exception ex)
            {
                log.Error("Error while attempting to send account activation email.", ex);
                throw;
            }
        }

        /// <summary>
        /// Creates a confirmation link that can be clicked to confirm an account.
        /// </summary>
        /// <param name="securityToken"></param>
        /// <param name="confirmationUrl"></param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        /// <exception cref="FormatException"></exception>
        private string CreateConfirmationLink(string securityToken, string confirmationUrl) {
            ConfirmationLink = string.Format("{0}/Account/Confirm?token={1}", confirmationUrl.ThrowIfNull("confirmationUrl"), securityToken.ThrowIfNull("securityToken"));

            return ConfirmationLink;
        }
    }
}