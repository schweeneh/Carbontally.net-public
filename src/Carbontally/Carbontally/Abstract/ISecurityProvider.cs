using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbontally.Abstract
{
    public interface ISecurityProvider
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
        string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);

        bool Login(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// Confirms that an account is valid and activates the account.
        /// </summary>
        /// <param name="accountConfirmationToken"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        bool ConfirmAccount(string accountConfirmationToken);

        void Logout();
    }
}
