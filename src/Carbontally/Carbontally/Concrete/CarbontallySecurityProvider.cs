using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carbontally.Abstract;
using WebMatrix.WebData;

namespace Carbontally.Concrete
{
    public class CarbontallySecurityProvider : ISecurityProvider
    {
        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
        }

        public bool Login(string userName, string password, bool persistCookie = false) {
            return WebSecurity.Login(userName, password, persistCookie);
        }
    }
}