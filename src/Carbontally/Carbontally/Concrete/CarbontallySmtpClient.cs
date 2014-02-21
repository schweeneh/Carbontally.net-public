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
        public void Send(MailMessage message) {
            using (SmtpClient client = new SmtpClient())
            {
                client.Send(message);
            }
        }
    }
}