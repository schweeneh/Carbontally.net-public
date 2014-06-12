using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Carbontally.Abstract;
using WebMatrix.WebData;
using WebMatrix;

namespace Carbontally.Concrete
{
    public class CarbontallySecurityProvider : ISecurityProvider
    {
        /// <summary>
        /// Creates a new user profile entry and a new membership account.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="propertyValues"></param>
        /// <param name="requireConfirmationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false)
        {
            return WebSecurity.CreateUserAndAccount(userName, password, propertyValues, requireConfirmationToken);
        }

        public bool Login(string userName, string password, bool persistCookie = false) {
            return WebSecurity.Login(userName, password, persistCookie);
        }

        /// <summary>
        /// Confirms that an account is valid and activates the account.
        /// </summary>
        /// <param name="accountConfirmationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public bool ConfirmAccount(string accountConfirmationToken) {
            return WebSecurity.ConfirmAccount(accountConfirmationToken);
        }

        public bool DeleteAccount(string userName) {
            return ((SimpleMembershipProvider)System.Web.Security.Membership.Provider).DeleteUser(userName, true);
        }

        public void Logout() {
            WebSecurity.Logout();
        }
    }
}