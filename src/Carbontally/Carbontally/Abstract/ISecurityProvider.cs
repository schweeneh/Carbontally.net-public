using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Carbontally.Abstract
{
    public interface ISecurityProvider
    {
        string CreateUserAndAccount(string userName, string password, object propertyValues = null, bool requireConfirmationToken = false);

        
    }
}
