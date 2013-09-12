using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carbontally.Abstract
{
    public interface IEmailProvider
    {
        void SendAccountActivationEmail(string emailAddress, string securityToken, string confirmationUrl);
    }
}