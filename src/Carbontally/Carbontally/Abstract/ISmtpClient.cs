using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace Carbontally.Abstract
{
    public interface ISmtpClient
    {
        void Send(MailMessage message);
    }
}