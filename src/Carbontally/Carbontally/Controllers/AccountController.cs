using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Carbontally.Abstract;
using Carbontally.Models;
using log4net;
using log4net.Config;

namespace Carbontally.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISecurityProvider _securityProvider;
        private readonly IEmailProvider _emailProvider;
        private static readonly ILog log = LogManager.GetLogger(typeof(AccountController));
        

        public AccountController(ISecurityProvider securityProvider, IEmailProvider emailProvider)
        {
            _securityProvider = securityProvider;
            _emailProvider = emailProvider;
        }

        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Created() {
            return View();
        }

        [AllowAnonymous]
        public ViewResult Confirm() {
            _securityProvider.ConfirmAccount("");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                try {
                    var securityToken = _securityProvider.CreateUserAndAccount(model.Email, model.Password, requireConfirmationToken: true);
                    _emailProvider.SendAccountActivationEmail(model.Email, securityToken, System.Configuration.ConfigurationManager.AppSettings["WebSiteUrl"]);
                    return RedirectToAction("Created", "Account");
                }
                catch (MembershipCreateUserException e) {
                    log.Info(string.Format("Could not create account for {0} - {1}",model.Email, e.Message));
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            return View();
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
    }
}
