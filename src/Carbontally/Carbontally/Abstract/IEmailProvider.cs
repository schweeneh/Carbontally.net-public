using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carbontally.Abstract
{
    public interface IEmailProvider
    {
        /// <summary>
        /// Send account activation email using security token and confirmation URL.
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="securityToken"></param>
        /// <param name="confirmationUrl"></param>
        /// <exception cref="SendActivationEmailException"></exception>
        void SendAccountActivationEmail(string emailAddress, string securityToken, string confirmationUrl);
    }
}