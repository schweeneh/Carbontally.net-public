using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using Carbontally.Abstract;

namespace Carbontally.Concrete
{
    public class CarbontallySmtpClient : ISmtpClient 
    {
        private SmtpClient client = new SmtpClient(); // Set this up properly with host and port.

        public void Send(MailMessage message) {
            client.Send(message);
        }
    }
}