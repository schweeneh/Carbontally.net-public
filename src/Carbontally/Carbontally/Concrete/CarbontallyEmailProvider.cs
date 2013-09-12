using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carbontally.Abstract;

namespace Carbontally.Concrete
{
    public class CarbontallyEmailProvider : IEmailProvider
    {
        public void SendAccountActivationEmail(string emailAddress, string securityToken, string confirmationUrl) {
            
        }
    }
}